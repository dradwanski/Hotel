using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel.Database.Migrations
{
    /// <inheritdoc />
    public partial class StatusOfReservation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReservationStatus",
                table: "Reservations");

            migrationBuilder.AddColumn<int>(
                name: "ReservationStatusStatusId",
                table: "Reservations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    StatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.StatusId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ReservationStatusStatusId",
                table: "Reservations",
                column: "ReservationStatusStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Status_ReservationStatusStatusId",
                table: "Reservations",
                column: "ReservationStatusStatusId",
                principalTable: "Status",
                principalColumn: "StatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Status_ReservationStatusStatusId",
                table: "Reservations");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_ReservationStatusStatusId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "ReservationStatusStatusId",
                table: "Reservations");

            migrationBuilder.AddColumn<string>(
                name: "ReservationStatus",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
