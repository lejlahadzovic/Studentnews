using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentOglasi.Migrations
{
    /// <inheritdoc />
    public partial class dodavanjeSlike : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Slika",
                table: "Smjestaj");

            migrationBuilder.AddColumn<string>(
                name: "Slika",
                table: "Oglas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Slika",
                table: "Oglas");

            migrationBuilder.AddColumn<string>(
                name: "Slika",
                table: "Smjestaj",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
