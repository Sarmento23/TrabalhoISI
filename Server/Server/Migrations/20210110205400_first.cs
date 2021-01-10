using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Server.Migrations
{
    public partial class first : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "stadiums",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Capacidade = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stadiums", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Contact = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "sectors",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Colunas = table.Column<int>(type: "int", nullable: false),
                    Linhas = table.Column<int>(type: "int", nullable: false),
                    Capacidade = table.Column<int>(type: "int", nullable: false),
                    StadiumID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sectors", x => x.ID);
                    table.ForeignKey(
                        name: "FK_sectors_stadiums_StadiumID",
                        column: x => x.StadiumID,
                        principalTable: "stadiums",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "events",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StadiumID = table.Column<int>(type: "int", nullable: false),
                    SectorID = table.Column<int>(type: "int", nullable: false),
                    Percentage = table.Column<int>(type: "int", nullable: false),
                    AvailableSeats = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_events", x => x.ID);
                    table.ForeignKey(
                        name: "FK_events_sectors_SectorID",
                        column: x => x.SectorID,
                        principalTable: "sectors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_events_stadiums_StadiumID",
                        column: x => x.StadiumID,
                        principalTable: "stadiums",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "tickets",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdeptName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdeptContact = table.Column<int>(type: "int", nullable: false),
                    StadiumID = table.Column<int>(type: "int", nullable: false),
                    EventID = table.Column<int>(type: "int", nullable: false),
                    SectorName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Local = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tickets", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tickets_events_EventID",
                        column: x => x.EventID,
                        principalTable: "events",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tickets_stadiums_StadiumID",
                        column: x => x.StadiumID,
                        principalTable: "stadiums",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_events_SectorID",
                table: "events",
                column: "SectorID");

            migrationBuilder.CreateIndex(
                name: "IX_events_StadiumID",
                table: "events",
                column: "StadiumID");

            migrationBuilder.CreateIndex(
                name: "IX_sectors_StadiumID",
                table: "sectors",
                column: "StadiumID");

            migrationBuilder.CreateIndex(
                name: "IX_tickets_EventID",
                table: "tickets",
                column: "EventID");

            migrationBuilder.CreateIndex(
                name: "IX_tickets_StadiumID",
                table: "tickets",
                column: "StadiumID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tickets");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "events");

            migrationBuilder.DropTable(
                name: "sectors");

            migrationBuilder.DropTable(
                name: "stadiums");
        }
    }
}
