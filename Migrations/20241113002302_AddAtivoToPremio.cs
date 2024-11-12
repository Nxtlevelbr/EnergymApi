using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnergymApi.Migrations
{
    /// <inheritdoc />
    public partial class AddAtivoToPremio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ativo",
                table: "tb_premios",
                type: "BOOLEAN",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ativo",
                table: "tb_premios");
        }
    }
}
