using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectHive.Services.TasksAPI.Migrations
{
    /// <inheritdoc />
    public partial class CorrectNmeEnt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_StatusGoals_StatusTaskIdId",
                table: "Tasks");

            migrationBuilder.RenameColumn(
                name: "StatusTaskIdId",
                table: "Tasks",
                newName: "StatusTaskId");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_StatusTaskIdId",
                table: "Tasks",
                newName: "IX_Tasks_StatusTaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_StatusGoals_StatusTaskId",
                table: "Tasks",
                column: "StatusTaskId",
                principalTable: "StatusGoals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_StatusGoals_StatusTaskId",
                table: "Tasks");

            migrationBuilder.RenameColumn(
                name: "StatusTaskId",
                table: "Tasks",
                newName: "StatusTaskIdId");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_StatusTaskId",
                table: "Tasks",
                newName: "IX_Tasks_StatusTaskIdId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_StatusGoals_StatusTaskIdId",
                table: "Tasks",
                column: "StatusTaskIdId",
                principalTable: "StatusGoals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
