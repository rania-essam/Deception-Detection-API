using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruthDetection.DAL.Migrations
{
    /// <inheritdoc />
    public partial class imagenameproperty_appuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProfilePictureURL",
                table: "AspNetUsers",
                newName: "ImageName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageName",
                table: "AspNetUsers",
                newName: "ProfilePictureURL");
        }
    }
}
