using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfrastructureMigration.Migrations
{
    /// <inheritdoc />
    public partial class CheckMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up( MigrationBuilder migrationBuilder )
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "EndEvent",
                table: "Event",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof( DateTime ),
                oldType: "timestamp with time zone" );

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartEvent",
                table: "Event",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof( DateTime ),
                oldType: "timestamp with time zone" );
        }

        /// <inheritdoc />
        protected override void Down( MigrationBuilder migrationBuilder )
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "EndEvent",
                table: "Event",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof( DateTime ),
                oldType: "timestamp without time zone" );

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartEvent",
                table: "Event",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof( DateTime ),
                oldType: "timestamp without time zone" );
        }
    }
}
