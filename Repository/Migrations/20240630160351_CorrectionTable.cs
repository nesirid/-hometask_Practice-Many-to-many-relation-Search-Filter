using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    public partial class CorrectionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "Educations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Educations_StudentId",
                table: "Educations",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Educations_Students_StudentId",
                table: "Educations",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Educations_Students_StudentId",
                table: "Educations");

            migrationBuilder.DropIndex(
                name: "IX_Educations_StudentId",
                table: "Educations");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Educations");
        }
    }
}
