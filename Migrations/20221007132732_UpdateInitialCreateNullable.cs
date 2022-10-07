using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace la_mia_pizzeria_static.Migrations
{
    public partial class UpdateInitialCreateNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ListaPizze_Categories_CategoryId",
                table: "ListaPizze");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "ListaPizze",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ListaPizze_Categories_CategoryId",
                table: "ListaPizze",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ListaPizze_Categories_CategoryId",
                table: "ListaPizze");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "ListaPizze",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ListaPizze_Categories_CategoryId",
                table: "ListaPizze",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
