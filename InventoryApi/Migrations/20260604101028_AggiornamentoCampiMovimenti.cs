using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryApi.Migrations
{
    /// <inheritdoc />
    public partial class AggiornamentoCampiMovimenti : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovimentiMagazzino_Utenti_OperatoreId",
                table: "MovimentiMagazzino");

            migrationBuilder.DropIndex(
                name: "IX_MovimentiMagazzino_OperatoreId",
                table: "MovimentiMagazzino");

            migrationBuilder.DropColumn(
                name: "OperatoreId",
                table: "MovimentiMagazzino");

            migrationBuilder.RenameColumn(
                name: "Data",
                table: "MovimentiMagazzino",
                newName: "DataMovimento");

            migrationBuilder.AddColumn<int>(
                name: "UtenteId",
                table: "MovimentiMagazzino",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MovimentiMagazzino_UtenteId",
                table: "MovimentiMagazzino",
                column: "UtenteId");

            migrationBuilder.AddForeignKey(
                name: "FK_MovimentiMagazzino_Utenti_UtenteId",
                table: "MovimentiMagazzino",
                column: "UtenteId",
                principalTable: "Utenti",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovimentiMagazzino_Utenti_UtenteId",
                table: "MovimentiMagazzino");

            migrationBuilder.DropIndex(
                name: "IX_MovimentiMagazzino_UtenteId",
                table: "MovimentiMagazzino");

            migrationBuilder.DropColumn(
                name: "UtenteId",
                table: "MovimentiMagazzino");

            migrationBuilder.RenameColumn(
                name: "DataMovimento",
                table: "MovimentiMagazzino",
                newName: "Data");

            migrationBuilder.AddColumn<int>(
                name: "OperatoreId",
                table: "MovimentiMagazzino",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MovimentiMagazzino_OperatoreId",
                table: "MovimentiMagazzino",
                column: "OperatoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_MovimentiMagazzino_Utenti_OperatoreId",
                table: "MovimentiMagazzino",
                column: "OperatoreId",
                principalTable: "Utenti",
                principalColumn: "Id");
        }
    }
}
