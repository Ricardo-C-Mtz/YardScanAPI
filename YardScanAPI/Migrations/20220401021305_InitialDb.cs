using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YardScanAPI.Migrations
{
    public partial class InitialDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UnitId = table.Column<int>(type: "int", nullable: false),
                    Coordinates = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Row = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    RowNumber = table.Column<int>(type: "int", nullable: false),
                    TrackInDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TrackOutDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Units");
        }
    }
}
