namespace InventoryAPI.Events;

public class ProdottoSottoSogliaEvent
{
    public int ProdottoId { get; set; }
    public string NomeProdotto { get; set; } = string.Empty;
    public int GiacenzaRimasta { get; set; }
}