using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Limon.Hive.E.Bazar.Infrastractures.Migrations
{
    /// <inheritdoc />
    public partial class AddNewPropertyInProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "Product",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Slug",
                table: "Product");
        }
    }
}
