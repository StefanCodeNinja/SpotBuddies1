using Microsoft.EntityFrameworkCore.Migrations;

namespace SpotBuddies.Data.Migrations
{
    public partial class AddGenderAndChildFieldsMenuItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Child",
                table: "MenuItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "MenuItem",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Child",
                table: "MenuItem");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "MenuItem");
        }
    }
}
