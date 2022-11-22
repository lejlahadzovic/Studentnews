using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentOglasi.Migrations
{
    /// <inheritdoc />
    public partial class izmjenagreske : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Student_Univerzitet_UniverzitetID",
                table: "Student");

            migrationBuilder.RenameColumn(
                name: "UniverzitetID",
                table: "Student",
                newName: "FakultetID");

            migrationBuilder.RenameIndex(
                name: "IX_Student_UniverzitetID",
                table: "Student",
                newName: "IX_Student_FakultetID");

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

            migrationBuilder.CreateIndex(
                name: "IX_Fakultet_UniverzitetID",
                table: "Fakultet",
                column: "UniverzitetID");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Fakultet_FakultetID",
                table: "Student",
                column: "FakultetID",
                principalTable: "Fakultet",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Student_Fakultet_FakultetID",
                table: "Student");

            migrationBuilder.DropTable(
                name: "Fakultet");

            migrationBuilder.RenameColumn(
                name: "FakultetID",
                table: "Student",
                newName: "UniverzitetID");

            migrationBuilder.RenameIndex(
                name: "IX_Student_FakultetID",
                table: "Student",
                newName: "IX_Student_UniverzitetID");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Univerzitet_UniverzitetID",
                table: "Student",
                column: "UniverzitetID",
                principalTable: "Univerzitet",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
