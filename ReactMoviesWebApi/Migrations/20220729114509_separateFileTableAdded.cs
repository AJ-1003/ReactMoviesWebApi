using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReactMoviesWebApi.Migrations
{
    public partial class separateFileTableAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45ae724b-97ae-4967-a494-ca060942bd92");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "82a35415-1c8d-431a-88dc-0461b44531c9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ae9bcb55-8d0a-4f20-bfb2-047319d5dfa3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b0544d96-cb43-47f3-8971-64bc189b6d2c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c9f75713-4a7a-4c94-b66f-0819fa6effcc");

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ActorPictures",
                columns: table => new
                {
                    PictureId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ActorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActorPictures", x => new { x.PictureId, x.ActorId });
                    table.ForeignKey(
                        name: "FK_ActorPictures_Actors_ActorId",
                        column: x => x.ActorId,
                        principalTable: "Actors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActorPictures_Files_PictureId",
                        column: x => x.PictureId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MoviePosters",
                columns: table => new
                {
                    PosterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MovieId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoviePosters", x => new { x.PosterId, x.MovieId });
                    table.ForeignKey(
                        name: "FK_MoviePosters_Files_PosterId",
                        column: x => x.PosterId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MoviePosters_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "45e22d34-9f8e-4ced-918a-badc10e07674", "1", "Admin", "Admin" },
                    { "7726087e-9f4e-412d-b751-85f75584dbc1", "4", "Manager Movies", "Manager Movies" },
                    { "874a6e89-d012-484c-87b0-279626e9c72b", "2", "Manager Actors", "Manager Actors" },
                    { "bc4d53a1-3627-48b3-951d-e5f5f5ea3878", "3", "Manager Genres", "Manager Genres" },
                    { "c1614af0-d57e-43e0-8f49-f336385666d8", "5", "Manager Movie Theaters", "Manager Movie Theaters" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActorPictures_ActorId",
                table: "ActorPictures",
                column: "ActorId");

            migrationBuilder.CreateIndex(
                name: "IX_MoviePosters_MovieId",
                table: "MoviePosters",
                column: "MovieId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActorPictures");

            migrationBuilder.DropTable(
                name: "MoviePosters");

            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45e22d34-9f8e-4ced-918a-badc10e07674");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7726087e-9f4e-412d-b751-85f75584dbc1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "874a6e89-d012-484c-87b0-279626e9c72b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bc4d53a1-3627-48b3-951d-e5f5f5ea3878");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c1614af0-d57e-43e0-8f49-f336385666d8");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "45ae724b-97ae-4967-a494-ca060942bd92", "3", "Manager Genres", "Manager Genres" },
                    { "82a35415-1c8d-431a-88dc-0461b44531c9", "1", "Admin", "Admin" },
                    { "ae9bcb55-8d0a-4f20-bfb2-047319d5dfa3", "2", "Manager Actors", "Manager Actors" },
                    { "b0544d96-cb43-47f3-8971-64bc189b6d2c", "5", "Manager Movie Theaters", "Manager Movie Theaters" },
                    { "c9f75713-4a7a-4c94-b66f-0819fa6effcc", "4", "Manager Movies", "Manager Movies" }
                });
        }
    }
}
