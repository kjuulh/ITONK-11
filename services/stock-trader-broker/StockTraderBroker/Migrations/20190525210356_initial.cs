using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StockTraderBroker.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Requests",
                columns: table => new
                {
                    RequestId = table.Column<Guid>(nullable: false),
                    ShareId = table.Column<Guid>(nullable: false),
                    OwnerAccountId = table.Column<Guid>(nullable: false),
                    PortfolioId = table.Column<Guid>(nullable: false),
                    Amount = table.Column<int>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    DateAdded = table.Column<DateTime>(nullable: false),
                    DateClosed = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.RequestId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Requests");
        }
    }
}
