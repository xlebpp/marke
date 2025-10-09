using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace marketplaceE.Migrations
{
    /// <inheritdoc />
    public partial class ChangeProductImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "ProductImages");

            migrationBuilder.AddColumn<byte[]>(
                name: "Url",
                table: "ProductImages",
                type: "bytea",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "ProductImages",
                type: "text",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "bytea");
        }
    }
}
