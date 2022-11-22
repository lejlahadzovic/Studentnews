using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentOglasi.Migrations
{
    /// <inheritdoc />
    public partial class prijave : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_PrijavaPraksa_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "PrijavaStipendija",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    StipendijaID = table.Column<int>(type: "int", nullable: false),
                    Dokumentacija = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CV = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProsjekOcjena = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrijavaStipendija", x => new { x.StudentId, x.StipendijaID });
                    table.ForeignKey(
                        name: "FK_PrijavaStipendija_Stipendija_StipendijaID",
                        column: x => x.StipendijaID,
                        principalTable: "Stipendija",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_PrijavaStipendija_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PrijavaPraksa_PraksaId",
                table: "PrijavaPraksa",
                column: "PraksaId");

            migrationBuilder.CreateIndex(
                name: "IX_PrijavaStipendija_StipendijaID",
                table: "PrijavaStipendija",
                column: "StipendijaID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PrijavaPraksa");

            migrationBuilder.DropTable(
                name: "PrijavaStipendija");
        }
    }
}
