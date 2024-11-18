using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnergymApi.Migrations
{
    /// <inheritdoc />
    public partial class RemoverAcademiaDeRegistroExercicio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_registros_exercicios_tb_academias_id_academia",
                table: "tb_registros_exercicios");

            migrationBuilder.RenameColumn(
                name: "id_academia",
                table: "tb_registros_exercicios",
                newName: "AcademiaId");

            migrationBuilder.RenameIndex(
                name: "IX_tb_registros_exercicios_id_academia",
                table: "tb_registros_exercicios",
                newName: "IX_tb_registros_exercicios_AcademiaId");

            migrationBuilder.AlterColumn<decimal>(
                name: "km",
                table: "tb_registros_exercicios",
                type: "FLOAT(38)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "FLOAT");

            migrationBuilder.AlterColumn<int>(
                name: "AcademiaId",
                table: "tb_registros_exercicios",
                type: "NUMBER(10)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "NUMBER(10)");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_registros_exercicios_tb_academias_AcademiaId",
                table: "tb_registros_exercicios",
                column: "AcademiaId",
                principalTable: "tb_academias",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_registros_exercicios_tb_academias_AcademiaId",
                table: "tb_registros_exercicios");

            migrationBuilder.RenameColumn(
                name: "AcademiaId",
                table: "tb_registros_exercicios",
                newName: "id_academia");

            migrationBuilder.RenameIndex(
                name: "IX_tb_registros_exercicios_AcademiaId",
                table: "tb_registros_exercicios",
                newName: "IX_tb_registros_exercicios_id_academia");

            migrationBuilder.AlterColumn<decimal>(
                name: "km",
                table: "tb_registros_exercicios",
                type: "FLOAT",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "FLOAT(38)");

            migrationBuilder.AlterColumn<int>(
                name: "id_academia",
                table: "tb_registros_exercicios",
                type: "NUMBER(10)",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "NUMBER(10)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_registros_exercicios_tb_academias_id_academia",
                table: "tb_registros_exercicios",
                column: "id_academia",
                principalTable: "tb_academias",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
