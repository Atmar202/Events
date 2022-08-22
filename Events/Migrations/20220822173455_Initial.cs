using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Events.Migrations
{
    public partial class Initial : Migration
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
                    lisainfo = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddEvents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompanyParticipants",
                columns: table => new
                {
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nimi = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Registrikood = table.Column<int>(type: "int", nullable: false),
                    Osavõtjate_arv = table.Column<int>(type: "int", nullable: false),
                    Maksmiseviis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lisainfo = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: true),
                    EventsId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyParticipants", x => x.CompanyId);
                    table.ForeignKey(
                        name: "FK_CompanyParticipants_AddEvents_EventsId",
                        column: x => x.EventsId,
                        principalTable: "AddEvents",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PrivateParticipants",
                columns: table => new
                {
                    PrivateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Eesnimi = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Perekonnanimi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Isikukood = table.Column<int>(type: "int", nullable: false),
                    Maksmisviis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lisainfo = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: true),
                    EventsId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrivateParticipants", x => x.PrivateId);
                    table.ForeignKey(
                        name: "FK_PrivateParticipants_AddEvents_EventsId",
                        column: x => x.EventsId,
                        principalTable: "AddEvents",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyParticipants_EventsId",
                table: "CompanyParticipants",
                column: "EventsId");

            migrationBuilder.CreateIndex(
                name: "IX_PrivateParticipants_EventsId",
                table: "PrivateParticipants",
                column: "EventsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyParticipants");

            migrationBuilder.DropTable(
                name: "PrivateParticipants");

            migrationBuilder.DropTable(
                name: "AddEvents");
        }
    }
}
