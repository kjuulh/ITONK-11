using Microsoft.EntityFrameworkCore.Migrations;

namespace Shares.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                "TotalValue",
                "Shares",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<decimal>(
                "SingleShareValue",
                "Shares",
                nullable: false,
                oldClrType: typeof(float));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                "TotalValue",
                "Shares",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<float>(
                "SingleShareValue",
                "Shares",
                nullable: false,
                oldClrType: typeof(decimal));
        }
    }
}