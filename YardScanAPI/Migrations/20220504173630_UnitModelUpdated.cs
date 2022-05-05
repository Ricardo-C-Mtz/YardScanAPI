using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YardScanAPI.Migrations
{
    public partial class UnitModelUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TrackOutLocationId",
                table: "Units",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrackOutLocationId",
                table: "Units");
        }
    }
}
