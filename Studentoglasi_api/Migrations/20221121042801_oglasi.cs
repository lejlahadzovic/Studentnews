using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentOglasi.Migrations
{
    /// <inheritdoc />
    public partial class oglasi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Oglas",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RokPrijave = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VrijemeObjave = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Oglas", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Praksa",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    Trajanje = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Kvalifikacije = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Benefiti = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Placena = table.Column<bool>(type: "bit", nullable: false),
                    UposlenikID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Praksa", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Praksa_Oglas_ID",
                        column: x => x.ID,
                        principalTable: "Oglas",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Praksa_UposlenikFirme_UposlenikID",
                        column: x => x.UposlenikID,
                        principalTable: "UposlenikFirme",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Smjestaj",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    Cijena = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kapacitet = table.Column<int>(type: "int", nullable: false),
                    DodatneUsluge = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrojSoba = table.Column<int>(type: "int", nullable: false),
                    Slika = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Parking = table.Column<bool>(type: "bit", nullable: false),
                    NacinGrijanja = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GradID = table.Column<int>(type: "int", nullable: false),
                    IzdavacID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Smjestaj", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Smjestaj_Grad_GradID",
                        column: x => x.GradID,
                        principalTable: "Grad",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Smjestaj_IzdavacSmjestaja_IzdavacID",
                        column: x => x.IzdavacID,
                        principalTable: "IzdavacSmjestaja",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Smjestaj_Oglas_ID",
                        column: x => x.ID,
                        principalTable: "Oglas",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stipendija",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    Uslovi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Iznos = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kriterij = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PotrebnaDokumentacija = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Izvor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NivoObrazovanja = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrojStipendisata = table.Column<int>(type: "int", nullable: false),
                    UposlenikID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stipendija", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Stipendija_Oglas_ID",
                        column: x => x.ID,
                        principalTable: "Oglas",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Stipendija_UposlenikStipenditora_UposlenikID",
                        column: x => x.UposlenikID,
                        principalTable: "UposlenikStipenditora",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Praksa_UposlenikID",
                table: "Praksa",
                column: "UposlenikID");

            migrationBuilder.CreateIndex(
                name: "IX_Smjestaj_GradID",
                table: "Smjestaj",
                column: "GradID");

            migrationBuilder.CreateIndex(
                name: "IX_Smjestaj_IzdavacID",
                table: "Smjestaj",
                column: "IzdavacID");

            migrationBuilder.CreateIndex(
                name: "IX_Stipendija_UposlenikID",
                table: "Stipendija",
                column: "UposlenikID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Praksa");

            migrationBuilder.DropTable(
                name: "Smjestaj");

            migrationBuilder.DropTable(
                name: "Stipendija");

            migrationBuilder.DropTable(
                name: "Oglas");
        }
    }
}
