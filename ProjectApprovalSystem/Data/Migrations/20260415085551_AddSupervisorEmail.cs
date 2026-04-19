using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectApprovalSystem.Data.Migrations
{
    public partial class AddSupervisorEmail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SupervisorEmail",
                table: "Proposals",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SupervisorEmail",
                table: "Proposals");
        }
    }
}
