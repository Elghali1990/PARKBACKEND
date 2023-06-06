using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace chrep.data.park.Migrations
{
    /// <inheritdoc />
    public partial class editTableVehiculeSeparator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Type_Matricule",
                table: "Vehicles",
                type: "nvarchar(max)",
                nullable: true,
                computedColumnSql: "[Matricule] + '-' + [TypeVehicule]",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComputedColumnSql: "[Matricule] + ', ' + [TypeVehicule]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Type_Matricule",
                table: "Vehicles",
                type: "nvarchar(max)",
                nullable: true,
                computedColumnSql: "[Matricule] + ', ' + [TypeVehicule]",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComputedColumnSql: "[Matricule] + '-' + [TypeVehicule]");
        }
    }
}
