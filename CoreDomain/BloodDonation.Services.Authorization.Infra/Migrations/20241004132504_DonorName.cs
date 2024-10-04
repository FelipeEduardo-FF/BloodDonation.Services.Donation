using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BloodDonation.Services.Donations.Infra.Migrations
{
    /// <inheritdoc />
    public partial class DonorName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DonorName",
                table: "donations",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DonorName",
                table: "donations");
        }
    }
}
