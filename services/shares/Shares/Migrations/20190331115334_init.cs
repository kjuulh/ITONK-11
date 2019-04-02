using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shares.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Shares",
                columns: table => new
                {
                    ShareId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    TotalValue = table.Column<float>(nullable: false),
                    TotalCount = table.Column<int>(nullable: false),
                    SingleShareValue = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shares", x => x.ShareId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Shares");
        }
    }
}
