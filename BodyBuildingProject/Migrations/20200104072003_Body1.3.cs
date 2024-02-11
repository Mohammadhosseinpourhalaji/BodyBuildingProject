using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BodyBuildingProject.Migrations
{
    public partial class Body13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TrainTypeId",
                table: "Tbl_UserTrainInfo",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TrainType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TrainName = table.Column<string>(nullable: true),
                    Price = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_UserTrainInfo_TrainTypeId",
                table: "Tbl_UserTrainInfo",
                column: "TrainTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tbl_UserTrainInfo_TrainType_TrainTypeId",
                table: "Tbl_UserTrainInfo",
                column: "TrainTypeId",
                principalTable: "TrainType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tbl_UserTrainInfo_TrainType_TrainTypeId",
                table: "Tbl_UserTrainInfo");

            migrationBuilder.DropTable(
                name: "TrainType");

            migrationBuilder.DropIndex(
                name: "IX_Tbl_UserTrainInfo_TrainTypeId",
                table: "Tbl_UserTrainInfo");

            migrationBuilder.DropColumn(
                name: "TrainTypeId",
                table: "Tbl_UserTrainInfo");
        }
    }
}
