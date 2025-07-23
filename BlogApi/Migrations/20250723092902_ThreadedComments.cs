using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogApi.Migrations
{
    /// <inheritdoc />
    public partial class ThreadedComments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentCommentId",
                table: "Comments",
                type: "integer",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ParentCommentId" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 7, 23, 9, 29, 1, 333, DateTimeKind.Unspecified).AddTicks(7017), new TimeSpan(0, 0, 0, 0, 0)), null });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ParentCommentId" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 7, 23, 9, 29, 1, 333, DateTimeKind.Unspecified).AddTicks(8027), new TimeSpan(0, 0, 0, 0, 0)), null });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 3,
                columns: new[] { "CreatedAt", "ParentCommentId" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 7, 23, 9, 29, 1, 333, DateTimeKind.Unspecified).AddTicks(8032), new TimeSpan(0, 0, 0, 0, 0)), null });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 7, 23, 9, 29, 1, 332, DateTimeKind.Unspecified).AddTicks(4493), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 7, 23, 9, 29, 1, 332, DateTimeKind.Unspecified).AddTicks(1872), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ParentCommentId",
                table: "Comments",
                column: "ParentCommentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Comments_ParentCommentId",
                table: "Comments",
                column: "ParentCommentId",
                principalTable: "Comments",
                principalColumn: "CommentId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Comments_ParentCommentId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_ParentCommentId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "ParentCommentId",
                table: "Comments");

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 7, 23, 1, 38, 5, 894, DateTimeKind.Unspecified).AddTicks(216), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 7, 23, 1, 38, 5, 894, DateTimeKind.Unspecified).AddTicks(1171), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 7, 23, 1, 38, 5, 894, DateTimeKind.Unspecified).AddTicks(1176), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 7, 23, 1, 38, 5, 893, DateTimeKind.Unspecified).AddTicks(950), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 7, 23, 1, 38, 5, 892, DateTimeKind.Unspecified).AddTicks(8825), new TimeSpan(0, 0, 0, 0, 0)));
        }
    }
}
