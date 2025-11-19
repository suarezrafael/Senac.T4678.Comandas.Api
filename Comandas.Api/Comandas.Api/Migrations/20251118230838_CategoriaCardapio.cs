using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Comandas.Api.Migrations
{
    /// <inheritdoc />
    public partial class CategoriaCardapio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoriaCardapioId",
                table: "CardapioItems",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CategoriaCardapio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriaCardapio", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "CardapioItems",
                keyColumn: "Id",
                keyValue: 1,
                column: "CategoriaCardapioId",
                value: null);

            migrationBuilder.UpdateData(
                table: "CardapioItems",
                keyColumn: "Id",
                keyValue: 2,
                column: "CategoriaCardapioId",
                value: null);

            migrationBuilder.UpdateData(
                table: "CardapioItems",
                keyColumn: "Id",
                keyValue: 3,
                column: "CategoriaCardapioId",
                value: null);

            migrationBuilder.InsertData(
                table: "CategoriaCardapio",
                columns: new[] { "Id", "Descricao", "Nome" },
                values: new object[,]
                {
                    { 1, null, "Lanches" },
                    { 2, null, "Bebidas" },
                    { 3, null, "Acompanhamentos" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComandaItens_CardapioItemId",
                table: "ComandaItens",
                column: "CardapioItemId");

            migrationBuilder.CreateIndex(
                name: "IX_CardapioItems_CategoriaCardapioId",
                table: "CardapioItems",
                column: "CategoriaCardapioId");

            migrationBuilder.AddForeignKey(
                name: "FK_CardapioItems_CategoriaCardapio_CategoriaCardapioId",
                table: "CardapioItems",
                column: "CategoriaCardapioId",
                principalTable: "CategoriaCardapio",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ComandaItens_CardapioItems_CardapioItemId",
                table: "ComandaItens",
                column: "CardapioItemId",
                principalTable: "CardapioItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CardapioItems_CategoriaCardapio_CategoriaCardapioId",
                table: "CardapioItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ComandaItens_CardapioItems_CardapioItemId",
                table: "ComandaItens");

            migrationBuilder.DropTable(
                name: "CategoriaCardapio");

            migrationBuilder.DropIndex(
                name: "IX_ComandaItens_CardapioItemId",
                table: "ComandaItens");

            migrationBuilder.DropIndex(
                name: "IX_CardapioItems_CategoriaCardapioId",
                table: "CardapioItems");

            migrationBuilder.DropColumn(
                name: "CategoriaCardapioId",
                table: "CardapioItems");
        }
    }
}
