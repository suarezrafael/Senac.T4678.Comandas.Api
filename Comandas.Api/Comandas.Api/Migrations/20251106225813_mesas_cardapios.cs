using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Comandas.Api.Migrations
{
    /// <inheritdoc />
    public partial class mesas_cardapios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "CardapioItems",
                columns: new[] { "Id", "Descricao", "PossuiPreparo", "Preco", "Titulo" },
                values: new object[,]
                {
                    { 1, "XIS CARNE", true, 25m, "XIS CARNE" },
                    { 2, "COCA COLA LATA 350ML", false, 6m, "COCA COLA LATA 350ML" },
                    { 3, "TORRADA SIMPLES", true, 8m, "TORRADA SIMPLES" }
                });

            migrationBuilder.InsertData(
                table: "Mesas",
                columns: new[] { "Id", "NumeroMesa", "SituacaoMesa" },
                values: new object[,]
                {
                    { 1, 1, 0 },
                    { 2, 2, 0 },
                    { 3, 3, 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CardapioItems",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CardapioItems",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CardapioItems",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Mesas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Mesas",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Mesas",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
