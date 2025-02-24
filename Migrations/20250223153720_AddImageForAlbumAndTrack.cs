using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoundScape.Migrations
{
    /// <inheritdoc />
    public partial class AddImageForAlbumAndTrack : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordResetCode",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PasswordResetCodeExpiration",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "MusicTracks",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Albums",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "MusicTracks");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Albums");

            migrationBuilder.AddColumn<string>(
                name: "PasswordResetCode",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "PasswordResetCodeExpiration",
                table: "Users",
                type: "timestamp with time zone",
                nullable: true);
        }
    }
}
