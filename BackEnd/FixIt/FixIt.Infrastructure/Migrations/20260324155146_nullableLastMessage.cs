using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FixIt.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class nullableLastMessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatRooms_Users_ClientId",
                table: "ChatRooms");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatRooms_Users_WorkerId",
                table: "ChatRooms");

            migrationBuilder.RenameColumn(
                name: "WorkerId",
                table: "ChatRooms",
                newName: "TargetUserId");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "ChatRooms",
                newName: "CurrentUserId");

            migrationBuilder.RenameIndex(
                name: "IX_ChatRooms_WorkerId",
                table: "ChatRooms",
                newName: "IX_ChatRooms_TargetUserId");

            migrationBuilder.RenameIndex(
                name: "IX_ChatRooms_ClientId",
                table: "ChatRooms",
                newName: "IX_ChatRooms_CurrentUserId");

            migrationBuilder.AlterColumn<string>(
                name: "LastMessage",
                table: "ChatRooms",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatRooms_Users_CurrentUserId",
                table: "ChatRooms",
                column: "CurrentUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatRooms_Users_TargetUserId",
                table: "ChatRooms",
                column: "TargetUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatRooms_Users_CurrentUserId",
                table: "ChatRooms");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatRooms_Users_TargetUserId",
                table: "ChatRooms");

            migrationBuilder.RenameColumn(
                name: "TargetUserId",
                table: "ChatRooms",
                newName: "WorkerId");

            migrationBuilder.RenameColumn(
                name: "CurrentUserId",
                table: "ChatRooms",
                newName: "ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_ChatRooms_TargetUserId",
                table: "ChatRooms",
                newName: "IX_ChatRooms_WorkerId");

            migrationBuilder.RenameIndex(
                name: "IX_ChatRooms_CurrentUserId",
                table: "ChatRooms",
                newName: "IX_ChatRooms_ClientId");

            migrationBuilder.AlterColumn<string>(
                name: "LastMessage",
                table: "ChatRooms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatRooms_Users_ClientId",
                table: "ChatRooms",
                column: "ClientId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatRooms_Users_WorkerId",
                table: "ChatRooms",
                column: "WorkerId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
