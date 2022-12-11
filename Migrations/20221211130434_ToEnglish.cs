using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace efproyecto.Migrations
{
    /// <inheritdoc />
    public partial class ToEnglish : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(180)", maxLength: 180, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Tarea",
                columns: table => new
                {
                    TodoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(180)", maxLength: 180, nullable: false),
                    TodoPriority = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tarea", x => x.TodoId);
                    table.ForeignKey(
                        name: "FK_Tarea_Categoria_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categoria",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categoria",
                columns: new[] { "CategoryId", "Description", "Title" },
                values: new object[,]
                {
                    { new Guid("4afb6792-125d-4a8d-8727-d756a620b611"), "Categoría de deportes", "Deporte" },
                    { new Guid("4afb6792-125d-4a8d-8727-d756a620b612"), "Categoría de estudios", "Estudios" }
                });

            migrationBuilder.InsertData(
                table: "Tarea",
                columns: new[] { "TodoId", "CategoryId", "CreationDate", "Description", "Title", "TodoPriority", "Weight" },
                values: new object[,]
                {
                    { new Guid("09aa00ba-6e45-49ab-819a-c6ed88d51991"), new Guid("4afb6792-125d-4a8d-8727-d756a620b611"), new DateTime(2022, 12, 11, 14, 4, 34, 531, DateTimeKind.Local).AddTicks(6182), "Cinta y ejercicios de hombros", "Ir al gimnasio", 0, 2 },
                    { new Guid("09aa00ba-6e45-49ab-819a-c6ed88d51992"), new Guid("4afb6792-125d-4a8d-8727-d756a620b612"), new DateTime(2022, 12, 11, 14, 4, 34, 531, DateTimeKind.Local).AddTicks(6223), "Repasar para entender mejor las bases", "Repasar curso APIs con .NET", 2, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tarea_CategoryId",
                table: "Tarea",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tarea");

            migrationBuilder.DropTable(
                name: "Categoria");
        }
    }
}
