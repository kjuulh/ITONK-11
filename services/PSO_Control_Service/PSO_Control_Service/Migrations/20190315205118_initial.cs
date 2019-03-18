using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace PSO_Control_Service.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserModel",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Username = table.Column<string>(nullable: true),
                    UserPassword = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserModel", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "ShareModel",
                columns: table => new
                {
                    ShareId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ShareName = table.Column<string>(nullable: true),
                    ShareCount = table.Column<int>(nullable: false),
                    ShareValue = table.Column<int>(nullable: false),
                    UserModelUserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShareModel", x => x.ShareId);
                    table.ForeignKey(
                        name: "FK_ShareModel_UserModel_UserModelUserId",
                        column: x => x.UserModelUserId,
                        principalTable: "UserModel",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShareModel_UserModelUserId",
                table: "ShareModel",
                column: "UserModelUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShareModel");

            migrationBuilder.DropTable(
                name: "UserModel");
        }
    }
}
