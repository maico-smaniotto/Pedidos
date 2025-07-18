using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PedidosApi.Migrations
{
    /// <inheritdoc />
    public partial class AtualizaEntidadesClienteEndereco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enderecos_Clientes_cliente_id",
                table: "Enderecos");

            migrationBuilder.DropForeignKey(
                name: "FK_Enderecos_Municipios_municipio_id",
                table: "Enderecos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Municipios",
                table: "Municipios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Clientes",
                table: "Clientes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Enderecos",
                table: "Enderecos");

            migrationBuilder.DropIndex(
                name: "IX_Enderecos_municipio_id",
                table: "Enderecos");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Municipios");

            migrationBuilder.DropColumn(
                name: "municipio_id",
                table: "Enderecos");

            migrationBuilder.RenameTable(
                name: "Municipios",
                newName: "municipios");

            migrationBuilder.RenameTable(
                name: "Clientes",
                newName: "clientes");

            migrationBuilder.RenameTable(
                name: "Enderecos",
                newName: "clientes_enderecos");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "clientes",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "clientes_enderecos",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_Enderecos_cliente_id",
                table: "clientes_enderecos",
                newName: "IX_clientes_enderecos_cliente_id");

            migrationBuilder.AlterColumn<string>(
                name: "nome",
                table: "municipios",
                type: "character varying(120)",
                maxLength: 120,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "codigo_ibge",
                table: "municipios",
                type: "character varying(7)",
                maxLength: 7,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "razao_social",
                table: "clientes",
                type: "character varying(120)",
                maxLength: 120,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "nome_fantasia",
                table: "clientes",
                type: "character varying(60)",
                maxLength: 60,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "clientes",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "cpf_cnpj",
                table: "clientes",
                type: "character varying(18)",
                maxLength: 18,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "numero",
                table: "clientes_enderecos",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "logradouro",
                table: "clientes_enderecos",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "complemento",
                table: "clientes_enderecos",
                type: "character varying(120)",
                maxLength: 120,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "codigo_postal",
                table: "clientes_enderecos",
                type: "character varying(9)",
                maxLength: 9,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "bairro",
                table: "clientes_enderecos",
                type: "character varying(60)",
                maxLength: 60,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "municipio_codigo_ibge",
                table: "clientes_enderecos",
                type: "character varying(7)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_municipios",
                table: "municipios",
                column: "codigo_ibge");

            migrationBuilder.AddPrimaryKey(
                name: "PK_clientes",
                table: "clientes",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_clientes_enderecos",
                table: "clientes_enderecos",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_clientes_enderecos_municipio_codigo_ibge",
                table: "clientes_enderecos",
                column: "municipio_codigo_ibge");

            migrationBuilder.AddForeignKey(
                name: "FK_clientes_enderecos_clientes_cliente_id",
                table: "clientes_enderecos",
                column: "cliente_id",
                principalTable: "clientes",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_clientes_enderecos_municipios_municipio_codigo_ibge",
                table: "clientes_enderecos",
                column: "municipio_codigo_ibge",
                principalTable: "municipios",
                principalColumn: "codigo_ibge",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_clientes_enderecos_clientes_cliente_id",
                table: "clientes_enderecos");

            migrationBuilder.DropForeignKey(
                name: "FK_clientes_enderecos_municipios_municipio_codigo_ibge",
                table: "clientes_enderecos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_municipios",
                table: "municipios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_clientes",
                table: "clientes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_clientes_enderecos",
                table: "clientes_enderecos");

            migrationBuilder.DropIndex(
                name: "IX_clientes_enderecos_municipio_codigo_ibge",
                table: "clientes_enderecos");

            migrationBuilder.DropColumn(
                name: "municipio_codigo_ibge",
                table: "clientes_enderecos");

            migrationBuilder.RenameTable(
                name: "municipios",
                newName: "Municipios");

            migrationBuilder.RenameTable(
                name: "clientes",
                newName: "Clientes");

            migrationBuilder.RenameTable(
                name: "clientes_enderecos",
                newName: "Enderecos");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Clientes",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Enderecos",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_clientes_enderecos_cliente_id",
                table: "Enderecos",
                newName: "IX_Enderecos_cliente_id");

            migrationBuilder.AlterColumn<string>(
                name: "nome",
                table: "Municipios",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(120)",
                oldMaxLength: 120);

            migrationBuilder.AlterColumn<string>(
                name: "codigo_ibge",
                table: "Municipios",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(7)",
                oldMaxLength: 7);

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "Municipios",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<string>(
                name: "razao_social",
                table: "Clientes",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(120)",
                oldMaxLength: 120);

            migrationBuilder.AlterColumn<string>(
                name: "nome_fantasia",
                table: "Clientes",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(60)",
                oldMaxLength: 60);

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "Clientes",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "cpf_cnpj",
                table: "Clientes",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(18)",
                oldMaxLength: 18);

            migrationBuilder.AlterColumn<string>(
                name: "numero",
                table: "Enderecos",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "logradouro",
                table: "Enderecos",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "complemento",
                table: "Enderecos",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(120)",
                oldMaxLength: 120,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "codigo_postal",
                table: "Enderecos",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(9)",
                oldMaxLength: 9);

            migrationBuilder.AlterColumn<string>(
                name: "bairro",
                table: "Enderecos",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(60)",
                oldMaxLength: 60);

            migrationBuilder.AddColumn<long>(
                name: "municipio_id",
                table: "Enderecos",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Municipios",
                table: "Municipios",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Clientes",
                table: "Clientes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Enderecos",
                table: "Enderecos",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Enderecos_municipio_id",
                table: "Enderecos",
                column: "municipio_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Enderecos_Clientes_cliente_id",
                table: "Enderecos",
                column: "cliente_id",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Enderecos_Municipios_municipio_id",
                table: "Enderecos",
                column: "municipio_id",
                principalTable: "Municipios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
