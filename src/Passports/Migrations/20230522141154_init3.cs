using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Passports.Migrations
{
    /// <inheritdoc />
    public partial class init3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Passports_IsActual",
                table: "Passports",
                column: "IsActual");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Passports_IsActual",
                table: "Passports");
        }
    }
}
