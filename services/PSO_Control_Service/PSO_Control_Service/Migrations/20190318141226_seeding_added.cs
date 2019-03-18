using Microsoft.EntityFrameworkCore.Migrations;

namespace PSO_Control_Service.Migrations
{
    public partial class seeding_added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShareModel_UserModel_UserModelId",
                table: "ShareModel");

            migrationBuilder.RenameColumn(
                name: "UserModelId",
                table: "ShareModel",
                newName: "OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_ShareModel_UserModelId",
                table: "ShareModel",
                newName: "IX_ShareModel_OwnerId");

            migrationBuilder.InsertData(
                table: "UserModel",
                columns: new[] { "Id", "Name", "Password" },
                values: new object[] { 1, "SeededUser", "pass" });

            migrationBuilder.InsertData(
                table: "ShareModel",
                columns: new[] { "Id", "Count", "Name", "OwnerId", "Value" },
                values: new object[] { 1, 1, "Seeded SHare", 1, 300 });

            migrationBuilder.AddForeignKey(
                name: "FK_ShareModel_UserModel_OwnerId",
                table: "ShareModel",
                column: "OwnerId",
                principalTable: "UserModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShareModel_UserModel_OwnerId",
                table: "ShareModel");

            migrationBuilder.DeleteData(
                table: "ShareModel",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UserModel",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "ShareModel",
                newName: "UserModelId");

            migrationBuilder.RenameIndex(
                name: "IX_ShareModel_OwnerId",
                table: "ShareModel",
                newName: "IX_ShareModel_UserModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShareModel_UserModel_UserModelId",
                table: "ShareModel",
                column: "UserModelId",
                principalTable: "UserModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
