using Microsoft.EntityFrameworkCore.Migrations;

namespace GradebookBackend.Migrations
{
    public partial class migr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassesSubjects_Subjects_ClassId",
                table: "ClassesSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassesSubjects_Classes_ClassId1",
                table: "ClassesSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_TeachersSubjects_Subjects_TeacherId",
                table: "TeachersSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_TeachersSubjects_Teachers_TeacherId1",
                table: "TeachersSubjects");

            migrationBuilder.DropIndex(
                name: "IX_TeachersSubjects_TeacherId1",
                table: "TeachersSubjects");

            migrationBuilder.DropIndex(
                name: "IX_ClassesSubjects_ClassId1",
                table: "ClassesSubjects");

            migrationBuilder.DropColumn(
                name: "TeacherId1",
                table: "TeachersSubjects");

            migrationBuilder.DropColumn(
                name: "ClassId1",
                table: "ClassesSubjects");

            migrationBuilder.CreateIndex(
                name: "IX_TeachersSubjects_SubjectId",
                table: "TeachersSubjects",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassesSubjects_SubjectId",
                table: "ClassesSubjects",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassesSubjects_Classes_ClassId",
                table: "ClassesSubjects",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClassesSubjects_Subjects_SubjectId",
                table: "ClassesSubjects",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeachersSubjects_Subjects_SubjectId",
                table: "TeachersSubjects",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeachersSubjects_Teachers_TeacherId",
                table: "TeachersSubjects",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassesSubjects_Classes_ClassId",
                table: "ClassesSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassesSubjects_Subjects_SubjectId",
                table: "ClassesSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_TeachersSubjects_Subjects_SubjectId",
                table: "TeachersSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_TeachersSubjects_Teachers_TeacherId",
                table: "TeachersSubjects");

            migrationBuilder.DropIndex(
                name: "IX_TeachersSubjects_SubjectId",
                table: "TeachersSubjects");

            migrationBuilder.DropIndex(
                name: "IX_ClassesSubjects_SubjectId",
                table: "ClassesSubjects");

            migrationBuilder.AddColumn<int>(
                name: "TeacherId1",
                table: "TeachersSubjects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ClassId1",
                table: "ClassesSubjects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TeachersSubjects_TeacherId1",
                table: "TeachersSubjects",
                column: "TeacherId1");

            migrationBuilder.CreateIndex(
                name: "IX_ClassesSubjects_ClassId1",
                table: "ClassesSubjects",
                column: "ClassId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassesSubjects_Subjects_ClassId",
                table: "ClassesSubjects",
                column: "ClassId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClassesSubjects_Classes_ClassId1",
                table: "ClassesSubjects",
                column: "ClassId1",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeachersSubjects_Subjects_TeacherId",
                table: "TeachersSubjects",
                column: "TeacherId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeachersSubjects_Teachers_TeacherId1",
                table: "TeachersSubjects",
                column: "TeacherId1",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
