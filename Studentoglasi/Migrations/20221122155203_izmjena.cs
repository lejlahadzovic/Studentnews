using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentOglasi.Migrations
{
    /// <inheritdoc />
    public partial class izmjena : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ocjena_Smjestaj_SmjestajId",
                table: "Ocjena");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ocjena",
                table: "Ocjena");

            migrationBuilder.RenameColumn(
                name: "SmjestajId",
                table: "Ocjena",
                newName: "SmjestajID");

            migrationBuilder.RenameIndex(
                name: "IX_Ocjena_SmjestajId",
                table: "Ocjena",
                newName: "IX_Ocjena_SmjestajID");

            migrationBuilder.AlterColumn<int>(
                name: "SmjestajID",
                table: "Ocjena",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "Ocjena",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "FakultetID",
                table: "Ocjena",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FirmaID",
                table: "Ocjena",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StipenditorID",
                table: "Ocjena",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UniverzitetID",
                table: "Ocjena",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ocjena",
                table: "Ocjena",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_Ocjena_FakultetID",
                table: "Ocjena",
                column: "FakultetID");

            migrationBuilder.CreateIndex(
                name: "IX_Ocjena_FirmaID",
                table: "Ocjena",
                column: "FirmaID");

            migrationBuilder.CreateIndex(
                name: "IX_Ocjena_StipenditorID",
                table: "Ocjena",
                column: "StipenditorID");

            migrationBuilder.CreateIndex(
                name: "IX_Ocjena_StudentId",
                table: "Ocjena",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Ocjena_UniverzitetID",
                table: "Ocjena",
                column: "UniverzitetID");

            migrationBuilder.AddForeignKey(
                name: "FK_Ocjena_Fakultet_FakultetID",
                table: "Ocjena",
                column: "FakultetID",
                principalTable: "Fakultet",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Ocjena_Firma_FirmaID",
                table: "Ocjena",
                column: "FirmaID",
                principalTable: "Firma",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Ocjena_Smjestaj_SmjestajID",
                table: "Ocjena",
                column: "SmjestajID",
                principalTable: "Smjestaj",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Ocjena_Stipenditor_StipenditorID",
                table: "Ocjena",
                column: "StipenditorID",
                principalTable: "Stipenditor",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Ocjena_Univerzitet_UniverzitetID",
                table: "Ocjena",
                column: "UniverzitetID",
                principalTable: "Univerzitet",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ocjena_Fakultet_FakultetID",
                table: "Ocjena");

            migrationBuilder.DropForeignKey(
                name: "FK_Ocjena_Firma_FirmaID",
                table: "Ocjena");

            migrationBuilder.DropForeignKey(
                name: "FK_Ocjena_Smjestaj_SmjestajID",
                table: "Ocjena");

            migrationBuilder.DropForeignKey(
                name: "FK_Ocjena_Stipenditor_StipenditorID",
                table: "Ocjena");

            migrationBuilder.DropForeignKey(
                name: "FK_Ocjena_Univerzitet_UniverzitetID",
                table: "Ocjena");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ocjena",
                table: "Ocjena");

            migrationBuilder.DropIndex(
                name: "IX_Ocjena_FakultetID",
                table: "Ocjena");

            migrationBuilder.DropIndex(
                name: "IX_Ocjena_FirmaID",
                table: "Ocjena");

            migrationBuilder.DropIndex(
                name: "IX_Ocjena_StipenditorID",
                table: "Ocjena");

            migrationBuilder.DropIndex(
                name: "IX_Ocjena_StudentId",
                table: "Ocjena");

            migrationBuilder.DropIndex(
                name: "IX_Ocjena_UniverzitetID",
                table: "Ocjena");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "Ocjena");

            migrationBuilder.DropColumn(
                name: "FakultetID",
                table: "Ocjena");

            migrationBuilder.DropColumn(
                name: "FirmaID",
                table: "Ocjena");

            migrationBuilder.DropColumn(
                name: "StipenditorID",
                table: "Ocjena");

            migrationBuilder.DropColumn(
                name: "UniverzitetID",
                table: "Ocjena");

            migrationBuilder.RenameColumn(
                name: "SmjestajID",
                table: "Ocjena",
                newName: "SmjestajId");

            migrationBuilder.RenameIndex(
                name: "IX_Ocjena_SmjestajID",
                table: "Ocjena",
                newName: "IX_Ocjena_SmjestajId");

            migrationBuilder.AlterColumn<int>(
                name: "SmjestajId",
                table: "Ocjena",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ocjena",
                table: "Ocjena",
                columns: new[] { "StudentId", "SmjestajId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Ocjena_Smjestaj_SmjestajId",
                table: "Ocjena",
                column: "SmjestajId",
                principalTable: "Smjestaj",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
