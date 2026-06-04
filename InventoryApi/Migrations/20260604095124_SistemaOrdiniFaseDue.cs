using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryApi.Migrations
{
    /// <inheritdoc />
    public partial class SistemaOrdiniFaseDue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovimentiMagazzino_Utenti_OperatoreId",
                table: "MovimentiMagazzino");

            migrationBuilder.CreateTable(
                name: "Ordini",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodiceOrdine = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Stato = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalePrezzo = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    UtenteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ordini", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ordini_Utenti_UtenteId",
                        column: x => x.UtenteId,
                        principalTable: "Utenti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RigheOrdine",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrdineId = table.Column<int>(type: "int", nullable: false),
                    ProdottoId = table.Column<int>(type: "int", nullable: false),
                    Quantita = table.Column<int>(type: "int", nullable: false),
                    PrezzoStorico = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RigheOrdine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RigheOrdine_Ordini_OrdineId",
                        column: x => x.OrdineId,
                        principalTable: "Ordini",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RigheOrdine_Prodotti_ProdottoId",
                        column: x => x.ProdottoId,
                        principalTable: "Prodotti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ordini_UtenteId",
                table: "Ordini",
                column: "UtenteId");

            migrationBuilder.CreateIndex(
                name: "IX_RigheOrdine_OrdineId",
                table: "RigheOrdine",
                column: "OrdineId");

            migrationBuilder.CreateIndex(
                name: "IX_RigheOrdine_ProdottoId",
                table: "RigheOrdine",
                column: "ProdottoId");

            migrationBuilder.AddForeignKey(
                name: "FK_MovimentiMagazzino_Utenti_OperatoreId",
                table: "MovimentiMagazzino",
                column: "OperatoreId",
                principalTable: "Utenti",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovimentiMagazzino_Utenti_OperatoreId",
                table: "MovimentiMagazzino");

            migrationBuilder.DropTable(
                name: "RigheOrdine");

            migrationBuilder.DropTable(
                name: "Ordini");

            migrationBuilder.AddForeignKey(
                name: "FK_MovimentiMagazzino_Utenti_OperatoreId",
                table: "MovimentiMagazzino",
                column: "OperatoreId",
                principalTable: "Utenti",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
