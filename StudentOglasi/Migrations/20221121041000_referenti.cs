using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentOglasi.Migrations
{
    /// <inheritdoc />
    public partial class referenti : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Komentar",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ObajvaID = table.Column<int>(type: "int", nullable: false),
                    KorisnikID = table.Column<int>(type: "int", nullable: false),
                    VrijemeObjave = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Komentar", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Komentar_Korisnik_KorisnikID",
                        column: x => x.KorisnikID,
                        principalTable: "Korisnik",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Komentar_Objava_ObajvaID",
                        column: x => x.ObajvaID,
                        principalTable: "Objava",
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
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                name: "ReferentUniverziteta",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
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

            migrationBuilder.CreateIndex(
                name: "IX_Komentar_KorisnikID",
                table: "Komentar",
                column: "KorisnikID");

            migrationBuilder.CreateIndex(
                name: "IX_Komentar_ObajvaID",
                table: "Komentar",
                column: "ObajvaID");

            migrationBuilder.CreateIndex(
                name: "IX_ReferentFakulteta_FakultetID",
                table: "ReferentFakulteta",
                column: "FakultetID");

            migrationBuilder.CreateIndex(
                name: "IX_ReferentUniverziteta_UnivetzitetID",
                table: "ReferentUniverziteta",
                column: "UnivetzitetID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Komentar");

            migrationBuilder.DropTable(
                name: "ReferentFakulteta");

            migrationBuilder.DropTable(
                name: "ReferentUniverziteta");
        }
    }
}
