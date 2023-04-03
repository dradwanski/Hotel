using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel.Database.Migrations
{
    /// <inheritdoc />
    public partial class modification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ModifikationDate",
                table: "Reservations",
                newName: "ModificationDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ModificationDate",
                table: "Reservations",
                newName: "ModifikationDate");
        }
    }
}
