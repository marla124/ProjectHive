using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectHive.Services.ProjectsAPI.Migrations
{
    /// <inheritdoc />
    public partial class TaskDependUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTasks_Users_UserId",
                table: "ProjectTasks");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "ProjectTasks",
                newName: "UserCreatorId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectTasks_UserId",
                table: "ProjectTasks",
                newName: "IX_ProjectTasks_UserCreatorId");

            migrationBuilder.AddColumn<Guid>(
                name: "UserExecutorId",
                table: "ProjectTasks",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTasks_UserExecutorId",
                table: "ProjectTasks",
                column: "UserExecutorId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTasks_Users_UserCreatorId",
                table: "ProjectTasks",
                column: "UserCreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTasks_Users_UserExecutorId",
                table: "ProjectTasks",
                column: "UserExecutorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTasks_Users_UserCreatorId",
                table: "ProjectTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTasks_Users_UserExecutorId",
                table: "ProjectTasks");

            migrationBuilder.DropIndex(
                name: "IX_ProjectTasks_UserExecutorId",
                table: "ProjectTasks");

            migrationBuilder.DropColumn(
                name: "UserExecutorId",
                table: "ProjectTasks");

            migrationBuilder.RenameColumn(
                name: "UserCreatorId",
                table: "ProjectTasks",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectTasks_UserCreatorId",
                table: "ProjectTasks",
                newName: "IX_ProjectTasks_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTasks_Users_UserId",
                table: "ProjectTasks",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
