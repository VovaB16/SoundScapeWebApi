using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoundScape.Migrations
{
    /// <inheritdoc />
    public partial class addPasswordForAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AvatarUrl", "BirthDay", "BirthMonth", "BirthYear", "Email", "EmailConfirmed", "Gender", "PasswordHash", "Role", "Username" },
                values: new object[] { 2, "default_avatar_url", 1, 1, 2000, "admin@gmail.com", true, "Other", "$2a$11$uvvUDzAYE8lerlmXzRmc3.CbNvJUiXEtawg.iNNcme3zmeepVZsJW", "Admin", "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AvatarUrl", "BirthDay", "BirthMonth", "BirthYear", "Email", "EmailConfirmed", "Gender", "PasswordHash", "Role", "Username" },
                values: new object[] { 1, "default_avatar_url", 1, 1, 2000, "admin@gmail.com", true, "Other", "admin", "Admin", "admin" });
        }
    }
}
