using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace chrep.data.park.Migrations
{
    /// <inheritdoc />
    public partial class addtabledemandeuserDemande : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Demandes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateDemande = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Objet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Detail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateDepart = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HourDepart = table.Column<TimeSpan>(type: "time", nullable: true),
                    DateBack = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HourBack = table.Column<TimeSpan>(type: "time", nullable: true),
                    Observation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusEnum = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Demandes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserDemande",
                columns: table => new
                {
                    DemandeId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    DemandeOwner = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDemande", x => new { x.DemandeId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserDemande_Demandes_DemandeId",
                        column: x => x.DemandeId,
                        principalTable: "Demandes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserDemande_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserDemande_UserId",
                table: "UserDemande",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserDemande");

            migrationBuilder.DropTable(
                name: "Demandes");
        }
    }
}
