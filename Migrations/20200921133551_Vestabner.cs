using Microsoft.EntityFrameworkCore.Migrations;

namespace VestaAbner.Migrations
{
    public partial class Vestabner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "User",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "firstname",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "lastName",
                table: "User",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "User");

            migrationBuilder.DropColumn(
                name: "firstname",
                table: "User");

            migrationBuilder.DropColumn(
                name: "lastName",
                table: "User");
        }
    }
}
