using Microsoft.EntityFrameworkCore.Migrations;

namespace Tests_server_app.Migrations
{
    public partial class fixedInitial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Cheched",
                table: "Tests",
                newName: "Checked");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Checked",
                table: "Tests",
                newName: "Cheched");
        }
    }
}
