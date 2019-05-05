using Microsoft.EntityFrameworkCore.Migrations;

namespace Tests_server_app.Migrations
{
    public partial class fixedInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoleName",
                table: "Roles");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Roles",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Roles");

            migrationBuilder.AddColumn<int>(
                name: "RoleName",
                table: "Roles",
                nullable: false,
                defaultValue: 0);
        }
    }
}
