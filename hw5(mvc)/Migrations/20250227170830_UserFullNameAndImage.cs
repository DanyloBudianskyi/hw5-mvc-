using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hw5_mvc_.Migrations
{
    /// <inheritdoc />
    public partial class UserFullNameAndImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "AspNetUsers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ImageFileId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ImageFileId",
                table: "AspNetUsers",
                column: "ImageFileId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_ImageFiles_ImageFileId",
                table: "AspNetUsers",
                column: "ImageFileId",
                principalTable: "ImageFiles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_ImageFiles_ImageFileId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ImageFileId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ImageFileId",
                table: "AspNetUsers");
        }
    }
}
