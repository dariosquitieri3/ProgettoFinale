using Microsoft.AspNetCore.Mvc;
using InventoryAPI.Services;

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

    [HttpPost("{id}/conferma")]
    public async Task<IActionResult> ConfermaOrdine(int id)
    {
        try
        {
            await _ordineService.ConfermaOrdineAsync(id);
            return Ok(new { messaggio = "Ordine confermato con successo e scorte aggiornate!" });
        }
        catch (ArgumentException ex)
        {
            return NotFound(new { messaggio = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { messaggio = ex.Message });
        }
    }
}