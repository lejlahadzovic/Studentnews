using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentOglasi.Migrations
{
    /// <inheritdoc />
    public partial class ispravka : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FakultetID_Univerzitet_UniverzitetID",
                table: "FakultetID");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_FakultetID_FakultetID",
                table: "Student");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FakultetID",
                table: "FakultetID");

            migrationBuilder.RenameTable(
                name: "FakultetID",
                newName: "Fakultet");

            migrationBuilder.RenameIndex(
                name: "IX_FakultetID_UniverzitetID",
                table: "Fakultet",
                newName: "IX_Fakultet_UniverzitetID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Fakultet",
                table: "Fakultet",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Fakultet_Univerzitet_UniverzitetID",
                table: "Fakultet",
                column: "UniverzitetID",
                principalTable: "Univerzitet",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_Fakultet_Univerzitet_UniverzitetID",
                table: "Fakultet");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_Fakultet_FakultetID",
                table: "Student");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Fakultet",
                table: "Fakultet");

            migrationBuilder.RenameTable(
                name: "Fakultet",
                newName: "FakultetID");

            migrationBuilder.RenameIndex(
                name: "IX_Fakultet_UniverzitetID",
                table: "FakultetID",
                newName: "IX_FakultetID_UniverzitetID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FakultetID",
                table: "FakultetID",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_FakultetID_Univerzitet_UniverzitetID",
                table: "FakultetID",
                column: "UniverzitetID",
                principalTable: "Univerzitet",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Student_FakultetID_FakultetID",
                table: "Student",
                column: "FakultetID",
                principalTable: "FakultetID",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
