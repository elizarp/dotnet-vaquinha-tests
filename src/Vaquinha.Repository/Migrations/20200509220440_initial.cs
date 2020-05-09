using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Vaquinha.Repository.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Endereco",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CEP = table.Column<string>(maxLength: 15, nullable: false),
                    TextoEndereco = table.Column<string>(maxLength: 250, nullable: false),
                    Numero = table.Column<string>(nullable: true),
                    Complemento = table.Column<string>(maxLength: 250, nullable: true),
                    Cidade = table.Column<string>(maxLength: 150, nullable: false),
                    Estado = table.Column<string>(maxLength: 2, nullable: false),
                    Telefone = table.Column<string>(maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Endereco", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pessoa",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(maxLength: 150, nullable: false),
                    Anonima = table.Column<bool>(nullable: false),
                    MensagemApoio = table.Column<string>(maxLength: 500, nullable: false),
                    Email = table.Column<string>(maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pessoa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Doacao",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(9,2)", nullable: false),
                    DadosPessoaisId = table.Column<Guid>(nullable: false),
                    EnderecoCobrancaId = table.Column<Guid>(nullable: false),
                    DetalheTransacaoId = table.Column<Guid>(nullable: false),
                    DataHora = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Doacao_Pessoa_DadosPessoaisId",
                        column: x => x.DadosPessoaisId,
                        principalTable: "Pessoa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Doacao_Endereco_EnderecoCobrancaId",
                        column: x => x.EnderecoCobrancaId,
                        principalTable: "Endereco",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Doacao_DadosPessoaisId",
                table: "Doacao",
                column: "DadosPessoaisId");

            migrationBuilder.CreateIndex(
                name: "IX_Doacao_EnderecoCobrancaId",
                table: "Doacao",
                column: "EnderecoCobrancaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Doacao");

            migrationBuilder.DropTable(
                name: "Pessoa");

            migrationBuilder.DropTable(
                name: "Endereco");
        }
    }
}
