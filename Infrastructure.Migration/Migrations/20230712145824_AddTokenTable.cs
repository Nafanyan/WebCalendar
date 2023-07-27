using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfrastructureMigration.Migrations
{
    /// <inheritdoc />
    public partial class AddTokenTable : Migration
    {
        /// <inheritdoc />
        protected override void Up( MigrationBuilder migrationBuilder )
        {
            migrationBuilder.CreateTable(
                name: "UserAuthorizationTokens",
                columns: table => new
                {
                    UserId = table.Column<long>( type: "bigint", nullable: false ),
                    RefreshToken = table.Column<string>( type: "text", nullable: false ),
                    ExpiryDate = table.Column<DateTime>( type: "timestamp without time zone", nullable: false )
                },
                constraints: table =>
                {
                    table.PrimaryKey( "PK_UserAuthorizationTokens", x => x.UserId );
                    table.ForeignKey(
                        name: "FK_UserAuthorizationTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade );
                } );
        }

        /// <inheritdoc />
        protected override void Down( MigrationBuilder migrationBuilder )
        {
            migrationBuilder.DropTable(
                name: "UserAuthorizationTokens" );
        }
    }
}
