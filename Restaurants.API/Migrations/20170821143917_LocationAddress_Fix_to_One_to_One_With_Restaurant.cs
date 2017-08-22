using Microsoft.EntityFrameworkCore.Migrations;

namespace Restaurants.API.Migrations
{
	public partial class LocationAddress_Fix_to_One_to_One_With_Restaurant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LocationContact_RestaurantId",
                table: "LocationContact");

            migrationBuilder.CreateIndex(
                name: "IX_LocationContact_RestaurantId",
                table: "LocationContact",
                column: "RestaurantId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LocationContact_RestaurantId",
                table: "LocationContact");

            migrationBuilder.CreateIndex(
                name: "IX_LocationContact_RestaurantId",
                table: "LocationContact",
                column: "RestaurantId");
        }
    }
}
