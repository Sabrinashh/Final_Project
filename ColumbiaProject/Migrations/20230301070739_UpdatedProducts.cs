using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ColumbiaProject.Migrations
{
    public partial class UpdatedProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductTypes_TypeId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "TypeId",
                table: "Products",
                newName: "ProductTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_TypeId",
                table: "Products",
                newName: "IX_Products_ProductTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductTypes_ProductTypeId",
                table: "Products",
                column: "ProductTypeId",
                principalTable: "ProductTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductTypes_ProductTypeId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "ProductTypeId",
                table: "Products",
                newName: "TypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_ProductTypeId",
                table: "Products",
                newName: "IX_Products_TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductTypes_TypeId",
                table: "Products",
                column: "TypeId",
                principalTable: "ProductTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
