using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mohafezApi.Migrations
{
    public partial class InitialCreat28 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Classroom",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "AspNetUsers",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "Classroom",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
