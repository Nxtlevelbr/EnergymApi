using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnergymApi.Migrations
{
    /// <inheritdoc />
    public partial class AtualizaConversaoBooleano : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ativo",
                table: "tb_premios",
                type: "NUMBER(1)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "BOOLEAN");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "ativo",
                table: "tb_premios",
                type: "BOOLEAN",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "NUMBER(1)");
        }
    }
}
