using Microsoft.EntityFrameworkCore.Migrations;

namespace TripsAndTravels.Web.Migrations
{
    public partial class FixDestinyCityLength : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DestinyCity",
                table: "Trips",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 4);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DestinyCity",
                table: "Trips",
                maxLength: 4,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 20);
        }
    }
}
