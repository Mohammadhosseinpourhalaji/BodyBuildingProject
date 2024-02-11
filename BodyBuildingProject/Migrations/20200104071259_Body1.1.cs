using Microsoft.EntityFrameworkCore.Migrations;

namespace BodyBuildingProject.Migrations
{
    public partial class Body11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "Tbl_UserTrainInfo",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comment",
                table: "Tbl_UserTrainInfo");
        }
    }
}
