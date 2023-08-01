using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mohafezApi.Migrations
{
    public partial class InitialCreate30 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Hours",
                table: "Tables",
                newName: "ToHours");

            migrationBuilder.AddColumn<string>(
                name: "FromHours",
                table: "Tables",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FromHours",
                table: "Tables");

            migrationBuilder.RenameColumn(
                name: "ToHours",
                table: "Tables",
                newName: "Hours");
        }
    }
}
