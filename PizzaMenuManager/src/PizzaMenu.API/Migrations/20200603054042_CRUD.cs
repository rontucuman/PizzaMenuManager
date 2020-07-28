using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PizzaMenu.API.Migrations
{
    public partial class CRUD : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "menu");

            migrationBuilder.RenameTable(
                name: "Pizza",
                schema: "PizzaMenu",
                newName: "Pizza",
                newSchema: "menu");

            migrationBuilder.AlterColumn<Guid>(
                name: "RowGuid",
                schema: "menu",
                table: "Pizza",
                nullable: false,
                defaultValueSql: "NEWID()",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "menu",
                table: "Pizza",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "Ingredient",
                schema: "menu",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RowGuid = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    Name = table.Column<string>(maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredient", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PizzaIngredient",
                schema: "menu",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PizzaId = table.Column<int>(nullable: false),
                    IngredientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PizzaIngredient", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PizzaIngredient_Ingredient_IngredientId",
                        column: x => x.IngredientId,
                        principalSchema: "menu",
                        principalTable: "Ingredient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PizzaIngredient_Pizza_PizzaId",
                        column: x => x.PizzaId,
                        principalSchema: "menu",
                        principalTable: "Pizza",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PizzaIngredient_IngredientId",
                schema: "menu",
                table: "PizzaIngredient",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_PizzaIngredient_PizzaId",
                schema: "menu",
                table: "PizzaIngredient",
                column: "PizzaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PizzaIngredient",
                schema: "menu");

            migrationBuilder.DropTable(
                name: "Ingredient",
                schema: "menu");

            migrationBuilder.EnsureSchema(
                name: "PizzaMenu");

            migrationBuilder.RenameTable(
                name: "Pizza",
                schema: "menu",
                newName: "Pizza",
                newSchema: "PizzaMenu");

            migrationBuilder.AlterColumn<Guid>(
                name: "RowGuid",
                schema: "PizzaMenu",
                table: "Pizza",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldDefaultValueSql: "NEWID()");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "PizzaMenu",
                table: "Pizza",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 64);
        }
    }
}
