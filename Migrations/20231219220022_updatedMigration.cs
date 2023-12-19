using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LabBackend.Migrations
{
    /// <inheritdoc />
    public partial class updatedMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrencyId",
                table: "Record",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Record_CurrencyId",
                table: "Record",
                column: "CurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Record_Currency_CurrencyId",
                table: "Record",
                column: "CurrencyId",
                principalTable: "Currency",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Record_Currency_CurrencyId",
                table: "Record");

            migrationBuilder.DropIndex(
                name: "IX_Record_CurrencyId",
                table: "Record");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "Record");
        }
    }
}
