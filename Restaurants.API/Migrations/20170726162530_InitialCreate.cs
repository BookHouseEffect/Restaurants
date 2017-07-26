using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Restaurants.API.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    CurrencyFullName = table.Column<string>(maxLength: 100, nullable: true),
                    CurrencySign = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeType",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    EmployeeTypeName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    LanguageName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderItemStatuses",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    StatusName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItemStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderStatuses",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    StatusName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedByUserId = table.Column<long>(nullable: false),
                    CreatedDateTime = table.Column<DateTime>(nullable: false),
                    ModifiedByUserId = table.Column<long>(nullable: false),
                    ModifiedDateTime = table.Column<DateTime>(nullable: false),
                    PersonFirstName = table.Column<string>(nullable: false),
                    PersonLastName = table.Column<string>(nullable: false),
                    PersonMiddleName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Id);
                    table.ForeignKey(
                        name: "FK_People_People_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_People_People_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Employers",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedByUserId = table.Column<long>(nullable: false),
                    CreatedDateTime = table.Column<DateTime>(nullable: false),
                    ModifiedByUserId = table.Column<long>(nullable: false),
                    ModifiedDateTime = table.Column<DateTime>(nullable: false),
                    PersonId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employers_People_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employers_People_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employers_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Guests",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedByUserId = table.Column<long>(nullable: false),
                    CreatedDateTime = table.Column<DateTime>(nullable: false),
                    ModifiedByUserId = table.Column<long>(nullable: false),
                    ModifiedDateTime = table.Column<DateTime>(nullable: false),
                    PersonId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Guests_People_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Guests_People_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Guests_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LocationPoints",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedByUserId = table.Column<long>(nullable: false),
                    CreatedDateTime = table.Column<DateTime>(nullable: false),
                    Latitude = table.Column<float>(nullable: false),
                    Longitude = table.Column<float>(nullable: false),
                    ModifiedByUserId = table.Column<long>(nullable: false),
                    ModifiedDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationPoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LocationPoints_People_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LocationPoints_People_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MenuCategories",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedByUserId = table.Column<long>(nullable: false),
                    CreatedDateTime = table.Column<DateTime>(nullable: false),
                    ModifiedByUserId = table.Column<long>(nullable: false),
                    ModifiedDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenuCategories_People_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MenuCategories_People_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RestaurantObjects",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedByUserId = table.Column<long>(nullable: false),
                    CreatedDateTime = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    ModifiedByUserId = table.Column<long>(nullable: false),
                    ModifiedDateTime = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestaurantObjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RestaurantObjects_People_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RestaurantObjects_People_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedByUserId = table.Column<long>(nullable: false),
                    CreatedDateTime = table.Column<DateTime>(nullable: false),
                    GuestsId = table.Column<long>(nullable: true),
                    ModifiedByUserId = table.Column<long>(nullable: false),
                    ModifiedDateTime = table.Column<DateTime>(nullable: false),
                    OrderStatusId = table.Column<long>(nullable: false),
                    Total = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_People_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Guests_GuestsId",
                        column: x => x.GuestsId,
                        principalTable: "Guests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_People_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_OrderStatuses_OrderStatusId",
                        column: x => x.OrderStatusId,
                        principalTable: "OrderStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedByUserId = table.Column<long>(nullable: false),
                    CreatedDateTime = table.Column<DateTime>(nullable: false),
                    ModifiedByUserId = table.Column<long>(nullable: false),
                    ModifiedDateTime = table.Column<DateTime>(nullable: false),
                    OwnerId = table.Column<long>(nullable: false),
                    PersonId = table.Column<long>(nullable: false),
                    RestaurantId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_People_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_People_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_Employers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Employers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_RestaurantObjects_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "RestaurantObjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployersRestaurants",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedByUserId = table.Column<long>(nullable: false),
                    CreatedDateTime = table.Column<DateTime>(nullable: false),
                    EmployerId = table.Column<long>(nullable: false),
                    ModifiedByUserId = table.Column<long>(nullable: false),
                    ModifiedDateTime = table.Column<DateTime>(nullable: false),
                    RestaurantId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployersRestaurants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployersRestaurants_People_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployersRestaurants_Employers_EmployerId",
                        column: x => x.EmployerId,
                        principalTable: "Employers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployersRestaurants_People_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployersRestaurants_RestaurantObjects_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "RestaurantObjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LocationContact",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AdministrativeAreaLevel1 = table.Column<string>(nullable: true),
                    AdministrativeAreaLevel2 = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: false),
                    CreatedByUserId = table.Column<long>(nullable: false),
                    CreatedDateTime = table.Column<DateTime>(nullable: false),
                    Floor = table.Column<int>(nullable: false),
                    GoogleLink = table.Column<string>(nullable: true),
                    Locality = table.Column<string>(nullable: false),
                    LocationPointId = table.Column<long>(nullable: false),
                    ModifiedByUserId = table.Column<long>(nullable: false),
                    ModifiedDateTime = table.Column<DateTime>(nullable: false),
                    RestaurantId = table.Column<long>(nullable: false),
                    Route = table.Column<string>(nullable: false),
                    StreetNumber = table.Column<string>(nullable: false),
                    ZipCode = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationContact", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LocationContact_People_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LocationContact_LocationPoints_LocationPointId",
                        column: x => x.LocationPointId,
                        principalTable: "LocationPoints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LocationContact_People_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LocationContact_RestaurantObjects_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "RestaurantObjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Menus",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedByUserId = table.Column<long>(nullable: false),
                    CreatedDateTime = table.Column<DateTime>(nullable: false),
                    ModifiedByUserId = table.Column<long>(nullable: false),
                    ModifiedDateTime = table.Column<DateTime>(nullable: false),
                    RestaurantId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Menus_People_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Menus_People_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Menus_RestaurantObjects_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "RestaurantObjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OpenHoursSchedule",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedByUserId = table.Column<long>(nullable: false),
                    CreatedDateTime = table.Column<DateTime>(nullable: false),
                    EndDay = table.Column<int>(nullable: false),
                    EndTime = table.Column<TimeSpan>(nullable: false),
                    ModifiedByUserId = table.Column<long>(nullable: false),
                    ModifiedDateTime = table.Column<DateTime>(nullable: false),
                    RestaurantId = table.Column<long>(nullable: false),
                    StartDay = table.Column<int>(nullable: false),
                    StartTime = table.Column<TimeSpan>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpenHoursSchedule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OpenHoursSchedule_People_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OpenHoursSchedule_People_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OpenHoursSchedule_RestaurantObjects_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "RestaurantObjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PhoneContacts",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedByUserId = table.Column<long>(nullable: false),
                    CreatedDateTime = table.Column<DateTime>(nullable: false),
                    ModifiedByUserId = table.Column<long>(nullable: false),
                    ModifiedDateTime = table.Column<DateTime>(nullable: false),
                    PhoneDescription = table.Column<string>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: false),
                    RestaurantId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneContacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhoneContacts_People_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PhoneContacts_People_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PhoneContacts_RestaurantObjects_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "RestaurantObjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AssignedEmployeeTypes",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedByUserId = table.Column<long>(nullable: false),
                    CreatedDateTime = table.Column<DateTime>(nullable: false),
                    EmployeeId = table.Column<long>(nullable: false),
                    ModifiedByUserId = table.Column<long>(nullable: false),
                    ModifiedDateTime = table.Column<DateTime>(nullable: false),
                    TypeId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignedEmployeeTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssignedEmployeeTypes_People_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssignedEmployeeTypes_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssignedEmployeeTypes_People_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssignedEmployeeTypes_EmployeeType_TypeId",
                        column: x => x.TypeId,
                        principalTable: "EmployeeType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MenuCurrencies",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedByUserId = table.Column<long>(nullable: false),
                    CreatedDateTime = table.Column<DateTime>(nullable: false),
                    CurrencyId = table.Column<long>(nullable: false),
                    MenuId = table.Column<long>(nullable: false),
                    ModifiedByUserId = table.Column<long>(nullable: false),
                    ModifiedDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuCurrencies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenuCurrencies_People_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MenuCurrencies_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MenuCurrencies_Menus_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MenuCurrencies_People_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MenuItems",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedByUserId = table.Column<long>(nullable: false),
                    CreatedDateTime = table.Column<DateTime>(nullable: false),
                    MenuCategoryId = table.Column<long>(nullable: false),
                    MenuId = table.Column<long>(nullable: false),
                    ModifiedByUserId = table.Column<long>(nullable: false),
                    ModifiedDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenuItems_People_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MenuItems_MenuCategories_MenuCategoryId",
                        column: x => x.MenuCategoryId,
                        principalTable: "MenuCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MenuItems_Menus_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MenuItems_People_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MenuLanguages",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedByUserId = table.Column<long>(nullable: false),
                    CreatedDateTime = table.Column<DateTime>(nullable: false),
                    LanguageId = table.Column<long>(nullable: false),
                    MenuId = table.Column<long>(nullable: false),
                    ModifiedByUserId = table.Column<long>(nullable: false),
                    ModifiedDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuLanguages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenuLanguages_People_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MenuLanguages_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MenuLanguages_Menus_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MenuLanguages_People_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OutOfSchedulePeriods",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedByUserId = table.Column<long>(nullable: false),
                    CreatedDateTime = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    ModifiedByUserId = table.Column<long>(nullable: false),
                    ModifiedDateTime = table.Column<DateTime>(nullable: false),
                    OpenHoursScheduleId = table.Column<long>(nullable: false),
                    OutOfSchedulePeriodEnds = table.Column<DateTime>(nullable: false),
                    OutOfSchedulePeriodStarts = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutOfSchedulePeriods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OutOfSchedulePeriods_People_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OutOfSchedulePeriods_People_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OutOfSchedulePeriods_OpenHoursSchedule_OpenHoursScheduleId",
                        column: x => x.OpenHoursScheduleId,
                        principalTable: "OpenHoursSchedule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MenuItemValues",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedByUserId = table.Column<long>(nullable: false),
                    CreatedDateTime = table.Column<DateTime>(nullable: false),
                    MenuCurrencyId = table.Column<long>(nullable: false),
                    MenuItemId = table.Column<long>(nullable: false),
                    ModifiedByUserId = table.Column<long>(nullable: false),
                    ModifiedDateTime = table.Column<DateTime>(nullable: false),
                    Price = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItemValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenuItemValues_People_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MenuItemValues_MenuCurrencies_MenuCurrencyId",
                        column: x => x.MenuCurrencyId,
                        principalTable: "MenuCurrencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MenuItemValues_MenuItems_MenuItemId",
                        column: x => x.MenuItemId,
                        principalTable: "MenuItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MenuItemValues_People_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Count = table.Column<int>(nullable: false),
                    CreatedByUserId = table.Column<long>(nullable: false),
                    CreatedDateTime = table.Column<DateTime>(nullable: false),
                    ModifiedByUserId = table.Column<long>(nullable: false),
                    ModifiedDateTime = table.Column<DateTime>(nullable: false),
                    OrderId = table.Column<long>(nullable: false),
                    OrderItemStatusId = table.Column<long>(nullable: false),
                    OrderedItemId = table.Column<long>(nullable: false),
                    SubTotal = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_People_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderItems_People_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderItems_OrderItemStatuses_OrderItemStatusId",
                        column: x => x.OrderItemStatusId,
                        principalTable: "OrderItemStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderItems_MenuItems_OrderedItemId",
                        column: x => x.OrderedItemId,
                        principalTable: "MenuItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CategoryDescription = table.Column<string>(nullable: true),
                    CategoryName = table.Column<string>(nullable: false),
                    CreatedByUserId = table.Column<long>(nullable: false),
                    CreatedDateTime = table.Column<DateTime>(nullable: false),
                    MenuCategoryId = table.Column<long>(nullable: false),
                    MenuLanguageId = table.Column<long>(nullable: false),
                    ModifiedByUserId = table.Column<long>(nullable: false),
                    ModifiedDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_People_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Categories_MenuCategories_MenuCategoryId",
                        column: x => x.MenuCategoryId,
                        principalTable: "MenuCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Categories_MenuLanguages_MenuLanguageId",
                        column: x => x.MenuLanguageId,
                        principalTable: "MenuLanguages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Categories_People_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MenuItemContents",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedByUserId = table.Column<long>(nullable: false),
                    CreatedDateTime = table.Column<DateTime>(nullable: false),
                    ItemDescription = table.Column<string>(nullable: false),
                    ItemName = table.Column<string>(nullable: false),
                    ItemWarnings = table.Column<string>(nullable: true),
                    MenuItemId = table.Column<long>(nullable: false),
                    MenuLanguageId = table.Column<long>(nullable: false),
                    ModifiedByUserId = table.Column<long>(nullable: false),
                    ModifiedDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItemContents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenuItemContents_People_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MenuItemContents_MenuItems_MenuItemId",
                        column: x => x.MenuItemId,
                        principalTable: "MenuItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MenuItemContents_MenuLanguages_MenuLanguageId",
                        column: x => x.MenuLanguageId,
                        principalTable: "MenuLanguages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MenuItemContents_People_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssignedEmployeeTypes_CreatedByUserId",
                table: "AssignedEmployeeTypes",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignedEmployeeTypes_ModifiedByUserId",
                table: "AssignedEmployeeTypes",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignedEmployeeTypes_TypeId",
                table: "AssignedEmployeeTypes",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignedEmployeeTypes_EmployeeId_TypeId",
                table: "AssignedEmployeeTypes",
                columns: new[] { "EmployeeId", "TypeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CreatedByUserId",
                table: "Categories",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_MenuCategoryId",
                table: "Categories",
                column: "MenuCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_MenuLanguageId",
                table: "Categories",
                column: "MenuLanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ModifiedByUserId",
                table: "Categories",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_CreatedByUserId",
                table: "Employees",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ModifiedByUserId",
                table: "Employees",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_OwnerId",
                table: "Employees",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_PersonId",
                table: "Employees",
                column: "PersonId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_RestaurantId",
                table: "Employees",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_Employers_CreatedByUserId",
                table: "Employers",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Employers_ModifiedByUserId",
                table: "Employers",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Employers_PersonId",
                table: "Employers",
                column: "PersonId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployersRestaurants_CreatedByUserId",
                table: "EmployersRestaurants",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployersRestaurants_ModifiedByUserId",
                table: "EmployersRestaurants",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployersRestaurants_RestaurantId",
                table: "EmployersRestaurants",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployersRestaurants_EmployerId_RestaurantId",
                table: "EmployersRestaurants",
                columns: new[] { "EmployerId", "RestaurantId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Guests_CreatedByUserId",
                table: "Guests",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Guests_ModifiedByUserId",
                table: "Guests",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Guests_PersonId",
                table: "Guests",
                column: "PersonId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LocationContact_CreatedByUserId",
                table: "LocationContact",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationContact_LocationPointId",
                table: "LocationContact",
                column: "LocationPointId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationContact_ModifiedByUserId",
                table: "LocationContact",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationContact_RestaurantId",
                table: "LocationContact",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationPoints_CreatedByUserId",
                table: "LocationPoints",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationPoints_ModifiedByUserId",
                table: "LocationPoints",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuCategories_CreatedByUserId",
                table: "MenuCategories",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuCategories_ModifiedByUserId",
                table: "MenuCategories",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuCurrencies_CreatedByUserId",
                table: "MenuCurrencies",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuCurrencies_CurrencyId",
                table: "MenuCurrencies",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuCurrencies_ModifiedByUserId",
                table: "MenuCurrencies",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuCurrencies_MenuId_CurrencyId",
                table: "MenuCurrencies",
                columns: new[] { "MenuId", "CurrencyId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MenuItemContents_CreatedByUserId",
                table: "MenuItemContents",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItemContents_MenuItemId",
                table: "MenuItemContents",
                column: "MenuItemId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItemContents_MenuLanguageId",
                table: "MenuItemContents",
                column: "MenuLanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItemContents_ModifiedByUserId",
                table: "MenuItemContents",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_CreatedByUserId",
                table: "MenuItems",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_MenuCategoryId",
                table: "MenuItems",
                column: "MenuCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_MenuId",
                table: "MenuItems",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_ModifiedByUserId",
                table: "MenuItems",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItemValues_CreatedByUserId",
                table: "MenuItemValues",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItemValues_MenuCurrencyId",
                table: "MenuItemValues",
                column: "MenuCurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItemValues_MenuItemId",
                table: "MenuItemValues",
                column: "MenuItemId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItemValues_ModifiedByUserId",
                table: "MenuItemValues",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuLanguages_CreatedByUserId",
                table: "MenuLanguages",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuLanguages_LanguageId",
                table: "MenuLanguages",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuLanguages_ModifiedByUserId",
                table: "MenuLanguages",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuLanguages_MenuId_LanguageId",
                table: "MenuLanguages",
                columns: new[] { "MenuId", "LanguageId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Menus_CreatedByUserId",
                table: "Menus",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Menus_ModifiedByUserId",
                table: "Menus",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Menus_RestaurantId",
                table: "Menus",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_OpenHoursSchedule_CreatedByUserId",
                table: "OpenHoursSchedule",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_OpenHoursSchedule_ModifiedByUserId",
                table: "OpenHoursSchedule",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_OpenHoursSchedule_RestaurantId",
                table: "OpenHoursSchedule",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_CreatedByUserId",
                table: "OrderItems",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ModifiedByUserId",
                table: "OrderItems",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderItemStatusId",
                table: "OrderItems",
                column: "OrderItemStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderedItemId",
                table: "OrderItems",
                column: "OrderedItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CreatedByUserId",
                table: "Orders",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_GuestsId",
                table: "Orders",
                column: "GuestsId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ModifiedByUserId",
                table: "Orders",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderStatusId",
                table: "Orders",
                column: "OrderStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_OutOfSchedulePeriods_CreatedByUserId",
                table: "OutOfSchedulePeriods",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_OutOfSchedulePeriods_ModifiedByUserId",
                table: "OutOfSchedulePeriods",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_OutOfSchedulePeriods_OpenHoursScheduleId",
                table: "OutOfSchedulePeriods",
                column: "OpenHoursScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_People_CreatedByUserId",
                table: "People",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_People_ModifiedByUserId",
                table: "People",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PhoneContacts_CreatedByUserId",
                table: "PhoneContacts",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PhoneContacts_ModifiedByUserId",
                table: "PhoneContacts",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PhoneContacts_RestaurantId",
                table: "PhoneContacts",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_RestaurantObjects_CreatedByUserId",
                table: "RestaurantObjects",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RestaurantObjects_ModifiedByUserId",
                table: "RestaurantObjects",
                column: "ModifiedByUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssignedEmployeeTypes");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "EmployersRestaurants");

            migrationBuilder.DropTable(
                name: "LocationContact");

            migrationBuilder.DropTable(
                name: "MenuItemContents");

            migrationBuilder.DropTable(
                name: "MenuItemValues");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "OutOfSchedulePeriods");

            migrationBuilder.DropTable(
                name: "PhoneContacts");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "EmployeeType");

            migrationBuilder.DropTable(
                name: "LocationPoints");

            migrationBuilder.DropTable(
                name: "MenuLanguages");

            migrationBuilder.DropTable(
                name: "MenuCurrencies");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "OrderItemStatuses");

            migrationBuilder.DropTable(
                name: "MenuItems");

            migrationBuilder.DropTable(
                name: "OpenHoursSchedule");

            migrationBuilder.DropTable(
                name: "Employers");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropTable(
                name: "Guests");

            migrationBuilder.DropTable(
                name: "OrderStatuses");

            migrationBuilder.DropTable(
                name: "MenuCategories");

            migrationBuilder.DropTable(
                name: "Menus");

            migrationBuilder.DropTable(
                name: "RestaurantObjects");

            migrationBuilder.DropTable(
                name: "People");
        }
    }
}
