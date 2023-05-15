using Microsoft.EntityFrameworkCore.Migrations;

namespace NeyroClinic.Migrations
{
    public partial class AddIsDeactiveColumnToAppointmantTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeactive",
                table: "Appointments",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeactive",
                table: "Appointments");
        }
    }
}
