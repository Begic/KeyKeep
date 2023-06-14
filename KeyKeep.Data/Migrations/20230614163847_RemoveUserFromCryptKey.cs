using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KeyKeep.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUserFromCryptKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CryptKeys_Users_UserId",
                table: "CryptKeys");

            migrationBuilder.DropIndex(
                name: "IX_CryptKeys_UserId",
                table: "CryptKeys");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "CryptKeys");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "CryptKeys",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CryptKeys_UserId",
                table: "CryptKeys",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CryptKeys_Users_UserId",
                table: "CryptKeys",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
