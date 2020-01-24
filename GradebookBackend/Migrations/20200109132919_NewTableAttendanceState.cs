using Microsoft.EntityFrameworkCore.Migrations;

namespace GradebookBackend.Migrations
{
    public partial class NewTableAttendanceState : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "Attendances");

            migrationBuilder.AddColumn<int>(
                name: "AttendanceStatusId",
                table: "Attendances",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AttendancesStatus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendancesStatus", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_AttendanceStatusId",
                table: "Attendances",
                column: "AttendanceStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_AttendancesStatus_AttendanceStatusId",
                table: "Attendances",
                column: "AttendanceStatusId",
                principalTable: "AttendancesStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_AttendancesStatus_AttendanceStatusId",
                table: "Attendances");

            migrationBuilder.DropTable(
                name: "AttendancesStatus");

            migrationBuilder.DropIndex(
                name: "IX_Attendances_AttendanceStatusId",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "AttendanceStatusId",
                table: "Attendances");

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Attendances",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
