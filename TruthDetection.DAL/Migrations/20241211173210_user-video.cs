using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruthDetection.DAL.Migrations
{
    /// <inheritdoc />
    public partial class uservideo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Video_AspNetUsers_ApplicationUserId",
                table: "Video");

            migrationBuilder.DropForeignKey(
                name: "FK_Video_UserVideo_uservideoVideoID",
                table: "Video");

            migrationBuilder.DropTable(
                name: "UserVideo");

            migrationBuilder.DropIndex(
                name: "IX_Video_ApplicationUserId",
                table: "Video");

            migrationBuilder.DropIndex(
                name: "IX_Video_uservideoVideoID",
                table: "Video");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Video");

            migrationBuilder.DropColumn(
                name: "uservideoVideoID",
                table: "Video");

            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "Video",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "uservideoId",
                table: "Video",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Video_uservideoId",
                table: "Video",
                column: "uservideoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Video_AspNetUsers_uservideoId",
                table: "Video",
                column: "uservideoId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Video_AspNetUsers_uservideoId",
                table: "Video");

            migrationBuilder.DropIndex(
                name: "IX_Video_uservideoId",
                table: "Video");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Video");

            migrationBuilder.DropColumn(
                name: "uservideoId",
                table: "Video");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Video",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "uservideoVideoID",
                table: "Video",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "UserVideo",
                columns: table => new
                {
                    VideoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserVideo", x => x.VideoID);
                    table.ForeignKey(
                        name: "FK_UserVideo_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Video_ApplicationUserId",
                table: "Video",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Video_uservideoVideoID",
                table: "Video",
                column: "uservideoVideoID");

            migrationBuilder.CreateIndex(
                name: "IX_UserVideo_UserID",
                table: "UserVideo",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Video_AspNetUsers_ApplicationUserId",
                table: "Video",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Video_UserVideo_uservideoVideoID",
                table: "Video",
                column: "uservideoVideoID",
                principalTable: "UserVideo",
                principalColumn: "VideoID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
