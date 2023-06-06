using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace chrep.data.park.Migrations
{
    /// <inheritdoc />
    public partial class addtableMissionUserMission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Missions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateDepart = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HourDepart = table.Column<TimeSpan>(type: "time", nullable: true),
                    Instruction = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Observation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChauffeurId = table.Column<int>(type: "int", nullable: true),
                    ChauffeurName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VehicleId = table.Column<int>(type: "int", nullable: true),
                    DemandeId = table.Column<int>(type: "int", nullable: false),
                    MissionType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Missions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Missions_Demandes_DemandeId",
                        column: x => x.DemandeId,
                        principalTable: "Demandes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Missions_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserMission",
                columns: table => new
                {
                    MissionId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    IsAbsent = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMission", x => new { x.MissionId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserMission_Missions_MissionId",
                        column: x => x.MissionId,
                        principalTable: "Missions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserMission_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Missions_DemandeId",
                table: "Missions",
                column: "DemandeId");

            migrationBuilder.CreateIndex(
                name: "IX_Missions_VehicleId",
                table: "Missions",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMission_UserId",
                table: "UserMission",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserMission");

            migrationBuilder.DropTable(
                name: "Missions");
        }
    }
}
