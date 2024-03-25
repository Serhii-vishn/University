using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace University.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "Review",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Review_StudentId",
                table: "Review",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Student_StudentId",
                table: "Review",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Review_Student_StudentId",
                table: "Review");

            migrationBuilder.DropIndex(
                name: "IX_Review_StudentId",
                table: "Review");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Review");
        }
    }
}
