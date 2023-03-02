using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ColumbiaProject.Migrations
{
    public partial class ProductUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "StockStatus",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StockStatus",
                table: "Products");
        }
    }
}
