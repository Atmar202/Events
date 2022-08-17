using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Events.Migrations
{
    public partial class Participants : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "lisainfo",
                table: "AddEvents",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "CompanyParticipants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nimi = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Registrikood = table.Column<int>(type: "int", nullable: false),
                    Osavõtjate_arv = table.Column<int>(type: "int", nullable: false),
                    Maksmiseviis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lisainfo = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyParticipants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PrivateParticipants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Eesnimi = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Perekonnanimi = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Isikukood = table.Column<int>(type: "int", nullable: false),
                    Maksmisviis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lisainfo = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrivateParticipants", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyParticipants");

            migrationBuilder.DropTable(
                name: "PrivateParticipants");

            migrationBuilder.AlterColumn<string>(
                name: "lisainfo",
                table: "AddEvents",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);
        }
    }
}
