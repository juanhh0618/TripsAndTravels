using Microsoft.EntityFrameworkCore.Migrations;

namespace TripsAndTravels.Web.Migrations
{
    public partial class AddBillPathColumnToExpensesEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BillPath",
                table: "TripDetails");

            migrationBuilder.DropColumn(
                name: "TripType",
                table: "TripDetails");

            migrationBuilder.AddColumn<string>(
                name: "BillPath",
                table: "Expenses",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BillPath",
                table: "Expenses");

            migrationBuilder.AddColumn<string>(
                name: "BillPath",
                table: "TripDetails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TripType",
                table: "TripDetails",
                nullable: false,
                defaultValue: "");
        }
    }
}
