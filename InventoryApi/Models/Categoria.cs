using System.Text.Json.Serialization;

namespace InventoryAPI.Models;

public class Categoria
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? Descrizione { get; set; }
    [JsonIgnore] 
    public List<Prodotto> Prodotti { get; set; } = new();
}