using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class updateFieldsCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "406fe93e-fe6d-48bd-aab6-795afa847428");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "713c8509-1c58-49b9-a9a7-652a6159cafa");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "73aa14d1-eac6-4106-935a-6a12d22a1ab0");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { "406fe93e-fe6d-48bd-aab6-795afa847428", null, "Admin", "ADMIN" },
                    { "713c8509-1c58-49b9-a9a7-652a6159cafa", null, "User", "USER" },
                    { "73aa14d1-eac6-4106-935a-6a12d22a1ab0", null, "Editor", "EDITOR" }
                });
        }
    }
}
