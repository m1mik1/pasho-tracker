#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace PaSho_Tracker.Migrations;

/// <inheritdoc />
public partial class SomeMigration : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
            "AssignedUserId",
            "Tasks",
            "text",
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<DateTime>(
            "Deadline",
            "Tasks",
            "timestamp with time zone",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.AddColumn<int>(
            "Priority",
            "Tasks",
            "integer",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.AddColumn<int>(
            "Status",
            "Tasks",
            "integer",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.AddColumn<int>(
            "RelatedTaskId",
            "Comments",
            "integer",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.CreateIndex(
            "IX_Comments_RelatedTaskId",
            "Comments",
            "RelatedTaskId");

        migrationBuilder.AddForeignKey(
            "FK_Comments_Tasks_RelatedTaskId",
            "Comments",
            "RelatedTaskId",
            "Tasks",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            "FK_Comments_Tasks_RelatedTaskId",
            "Comments");

        migrationBuilder.DropIndex(
            "IX_Comments_RelatedTaskId",
            "Comments");

        migrationBuilder.DropColumn(
            "AssignedUserId",
            "Tasks");

        migrationBuilder.DropColumn(
            "Deadline",
            "Tasks");

        migrationBuilder.DropColumn(
            "Priority",
            "Tasks");

        migrationBuilder.DropColumn(
            "Status",
            "Tasks");

        migrationBuilder.DropColumn(
            "RelatedTaskId",
            "Comments");
    }
}