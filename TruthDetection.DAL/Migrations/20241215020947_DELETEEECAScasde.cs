using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruthDetection.DAL.Migrations
{
    /// <inheritdoc />
    public partial class DELETEEECAScasde : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Result_Video_VideoID",
                table: "Result");

            migrationBuilder.DropForeignKey(
                name: "FK_ResultDetails_Result_ResultID",
                table: "ResultDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Video_AspNetUsers_UserID",
                table: "Video");

            migrationBuilder.AddForeignKey(
                name: "FK_Result_Video_VideoID",
                table: "Result",
                column: "VideoID",
                principalTable: "Video",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ResultDetails_Result_ResultID",
                table: "ResultDetails",
                column: "ResultID",
                principalTable: "Result",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Video_AspNetUsers_UserID",
                table: "Video",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Result_Video_VideoID",
                table: "Result");

            migrationBuilder.DropForeignKey(
                name: "FK_ResultDetails_Result_ResultID",
                table: "ResultDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Video_AspNetUsers_UserID",
                table: "Video");

            migrationBuilder.AddForeignKey(
                name: "FK_Result_Video_VideoID",
                table: "Result",
                column: "VideoID",
                principalTable: "Video",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ResultDetails_Result_ResultID",
                table: "ResultDetails",
                column: "ResultID",
                principalTable: "Result",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Video_AspNetUsers_UserID",
                table: "Video",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
