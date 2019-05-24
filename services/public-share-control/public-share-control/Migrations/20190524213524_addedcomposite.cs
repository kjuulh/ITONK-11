using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PublicShareControl.Migrations
{
    public partial class addedcomposite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Share",
                table: "Share");

            migrationBuilder.AlterColumn<Guid>(
                name: "PortfolioId",
                table: "Share",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Share",
                table: "Share",
                columns: new[] { "ShareId", "PortfolioId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Share",
                table: "Share");

            migrationBuilder.AlterColumn<Guid>(
                name: "PortfolioId",
                table: "Share",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Share",
                table: "Share",
                column: "ShareId");
        }
    }
}
