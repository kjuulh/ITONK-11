using Microsoft.EntityFrameworkCore.Migrations;

namespace PSO_Control_Service.Migrations
{
    public partial class fixed_models : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShareModel_UserModel_UserModelUserId",
                table: "ShareModel");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "UserModel",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "UserPassword",
                table: "UserModel",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UserModel",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "UserModelUserId",
                table: "ShareModel",
                newName: "UserModelId");

            migrationBuilder.RenameColumn(
                name: "ShareValue",
                table: "ShareModel",
                newName: "Value");

            migrationBuilder.RenameColumn(
                name: "ShareName",
                table: "ShareModel",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "ShareCount",
                table: "ShareModel",
                newName: "Count");

            migrationBuilder.RenameColumn(
                name: "ShareId",
                table: "ShareModel",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_ShareModel_UserModelUserId",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShareModel_UserModel_UserModelId",
                table: "ShareModel");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "UserModel",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "UserModel",
                newName: "UserPassword");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "UserModel",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "ShareModel",
                newName: "ShareValue");

            migrationBuilder.RenameColumn(
                name: "UserModelId",
                table: "ShareModel",
                newName: "UserModelUserId");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "ShareModel",
                newName: "ShareName");

            migrationBuilder.RenameColumn(
                name: "Count",
                table: "ShareModel",
                newName: "ShareCount");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ShareModel",
                newName: "ShareId");

            migrationBuilder.RenameIndex(
                name: "IX_ShareModel_UserModelId",
                table: "ShareModel",
                newName: "IX_ShareModel_UserModelUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShareModel_UserModel_UserModelUserId",
                table: "ShareModel",
                column: "UserModelUserId",
                principalTable: "UserModel",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
