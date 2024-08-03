using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PermissionsWeb.Data.Migrations
{
    /// <inheritdoc />
    public partial class Corrigiendoatributos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "TiposPermiso");

            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "Permisos",
                newName: "NombreEmpleado");

            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "TiposPermiso",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ApellidoEmpleado",
                table: "Permisos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaPermiso",
                table: "Permisos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "TiposPermiso");

            migrationBuilder.DropColumn(
                name: "ApellidoEmpleado",
                table: "Permisos");

            migrationBuilder.DropColumn(
                name: "FechaPermiso",
                table: "Permisos");

            migrationBuilder.RenameColumn(
                name: "NombreEmpleado",
                table: "Permisos",
                newName: "Nombre");

            migrationBuilder.AddColumn<int>(
                name: "Nombre",
                table: "TiposPermiso",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
