using Microsoft.EntityFrameworkCore.Migrations;

namespace Commander.Migrations
{
    public partial class Categories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Commands",
                nullable: false
                );

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Commands_CategoryId",
                table: "Commands",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Commands_Categories_CategoryId",
                table: "Commands",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commands_Categories_CategoryId",
                table: "Commands");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Commands_CategoryId",
                table: "Commands");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Commands");
        }
    }
}
