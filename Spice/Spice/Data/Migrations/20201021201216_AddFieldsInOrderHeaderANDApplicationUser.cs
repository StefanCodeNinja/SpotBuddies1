using Microsoft.EntityFrameworkCore.Migrations;

namespace Spice.Data.Migrations
{
    public partial class AddFieldsInOrderHeaderANDApplicationUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeliveryCity",
                table: "OrderHeader",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeliveryCountry",
                table: "OrderHeader",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeliveryPostalCode",
                table: "OrderHeader",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeliveryStreet",
                table: "OrderHeader",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeliveryStreetNumber",
                table: "OrderHeader",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Reciever",
                table: "OrderHeader",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryCity",
                table: "OrderHeader");

            migrationBuilder.DropColumn(
                name: "DeliveryCountry",
                table: "OrderHeader");

            migrationBuilder.DropColumn(
                name: "DeliveryPostalCode",
                table: "OrderHeader");

            migrationBuilder.DropColumn(
                name: "DeliveryStreet",
                table: "OrderHeader");

            migrationBuilder.DropColumn(
                name: "DeliveryStreetNumber",
                table: "OrderHeader");

            migrationBuilder.DropColumn(
                name: "Reciever",
                table: "OrderHeader");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "AspNetUsers");
        }
    }
}
