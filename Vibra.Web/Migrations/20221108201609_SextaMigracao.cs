using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vibra.Web.Migrations
{
    public partial class SextaMigracao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "legal_name",
                table: "Customers",
                newName: "legalName");

            migrationBuilder.RenameColumn(
                name: "commercial_name",
                table: "Customers",
                newName: "commercialName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "legalName",
                table: "Customers",
                newName: "legal_name");

            migrationBuilder.RenameColumn(
                name: "commercialName",
                table: "Customers",
                newName: "commercial_name");
        }
    }
}
