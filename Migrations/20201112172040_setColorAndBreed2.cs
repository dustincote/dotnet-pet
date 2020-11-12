using Microsoft.EntityFrameworkCore.Migrations;

namespace dotnet_bakery.Migrations
{
    public partial class setColorAndBreed2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PetBreed",
                table: "pets");

            migrationBuilder.DropColumn(
                name: "PetColor",
                table: "pets");

            migrationBuilder.AddColumn<string>(
                name: "Breed",
                table: "pets",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "pets",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Breed",
                table: "pets");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "pets");

            migrationBuilder.AddColumn<string>(
                name: "PetBreed",
                table: "pets",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PetColor",
                table: "pets",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
