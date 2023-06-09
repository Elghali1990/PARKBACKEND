using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace chrep.data.park.Migrations
{
    /// <inheritdoc />
    public partial class addForeighkeyUserIdToTableAbsence : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Absences_Users_MissionId",
                table: "Absences");

            migrationBuilder.CreateIndex(
                name: "IX_Absences_UserId",
                table: "Absences",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Absences_Users_UserId",
                table: "Absences",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Absences_Users_UserId",
                table: "Absences");

            migrationBuilder.DropIndex(
                name: "IX_Absences_UserId",
                table: "Absences");

            migrationBuilder.AddForeignKey(
                name: "FK_Absences_Users_MissionId",
                table: "Absences",
                column: "MissionId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
