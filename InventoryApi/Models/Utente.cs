namespace InventoryAPI.Models;

public class Utente
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string Ruolo { get; set; } = "Operatore"; 
    public List<MovimentoMagazzino> Movimenti { get; set; } = new();
}