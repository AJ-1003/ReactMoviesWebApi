using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReactMoviesWebApi.Migrations
{
    public partial class actorsTableUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    { "45ae724b-97ae-4967-a494-ca060942bd92", "3", "Manager Genres", "Manager Genres" },
                    { "82a35415-1c8d-431a-88dc-0461b44531c9", "1", "Admin", "Admin" },
                    { "ae9bcb55-8d0a-4f20-bfb2-047319d5dfa3", "2", "Manager Actors", "Manager Actors" },
                    { "b0544d96-cb43-47f3-8971-64bc189b6d2c", "5", "Manager Movie Theaters", "Manager Movie Theaters" },
                    { "c9f75713-4a7a-4c94-b66f-0819fa6effcc", "4", "Manager Movies", "Manager Movies" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
