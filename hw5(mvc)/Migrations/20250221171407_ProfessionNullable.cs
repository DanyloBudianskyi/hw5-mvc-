using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hw5_mvc_.Migrations
{
    /// <inheritdoc />
    public partial class ProfessionNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Profession",
                table: "UserInfos");

            migrationBuilder.AddColumn<int>(
                name: "ProfessionId",
                table: "UserInfos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Profession",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profession", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserInfos_ProfessionId",
                table: "UserInfos",
                column: "ProfessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserInfos_Profession_ProfessionId",
                table: "UserInfos",
                column: "ProfessionId",
                principalTable: "Profession",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserInfos_Profession_ProfessionId",
                table: "UserInfos");

            migrationBuilder.DropTable(
                name: "Profession");

            migrationBuilder.DropIndex(
                name: "IX_UserInfos_ProfessionId",
                table: "UserInfos");

            migrationBuilder.DropColumn(
                name: "ProfessionId",
                table: "UserInfos");

            migrationBuilder.AddColumn<string>(
                name: "Profession",
                table: "UserInfos",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
