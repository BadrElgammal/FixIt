using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FixIt.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class confirmEmail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ConfirmationToken",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsEmailConfirmed",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfirmationToken",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsEmailConfirmed",
                table: "Users");
        }
    }
}
