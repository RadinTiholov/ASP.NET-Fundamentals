using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskBoardApp.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Boards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Task",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BoardId = table.Column<int>(type: "int", nullable: false),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Task", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Task_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Task_Boards_BoardId",
                        column: x => x.BoardId,
                        principalTable: "Boards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "0633b493-039e-4170-bae7-13fd5b945397", 0, "c25ab805-2617-4379-965a-7351390b6ea6", "radin@mail.bg", false, "Radin", "Tiholov", false, null, "RADIN@MAIL.BG", "RADIN", "AQAAAAEAACcQAAAAEJBAmYhYGr5nN72fp5kDu6EJqxEiqutR/S1GcgdG8R6jHysHzzKidNubRATRzIsN5g==", null, false, "da5dcb7e-ff55-4fe8-a2bf-8fd4a302a9d5", false, "Radin" });

            migrationBuilder.InsertData(
                table: "Boards",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Open" },
                    { 2, "InProgress" },
                    { 3, "Done" }
                });

            migrationBuilder.InsertData(
                table: "Task",
                columns: new[] { "Id", "BoardId", "CreatedOn", "Description", "OwnerId", "Title" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2022, 10, 16, 15, 45, 3, 728, DateTimeKind.Local).AddTicks(7200), "Study as hard as you can bitch!", "0633b493-039e-4170-bae7-13fd5b945397", "Study for CS" },
                    { 2, 3, new DateTime(2022, 10, 16, 15, 45, 3, 728, DateTimeKind.Local).AddTicks(7240), "Study as hard as you can bitch!", "0633b493-039e-4170-bae7-13fd5b945397", "Study for Math" },
                    { 3, 1, new DateTime(2022, 10, 16, 15, 45, 3, 728, DateTimeKind.Local).AddTicks(7243), "Workout as hard as you can bitch!", "0633b493-039e-4170-bae7-13fd5b945397", "Workout" },
                    { 4, 2, new DateTime(2022, 10, 16, 15, 45, 3, 728, DateTimeKind.Local).AddTicks(7246), "Read as fast as you can bitch!", "0633b493-039e-4170-bae7-13fd5b945397", "Read" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Task_BoardId",
                table: "Task",
                column: "BoardId");

            migrationBuilder.CreateIndex(
                name: "IX_Task_OwnerId",
                table: "Task",
                column: "OwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Task");

            migrationBuilder.DropTable(
                name: "Boards");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0633b493-039e-4170-bae7-13fd5b945397");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");
        }
    }
}
