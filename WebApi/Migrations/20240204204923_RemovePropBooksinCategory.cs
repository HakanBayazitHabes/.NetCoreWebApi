using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class RemovePropBooksinCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "92b6f951-4469-4e73-86bb-d26154a26dd6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "93946cd2-2a94-41bc-986f-bd062de801e1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a1c95687-109a-47de-addb-83666b82803b");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "11422da2-2c53-4f1b-aaf0-ba15ffb6814f", null, "Editor", "EDITOR" },
                    { "48560912-edd7-4b20-a119-c91439816133", null, "Admin", "ADMIN" },
                    { "7361d27a-af9b-4f2f-a58f-dffcffbe1fb2", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "11422da2-2c53-4f1b-aaf0-ba15ffb6814f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "48560912-edd7-4b20-a119-c91439816133");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7361d27a-af9b-4f2f-a58f-dffcffbe1fb2");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "92b6f951-4469-4e73-86bb-d26154a26dd6", null, "User", "USER" },
                    { "93946cd2-2a94-41bc-986f-bd062de801e1", null, "Admin", "ADMIN" },
                    { "a1c95687-109a-47de-addb-83666b82803b", null, "Editor", "EDITOR" }
                });
        }
    }
}
