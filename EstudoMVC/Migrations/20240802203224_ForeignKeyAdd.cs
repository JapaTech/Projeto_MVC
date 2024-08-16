using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EstudoMVC.Migrations
{
    /// <inheritdoc />
    public partial class ForeignKeyAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReviewDate",
                table: "Reviews",
                newName: "CreationDate");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "TouristAttractions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TouristAttractionId",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_TouristAttractionId",
                table: "Reviews",
                column: "TouristAttractionId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserId",
                table: "Reviews",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_TouristAttractions_TouristAttractionId",
                table: "Reviews",
                column: "TouristAttractionId",
                principalTable: "TouristAttractions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Users_UserId",
                table: "Reviews",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_TouristAttractions_TouristAttractionId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Users_UserId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_TouristAttractionId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_UserId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "TouristAttractions");

            migrationBuilder.DropColumn(
                name: "TouristAttractionId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Reviews");

            migrationBuilder.RenameColumn(
                name: "CreationDate",
                table: "Reviews",
                newName: "ReviewDate");
        }
    }
}
