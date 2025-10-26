using Microsoft.EntityFrameworkCore.Migrations;
using TruthDetection.DAL.Data.SeedingModels;

#nullable disable

namespace TruthDetection.DAL.Migrations
{
    /// <inheritdoc />
    public partial class SeedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
        
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns : new[] {"Id"  , "Name" , "NormalizedName", "ConcurrencyStamp" },
                values : new object[] { Guid.NewGuid().ToString() , new SeedRole().Admin , new SeedRole().Admin.ToUpper() , Guid.NewGuid().ToString()  }
            );

            migrationBuilder.InsertData(
             table: "AspNetRoles",
             columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
             values: new object[] { Guid.NewGuid().ToString(), new SeedRole().User, new SeedRole().User.ToUpper(), Guid.NewGuid().ToString() }
         );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [AspNetRoles]");
        }
    }
}
