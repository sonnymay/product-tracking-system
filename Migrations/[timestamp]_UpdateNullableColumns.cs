using Microsoft.EntityFrameworkCore.Migrations;

namespace MySimpleWebApp.Migrations
{
    public partial class UpdateNullableColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // First update NULL values with defaults
            migrationBuilder.Sql(@"
                UPDATE Products SET 
                    SerialNumber = 'N/A' WHERE SerialNumber IS NULL;
                UPDATE Products SET 
                    RequestedBy = 'Unknown' WHERE RequestedBy IS NULL;
                UPDATE Products SET 
                    RmaNumber = 'N/A' WHERE RmaNumber IS NULL;
                UPDATE Products SET 
                    RecordType = 'In' WHERE RecordType IS NULL;
                UPDATE Products SET 
                    Name = 'Untitled' WHERE Name IS NULL;
                UPDATE Products SET 
                    Category = 'Uncategorized' WHERE Category IS NULL;"
            );

            // Then make columns non-nullable
            migrationBuilder.AlterColumn<string>(
                name: "RecordType",
                table: "Products",
                type: "TEXT",
                nullable: false,
                defaultValue: "In");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Products",
                type: "TEXT",
                nullable: false,
                defaultValue: "Untitled");

            migrationBuilder.AlterColumn<string>(
                name: "Category",
                table: "Products",
                type: "TEXT",
                nullable: false,
                defaultValue: "Uncategorized");

            migrationBuilder.AlterColumn<string>(
                name: "SerialNumber",
                table: "Products",
                type: "TEXT",
                nullable: false,
                defaultValue: "N/A");

            migrationBuilder.AlterColumn<string>(
                name: "RequestedBy",
                table: "Products",
                type: "TEXT",
                nullable: false,
                defaultValue: "Unknown");

            migrationBuilder.AlterColumn<string>(
                name: "RmaNumber",
                table: "Products",
                type: "TEXT",
                nullable: false,
                defaultValue: "N/A");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Make columns nullable again if needed to roll back
            migrationBuilder.AlterColumn<string>(
                name: "RecordType",
                table: "Products",
                type: "TEXT",
                nullable: true);

            // ...similar for other columns...
        }
    }
}
