using Microsoft.Extensions.Logging;

namespace NotificaService;

public class NotificaService
{
    private readonly ILogger<NotificaService> _logger;

    public NotificaService(ILogger<NotificaService> logger)
    {
        _logger = logger;
    }
    public void InviaAllarmeGiacenza(dynamic ev)
    {
        _logger.LogWarning($"[ALLERTA MAGAZZINO] !!! Il prodotto '{ev.NomeProdotto}' (ID: {ev.ProdottoId}) sta per finire! Giacenza rimasta: {ev.GiacenzaRimasta} pezzi.");
    }
}