using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVilla_VillaAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddForeingKeyToVillaTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VillaId",
                table: "VillaNumber",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_VillaNumber_VillaId",
                table: "VillaNumber",
                column: "VillaId");

            migrationBuilder.AddForeignKey(
                name: "FK_VillaNumber_Villas_VillaId",
                table: "VillaNumber",
                column: "VillaId",
                principalTable: "Villas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VillaNumber_Villas_VillaId",
                table: "VillaNumber");

            migrationBuilder.DropIndex(
                name: "IX_VillaNumber_VillaId",
                table: "VillaNumber");

            migrationBuilder.DropColumn(
                name: "VillaId",
                table: "VillaNumber");
        }
    }
}
