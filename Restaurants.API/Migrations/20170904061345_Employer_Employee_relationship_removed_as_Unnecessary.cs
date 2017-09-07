using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Restaurants.API.Migrations
{
    public partial class Employer_Employee_relationship_removed_as_Unnecessary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Employers_OwnerId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_OwnerId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Employees");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "OwnerId",
                table: "Employees",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_OwnerId",
                table: "Employees",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Employers_OwnerId",
                table: "Employees",
                column: "OwnerId",
                principalTable: "Employers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
