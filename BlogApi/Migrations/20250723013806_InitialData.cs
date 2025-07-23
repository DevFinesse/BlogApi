using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BlogApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Posts",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Name" },
                values: new object[,]
                {
                    { 1, "Category One" },
                    { 2, "Category 2" }
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "PostId", "CategoryId", "Content", "CreatedAt", "LastUpdatedAt", "Slug", "Status", "Title" },
                values: new object[,]
                {
                    { new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"), 1, "Post Two Contents", new DateTimeOffset(new DateTime(2025, 7, 23, 1, 38, 5, 893, DateTimeKind.Unspecified).AddTicks(950), new TimeSpan(0, 0, 0, 0, 0)), null, "Post-Two-Title", "Draft", "Post Two Title" },
                    { new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), 2, "Post one Contents", new DateTimeOffset(new DateTime(2025, 7, 23, 1, 38, 5, 892, DateTimeKind.Unspecified).AddTicks(8825), new TimeSpan(0, 0, 0, 0, 0)), null, "Post-One-Title", "Published", "Post One Title" }
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "CommentId", "Content", "CreatedAt", "LastUpdatedAt", "PostId" },
                values: new object[,]
                {
                    { 1, "Comment one", new DateTimeOffset(new DateTime(2025, 7, 23, 1, 38, 5, 894, DateTimeKind.Unspecified).AddTicks(216), new TimeSpan(0, 0, 0, 0, 0)), null, new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870") },
                    { 2, "Comment Two", new DateTimeOffset(new DateTime(2025, 7, 23, 1, 38, 5, 894, DateTimeKind.Unspecified).AddTicks(1171), new TimeSpan(0, 0, 0, 0, 0)), null, new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3") },
                    { 3, "Comment Three", new DateTimeOffset(new DateTime(2025, 7, 23, 1, 38, 5, 894, DateTimeKind.Unspecified).AddTicks(1176), new TimeSpan(0, 0, 0, 0, 0)), null, new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"));

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Posts",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldNullable: true);
        }
    }
}
