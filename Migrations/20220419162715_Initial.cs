using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace task1_databases.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RoomClasses",
                columns: table => new
                {
                    RoomClassId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClassOfRoom = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomClasses", x => x.RoomClassId);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    RoomId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IsFree = table.Column<bool>(type: "boolean", nullable: false),
                    RoomSize = table.Column<int>(type: "integer", nullable: false),
                    RoomClassId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.RoomId);
                    table.ForeignKey(
                        name: "FK_Rooms_RoomClasses_RoomClassId",
                        column: x => x.RoomClassId,
                        principalTable: "RoomClasses",
                        principalColumn: "RoomClassId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "RoomClasses",
                columns: new[] { "RoomClassId", "ClassOfRoom", "Price" },
                values: new object[,]
                {
                    { 1, "Эконом", 2000 },
                    { 2, "Бизнес", 3000 },
                    { 3, "Люкс", 5000 }
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "RoomId", "IsFree", "RoomClassId", "RoomSize" },
                values: new object[,]
                {
                    { 1, true, 1, 2 },
                    { 2, false, 1, 1 },
                    { 3, false, 2, 3 },
                    { 4, false, 2, 1 },
                    { 5, false, 3, 2 },
                    { 6, true, 3, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_RoomClassId",
                table: "Rooms",
                column: "RoomClassId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "RoomClasses");
        }
    }
}
