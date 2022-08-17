using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Events.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AddEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nimi = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Toimumisaeg = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Koht = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    lisainfo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddEvents", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AddEvents");
        }
    }
}
