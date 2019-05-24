using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shares.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Shares",
                table => new
                {
                    ShareId = table.Column<Guid>(),
                    Name = table.Column<string>(nullable: true),
                    TotalValue = table.Column<float>(),
                    TotalCount = table.Column<int>(),
                    SingleShareValue = table.Column<float>()
                },
                constraints: table => { table.PrimaryKey("PK_Shares", x => x.ShareId); });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Shares");
        }
    }
}