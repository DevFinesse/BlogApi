using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BlogApi.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    PostId = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    Slug = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LastUpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    IsPublished = table.Column<bool>(type: "boolean", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.PostId);
                    table.ForeignKey(
                        name: "FK_Posts_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    CommentId = table.Column<Guid>(type: "uuid", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    ParentCommentId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LastUpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    PostId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.CommentId);
                    table.ForeignKey(
                        name: "FK_Comments_Comments_ParentCommentId",
                        column: x => x.ParentCommentId,
                        principalTable: "Comments",
                        principalColumn: "CommentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comments_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "PostId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Name" },
                values: new object[,]
                {
                    { new Guid("21c77926-23b5-42ce-96ea-d389e08faab8"), "Category 2" },
                    { new Guid("f67c36e4-8428-4570-858b-6c8b7edad5c3"), "Category One" }
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "PostId", "CategoryId", "Content", "CreatedAt", "IsPublished", "LastUpdatedAt", "Slug", "Title" },
                values: new object[,]
                {
                    { new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"), new Guid("f67c36e4-8428-4570-858b-6c8b7edad5c3"), "Post Two Contents", new DateTimeOffset(new DateTime(2025, 7, 26, 12, 6, 54, 198, DateTimeKind.Unspecified).AddTicks(698), new TimeSpan(0, 0, 0, 0, 0)), true, new DateTimeOffset(new DateTime(2025, 7, 26, 12, 6, 54, 198, DateTimeKind.Unspecified).AddTicks(699), new TimeSpan(0, 0, 0, 0, 0)), "post-two-title", "Post Two Title" },
                    { new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), new Guid("21c77926-23b5-42ce-96ea-d389e08faab8"), "Post one Contents", new DateTimeOffset(new DateTime(2025, 7, 26, 12, 6, 54, 197, DateTimeKind.Unspecified).AddTicks(8528), new TimeSpan(0, 0, 0, 0, 0)), false, new DateTimeOffset(new DateTime(2025, 7, 26, 12, 6, 54, 197, DateTimeKind.Unspecified).AddTicks(8890), new TimeSpan(0, 0, 0, 0, 0)), "post-one-title", "Post One Title" }
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "CommentId", "Content", "CreatedAt", "LastUpdatedAt", "ParentCommentId", "PostId" },
                values: new object[,]
                {
                    { new Guid("0bd63e36-ff8e-409f-84a6-ffba66186e48"), "Comment one", new DateTimeOffset(new DateTime(2025, 7, 26, 12, 6, 54, 199, DateTimeKind.Unspecified).AddTicks(73), new TimeSpan(0, 0, 0, 0, 0)), null, null, new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870") },
                    { new Guid("8e330783-ccb4-47be-883d-dad77dac71c1"), "Comment Three", new DateTimeOffset(new DateTime(2025, 7, 26, 12, 6, 54, 199, DateTimeKind.Unspecified).AddTicks(1139), new TimeSpan(0, 0, 0, 0, 0)), null, null, new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3") },
                    { new Guid("8faac1fb-eadc-4b12-9f48-ff9632fea906"), "Comment Two", new DateTimeOffset(new DateTime(2025, 7, 26, 12, 6, 54, 199, DateTimeKind.Unspecified).AddTicks(1134), new TimeSpan(0, 0, 0, 0, 0)), null, null, new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ParentCommentId",
                table: "Comments",
                column: "ParentCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PostId",
                table: "Comments",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_CategoryId",
                table: "Posts",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_Slug",
                table: "Posts",
                column: "Slug",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
