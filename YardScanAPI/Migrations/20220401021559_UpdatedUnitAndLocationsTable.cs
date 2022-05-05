using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YardScanAPI.Migrations
{
    public partial class UpdatedUnitAndLocationsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Units",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Units_LocationId",
                table: "Units",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Units_Locations_LocationId",
                table: "Units",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Units_Locations_LocationId",
                table: "Units");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_Units_LocationId",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Units");
        }
    }
}
