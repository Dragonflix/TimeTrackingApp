using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeTrackingDAL.Migrations
{
    /// <inheritdoc />
    public partial class mig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ActivityTypeId",
                table: "TimeReports",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_TimeReports_ActivityTypeId",
                table: "TimeReports",
                column: "ActivityTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeReports_ActivityTypes_ActivityTypeId",
                table: "TimeReports",
                column: "ActivityTypeId",
                principalTable: "ActivityTypes",
                principalColumn: "ActivityTypeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeReports_ActivityTypes_ActivityTypeId",
                table: "TimeReports");

            migrationBuilder.DropIndex(
                name: "IX_TimeReports_ActivityTypeId",
                table: "TimeReports");

            migrationBuilder.DropColumn(
                name: "ActivityTypeId",
                table: "TimeReports");
        }
    }
}
