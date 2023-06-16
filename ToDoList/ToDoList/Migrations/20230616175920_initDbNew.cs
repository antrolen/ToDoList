using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoList.Migrations
{
    /// <inheritdoc />
    public partial class initDbNew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "TaskLists",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Сompletions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Сompletions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaskСompletions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskListId = table.Column<int>(type: "int", nullable: false),
                    СompletionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskСompletions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskСompletions_TaskLists_TaskListId",
                        column: x => x.TaskListId,
                        principalTable: "TaskLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskСompletions_Сompletions_СompletionId",
                        column: x => x.СompletionId,
                        principalTable: "Сompletions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskLists_CategoryId",
                table: "TaskLists",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskСompletions_СompletionId",
                table: "TaskСompletions",
                column: "СompletionId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskСompletions_TaskListId",
                table: "TaskСompletions",
                column: "TaskListId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskLists_Categories_CategoryId",
                table: "TaskLists",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskLists_Categories_CategoryId",
                table: "TaskLists");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "TaskСompletions");

            migrationBuilder.DropTable(
                name: "Сompletions");

            migrationBuilder.DropIndex(
                name: "IX_TaskLists_CategoryId",
                table: "TaskLists");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "TaskLists");
        }
    }
}
