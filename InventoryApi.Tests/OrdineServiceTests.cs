using InventoryAPI.Data;
using InventoryAPI.Models;
using InventoryAPI.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace InventoryAPI.Tests;

public class OrdineServiceTests
{
    private AppDbContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new AppDbContext(options);
    }

    [Fact]
    public async Task ConfermaOrdine_SeGiacenzaInizialeEZero_DeveAnnullareOrdine()
    {
        using var context = GetDbContext();
        var magazzinoService = new MagazzinoService(context);
        var ordineService = new OrdineService(context, magazzinoService);

        var prodotto = new Prodotto { Id = 1, Nome = "Mouse Gaming", Prezzo = 29.99m, CategoriaId = 1 };
        context.Prodotti.Add(prodotto);

        var ordine = new Ordine
        {
            Id = 10,
            UtenteId = 1,
            Stato = "Bozza",
            DataOrdine = DateTime.UtcNow,
            RigheOrdine = new List<RigaOrdine>
            {
                new RigaOrdine { Id = 1, ProdottoId = 1, Quantita = 2, PrezzoUnitario = 29.99m }
            }
        };
        context.Ordini.Add(ordine);
        await context.SaveChangesAsync();

        await Assert.ThrowsAsync<InvalidOperationException>(async () =>
        {
            await ordineService.ConfermaOrdineAsync(10);
        });

        var ordineAggiornato = await context.Ordini.FindAsync(10);
        Assert.Equal("Annullato", ordineAggiornato!.Stato);
    }
}