using Microsoft.EntityFrameworkCore.Migrations;

namespace sziLMS.Migrations
{
    public partial class SubNoAction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_Enrollments_EnrollmentId",
                table: "Submissions");

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_Enrollments_EnrollmentId",
                table: "Submissions",
                column: "EnrollmentId",
                principalTable: "Enrollments",
                principalColumn: "EnrollmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_Enrollments_EnrollmentId",
                table: "Submissions");

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_Enrollments_EnrollmentId",
                table: "Submissions",
                column: "EnrollmentId",
                principalTable: "Enrollments",
                principalColumn: "EnrollmentId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
