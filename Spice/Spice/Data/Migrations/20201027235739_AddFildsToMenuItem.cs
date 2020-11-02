using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Spice.Data.Migrations
{
    public partial class AddFildsToMenuItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CountStorage",
                table: "MenuItem",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Image1",
                table: "MenuItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image2",
                table: "MenuItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image3",
                table: "MenuItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShortDesc",
                table: "MenuItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SizeLetter",
                table: "MenuItem",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "SizeNum",
                table: "MenuItem",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "SubCar3",
                table: "MenuItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubCat1",
                table: "MenuItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubCat2",
                table: "MenuItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UploadByUser",
                table: "MenuItem",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UploadDate",
                table: "MenuItem",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "active",
                table: "MenuItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageData = table.Column<byte[]>(nullable: true),
                    ProductId = table.Column<int>(nullable: false),
                    MenuItemId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Image_MenuItem_MenuItemId",
                        column: x => x.MenuItemId,
                        principalTable: "MenuItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Image_MenuItemId",
                table: "Image",
                column: "MenuItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.DropColumn(
                name: "CountStorage",
                table: "MenuItem");

            migrationBuilder.DropColumn(
                name: "Image1",
                table: "MenuItem");

            migrationBuilder.DropColumn(
                name: "Image2",
                table: "MenuItem");

            migrationBuilder.DropColumn(
                name: "Image3",
                table: "MenuItem");

            migrationBuilder.DropColumn(
                name: "ShortDesc",
                table: "MenuItem");

            migrationBuilder.DropColumn(
                name: "SizeLetter",
                table: "MenuItem");

            migrationBuilder.DropColumn(
                name: "SizeNum",
                table: "MenuItem");

            migrationBuilder.DropColumn(
                name: "SubCar3",
                table: "MenuItem");

            migrationBuilder.DropColumn(
                name: "SubCat1",
                table: "MenuItem");

            migrationBuilder.DropColumn(
                name: "SubCat2",
                table: "MenuItem");

            migrationBuilder.DropColumn(
                name: "UploadByUser",
                table: "MenuItem");

            migrationBuilder.DropColumn(
                name: "UploadDate",
                table: "MenuItem");

            migrationBuilder.DropColumn(
                name: "active",
                table: "MenuItem");
        }
    }
}
