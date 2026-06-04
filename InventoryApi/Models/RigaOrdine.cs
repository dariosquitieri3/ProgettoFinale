namespace InventoryAPI.Models;

public class RigaOrdine
{
    public int Id { get; set; }

    public int OrdineId { get; set; }
    public Ordine? Ordine { get; set; }

    public int ProdottoId { get; set; }
    public Prodotto? Prodotto { get; set; }

    public int Quantita { get; set; }
    public decimal PrezzoUnitario { get; set; } 
}