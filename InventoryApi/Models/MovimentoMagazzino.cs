namespace InventoryAPI.Models;

public class MovimentoMagazzino
{
    public int Id { get; set; }

    public int ProdottoId { get; set; }
    public Prodotto? Prodotto { get; set; }

    public int Quantita { get; set; }

    public DateTime DataMovimento { get; set; } = DateTime.UtcNow;

    public string Tipo { get; set; } = string.Empty;
}