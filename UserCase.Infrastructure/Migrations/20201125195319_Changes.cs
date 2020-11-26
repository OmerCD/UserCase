using Microsoft.EntityFrameworkCore.Migrations;

namespace UserCase.Infrastructure.Migrations
{
    public partial class Changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Company",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Address",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Address");
        }
    }
}
