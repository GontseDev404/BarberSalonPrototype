using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BarberSalonPrototype.Migrations
{
    /// <inheritdoc />
    public partial class FixGalleryImageStaffMemberFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GalleryImages_StaffMembers_StaffMemberId1",
                table: "GalleryImages");

            migrationBuilder.DropIndex(
                name: "IX_GalleryImages_StaffMemberId1",
                table: "GalleryImages");

            migrationBuilder.DropColumn(
                name: "StaffMemberId1",
                table: "GalleryImages");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StaffMemberId1",
                table: "GalleryImages",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GalleryImages_StaffMemberId1",
                table: "GalleryImages",
                column: "StaffMemberId1");

            migrationBuilder.AddForeignKey(
                name: "FK_GalleryImages_StaffMembers_StaffMemberId1",
                table: "GalleryImages",
                column: "StaffMemberId1",
                principalTable: "StaffMembers",
                principalColumn: "Id");
        }
    }
}
