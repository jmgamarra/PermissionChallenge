using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PermissionsWeb.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TiposPermiso",
                columns: new[] { "Id", "Descripcion" },
                values: new object[,]
                {
                    { 1, "Tipo 1" },
                    { 2, "Tipo 2" }
                });

            migrationBuilder.InsertData(
                table: "Permisos",
                columns: new[] { "Id", "ApellidoEmpleado", "FechaPermiso", "NombreEmpleado", "TipoPermisoId" },
                values: new object[] { 1, "Soto", new DateTime(2024, 3, 14, 13, 46, 40, 620, DateTimeKind.Utc).AddTicks(4487), "Jose", 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TiposPermiso",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TiposPermiso",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
