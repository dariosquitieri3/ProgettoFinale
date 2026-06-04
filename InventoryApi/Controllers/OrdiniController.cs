using InventoryAPI.Models;
using InventoryAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdiniController : ControllerBase
{
    private readonly OrdineService _ordineService;

    public OrdiniController(OrdineService ordineService)
    {
        _ordineService = ordineService;
    }
    [HttpPost]
    public async Task<IActionResult> CreaOrdine([FromBody] Ordine ordine)
    {
        var nuovoOrdine = await _ordineService.CreaOrdineAsync(ordine);
        return Ok(nuovoOrdine);
    }
    [HttpPost("{id}/conferma")]
    public async Task<IActionResult> ConfermaOrdine(int id)
    {
        try
        {
            await _ordineService.ConfermaOrdineAsync(id);
            return Ok(new { Messaggio = "Ordine confermato con successo e magazzino aggiornato!" });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { Errore = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
    }
}