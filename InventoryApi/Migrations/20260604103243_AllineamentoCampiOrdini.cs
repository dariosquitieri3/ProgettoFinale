using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryApi.Migrations
{
    /// <inheritdoc />
    public partial class AllineamentoCampiOrdini : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PrezzoStorico",
                table: "RigheOrdine",
                newName: "PrezzoUnitario");

            migrationBuilder.RenameColumn(
                name: "Data",
                table: "Ordini",
                newName: "DataOrdine");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PrezzoUnitario",
                table: "RigheOrdine",
                newName: "PrezzoStorico");

            migrationBuilder.RenameColumn(
                name: "DataOrdine",
                table: "Ordini",
                newName: "Data");
        }
    }
}
