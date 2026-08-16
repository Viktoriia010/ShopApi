using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AllowNullForPassword : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefreshTokens_users_user_id",
                table: "RefreshTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RefreshTokens",
                table: "RefreshTokens");

            migrationBuilder.RenameTable(
                name: "RefreshTokens",
                newName: "refresh-token");

            migrationBuilder.RenameIndex(
                name: "IX_RefreshTokens_user_id",
                table: "refresh-token",
                newName: "IX_refresh-token_user_id");

            migrationBuilder.AlterColumn<string>(
                name: "password_hash",
                table: "users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_refresh-token",
                table: "refresh-token",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_refresh-token_users_user_id",
                table: "refresh-token",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_refresh-token_users_user_id",
                table: "refresh-token");

            migrationBuilder.DropPrimaryKey(
                name: "PK_refresh-token",
                table: "refresh-token");

            migrationBuilder.RenameTable(
                name: "refresh-token",
                newName: "RefreshTokens");

            migrationBuilder.RenameIndex(
                name: "IX_refresh-token_user_id",
                table: "RefreshTokens",
                newName: "IX_RefreshTokens_user_id");

            migrationBuilder.AlterColumn<string>(
                name: "password_hash",
                table: "users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RefreshTokens",
                table: "RefreshTokens",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshTokens_users_user_id",
                table: "RefreshTokens",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
