namespace InventoryAPI.Models;

public class Prodotto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string SKU { get; set; } = string.Empty; 
    public decimal Prezzo { get; set; }

    public int CategoriaId { get; set; }
    public Categoria? Categoria { get; set; }
}