using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace simple_blog_api.Migrations
{
    /// <inheritdoc />
    public partial class AddCreatorToPost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatorId",
                table: "Posts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId1",
                table: "Posts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Posts_CreatorId1",
                table: "Posts",
                column: "CreatorId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Users_CreatorId1",
                table: "Posts",
                column: "CreatorId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Users_CreatorId1",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_CreatorId1",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "CreatorId1",
                table: "Posts");
        }
    }
}
