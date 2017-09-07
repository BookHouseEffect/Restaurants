using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Restaurants.API.Migrations
{
    public partial class Employee_Restaurant_relationship_set_to_Nullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "RestaurantId",
                table: "Employees",
                nullable: true,
                oldClrType: typeof(long));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "RestaurantId",
                table: "Employees",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);
        }
    }
}
