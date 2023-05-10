using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentOglasi.Migrations
{
    /// <inheritdoc />
    public partial class twoFactorAuth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "UposlenikStipenditora");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "UposlenikFirme");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "ReferentUniverziteta");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "ReferentFakulteta");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "IzdavacSmjestaja");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Administrator");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Korisnik",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Korisnik");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "UposlenikStipenditora",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "UposlenikFirme",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "ReferentUniverziteta",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "ReferentFakulteta",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "IzdavacSmjestaja",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Administrator",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
