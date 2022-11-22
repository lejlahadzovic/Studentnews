using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentOglasi.Migrations
{
    /// <inheritdoc />
    public partial class objave1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReferentFakultetaID",
                table: "Objava",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReferentUniverzitetaID",
                table: "Objava",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UposlenikFirmeID",
                table: "Objava",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UposlenikStipenditoraID",
                table: "Objava",
                type: "int",
                nullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Objava_ReferentFakulteta_ReferentFakultetaID",
                table: "Objava",
                column: "ReferentFakultetaID",
                principalTable: "ReferentFakulteta",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Objava_ReferentUniverziteta_ReferentUniverzitetaID",
                table: "Objava",
                column: "ReferentUniverzitetaID",
                principalTable: "ReferentUniverziteta",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Objava_UposlenikFirme_UposlenikFirmeID",
                table: "Objava",
                column: "UposlenikFirmeID",
                principalTable: "UposlenikFirme",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Objava_UposlenikStipenditora_UposlenikStipenditoraID",
                table: "Objava",
                column: "UposlenikStipenditoraID",
                principalTable: "UposlenikStipenditora",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Objava_ReferentFakulteta_ReferentFakultetaID",
                table: "Objava");

            migrationBuilder.DropForeignKey(
                name: "FK_Objava_ReferentUniverziteta_ReferentUniverzitetaID",
                table: "Objava");

            migrationBuilder.DropForeignKey(
                name: "FK_Objava_UposlenikFirme_UposlenikFirmeID",
                table: "Objava");

            migrationBuilder.DropForeignKey(
                name: "FK_Objava_UposlenikStipenditora_UposlenikStipenditoraID",
                table: "Objava");

            migrationBuilder.DropIndex(
                name: "IX_Objava_ReferentFakultetaID",
                table: "Objava");

            migrationBuilder.DropIndex(
                name: "IX_Objava_ReferentUniverzitetaID",
                table: "Objava");

            migrationBuilder.DropIndex(
                name: "IX_Objava_UposlenikFirmeID",
                table: "Objava");

            migrationBuilder.DropIndex(
                name: "IX_Objava_UposlenikStipenditoraID",
                table: "Objava");

            migrationBuilder.DropColumn(
                name: "ReferentFakultetaID",
                table: "Objava");

            migrationBuilder.DropColumn(
                name: "ReferentUniverzitetaID",
                table: "Objava");

            migrationBuilder.DropColumn(
                name: "UposlenikFirmeID",
                table: "Objava");

            migrationBuilder.DropColumn(
                name: "UposlenikStipenditoraID",
                table: "Objava");
        }
    }
}
