using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HaveARoom_CoreProject.Migrations
{
    public partial class TotalPriceAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "TotalPrice",
                table: "Reservations",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "Reservations");
        }
    }
}
