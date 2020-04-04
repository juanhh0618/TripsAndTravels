using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TripsAndTravels.Web.Migrations
{
    public partial class DropExpenseDataFromExpenseEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpenseDate",
                table: "Expenses");

            migrationBuilder.RenameColumn(
                name: "BillPath",
                table: "AspNetUsers",
                newName: "PicturePath");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PicturePath",
                table: "AspNetUsers",
                newName: "BillPath");

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpenseDate",
                table: "Expenses",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
