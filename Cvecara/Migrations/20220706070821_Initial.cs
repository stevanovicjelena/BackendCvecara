using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Cvecara.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cvet",
                columns: table => new
                {
                    cvetID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    bojaCveta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cenaCveta = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    kolicina = table.Column<int>(type: "int", nullable: false),
                    vrstaCvetaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cvet", x => x.cvetID);
                });

            migrationBuilder.CreateTable(
                name: "CvetniAranzman",
                columns: table => new
                {
                    cvetniAranzmanID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nazivAranzmana = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cenaAranzmana = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    opisAranzmana = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    kolicina = table.Column<int>(type: "int", nullable: false),
                    pakovanjeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CvetniAranzman", x => x.cvetniAranzmanID);
                });

            migrationBuilder.CreateTable(
                name: "CvetniAranzman_Cvet",
                columns: table => new
                {
                    cvetniAranzman_Cvet_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cvetniAranzmanID = table.Column<int>(type: "int", nullable: false),
                    cvetID = table.Column<int>(type: "int", nullable: false),
                    brojCvetova = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CvetniAranzman_Cvet", x => x.cvetniAranzman_Cvet_ID);
                });

            migrationBuilder.CreateTable(
                name: "Dodatak",
                columns: table => new
                {
                    dodatakID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    bojaDodatka = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cenaDodatka = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    kolicina = table.Column<int>(type: "int", nullable: false),
                    tipDodatkaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dodatak", x => x.dodatakID);
                });

            migrationBuilder.CreateTable(
                name: "Lokacije",
                columns: table => new
                {
                    lokacijaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nazivLokacije = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lokacije", x => x.lokacijaID);
                });

            migrationBuilder.CreateTable(
                name: "Pakovanje",
                columns: table => new
                {
                    pakovanjeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nazivPakovanja = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    bojaPakovanja = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    opisPakovanja = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cenaPakovanja = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    kolicina = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pakovanje", x => x.pakovanjeID);
                });

            migrationBuilder.CreateTable(
                name: "Porudzbina",
                columns: table => new
                {
                    porudzbinaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    kolicina = table.Column<int>(type: "int", nullable: false),
                    cenaPorudzbine = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    statusPorudzbine = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    datumPorudzbine = table.Column<DateTime>(type: "datetime2", nullable: false),
                    cvetniAranzmanID = table.Column<int>(type: "int", nullable: false),
                    zaposleniID = table.Column<int>(type: "int", nullable: false),
                    kupacID = table.Column<int>(type: "int", nullable: false),
                    lokacijaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Porudzbina", x => x.porudzbinaID);
                });

            migrationBuilder.CreateTable(
                name: "Porudzbina_Dodatak",
                columns: table => new
                {
                    porudzbina_Dodatak_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    porudzbinaID = table.Column<int>(type: "int", nullable: false),
                    dodatakID = table.Column<int>(type: "int", nullable: false),
                    kolicinaDodatka = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Porudzbina_Dodatak", x => x.porudzbina_Dodatak_ID);
                });

            migrationBuilder.CreateTable(
                name: "tblUser",
                columns: table => new
                {
                    userID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    imeUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    prezimeUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    telefon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    emailUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    korisnickoImeUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lozinkaUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    uloga = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblUser", x => x.userID);
                });

            migrationBuilder.CreateTable(
                name: "TipDodatka",
                columns: table => new
                {
                    tipDodatkaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nazivTipaDodatka = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    opisTipaDodatka = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipDodatka", x => x.tipDodatkaID);
                });

            migrationBuilder.CreateTable(
                name: "VrstaCveta",
                columns: table => new
                {
                    vrstaCvetaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nazivVrste = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    opisVrste = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VrstaCveta", x => x.vrstaCvetaID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cvet");

            migrationBuilder.DropTable(
                name: "CvetniAranzman");

            migrationBuilder.DropTable(
                name: "CvetniAranzman_Cvet");

            migrationBuilder.DropTable(
                name: "Dodatak");

            migrationBuilder.DropTable(
                name: "Lokacije");

            migrationBuilder.DropTable(
                name: "Pakovanje");

            migrationBuilder.DropTable(
                name: "Porudzbina");

            migrationBuilder.DropTable(
                name: "Porudzbina_Dodatak");

            migrationBuilder.DropTable(
                name: "tblUser");

            migrationBuilder.DropTable(
                name: "TipDodatka");

            migrationBuilder.DropTable(
                name: "VrstaCveta");
        }
    }
}
