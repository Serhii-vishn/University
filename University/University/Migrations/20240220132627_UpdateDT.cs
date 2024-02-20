using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace University.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDT : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Faculty_Name",
                table: "Faculty",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Departmant_Name",
                table: "Departmant",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Faculty_Name",
                table: "Faculty");

            migrationBuilder.DropIndex(
                name: "IX_Departmant_Name",
                table: "Departmant");
        }
    }
}
