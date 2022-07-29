using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReactMoviesWebApi.Migrations
{
    public partial class userRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0d08b73c-3419-45e2-b294-4be12127a592", "3", "Manager Genres", "Manager Genres" },
                    { "2da51f03-4cff-4fba-ad95-8cbfb002c8f8", "5", "Manager Movie Theaters", "Manager Movie Theaters" },
                    { "4c8fc76a-5c1a-4685-ba4a-28b07908d7e1", "4", "Manager Movies", "Manager Movies" },
                    { "6d3b3148-6ba6-4342-b03b-dc8719cef1b6", "1", "Admin", "Admin" },
                    { "c5138188-1b3a-40f9-8035-ca18a357f0f7", "2", "Manager Actors", "Manager Actors" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0d08b73c-3419-45e2-b294-4be12127a592");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2da51f03-4cff-4fba-ad95-8cbfb002c8f8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4c8fc76a-5c1a-4685-ba4a-28b07908d7e1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6d3b3148-6ba6-4342-b03b-dc8719cef1b6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c5138188-1b3a-40f9-8035-ca18a357f0f7");
        }
    }
}
