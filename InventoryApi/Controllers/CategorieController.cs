using InventoryAPI.Data;
using InventoryAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategorieController : ControllerBase
{
    private readonly AppDbContext _context;

    public CategorieController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Categoria>>> GetCategorie()
    {
        return await _context.Categorie.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategoria(int id)
    {
        var categoria = await _context.Categorie.FindAsync(id);

        if (categoria == null)
            return NotFound("Categoria non trovata.");

        return Ok(categoria);
    }


    [HttpPost]
    public async Task<IActionResult> CreaCategoria([FromBody] Categoria categoria)
    {

        var nomeDuplicato = await _context.Categorie.AnyAsync(c => c.Nome == categoria.Nome);
        if (nomeDuplicato)
            return BadRequest("Esiste già una categoria con questo nome.");

        _context.Categorie.Add(categoria);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetCategoria), new { id = categoria.Id }, categoria);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> AggiornaCategoria(int id, [FromBody] Categoria categoriaAggiornata)
    {
        var categoriaEsistente = await _context.Categorie.FindAsync(id);
        if (categoriaEsistente == null)
            return NotFound("Categoria non trovata.");
        if (categoriaEsistente.Nome != categoriaAggiornata.Nome)
        {
            var nomeDuplicato = await _context.Categorie.AnyAsync(c => c.Nome == categoriaAggiornata.Nome && c.Id != id);
            if (nomeDuplicato)
                return BadRequest("Questo nome di categoria è già in uso.");
        }

        categoriaEsistente.Nome = categoriaAggiornata.Nome;

        await _context.SaveChangesAsync();
        return NoContent();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> EliminaCategoria(int id)
    {
        var categoria = await _context.Categorie.Include(c => c.Prodotti).FirstOrDefaultAsync(c => c.Id == id);
        if (categoria == null)
            return NotFound("Categoria non trovata.");

        if (categoria.Prodotti != null && categoria.Prodotti.Any())
            return BadRequest("Impossibile eliminare la categoria perché contiene dei prodotti associati. Sposta o elimina prima i prodotti.");

        _context.Categorie.Remove(categoria);
        await _context.SaveChangesAsync();

        return Ok(new { Messaggio = $"Categoria '{categoria.Nome}' eliminata con successo." });
    }
}