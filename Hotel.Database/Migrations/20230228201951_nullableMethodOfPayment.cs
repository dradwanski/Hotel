using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel.Database.Migrations
{
    /// <inheritdoc />
    public partial class nullableMethodOfPayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_MethodOfPayments_MethodOfPaymentId",
                table: "Reservations");

            migrationBuilder.AlterColumn<int>(
                name: "MethodOfPaymentId",
                table: "Reservations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_MethodOfPayments_MethodOfPaymentId",
                table: "Reservations",
                column: "MethodOfPaymentId",
                principalTable: "MethodOfPayments",
                principalColumn: "MethodOfPaymentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_MethodOfPayments_MethodOfPaymentId",
                table: "Reservations");

            migrationBuilder.AlterColumn<int>(
                name: "MethodOfPaymentId",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_MethodOfPayments_MethodOfPaymentId",
                table: "Reservations",
                column: "MethodOfPaymentId",
                principalTable: "MethodOfPayments",
                principalColumn: "MethodOfPaymentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
