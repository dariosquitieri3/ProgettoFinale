using Microsoft.EntityFrameworkCore;
using InventoryAPI.Models;

namespace InventoryAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    public DbSet<Prodotto> Prodotti { get; set; }
    public DbSet<Categoria> Categorie { get; set; }
    public DbSet<Utente> Utenti { get; set; }
    public DbSet<Fornitore> Fornitori { get; set; }
    public DbSet<MovimentoMagazzino> MovimentiMagazzino { get; set; }
    public DbSet<Ordine> Ordini { get; set; }
    public DbSet<RigaOrdine> RigheOrdine { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Prodotto>().Property(p => p.Prezzo).HasPrecision(18, 2);
        modelBuilder.Entity<Ordine>().Property(o => o.TotalePrezzo).HasPrecision(18, 2);
        modelBuilder.Entity<RigaOrdine>().Property(r => r.PrezzoUnitario).HasPrecision(18, 2);
        modelBuilder.Entity<Prodotto>().HasIndex(p => p.SKU).IsUnique();
        modelBuilder.Entity<Utente>().HasIndex(u => u.Email).IsUnique();
        modelBuilder.Entity<Ordine>()
            .HasOne(o => o.Utente)
            .WithMany()
            .HasForeignKey(o => o.UtenteId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}