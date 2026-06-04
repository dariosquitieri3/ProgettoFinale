using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryApi.Migrations
{
    /// <inheritdoc />
    public partial class AggiuntaMovimentiEAllineamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovimentiMagazzino_Utenti_UtenteId",
                table: "MovimentiMagazzino");

            migrationBuilder.DropColumn(
                name: "Stato",
                table: "MovimentiMagazzino");

            migrationBuilder.RenameColumn(
                name: "Qta",
                table: "MovimentiMagazzino",
                newName: "Quantita");

            migrationBuilder.AlterColumn<int>(
                name: "UtenteId",
                table: "MovimentiMagazzino",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_MovimentiMagazzino_Utenti_UtenteId",
                table: "MovimentiMagazzino",
                column: "UtenteId",
                principalTable: "Utenti",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovimentiMagazzino_Utenti_UtenteId",
                table: "MovimentiMagazzino");

            migrationBuilder.RenameColumn(
                name: "Quantita",
                table: "MovimentiMagazzino",
                newName: "Qta");

            migrationBuilder.AlterColumn<int>(
                name: "UtenteId",
                table: "MovimentiMagazzino",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Stato",
                table: "MovimentiMagazzino",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_MovimentiMagazzino_Utenti_UtenteId",
                table: "MovimentiMagazzino",
                column: "UtenteId",
                principalTable: "Utenti",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
