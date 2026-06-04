namespace InventoryAPI.Models;

public class Fornitore
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string PartitaIva { get; set; } = string.Empty;
    public List<Prodotto> Prodotti { get; set; } = new();
}