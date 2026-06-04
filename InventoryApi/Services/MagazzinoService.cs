using InventoryAPI.Data;
using InventoryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryAPI.Services;

public class MagazzinoService
{
    private readonly AppDbContext _context;

    public MagazzinoService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<int> GetGiacenzaDisponibileAsync(int prodottoId)
    {
 
        var movimenti = await _context.MovimentiMagazzino
            .Where(m => m.ProdottoId == prodottoId)
            .ToListAsync();

        var carichi = movimenti.Where(m => m.Tipo == "Carico").Sum(m => m.Quantita);
        var scarichi = movimenti.Where(m => m.Tipo == "Scarico").Sum(m => m.Quantita);

        return carichi - scarichi;
    }

    public async Task RifornisciProdottoAsync(int prodottoId, int quantita)
    {
        if (quantita <= 0)
            throw new ArgumentException("La quantità di carico deve essere maggiore di zero.");

        var movimento = new MovimentoMagazzino
        {
            ProdottoId = prodottoId,
            Quantita = quantita,
            Tipo = "Carico",
            DataMovimento = DateTime.UtcNow
        };

        _context.MovimentiMagazzino.Add(movimento);
        await _context.SaveChangesAsync();
    }
    public async Task ScaricaProdottoAsync(int prodottoId, int quantita)
    {
        if (quantita <= 0)
            throw new ArgumentException("La quantità di scarico deve essere maggiore di zero.");

        var giacenzaAttuale = await GetGiacenzaDisponibileAsync(prodottoId);
        if (giacenzaAttuale < quantita)
            throw new InvalidOperationException("Giacenza insufficiente in magazzino per effettuare lo scarico.");

        var movimento = new MovimentoMagazzino
        {
            ProdottoId = prodottoId,
            Quantita = quantita,
            Tipo = "Scarico",
            DataMovimento = DateTime.UtcNow
        };

        _context.MovimentiMagazzino.Add(movimento);
        await _context.SaveChangesAsync();
    }
}