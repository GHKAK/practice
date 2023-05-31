using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Passports.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuditPassportEntries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Series = table.Column<short>(type: "smallint", nullable: false),
                    Number = table.Column<int>(type: "integer", nullable: false),
                    ChangeDate = table.Column<DateOnly>(type: "date", nullable: false),
                    IsActual = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditPassportEntries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Passports",
                columns: table => new
                {
                    Series = table.Column<short>(type: "smallint", nullable: false),
                    Number = table.Column<int>(type: "integer", nullable: false),
                    IsActual = table.Column<bool>(type: "boolean", nullable: false),
                    ChangeDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passports", x => new { x.Series, x.Number });
                });

            migrationBuilder.CreateIndex(
                name: "IX_Passports_IsActual",
                table: "Passports",
                column: "IsActual");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditPassportEntries");

            migrationBuilder.DropTable(
                name: "Passports");
        }
    }
}
