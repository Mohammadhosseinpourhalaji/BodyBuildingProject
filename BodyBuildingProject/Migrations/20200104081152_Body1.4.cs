using Microsoft.EntityFrameworkCore.Migrations;

namespace BodyBuildingProject.Migrations
{
    public partial class Body14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tbl_UserTrainInfo_TrainType_TrainTypeId",
                table: "Tbl_UserTrainInfo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TrainType",
                table: "TrainType");

            migrationBuilder.RenameTable(
                name: "TrainType",
                newName: "Tbl_TrainType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tbl_TrainType",
                table: "Tbl_TrainType",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tbl_UserTrainInfo_Tbl_TrainType_TrainTypeId",
                table: "Tbl_UserTrainInfo",
                column: "TrainTypeId",
                principalTable: "Tbl_TrainType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tbl_UserTrainInfo_Tbl_TrainType_TrainTypeId",
                table: "Tbl_UserTrainInfo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tbl_TrainType",
                table: "Tbl_TrainType");

            migrationBuilder.RenameTable(
                name: "Tbl_TrainType",
                newName: "TrainType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrainType",
                table: "TrainType",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tbl_UserTrainInfo_TrainType_TrainTypeId",
                table: "Tbl_UserTrainInfo",
                column: "TrainTypeId",
                principalTable: "TrainType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
