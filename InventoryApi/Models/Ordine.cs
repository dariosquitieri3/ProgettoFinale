namespace InventoryAPI.Models;

public class Ordine
{
    public int Id { get; set; }
    public string CodiceOrdine { get; set; } = string.Empty; 
    public DateTime DataOrdine { get; set; } = DateTime.UtcNow;
    public string Stato { get; set; } = "Bozza";
    public decimal TotalePrezzo { get; set; }

  
    public int UtenteId { get; set; }
    public Utente? Utente { get; set; }

   
    public List<RigaOrdine> RigheOrdine { get; set; } = new();
}