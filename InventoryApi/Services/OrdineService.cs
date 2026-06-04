using InventoryAPI.Data;
using InventoryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryAPI.Services;

public class OrdineService
{
    private readonly AppDbContext _context;
    private readonly MagazzinoService _magazzinoService;

    public OrdineService(AppDbContext context, MagazzinoService magazzinoService)
    {
        _context = context;
        _magazzinoService = magazzinoService;
    }

    public async Task<Ordine> CreaOrdineAsync(Ordine ordine)
    {
        ordine.Stato = "Bozza";
        ordine.DataOrdine = DateTime.UtcNow;

        _context.Ordini.Add(ordine);
        await _context.SaveChangesAsync();
        return ordine;
    }

    public async Task ConfermaOrdineAsync(int ordineId)
    {
        var ordine = await _context.Ordini
            .Include(o => o.RigheOrdine)
            .FirstOrDefaultAsync(o => o.Id == ordineId);

        if (ordine == null)
            throw new ArgumentException("Ordine non trovato.");

        if (ordine.Stato != "Bozza")
            throw new InvalidOperationException("Si possono confermare solo gli ordini in stato Bozza.");

        foreach (var riga in ordine.RigheOrdine)
        {
            var giacenzaDisponibile = await _magazzinoService.GetGiacenzaDisponibileAsync(riga.ProdottoId);

            if (giacenzaDisponibile < riga.Quantita)
            {
     
                ordine.Stato = "Annullato";
                await _context.SaveChangesAsync();
                throw new InvalidOperationException($"Giacenza insufficiente per il prodotto ID {riga.ProdottoId}. Ordine annullato.");
            }
        }

        foreach (var riga in ordine.RigheOrdine)
        {
            await _magazzinoService.ScaricaProdottoAsync(riga.ProdottoId, riga.Quantita);
        }

        ordine.Stato = "Eseguito";
        await _context.SaveChangesAsync();
    }
}