using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace projectef.Migrations
{
    /// <inheritdoc />
    public partial class InitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Task",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Category",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "Weight",
                table: "Category",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "CategoryId", "Description", "Name", "Weight" },
                values: new object[,]
                {
                    { new Guid("3871496c-0a27-45f9-b3fe-e7342d33b982"), null, "Actividades Pendientes", 20 },
                    { new Guid("3871496c-0a27-45f9-b3fe-e7342d33b9ef"), null, "Actividades Personales", 50 }
                });

            migrationBuilder.InsertData(
                table: "Task",
                columns: new[] { "JobId", "CategoryId", "CreationDate", "Description", "JobPriority", "Title" },
                values: new object[,]
                {
                    { new Guid("3871496c-0a27-45f9-b3fe-e7342d33b345"), new Guid("3871496c-0a27-45f9-b3fe-e7342d33b982"), new DateTime(2023, 9, 2, 10, 58, 41, 140, DateTimeKind.Local).AddTicks(1496), null, 1, "Pago de Servicios Publicos" },
                    { new Guid("3871496c-0a27-45f9-b3fe-e7342d33c345"), new Guid("3871496c-0a27-45f9-b3fe-e7342d33b9ef"), new DateTime(2023, 9, 2, 10, 58, 41, 140, DateTimeKind.Local).AddTicks(1512), null, 0, "Pago de Cuota Pase" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Task",
                keyColumn: "JobId",
                keyValue: new Guid("3871496c-0a27-45f9-b3fe-e7342d33b345"));

            migrationBuilder.DeleteData(
                table: "Task",
                keyColumn: "JobId",
                keyValue: new Guid("3871496c-0a27-45f9-b3fe-e7342d33c345"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: new Guid("3871496c-0a27-45f9-b3fe-e7342d33b982"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: new Guid("3871496c-0a27-45f9-b3fe-e7342d33b9ef"));

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Category");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Task",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Category",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
