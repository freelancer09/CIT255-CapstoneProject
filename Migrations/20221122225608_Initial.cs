using Microsoft.EntityFrameworkCore.Migrations;

namespace CapstoneProject.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GameLists",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameLists", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    GameID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameListID = table.Column<int>(nullable: false),
                    RawgID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.GameID);
                    table.ForeignKey(
                        name: "FK_Games_GameLists_GameListID",
                        column: x => x.GameListID,
                        principalTable: "GameLists",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "GameLists",
                columns: new[] { "ID", "Name" },
                values: new object[,]
                {
                    { 1, "Collection" },
                    { 2, "Wish List" },
                    { 3, "List 1" },
                    { 4, "List 2" },
                    { 5, "List 3" }
                });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "GameID", "GameListID", "RawgID" },
                values: new object[,]
                {
                    { 1, 1, 58175 },
                    { 2, 1, 494384 },
                    { 3, 1, 29179 },
                    { 4, 1, 39707 },
                    { 5, 1, 3265 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Games_GameListID",
                table: "Games",
                column: "GameListID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "GameLists");
        }
    }
}
