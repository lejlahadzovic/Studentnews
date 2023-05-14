using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentOglasi.Migrations
{
    /// <inheritdoc />
    public partial class lokacija : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LokacijaID",
                table: "Univerzitet",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LokacijaID",
                table: "Fakultet",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Lokacija",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Lat = table.Column<double>(type: "float", nullable: false),
                    Lng = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lokacija", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Univerzitet_LokacijaID",
                table: "Univerzitet",
                column: "LokacijaID");

            migrationBuilder.CreateIndex(
                name: "IX_Fakultet_LokacijaID",
                table: "Fakultet",
                column: "LokacijaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Fakultet_Lokacija_LokacijaID",
                table: "Fakultet",
                column: "LokacijaID",
                principalTable: "Lokacija",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Univerzitet_Lokacija_LokacijaID",
                table: "Univerzitet",
                column: "LokacijaID",
                principalTable: "Lokacija",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fakultet_Lokacija_LokacijaID",
                table: "Fakultet");

            migrationBuilder.DropForeignKey(
                name: "FK_Univerzitet_Lokacija_LokacijaID",
                table: "Univerzitet");

            migrationBuilder.DropTable(
                name: "Lokacija");

            migrationBuilder.DropIndex(
                name: "IX_Univerzitet_LokacijaID",
                table: "Univerzitet");

            migrationBuilder.DropIndex(
                name: "IX_Fakultet_LokacijaID",
                table: "Fakultet");

            migrationBuilder.DropColumn(
                name: "LokacijaID",
                table: "Univerzitet");

            migrationBuilder.DropColumn(
                name: "LokacijaID",
                table: "Fakultet");
        }
    }
}
