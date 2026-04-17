using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Layer.Migrations
{
    /// <inheritdoc />
    public partial class AddSummaryToToy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Summary",
                table: "Toys",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Summary",
                table: "Toys");
        }
    }
}
