using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TestesMicroservicoDocker.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "pessoatipo",
                columns: table => new
                {
                    idpessoatipo = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tag = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    descricao = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pessoatipo", x => x.idpessoatipo);
                });

            migrationBuilder.CreateTable(
                name: "pessoa",
                columns: table => new
                {
                    idpessoa = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nomecompleto = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    nomeresumido = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    idpessoatipo = table.Column<int>(type: "integer", nullable: false),
                    documento = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    datanascimento = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    datafundacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pessoa", x => x.idpessoa);
                    table.ForeignKey(
                        name: "fk_pessoa_pessoatipo",
                        column: x => x.idpessoatipo,
                        principalTable: "pessoatipo",
                        principalColumn: "idpessoatipo");
                });

            migrationBuilder.CreateIndex(
                name: "IX_pessoa_idpessoatipo",
                table: "pessoa",
                column: "idpessoatipo");

            migrationBuilder.CreateIndex(
                name: "uk_pessoa",
                table: "pessoa",
                column: "documento",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "uk_pessoatipo",
                table: "pessoatipo",
                column: "descricao",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "pessoa");

            migrationBuilder.DropTable(
                name: "pessoatipo");
        }
    }
}
