using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace chrep.data.park.Migrations
{
    /// <inheritdoc />
    public partial class editTableVehicule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TypeVehicule",
                table: "Vehicles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type_Matricule",
                table: "Vehicles",
                type: "nvarchar(max)",
                nullable: true,
                computedColumnSql: "[Matricule] + ', ' + [TypeVehicule]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type_Matricule",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "TypeVehicule",
                table: "Vehicles");
        }
    }
}
