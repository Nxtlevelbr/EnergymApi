using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnergymApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_enderecos",
                columns: table => new
                {
                    id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    cep = table.Column<string>(type: "NVARCHAR2(8)", maxLength: 8, nullable: false),
                    estado = table.Column<string>(type: "NVARCHAR2(2)", maxLength: 2, nullable: false),
                    cidade = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    rua = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    numero = table.Column<string>(type: "NVARCHAR2(10)", maxLength: 10, nullable: false),
                    complemento = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_enderecos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tb_premios",
                columns: table => new
                {
                    id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    descricao = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false),
                    pontos = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    empresa = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    tipo = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_premios", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tb_usuarios",
                columns: table => new
                {
                    id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    username = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    cpf = table.Column<string>(type: "NVARCHAR2(11)", maxLength: 11, nullable: false),
                    senha = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    data_nascimento = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    sexo = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    pontos = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_usuarios", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tb_academias",
                columns: table => new
                {
                    id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    cnpj = table.Column<string>(type: "NVARCHAR2(14)", maxLength: 14, nullable: false),
                    nome = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    observacao = table.Column<string>(type: "NVARCHAR2(255)", maxLength: 255, nullable: true),
                    id_endereco = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    usuario = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    senha = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_academias", x => x.id);
                    table.ForeignKey(
                        name: "FK_tb_academias_tb_enderecos_id_endereco",
                        column: x => x.id_endereco,
                        principalTable: "tb_enderecos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_resgates",
                columns: table => new
                {
                    id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    id_usuario = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    id_premio = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    data_hora = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_resgates", x => x.id);
                    table.ForeignKey(
                        name: "FK_tb_resgates_tb_premios_id_premio",
                        column: x => x.id_premio,
                        principalTable: "tb_premios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_resgates_tb_usuarios_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "tb_usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_registros_exercicios",
                columns: table => new
                {
                    id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    id_usuario = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    id_academia = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    km = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    data_hora = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_registros_exercicios", x => x.id);
                    table.ForeignKey(
                        name: "FK_tb_registros_exercicios_tb_academias_id_academia",
                        column: x => x.id_academia,
                        principalTable: "tb_academias",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_registros_exercicios_tb_usuarios_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "tb_usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_academias_cnpj",
                table: "tb_academias",
                column: "cnpj",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_academias_id_endereco",
                table: "tb_academias",
                column: "id_endereco");

            migrationBuilder.CreateIndex(
                name: "IX_tb_registros_exercicios_id_academia",
                table: "tb_registros_exercicios",
                column: "id_academia");

            migrationBuilder.CreateIndex(
                name: "IX_tb_registros_exercicios_id_usuario",
                table: "tb_registros_exercicios",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_tb_resgates_id_premio",
                table: "tb_resgates",
                column: "id_premio");

            migrationBuilder.CreateIndex(
                name: "IX_tb_resgates_id_usuario",
                table: "tb_resgates",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_tb_usuarios_cpf",
                table: "tb_usuarios",
                column: "cpf",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_usuarios_email",
                table: "tb_usuarios",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_usuarios_username",
                table: "tb_usuarios",
                column: "username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_registros_exercicios");

            migrationBuilder.DropTable(
                name: "tb_resgates");

            migrationBuilder.DropTable(
                name: "tb_academias");

            migrationBuilder.DropTable(
                name: "tb_premios");

            migrationBuilder.DropTable(
                name: "tb_usuarios");

            migrationBuilder.DropTable(
                name: "tb_enderecos");
        }
    }
}
