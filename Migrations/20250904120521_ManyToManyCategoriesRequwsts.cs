using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace marketplaceE.Migrations
{
    /// <inheritdoc />
    public partial class ManyToManyCategoriesRequwsts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Requests_RequestId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_RequestId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "RequestId",
                table: "Categories");

            migrationBuilder.CreateTable(
                name: "CategoryRequest",
                columns: table => new
                {
                    CategoriesId = table.Column<int>(type: "integer", nullable: false),
                    RequestsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryRequest", x => new { x.CategoriesId, x.RequestsId });
                    table.ForeignKey(
                        name: "FK_CategoryRequest_Categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryRequest_Requests_RequestsId",
                        column: x => x.RequestsId,
                        principalTable: "Requests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryRequest_RequestsId",
                table: "CategoryRequest",
                column: "RequestsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryRequest");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Requests",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RequestId",
                table: "Categories",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_RequestId",
                table: "Categories",
                column: "RequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Requests_RequestId",
                table: "Categories",
                column: "RequestId",
                principalTable: "Requests",
                principalColumn: "Id");
        }
    }
}
