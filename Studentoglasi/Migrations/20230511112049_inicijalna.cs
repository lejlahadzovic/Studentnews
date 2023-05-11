using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentOglasi.Migrations
{
    /// <inheritdoc />
    public partial class inicijalna : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Grad",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grad", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Kategorija",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategorija", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Korisnik",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slika = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisnik", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Oglas",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naslov = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RokPrijave = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VrijemeObjave = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Slika = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Oglas", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Firma",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adresa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GradID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Firma", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Firma_Grad_GradID",
                        column: x => x.GradID,
                        principalTable: "Grad",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stipenditor",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adresa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipUstanove = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GradID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stipenditor", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Stipenditor_Grad_GradID",
                        column: x => x.GradID,
                        principalTable: "Grad",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Univerzitet",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Logo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Slika = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GradID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Univerzitet", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Univerzitet_Grad_GradID",
                        column: x => x.GradID,
                        principalTable: "Grad",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Administrator",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administrator", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Administrator_Korisnik_ID",
                        column: x => x.ID,
                        principalTable: "Korisnik",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AutentifikacijaToken",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    vrijednost = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KorisnikId = table.Column<int>(type: "int", nullable: false),
                    vrijemeEvidentiranja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    twoFactorCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    twoFactorOtkljucano = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutentifikacijaToken", x => x.id);
                    table.ForeignKey(
                        name: "FK_AutentifikacijaToken_Korisnik_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "Korisnik",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IzdavacSmjestaja",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IzdavacSmjestaja", x => x.ID);
                    table.ForeignKey(
                        name: "FK_IzdavacSmjestaja_Korisnik_ID",
                        column: x => x.ID,
                        principalTable: "Korisnik",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LogKretanjePoSistemu",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    korisnikID = table.Column<int>(type: "int", nullable: true),
                    queryPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    postData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    vrijeme = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ipAdresa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    exceptionMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isException = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogKretanjePoSistemu", x => x.id);
                    table.ForeignKey(
                        name: "FK_LogKretanjePoSistemu_Korisnik_korisnikID",
                        column: x => x.korisnikID,
                        principalTable: "Korisnik",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "UposlenikFirme",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pozicija = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirmaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UposlenikFirme", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UposlenikFirme_Firma_FirmaID",
                        column: x => x.FirmaID,
                        principalTable: "Firma",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UposlenikFirme_Korisnik_ID",
                        column: x => x.ID,
                        principalTable: "Korisnik",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UposlenikStipenditora",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StipenditorID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UposlenikStipenditora", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UposlenikStipenditora_Korisnik_ID",
                        column: x => x.ID,
                        principalTable: "Korisnik",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UposlenikStipenditora_Stipenditor_StipenditorID",
                        column: x => x.StipenditorID,
                        principalTable: "Stipenditor",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Fakultet",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adresa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Logo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Slika = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UniverzitetID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fakultet", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Fakultet_Univerzitet_UniverzitetID",
                        column: x => x.UniverzitetID,
                        principalTable: "Univerzitet",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReferentUniverziteta",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnivetzitetID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReferentUniverziteta", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ReferentUniverziteta_Korisnik_ID",
                        column: x => x.ID,
                        principalTable: "Korisnik",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReferentUniverziteta_Univerzitet_UnivetzitetID",
                        column: x => x.UnivetzitetID,
                        principalTable: "Univerzitet",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Smjestaj",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    Cijena = table.Column<double>(type: "float", nullable: false),
                    Kapacitet = table.Column<int>(type: "int", nullable: false),
                    DodatneUsluge = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrojSoba = table.Column<int>(type: "int", nullable: false),
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
                name: "Praksa",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    PocetakPrakse = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KrajPrakse = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                name: "Stipendija",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    Uslovi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Iznos = table.Column<double>(type: "float", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "ReferentFakulteta",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FakultetID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReferentFakulteta", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ReferentFakulteta_Fakultet_FakultetID",
                        column: x => x.FakultetID,
                        principalTable: "Fakultet",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReferentFakulteta_Korisnik_ID",
                        column: x => x.ID,
                        principalTable: "Korisnik",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrojIndeksa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GodinaStudija = table.Column<int>(type: "int", nullable: false),
                    NacinStudiranja = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FakultetID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Student_Fakultet_FakultetID",
                        column: x => x.FakultetID,
                        principalTable: "Fakultet",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Student_Korisnik_ID",
                        column: x => x.ID,
                        principalTable: "Korisnik",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Objava",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naslov = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sadrzaj = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VrijemeObjave = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Slika = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KategorijaID = table.Column<int>(type: "int", nullable: false),
                    ReferentFakultetaID = table.Column<int>(type: "int", nullable: true),
                    ReferentUniverzitetaID = table.Column<int>(type: "int", nullable: true),
                    UposlenikFirmeID = table.Column<int>(type: "int", nullable: true),
                    UposlenikStipenditoraID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Objava", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Objava_Kategorija_KategorijaID",
                        column: x => x.KategorijaID,
                        principalTable: "Kategorija",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Objava_ReferentFakulteta_ReferentFakultetaID",
                        column: x => x.ReferentFakultetaID,
                        principalTable: "ReferentFakulteta",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Objava_ReferentUniverziteta_ReferentUniverzitetaID",
                        column: x => x.ReferentUniverzitetaID,
                        principalTable: "ReferentUniverziteta",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Objava_UposlenikFirme_UposlenikFirmeID",
                        column: x => x.UposlenikFirmeID,
                        principalTable: "UposlenikFirme",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Objava_UposlenikStipenditora_UposlenikStipenditoraID",
                        column: x => x.UposlenikStipenditoraID,
                        principalTable: "UposlenikStipenditora",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Ocjena",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    Vrijednost = table.Column<int>(type: "int", nullable: false),
                    Komentar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FakultetID = table.Column<int>(type: "int", nullable: true),
                    FirmaID = table.Column<int>(type: "int", nullable: true),
                    SmjestajID = table.Column<int>(type: "int", nullable: true),
                    StipenditorID = table.Column<int>(type: "int", nullable: true),
                    UniverzitetID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ocjena", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Ocjena_Fakultet_FakultetID",
                        column: x => x.FakultetID,
                        principalTable: "Fakultet",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Ocjena_Firma_FirmaID",
                        column: x => x.FirmaID,
                        principalTable: "Firma",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Ocjena_Smjestaj_SmjestajID",
                        column: x => x.SmjestajID,
                        principalTable: "Smjestaj",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Ocjena_Stipenditor_StipenditorID",
                        column: x => x.StipenditorID,
                        principalTable: "Stipenditor",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Ocjena_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ocjena_Univerzitet_UniverzitetID",
                        column: x => x.UniverzitetID,
                        principalTable: "Univerzitet",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "PrijavaPraksa",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    PraksaId = table.Column<int>(type: "int", nullable: false),
                    PropratnoPismo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CV = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Certifikati = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrijavaPraksa", x => new { x.StudentId, x.PraksaId });
                    table.ForeignKey(
                        name: "FK_PrijavaPraksa_Praksa_PraksaId",
                        column: x => x.PraksaId,
                        principalTable: "Praksa",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_PrijavaPraksa_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "PrijavaStipendija",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    StipendijaID = table.Column<int>(type: "int", nullable: false),
                    Dokumentacija = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CV = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProsjekOcjena = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrijavaStipendija", x => new { x.StudentId, x.StipendijaID });
                    table.ForeignKey(
                        name: "FK_PrijavaStipendija_Stipendija_StipendijaID",
                        column: x => x.StipendijaID,
                        principalTable: "Stipendija",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_PrijavaStipendija_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Rezervacija",
                columns: table => new
                {
                    SmjestajId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    DatumPrijave = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DatumOdjave = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BrojOsoba = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rezervacija", x => new { x.StudentId, x.SmjestajId });
                    table.ForeignKey(
                        name: "FK_Rezervacija_Smjestaj_SmjestajId",
                        column: x => x.SmjestajId,
                        principalTable: "Smjestaj",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Rezervacija_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Komentar",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ObjavaID = table.Column<int>(type: "int", nullable: true),
                    OglasID = table.Column<int>(type: "int", nullable: true),
                    KomentarID = table.Column<int>(type: "int", nullable: true),
                    KorisnikID = table.Column<int>(type: "int", nullable: false),
                    VrijemeObjave = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Komentar", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Komentar_Komentar_KomentarID",
                        column: x => x.KomentarID,
                        principalTable: "Komentar",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Komentar_Korisnik_KorisnikID",
                        column: x => x.KorisnikID,
                        principalTable: "Korisnik",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Komentar_Objava_ObjavaID",
                        column: x => x.ObjavaID,
                        principalTable: "Objava",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Komentar_Oglas_OglasID",
                        column: x => x.OglasID,
                        principalTable: "Oglas",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AutentifikacijaToken_KorisnikId",
                table: "AutentifikacijaToken",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_Fakultet_UniverzitetID",
                table: "Fakultet",
                column: "UniverzitetID");

            migrationBuilder.CreateIndex(
                name: "IX_Firma_GradID",
                table: "Firma",
                column: "GradID");

            migrationBuilder.CreateIndex(
                name: "IX_Komentar_KomentarID",
                table: "Komentar",
                column: "KomentarID");

            migrationBuilder.CreateIndex(
                name: "IX_Komentar_KorisnikID",
                table: "Komentar",
                column: "KorisnikID");

            migrationBuilder.CreateIndex(
                name: "IX_Komentar_ObjavaID",
                table: "Komentar",
                column: "ObjavaID");

            migrationBuilder.CreateIndex(
                name: "IX_Komentar_OglasID",
                table: "Komentar",
                column: "OglasID");

            migrationBuilder.CreateIndex(
                name: "IX_LogKretanjePoSistemu_korisnikID",
                table: "LogKretanjePoSistemu",
                column: "korisnikID");

            migrationBuilder.CreateIndex(
                name: "IX_Objava_KategorijaID",
                table: "Objava",
                column: "KategorijaID");

            migrationBuilder.CreateIndex(
                name: "IX_Objava_ReferentFakultetaID",
                table: "Objava",
                column: "ReferentFakultetaID");

            migrationBuilder.CreateIndex(
                name: "IX_Objava_ReferentUniverzitetaID",
                table: "Objava",
                column: "ReferentUniverzitetaID");

            migrationBuilder.CreateIndex(
                name: "IX_Objava_UposlenikFirmeID",
                table: "Objava",
                column: "UposlenikFirmeID");

            migrationBuilder.CreateIndex(
                name: "IX_Objava_UposlenikStipenditoraID",
                table: "Objava",
                column: "UposlenikStipenditoraID");

            migrationBuilder.CreateIndex(
                name: "IX_Ocjena_FakultetID",
                table: "Ocjena",
                column: "FakultetID");

            migrationBuilder.CreateIndex(
                name: "IX_Ocjena_FirmaID",
                table: "Ocjena",
                column: "FirmaID");

            migrationBuilder.CreateIndex(
                name: "IX_Ocjena_SmjestajID",
                table: "Ocjena",
                column: "SmjestajID");

            migrationBuilder.CreateIndex(
                name: "IX_Ocjena_StipenditorID",
                table: "Ocjena",
                column: "StipenditorID");

            migrationBuilder.CreateIndex(
                name: "IX_Ocjena_StudentId",
                table: "Ocjena",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Ocjena_UniverzitetID",
                table: "Ocjena",
                column: "UniverzitetID");

            migrationBuilder.CreateIndex(
                name: "IX_Praksa_UposlenikID",
                table: "Praksa",
                column: "UposlenikID");

            migrationBuilder.CreateIndex(
                name: "IX_PrijavaPraksa_PraksaId",
                table: "PrijavaPraksa",
                column: "PraksaId");

            migrationBuilder.CreateIndex(
                name: "IX_PrijavaStipendija_StipendijaID",
                table: "PrijavaStipendija",
                column: "StipendijaID");

            migrationBuilder.CreateIndex(
                name: "IX_ReferentFakulteta_FakultetID",
                table: "ReferentFakulteta",
                column: "FakultetID");

            migrationBuilder.CreateIndex(
                name: "IX_ReferentUniverziteta_UnivetzitetID",
                table: "ReferentUniverziteta",
                column: "UnivetzitetID");

            migrationBuilder.CreateIndex(
                name: "IX_Rezervacija_SmjestajId",
                table: "Rezervacija",
                column: "SmjestajId");

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

            migrationBuilder.CreateIndex(
                name: "IX_Stipenditor_GradID",
                table: "Stipenditor",
                column: "GradID");

            migrationBuilder.CreateIndex(
                name: "IX_Student_FakultetID",
                table: "Student",
                column: "FakultetID");

            migrationBuilder.CreateIndex(
                name: "IX_Univerzitet_GradID",
                table: "Univerzitet",
                column: "GradID");

            migrationBuilder.CreateIndex(
                name: "IX_UposlenikFirme_FirmaID",
                table: "UposlenikFirme",
                column: "FirmaID");

            migrationBuilder.CreateIndex(
                name: "IX_UposlenikStipenditora_StipenditorID",
                table: "UposlenikStipenditora",
                column: "StipenditorID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Administrator");

            migrationBuilder.DropTable(
                name: "AutentifikacijaToken");

            migrationBuilder.DropTable(
                name: "Komentar");

            migrationBuilder.DropTable(
                name: "LogKretanjePoSistemu");

            migrationBuilder.DropTable(
                name: "Ocjena");

            migrationBuilder.DropTable(
                name: "PrijavaPraksa");

            migrationBuilder.DropTable(
                name: "PrijavaStipendija");

            migrationBuilder.DropTable(
                name: "Rezervacija");

            migrationBuilder.DropTable(
                name: "Objava");

            migrationBuilder.DropTable(
                name: "Praksa");

            migrationBuilder.DropTable(
                name: "Stipendija");

            migrationBuilder.DropTable(
                name: "Smjestaj");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "Kategorija");

            migrationBuilder.DropTable(
                name: "ReferentFakulteta");

            migrationBuilder.DropTable(
                name: "ReferentUniverziteta");

            migrationBuilder.DropTable(
                name: "UposlenikFirme");

            migrationBuilder.DropTable(
                name: "UposlenikStipenditora");

            migrationBuilder.DropTable(
                name: "IzdavacSmjestaja");

            migrationBuilder.DropTable(
                name: "Oglas");

            migrationBuilder.DropTable(
                name: "Fakultet");

            migrationBuilder.DropTable(
                name: "Firma");

            migrationBuilder.DropTable(
                name: "Stipenditor");

            migrationBuilder.DropTable(
                name: "Korisnik");

            migrationBuilder.DropTable(
                name: "Univerzitet");

            migrationBuilder.DropTable(
                name: "Grad");
        }
    }
}
