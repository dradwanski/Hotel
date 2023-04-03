using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel.Database.Migrations
{
    /// <inheritdoc />
    public partial class NotNullableReservationStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Status_ReservationStatusStatusId",
                table: "Reservations");

            migrationBuilder.AlterColumn<int>(
                name: "ReservationStatusStatusId",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Status_ReservationStatusStatusId",
                table: "Reservations",
                column: "ReservationStatusStatusId",
                principalTable: "Status",
                principalColumn: "StatusId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Status_ReservationStatusStatusId",
                table: "Reservations");

            migrationBuilder.AlterColumn<int>(
                name: "ReservationStatusStatusId",
                table: "Reservations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Status_ReservationStatusStatusId",
                table: "Reservations",
                column: "ReservationStatusStatusId",
                principalTable: "Status",
                principalColumn: "StatusId");
        }
    }
}
