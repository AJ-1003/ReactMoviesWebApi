using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReactMoviesWebApi.Migrations
{
    public partial class usersTableAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "AppUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsers", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "080ea92f-6959-4f3f-9721-d8dcf4401090", "1", "Admin", "Admin" },
                    { "1f4c6912-a7fd-4edf-a6bc-6b695a76d6c8", "5", "Manager Movie Theaters", "Manager Movie Theaters" },
                    { "686e129a-bf45-4d82-b0ce-f5c0c21c3365", "3", "Manager Genres", "Manager Genres" },
                    { "7bcbfd0f-ab09-4108-8ea1-310662cedc22", "2", "Manager Actors", "Manager Actors" },
                    { "d046db9a-67a6-43b7-9203-2d3053934947", "4", "Manager Movies", "Manager Movies" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUsers");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "080ea92f-6959-4f3f-9721-d8dcf4401090");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1f4c6912-a7fd-4edf-a6bc-6b695a76d6c8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "686e129a-bf45-4d82-b0ce-f5c0c21c3365");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7bcbfd0f-ab09-4108-8ea1-310662cedc22");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d046db9a-67a6-43b7-9203-2d3053934947");

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
    }
}
