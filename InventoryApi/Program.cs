using Microsoft.EntityFrameworkCore;
using InventoryAPI.Data;
using InventoryAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=InventoryDb_Esercizio;Trusted_Connection=True;MultipleActiveResultSets=true"));

builder.Services.AddScoped<MagazzinoService>();
builder.Services.AddScoped<OrdineService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<InventoryAPI.Data.AppDbContext>();

    if (!context.Categorie.Any())
    {
        context.Categorie.Add(new InventoryAPI.Models.Categoria
        {
            Nome = "Generale",
            Descrizione = "Categoria di test per il magazzino"
        });
        context.SaveChanges();
    }
}

app.Run();