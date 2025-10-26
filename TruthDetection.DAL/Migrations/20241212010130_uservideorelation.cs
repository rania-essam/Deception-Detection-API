using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruthDetection.DAL.Migrations
{
    /// <inheritdoc />
    public partial class uservideorelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Video_AspNetUsers_uservideoId",
                table: "Video");

            migrationBuilder.DropIndex(
                name: "IX_Video_uservideoId",
                table: "Video");

            migrationBuilder.DropColumn(
                name: "uservideoId",
                table: "Video");

            migrationBuilder.CreateTable(
                name: "UserVideo",
                columns: table => new
                {
                    VideoID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserVideo", x => x.VideoID);
                    table.ForeignKey(
                        name: "FK_UserVideo_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserVideo_Video_VideoID",
                        column: x => x.VideoID,
                        principalTable: "Video",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserVideo_ApplicationUserId",
                table: "UserVideo",
                column: "ApplicationUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserVideo");

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
    }
}
