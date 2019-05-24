using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PublicShareControl.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Portfolios",
                columns: table => new
                {
                    PortfolioId = table.Column<Guid>(nullable: false),
                    OwnerId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Portfolios", x => x.PortfolioId);
                });

            migrationBuilder.CreateTable(
                name: "Share",
                columns: table => new
                {
                    ShareId = table.Column<Guid>(nullable: false),
                    Count = table.Column<int>(nullable: false),
                    PortfolioId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Share", x => x.ShareId);
                    table.ForeignKey(
                        name: "FK_Share_Portfolios_PortfolioId",
                        column: x => x.PortfolioId,
                        principalTable: "Portfolios",
                        principalColumn: "PortfolioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Share_PortfolioId",
                table: "Share",
                column: "PortfolioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Share");

            migrationBuilder.DropTable(
                name: "Portfolios");
        }
    }
}
