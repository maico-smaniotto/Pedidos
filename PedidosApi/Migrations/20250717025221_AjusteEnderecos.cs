using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PedidosApi.Migrations
{
    /// <inheritdoc />
    public partial class AjusteEnderecos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "padrao_entrega",
                table: "clientes_enderecos",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "padrao_faturamento",
                table: "clientes_enderecos",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "status_registro",
                table: "clientes_enderecos",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "padrao_entrega",
                table: "clientes_enderecos");

            migrationBuilder.DropColumn(
                name: "padrao_faturamento",
                table: "clientes_enderecos");

            migrationBuilder.DropColumn(
                name: "status_registro",
                table: "clientes_enderecos");
        }
    }
}
