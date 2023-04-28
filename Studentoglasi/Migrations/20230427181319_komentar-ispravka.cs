using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentOglasi.Migrations
{
    /// <inheritdoc />
    public partial class komentarispravka : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "KomentarID",
                table: "Komentar",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Komentar_KomentarID",
                table: "Komentar",
                column: "KomentarID");

            migrationBuilder.AddForeignKey(
                name: "FK_Komentar_Komentar_KomentarID",
                table: "Komentar",
                column: "KomentarID",
                principalTable: "Komentar",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Komentar_Komentar_KomentarID",
                table: "Komentar");

            migrationBuilder.DropIndex(
                name: "IX_Komentar_KomentarID",
                table: "Komentar");

            migrationBuilder.DropColumn(
                name: "KomentarID",
                table: "Komentar");
        }
    }
}
