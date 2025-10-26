using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruthDetection.DAL.Migrations
{
    /// <inheritdoc />
    public partial class editrelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResultDetails_Video_VideoID",
                table: "ResultDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Video_AspNetUsers_UserId",
                table: "Video");

            migrationBuilder.DropIndex(
                name: "IX_Video_UserId",
                table: "Video");

            migrationBuilder.DropColumn(
                name: "DetectionResult",
                table: "Video");

            migrationBuilder.DropColumn(
                name: "NationaId",
                table: "Video");

            migrationBuilder.DropColumn(
                name: "Timestamp",
                table: "Video");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Video");

            migrationBuilder.RenameColumn(
                name: "VideoID",
                table: "ResultDetails",
                newName: "ResultID");

            migrationBuilder.RenameIndex(
                name: "IX_ResultDetails_VideoID",
                table: "ResultDetails",
                newName: "IX_ResultDetails_ResultID");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletedAt",
                table: "Video",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AddedAt",
                table: "Video",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletedAt",
                table: "UserRole",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Role",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Role",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletedAt",
                table: "ResultDetails",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletedAt",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Result",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DetectionResult = table.Column<bool>(type: "bit", nullable: false),
                    VideoID = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Result", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Result_Video_VideoID",
                        column: x => x.VideoID,
                        principalTable: "Video",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "UserVideo",
                columns: table => new
                {
                    VideoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserVideo", x => x.VideoID);
                    table.ForeignKey(
                        name: "FK_UserVideo_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
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
                name: "IX_Result_VideoID",
                table: "Result",
                column: "VideoID");

            migrationBuilder.CreateIndex(
                name: "IX_UserVideo_UserID",
                table: "UserVideo",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_ResultDetails_Result_ResultID",
                table: "ResultDetails",
                column: "ResultID",
                principalTable: "Result",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

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
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResultDetails_Result_ResultID",
                table: "ResultDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Video_AspNetUsers_ApplicationUserId",
                table: "Video");

            migrationBuilder.DropForeignKey(
                name: "FK_Video_UserVideo_uservideoVideoID",
                table: "Video");

            migrationBuilder.DropTable(
                name: "Result");

            migrationBuilder.DropTable(
                name: "UserVideo");

            migrationBuilder.DropIndex(
                name: "IX_Video_ApplicationUserId",
                table: "Video");

            migrationBuilder.DropIndex(
                name: "IX_Video_uservideoVideoID",
                table: "Video");

            migrationBuilder.DropColumn(
                name: "AddedAt",
                table: "Video");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Video");

            migrationBuilder.DropColumn(
                name: "uservideoVideoID",
                table: "Video");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Role");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Role");

            migrationBuilder.RenameColumn(
                name: "ResultID",
                table: "ResultDetails",
                newName: "VideoID");

            migrationBuilder.RenameIndex(
                name: "IX_ResultDetails_ResultID",
                table: "ResultDetails",
                newName: "IX_ResultDetails_VideoID");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "Video",
                type: "datetimeoffset",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DetectionResult",
                table: "Video",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "NationaId",
                table: "Video",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Timestamp",
                table: "Video",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Video",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "UserRole",
                type: "datetimeoffset",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "ResultDetails",
                type: "datetimeoffset",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "AspNetUsers",
                type: "datetimeoffset",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Video_UserId",
                table: "Video",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ResultDetails_Video_VideoID",
                table: "ResultDetails",
                column: "VideoID",
                principalTable: "Video",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Video_AspNetUsers_UserId",
                table: "Video",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
