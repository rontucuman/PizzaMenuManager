using Microsoft.EntityFrameworkCore.Migrations;

namespace PizzaMenu.API.Migrations
{
    public partial class Added_IsInactive_field_to_Pizza : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsInactive",
                schema: "menu",
                table: "Pizza",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsInactive",
                schema: "menu",
                table: "Pizza");
        }
    }
}
