using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReactMoviesWebApi.Migrations
{
    public partial class fileTablesUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActorPictures_Files_PictureId",
                table: "ActorPictures");

            migrationBuilder.DropForeignKey(
                name: "FK_MoviePosters_Files_PosterId",
                table: "MoviePosters");

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
                    { "095c8b91-0408-4c8f-8d92-073a7482a0f7", "3", "Manager Genres", "Manager Genres" },
                    { "6ce92446-cfff-433f-a131-01f60e878d46", "5", "Manager Movie Theaters", "Manager Movie Theaters" },
                    { "a5be381b-a4b6-4305-ae54-6eb2752f9898", "4", "Manager Movies", "Manager Movies" },
                    { "a6bc2935-83c4-4b59-b7e7-a092151f5191", "1", "Admin", "Admin" },
                    { "c63bfff0-9dc8-44a2-9049-741a1516d57a", "2", "Manager Actors", "Manager Actors" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "095c8b91-0408-4c8f-8d92-073a7482a0f7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6ce92446-cfff-433f-a131-01f60e878d46");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a5be381b-a4b6-4305-ae54-6eb2752f9898");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a6bc2935-83c4-4b59-b7e7-a092151f5191");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c63bfff0-9dc8-44a2-9049-741a1516d57a");

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

            migrationBuilder.AddForeignKey(
                name: "FK_ActorPictures_Files_PictureId",
                table: "ActorPictures",
                column: "PictureId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MoviePosters_Files_PosterId",
                table: "MoviePosters",
                column: "PosterId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
