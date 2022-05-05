using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YardScanAPI.Migrations
{
    public partial class AddedTrackInUnitStories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TrackInUnitHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UnitId = table.Column<int>(type: "int", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    Coordinates = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Row = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    RowNumber = table.Column<int>(type: "int", nullable: false),
                    TrackInDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrackInUnitHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrackInUnitHistories_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrackInUnitHistories_LocationId",
                table: "TrackInUnitHistories",
                column: "LocationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrackInUnitHistories");
        }
    }
}
