using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoundScape.Migrations
{
    /// <inheritdoc />
    public partial class addFavouriteTracks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserFavoriteTracks",
                columns: table => new
                {
                    FavoriteTracksId = table.Column<int>(type: "integer", nullable: false),
                    FavoritedByUsersId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFavoriteTracks", x => new { x.FavoriteTracksId, x.FavoritedByUsersId });
                    table.ForeignKey(
                        name: "FK_UserFavoriteTracks_MusicTracks_FavoriteTracksId",
                        column: x => x.FavoriteTracksId,
                        principalTable: "MusicTracks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserFavoriteTracks_Users_FavoritedByUsersId",
                        column: x => x.FavoritedByUsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserFavoriteTracks_FavoritedByUsersId",
                table: "UserFavoriteTracks",
                column: "FavoritedByUsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserFavoriteTracks");
        }
    }
}
