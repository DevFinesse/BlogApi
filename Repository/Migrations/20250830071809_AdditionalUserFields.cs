using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class AdditionalUserFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "135763e2-9903-4bad-aa8a-e57ec5acf690");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f8dde15c-8c66-4ee7-ab7a-7e8d4545ae72");

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryTime",
                table: "AspNetUsers",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1f972c73-5f17-4533-a6a1-9bacce5107dd", null, "Author", "AUTHOR" },
                    { "d94cf806-ba4a-4a54-8e13-30d8afe49f10", null, "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: new Guid("0bd63e36-ff8e-409f-84a6-ffba66186e48"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 8, 30, 7, 18, 8, 584, DateTimeKind.Unspecified).AddTicks(4250), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: new Guid("8e330783-ccb4-47be-883d-dad77dac71c1"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 8, 30, 7, 18, 8, 584, DateTimeKind.Unspecified).AddTicks(5676), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: new Guid("8faac1fb-eadc-4b12-9f48-ff9632fea906"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 8, 30, 7, 18, 8, 584, DateTimeKind.Unspecified).AddTicks(5671), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"),
                columns: new[] { "CreatedAt", "LastUpdatedAt" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 8, 30, 7, 18, 8, 585, DateTimeKind.Unspecified).AddTicks(1590), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 8, 30, 7, 18, 8, 585, DateTimeKind.Unspecified).AddTicks(1591), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                columns: new[] { "CreatedAt", "LastUpdatedAt" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 8, 30, 7, 18, 8, 584, DateTimeKind.Unspecified).AddTicks(9803), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 8, 30, 7, 18, 8, 584, DateTimeKind.Unspecified).AddTicks(9804), new TimeSpan(0, 0, 0, 0, 0)) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1f972c73-5f17-4533-a6a1-9bacce5107dd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d94cf806-ba4a-4a54-8e13-30d8afe49f10");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryTime",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "135763e2-9903-4bad-aa8a-e57ec5acf690", null, "Administrator", "ADMINISTRATOR" },
                    { "f8dde15c-8c66-4ee7-ab7a-7e8d4545ae72", null, "Author", "AUTHOR" }
                });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: new Guid("0bd63e36-ff8e-409f-84a6-ffba66186e48"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 8, 29, 5, 47, 39, 830, DateTimeKind.Unspecified).AddTicks(6842), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: new Guid("8e330783-ccb4-47be-883d-dad77dac71c1"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 8, 29, 5, 47, 39, 830, DateTimeKind.Unspecified).AddTicks(8196), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: new Guid("8faac1fb-eadc-4b12-9f48-ff9632fea906"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 8, 29, 5, 47, 39, 830, DateTimeKind.Unspecified).AddTicks(8191), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"),
                columns: new[] { "CreatedAt", "LastUpdatedAt" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 8, 29, 5, 47, 39, 831, DateTimeKind.Unspecified).AddTicks(3841), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 8, 29, 5, 47, 39, 831, DateTimeKind.Unspecified).AddTicks(3842), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                columns: new[] { "CreatedAt", "LastUpdatedAt" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 8, 29, 5, 47, 39, 831, DateTimeKind.Unspecified).AddTicks(2022), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 8, 29, 5, 47, 39, 831, DateTimeKind.Unspecified).AddTicks(2024), new TimeSpan(0, 0, 0, 0, 0)) });
        }
    }
}
