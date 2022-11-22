using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentOglasi.Migrations
{
    /// <inheritdoc />
    public partial class test1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Korisnik_Univerzitet_UniverzitetID",
                table: "Korisnik");

            migrationBuilder.DropIndex(
                name: "IX_Korisnik_UniverzitetID",
                table: "Korisnik");

            migrationBuilder.DropColumn(
                name: "BrojIndeksa",
                table: "Korisnik");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Korisnik");

            migrationBuilder.DropColumn(
                name: "GodinaStudija",
                table: "Korisnik");

            migrationBuilder.DropColumn(
                name: "Ime",
                table: "Korisnik");

            migrationBuilder.DropColumn(
                name: "NacinStudiranja",
                table: "Korisnik");

            migrationBuilder.DropColumn(
                name: "Prezime",
                table: "Korisnik");

            migrationBuilder.DropColumn(
                name: "UniverzitetID",
                table: "Korisnik");

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
                    UniverzitetID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Student_Korisnik_ID",
                        column: x => x.ID,
                        principalTable: "Korisnik",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Student_Univerzitet_UniverzitetID",
                        column: x => x.UniverzitetID,
                        principalTable: "Univerzitet",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Student_UniverzitetID",
                table: "Student",
                column: "UniverzitetID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.AddColumn<string>(
                name: "BrojIndeksa",
                table: "Korisnik",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Korisnik",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "GodinaStudija",
                table: "Korisnik",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ime",
                table: "Korisnik",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NacinStudiranja",
                table: "Korisnik",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Prezime",
                table: "Korisnik",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UniverzitetID",
                table: "Korisnik",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Korisnik_UniverzitetID",
                table: "Korisnik",
                column: "UniverzitetID");

            migrationBuilder.AddForeignKey(
                name: "FK_Korisnik_Univerzitet_UniverzitetID",
                table: "Korisnik",
                column: "UniverzitetID",
                principalTable: "Univerzitet",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
