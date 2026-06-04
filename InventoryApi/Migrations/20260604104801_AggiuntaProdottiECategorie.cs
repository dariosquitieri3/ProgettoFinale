using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryApi.Migrations
{
    /// <inheritdoc />
    public partial class AggiuntaProdottiECategorie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FornitoreProdotto");

            migrationBuilder.AddColumn<int>(
                name: "FornitoreId",
                table: "Prodotti",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Descrizione",
                table: "Categorie",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Prodotti_FornitoreId",
                table: "Prodotti",
                column: "FornitoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prodotti_Fornitori_FornitoreId",
                table: "Prodotti",
                column: "FornitoreId",
                principalTable: "Fornitori",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prodotti_Fornitori_FornitoreId",
                table: "Prodotti");

            migrationBuilder.DropIndex(
                name: "IX_Prodotti_FornitoreId",
                table: "Prodotti");

            migrationBuilder.DropColumn(
                name: "FornitoreId",
                table: "Prodotti");

            migrationBuilder.AlterColumn<string>(
                name: "Descrizione",
                table: "Categorie",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "FornitoreProdotto",
                columns: table => new
                {
                    FornitoriId = table.Column<int>(type: "int", nullable: false),
                    ProdottiId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FornitoreProdotto", x => new { x.FornitoriId, x.ProdottiId });
                    table.ForeignKey(
                        name: "FK_FornitoreProdotto_Fornitori_FornitoriId",
                        column: x => x.FornitoriId,
                        principalTable: "Fornitori",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FornitoreProdotto_Prodotti_ProdottiId",
                        column: x => x.ProdottiId,
                        principalTable: "Prodotti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FornitoreProdotto_ProdottiId",
                table: "FornitoreProdotto",
                column: "ProdottiId");
        }
    }
}
