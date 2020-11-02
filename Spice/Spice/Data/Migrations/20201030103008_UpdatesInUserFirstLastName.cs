using Microsoft.EntityFrameworkCore.Migrations;

namespace SpotBuddies.Data.Migrations
{
    public partial class UpdatesInUserFirstLastName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Reciever",
                table: "OrderHeader");

            migrationBuilder.DropColumn(
                name: "RecieverFamilyName",
                table: "OrderHeader");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "OrderHeader",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "OrderHeader",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "OrderHeader");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "OrderHeader");

            migrationBuilder.AddColumn<string>(
                name: "Reciever",
                table: "OrderHeader",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RecieverFamilyName",
                table: "OrderHeader",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
