using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hw5_mvc_.Migrations
{
    /// <inheritdoc />
    public partial class onmodelcreating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImageFiles_UserInfos_UserInfoId",
                table: "ImageFiles");

            migrationBuilder.DropIndex(
                name: "IX_ImageFiles_UserInfoId",
                table: "ImageFiles");

            migrationBuilder.DropColumn(
                name: "UserInfoId",
                table: "ImageFiles");

            migrationBuilder.CreateTable(
                name: "ImageFileUserInfo",
                columns: table => new
                {
                    ImageFilesId = table.Column<int>(type: "int", nullable: false),
                    UserInfosId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageFileUserInfo", x => new { x.ImageFilesId, x.UserInfosId });
                    table.ForeignKey(
                        name: "FK_ImageFileUserInfo_ImageFiles_ImageFilesId",
                        column: x => x.ImageFilesId,
                        principalTable: "ImageFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ImageFileUserInfo_UserInfos_UserInfosId",
                        column: x => x.UserInfosId,
                        principalTable: "UserInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ImageFileUserInfo_UserInfosId",
                table: "ImageFileUserInfo",
                column: "UserInfosId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImageFileUserInfo");

            migrationBuilder.AddColumn<int>(
                name: "UserInfoId",
                table: "ImageFiles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ImageFiles_UserInfoId",
                table: "ImageFiles",
                column: "UserInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_ImageFiles_UserInfos_UserInfoId",
                table: "ImageFiles",
                column: "UserInfoId",
                principalTable: "UserInfos",
                principalColumn: "Id");
        }
    }
}
