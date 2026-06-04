using InventoryAPI.Data;
using InventoryAPI.Models;
using InventoryAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProdottiController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly MagazzinoService _magazzinoService;

    public ProdottiController(AppDbContext context, MagazzinoService magazzinoService)
    {
        _context = context;
        _magazzinoService = magazzinoService;
    }

    [HttpGet]
    public async Task<IActionResult> GetProdotti()
    {
        var prodotti = await _context.Prodotti.Include(p => p.Categoria).ToListAsync();

        // Creiamo una risposta anonima per includere la giacenza calcolata al volo
        var risultato = new List<object>();
        foreach (var p in prodotti)
        {
            var giacenza = await _magazzinoService.GetGiacenzaDisponibileAsync(p.Id);
            risultato.Add(new
            {
                p.Id,
                p.Nome,
                p.SKU,
                p.Prezzo,
                Categoria = p.Categoria?.Nome,
                GiacenzaAttuale = giacenza
            });
        }

        return Ok(risultato);
    }

    [HttpPost]
    public async Task<IActionResult> CreaProdotto([FromBody] Prodotto prodotto)
    {
        // Controlliamo se la categoria inserita esiste davvero
        var categoriaEsiste = await _context.Categorie.AnyAsync(c => c.Id == prodotto.CategoriaId);
        if (!categoriaEsiste)
            return BadRequest("La CategoriaId specificata non esiste.");

        _context.Prodotti.Add(prodotto);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetProdotti), new { id = prodotto.Id }, prodotto);
    }

    [HttpPost("{id}/rifornisci")]
    public async Task<IActionResult> RifornisciProdotto(int id, [FromBody] int quantita)
    {
        var prodottoEsiste = await _context.Prodotti.AnyAsync(p => p.Id == id);
        if (!prodottoEsiste)
            return NotFound("Prodotto non trovato.");

        try
        {
            await _magazzinoService.RifornisciProdottoAsync(id, quantita);
            return Ok(new { Messaggio = $"Carico di {quantita} pezzi registrato con successo." });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}