using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TMS_V1.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedingRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [Security].[Roles]" +
                $" VALUES ('{Guid.NewGuid().ToString()}', " +
                $"          '{Role.ADMIN}', " +
                $"          '{Role.ADMIN.ToUpper()}'," +
                $"          '{Guid.NewGuid().ToString()}')" );

            migrationBuilder.Sql("INSERT INTO [Security].[Roles]" +
                $" VALUES ('{Guid.NewGuid().ToString()}', " +
                $"          '{Role.TEAM_LEAD}', " +
                $"          '{Role.TEAM_LEAD.ToUpper()}'," +
                $"          '{Guid.NewGuid().ToString()}')" );

            migrationBuilder.Sql("INSERT INTO [Security].[Roles]" +
                $" VALUES ('{Guid.NewGuid().ToString()}', " +
                $"          '{Role.REG_USER}', " +
                $"          '{Role.REG_USER.ToUpper()}'," +
                $"          '{Guid.NewGuid().ToString()}')" );

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
