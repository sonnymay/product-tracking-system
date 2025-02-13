using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MySimpleWebApp.Migrations
{
    /// <inheritdoc />
    public partial class AddRecordType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RecordType",
                table: "Products",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecordType",
                table: "Products");
        }
    }
}
