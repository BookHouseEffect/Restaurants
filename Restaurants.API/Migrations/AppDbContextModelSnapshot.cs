using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Restaurants.API.Models.Context;

namespace Restaurants.API.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Restaurants.API.Models.EntityFramework.AssignedEmployeeTypes", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("CreatedByUserId");

                    b.Property<DateTimeOffset>("CreatedDateTime");

                    b.Property<long>("EmployeeId");

                    b.Property<long?>("ModifiedByUserId");

                    b.Property<DateTimeOffset>("ModifiedDateTime");

                    b.Property<long>("TypeId");

                    b.HasKey("Id");

                    b.HasIndex("CreatedByUserId");

                    b.HasIndex("ModifiedByUserId");

                    b.HasIndex("TypeId");

                    b.HasIndex("EmployeeId", "TypeId")
                        .IsUnique();

                    b.ToTable("AssignedEmployeeTypes");
                });

            modelBuilder.Entity("Restaurants.API.Models.EntityFramework.Categories", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CategoryDescription");

                    b.Property<string>("CategoryName")
                        .IsRequired();

                    b.Property<long?>("CreatedByUserId");

                    b.Property<DateTimeOffset>("CreatedDateTime");

                    b.Property<long>("MenuCategoryId");

                    b.Property<long>("MenuLanguageId");

                    b.Property<long?>("ModifiedByUserId");

                    b.Property<DateTimeOffset>("ModifiedDateTime");

                    b.HasKey("Id");

                    b.HasIndex("CreatedByUserId");

                    b.HasIndex("MenuCategoryId");

                    b.HasIndex("MenuLanguageId");

                    b.HasIndex("ModifiedByUserId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Restaurants.API.Models.EntityFramework.Currencies", b =>
                {
                    b.Property<long>("Id");

                    b.Property<string>("CurrencyFullName")
                        .HasMaxLength(100);

                    b.Property<string>("CurrencySign")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Currencies");
                });

            modelBuilder.Entity("Restaurants.API.Models.EntityFramework.Employees", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("CreatedByUserId");

                    b.Property<DateTimeOffset>("CreatedDateTime");

                    b.Property<long?>("ModifiedByUserId");

                    b.Property<DateTimeOffset>("ModifiedDateTime");

                    b.Property<long>("OwnerId");

                    b.Property<long>("PersonId");

                    b.Property<long>("RestaurantId");

                    b.HasKey("Id");

                    b.HasIndex("CreatedByUserId");

                    b.HasIndex("ModifiedByUserId");

                    b.HasIndex("OwnerId");

                    b.HasIndex("PersonId")
                        .IsUnique();

                    b.HasIndex("RestaurantId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Restaurants.API.Models.EntityFramework.EmployeeTypes", b =>
                {
                    b.Property<long>("Id");

                    b.Property<string>("EmployeeTypeName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("EmployeeType");
                });

            modelBuilder.Entity("Restaurants.API.Models.EntityFramework.Employers", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("CreatedByUserId");

                    b.Property<DateTimeOffset>("CreatedDateTime");

                    b.Property<long?>("ModifiedByUserId");

                    b.Property<DateTimeOffset>("ModifiedDateTime");

                    b.Property<long>("PersonId");

                    b.HasKey("Id");

                    b.HasIndex("CreatedByUserId");

                    b.HasIndex("ModifiedByUserId");

                    b.HasIndex("PersonId")
                        .IsUnique();

                    b.ToTable("Employers");
                });

            modelBuilder.Entity("Restaurants.API.Models.EntityFramework.EmployersRestaurants", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("CreatedByUserId");

                    b.Property<DateTimeOffset>("CreatedDateTime");

                    b.Property<long>("EmployerId");

                    b.Property<long?>("ModifiedByUserId");

                    b.Property<DateTimeOffset>("ModifiedDateTime");

                    b.Property<long>("RestaurantId");

                    b.HasKey("Id");

                    b.HasIndex("CreatedByUserId");

                    b.HasIndex("ModifiedByUserId");

                    b.HasIndex("RestaurantId");

                    b.HasIndex("EmployerId", "RestaurantId")
                        .IsUnique();

                    b.ToTable("EmployersRestaurants");
                });

            modelBuilder.Entity("Restaurants.API.Models.EntityFramework.Guests", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("CreatedByUserId");

                    b.Property<DateTimeOffset>("CreatedDateTime");

                    b.Property<long?>("ModifiedByUserId");

                    b.Property<DateTimeOffset>("ModifiedDateTime");

                    b.Property<long>("PersonId");

                    b.HasKey("Id");

                    b.HasIndex("CreatedByUserId");

                    b.HasIndex("ModifiedByUserId");

                    b.HasIndex("PersonId")
                        .IsUnique();

                    b.ToTable("Guests");
                });

            modelBuilder.Entity("Restaurants.API.Models.EntityFramework.Languages", b =>
                {
                    b.Property<long>("Id");

                    b.Property<string>("LanguageName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("Restaurants.API.Models.EntityFramework.LocationContact", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AdministrativeAreaLevel1");

                    b.Property<string>("AdministrativeAreaLevel2");

                    b.Property<string>("Country")
                        .IsRequired();

                    b.Property<long?>("CreatedByUserId");

                    b.Property<DateTimeOffset>("CreatedDateTime");

                    b.Property<int>("Floor");

                    b.Property<string>("GoogleLink");

                    b.Property<string>("Locality")
                        .IsRequired();

                    b.Property<long>("LocationPointId");

                    b.Property<long?>("ModifiedByUserId");

                    b.Property<DateTimeOffset>("ModifiedDateTime");

                    b.Property<long>("RestaurantId");

                    b.Property<string>("Route")
                        .IsRequired();

                    b.Property<string>("StreetNumber")
                        .IsRequired();

                    b.Property<int>("ZipCode");

                    b.HasKey("Id");

                    b.HasIndex("CreatedByUserId");

                    b.HasIndex("LocationPointId");

                    b.HasIndex("ModifiedByUserId");

                    b.HasIndex("RestaurantId");

                    b.ToTable("LocationContact");
                });

            modelBuilder.Entity("Restaurants.API.Models.EntityFramework.LocationPoints", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("CreatedByUserId");

                    b.Property<DateTimeOffset>("CreatedDateTime");

                    b.Property<float>("Latitude");

                    b.Property<float>("Longitude");

                    b.Property<long?>("ModifiedByUserId");

                    b.Property<DateTimeOffset>("ModifiedDateTime");

                    b.HasKey("Id");

                    b.HasIndex("CreatedByUserId");

                    b.HasIndex("ModifiedByUserId");

                    b.ToTable("LocationPoints");
                });

            modelBuilder.Entity("Restaurants.API.Models.EntityFramework.MenuCategories", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("CreatedByUserId");

                    b.Property<DateTimeOffset>("CreatedDateTime");

                    b.Property<long?>("ModifiedByUserId");

                    b.Property<DateTimeOffset>("ModifiedDateTime");

                    b.HasKey("Id");

                    b.HasIndex("CreatedByUserId");

                    b.HasIndex("ModifiedByUserId");

                    b.ToTable("MenuCategories");
                });

            modelBuilder.Entity("Restaurants.API.Models.EntityFramework.MenuCurrencies", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("CreatedByUserId");

                    b.Property<DateTimeOffset>("CreatedDateTime");

                    b.Property<long>("CurrencyId");

                    b.Property<long>("MenuId");

                    b.Property<long?>("ModifiedByUserId");

                    b.Property<DateTimeOffset>("ModifiedDateTime");

                    b.HasKey("Id");

                    b.HasIndex("CreatedByUserId");

                    b.HasIndex("CurrencyId");

                    b.HasIndex("ModifiedByUserId");

                    b.HasIndex("MenuId", "CurrencyId")
                        .IsUnique();

                    b.ToTable("MenuCurrencies");
                });

            modelBuilder.Entity("Restaurants.API.Models.EntityFramework.MenuItemContents", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("CreatedByUserId");

                    b.Property<DateTimeOffset>("CreatedDateTime");

                    b.Property<string>("ItemDescription")
                        .IsRequired();

                    b.Property<string>("ItemName")
                        .IsRequired();

                    b.Property<string>("ItemWarnings");

                    b.Property<long>("MenuItemId");

                    b.Property<long>("MenuLanguageId");

                    b.Property<long?>("ModifiedByUserId");

                    b.Property<DateTimeOffset>("ModifiedDateTime");

                    b.HasKey("Id");

                    b.HasIndex("CreatedByUserId");

                    b.HasIndex("MenuItemId");

                    b.HasIndex("MenuLanguageId");

                    b.HasIndex("ModifiedByUserId");

                    b.ToTable("MenuItemContents");
                });

            modelBuilder.Entity("Restaurants.API.Models.EntityFramework.MenuItems", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("CreatedByUserId");

                    b.Property<DateTimeOffset>("CreatedDateTime");

                    b.Property<long>("MenuCategoryId");

                    b.Property<long>("MenuId");

                    b.Property<long?>("ModifiedByUserId");

                    b.Property<DateTimeOffset>("ModifiedDateTime");

                    b.HasKey("Id");

                    b.HasIndex("CreatedByUserId");

                    b.HasIndex("MenuCategoryId");

                    b.HasIndex("MenuId");

                    b.HasIndex("ModifiedByUserId");

                    b.ToTable("MenuItems");
                });

            modelBuilder.Entity("Restaurants.API.Models.EntityFramework.MenuItemValues", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("CreatedByUserId");

                    b.Property<DateTimeOffset>("CreatedDateTime");

                    b.Property<long>("MenuCurrencyId");

                    b.Property<long>("MenuItemId");

                    b.Property<long?>("ModifiedByUserId");

                    b.Property<DateTimeOffset>("ModifiedDateTime");

                    b.Property<float>("Price");

                    b.HasKey("Id");

                    b.HasIndex("CreatedByUserId");

                    b.HasIndex("MenuCurrencyId");

                    b.HasIndex("MenuItemId");

                    b.HasIndex("ModifiedByUserId");

                    b.ToTable("MenuItemValues");
                });

            modelBuilder.Entity("Restaurants.API.Models.EntityFramework.MenuLanguages", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("CreatedByUserId");

                    b.Property<DateTimeOffset>("CreatedDateTime");

                    b.Property<long>("LanguageId");

                    b.Property<long>("MenuId");

                    b.Property<long?>("ModifiedByUserId");

                    b.Property<DateTimeOffset>("ModifiedDateTime");

                    b.HasKey("Id");

                    b.HasIndex("CreatedByUserId");

                    b.HasIndex("LanguageId");

                    b.HasIndex("ModifiedByUserId");

                    b.HasIndex("MenuId", "LanguageId")
                        .IsUnique();

                    b.ToTable("MenuLanguages");
                });

            modelBuilder.Entity("Restaurants.API.Models.EntityFramework.Menus", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("CreatedByUserId");

                    b.Property<DateTimeOffset>("CreatedDateTime");

                    b.Property<long?>("ModifiedByUserId");

                    b.Property<DateTimeOffset>("ModifiedDateTime");

                    b.Property<long>("RestaurantId");

                    b.HasKey("Id");

                    b.HasIndex("CreatedByUserId");

                    b.HasIndex("ModifiedByUserId");

                    b.HasIndex("RestaurantId");

                    b.ToTable("Menus");
                });

            modelBuilder.Entity("Restaurants.API.Models.EntityFramework.OpenHoursSchedule", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("CreatedByUserId");

                    b.Property<DateTimeOffset>("CreatedDateTime");

                    b.Property<int>("EndDay");

                    b.Property<TimeSpan>("EndTime");

                    b.Property<long?>("ModifiedByUserId");

                    b.Property<DateTimeOffset>("ModifiedDateTime");

                    b.Property<long>("RestaurantId");

                    b.Property<int>("StartDay");

                    b.Property<TimeSpan>("StartTime");

                    b.HasKey("Id");

                    b.HasIndex("CreatedByUserId");

                    b.HasIndex("ModifiedByUserId");

                    b.HasIndex("RestaurantId");

                    b.ToTable("OpenHoursSchedule");
                });

            modelBuilder.Entity("Restaurants.API.Models.EntityFramework.OrderItems", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Count");

                    b.Property<long?>("CreatedByUserId");

                    b.Property<DateTimeOffset>("CreatedDateTime");

                    b.Property<long?>("ModifiedByUserId");

                    b.Property<DateTimeOffset>("ModifiedDateTime");

                    b.Property<long>("OrderId");

                    b.Property<long>("OrderItemStatusId");

                    b.Property<long>("OrderedItemId");

                    b.Property<float>("SubTotal");

                    b.HasKey("Id");

                    b.HasIndex("CreatedByUserId");

                    b.HasIndex("ModifiedByUserId");

                    b.HasIndex("OrderId");

                    b.HasIndex("OrderItemStatusId");

                    b.HasIndex("OrderedItemId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("Restaurants.API.Models.EntityFramework.OrderItemStatuses", b =>
                {
                    b.Property<long>("Id");

                    b.Property<string>("StatusName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("OrderItemStatuses");
                });

            modelBuilder.Entity("Restaurants.API.Models.EntityFramework.Orders", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("CreatedByUserId");

                    b.Property<DateTimeOffset>("CreatedDateTime");

                    b.Property<long?>("GuestsId");

                    b.Property<long?>("ModifiedByUserId");

                    b.Property<DateTimeOffset>("ModifiedDateTime");

                    b.Property<long>("OrderStatusId");

                    b.Property<float>("Total");

                    b.HasKey("Id");

                    b.HasIndex("CreatedByUserId");

                    b.HasIndex("GuestsId");

                    b.HasIndex("ModifiedByUserId");

                    b.HasIndex("OrderStatusId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Restaurants.API.Models.EntityFramework.OrderStatuses", b =>
                {
                    b.Property<long>("Id");

                    b.Property<string>("StatusName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("OrderStatuses");
                });

            modelBuilder.Entity("Restaurants.API.Models.EntityFramework.OutOfSchedulePeriods", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("CreatedByUserId");

                    b.Property<DateTimeOffset>("CreatedDateTime");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<long?>("ModifiedByUserId");

                    b.Property<DateTimeOffset>("ModifiedDateTime");

                    b.Property<long>("OpenHoursScheduleId");

                    b.Property<DateTimeOffset>("OutOfSchedulePeriodEnds");

                    b.Property<DateTimeOffset>("OutOfSchedulePeriodStarts");

                    b.HasKey("Id");

                    b.HasIndex("CreatedByUserId");

                    b.HasIndex("ModifiedByUserId");

                    b.HasIndex("OpenHoursScheduleId");

                    b.ToTable("OutOfSchedulePeriods");
                });

            modelBuilder.Entity("Restaurants.API.Models.EntityFramework.People", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("CreatedByUserId");

                    b.Property<DateTimeOffset>("CreatedDateTime");

                    b.Property<long?>("ModifiedByUserId");

                    b.Property<DateTimeOffset>("ModifiedDateTime");

                    b.Property<string>("PersonFirstName")
                        .IsRequired();

                    b.Property<string>("PersonLastName")
                        .IsRequired();

                    b.Property<string>("PersonMiddleName");

                    b.HasKey("Id");

                    b.HasIndex("CreatedByUserId");

                    b.HasIndex("ModifiedByUserId");

                    b.ToTable("People");
                });

            modelBuilder.Entity("Restaurants.API.Models.EntityFramework.PhoneContacts", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("CreatedByUserId");

                    b.Property<DateTimeOffset>("CreatedDateTime");

                    b.Property<long?>("ModifiedByUserId");

                    b.Property<DateTimeOffset>("ModifiedDateTime");

                    b.Property<string>("PhoneDescription")
                        .IsRequired();

                    b.Property<string>("PhoneNumber")
                        .IsRequired();

                    b.Property<long>("RestaurantId");

                    b.HasKey("Id");

                    b.HasIndex("CreatedByUserId");

                    b.HasIndex("ModifiedByUserId");

                    b.HasIndex("RestaurantId");

                    b.ToTable("PhoneContacts");
                });

            modelBuilder.Entity("Restaurants.API.Models.EntityFramework.RestaurantObjects", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("CreatedByUserId");

                    b.Property<DateTimeOffset>("CreatedDateTime");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<long?>("ModifiedByUserId");

                    b.Property<DateTimeOffset>("ModifiedDateTime");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("CreatedByUserId");

                    b.HasIndex("ModifiedByUserId");

                    b.ToTable("RestaurantObjects");
                });

            modelBuilder.Entity("Restaurants.API.Models.EntityFramework.AssignedEmployeeTypes", b =>
                {
                    b.HasOne("Restaurants.API.Models.EntityFramework.People", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedByUserId");

                    b.HasOne("Restaurants.API.Models.EntityFramework.Employees", "TheEmployee")
                        .WithMany("TheAssignedTypes")
                        .HasForeignKey("EmployeeId");

                    b.HasOne("Restaurants.API.Models.EntityFramework.People", "ModifiedBy")
                        .WithMany()
                        .HasForeignKey("ModifiedByUserId");

                    b.HasOne("Restaurants.API.Models.EntityFramework.EmployeeTypes", "TheType")
                        .WithMany("TheAssignedEmployees")
                        .HasForeignKey("TypeId");
                });

            modelBuilder.Entity("Restaurants.API.Models.EntityFramework.Categories", b =>
                {
                    b.HasOne("Restaurants.API.Models.EntityFramework.People", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedByUserId");

                    b.HasOne("Restaurants.API.Models.EntityFramework.MenuCategories", "TheMenuCategory")
                        .WithMany("TheCategories")
                        .HasForeignKey("MenuCategoryId");

                    b.HasOne("Restaurants.API.Models.EntityFramework.MenuLanguages", "TheMenuLanguage")
                        .WithMany("TheMenuLanguageCategories")
                        .HasForeignKey("MenuLanguageId");

                    b.HasOne("Restaurants.API.Models.EntityFramework.People", "ModifiedBy")
                        .WithMany()
                        .HasForeignKey("ModifiedByUserId");
                });

            modelBuilder.Entity("Restaurants.API.Models.EntityFramework.Employees", b =>
                {
                    b.HasOne("Restaurants.API.Models.EntityFramework.People", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedByUserId");

                    b.HasOne("Restaurants.API.Models.EntityFramework.People", "ModifiedBy")
                        .WithMany()
                        .HasForeignKey("ModifiedByUserId");

                    b.HasOne("Restaurants.API.Models.EntityFramework.Employers", "TheEmployerEmployeeWorksFor")
                        .WithMany("TheEmployees")
                        .HasForeignKey("OwnerId");

                    b.HasOne("Restaurants.API.Models.EntityFramework.People", "TheEmployeeDetails")
                        .WithOne("ThePersonAsEmployee")
                        .HasForeignKey("Restaurants.API.Models.EntityFramework.Employees", "PersonId");

                    b.HasOne("Restaurants.API.Models.EntityFramework.RestaurantObjects", "TheRestaurantEmployeeWorksIn")
                        .WithMany("TheRestaurantsEmployees")
                        .HasForeignKey("RestaurantId");
                });

            modelBuilder.Entity("Restaurants.API.Models.EntityFramework.Employers", b =>
                {
                    b.HasOne("Restaurants.API.Models.EntityFramework.People", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedByUserId");

                    b.HasOne("Restaurants.API.Models.EntityFramework.People", "ModifiedBy")
                        .WithMany()
                        .HasForeignKey("ModifiedByUserId");

                    b.HasOne("Restaurants.API.Models.EntityFramework.People", "TheEmployerDetails")
                        .WithOne("ThePersonAsEmployer")
                        .HasForeignKey("Restaurants.API.Models.EntityFramework.Employers", "PersonId");
                });

            modelBuilder.Entity("Restaurants.API.Models.EntityFramework.EmployersRestaurants", b =>
                {
                    b.HasOne("Restaurants.API.Models.EntityFramework.People", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedByUserId");

                    b.HasOne("Restaurants.API.Models.EntityFramework.Employers", "TheEmployer")
                        .WithMany("TheEmployerRestaurantsOwned")
                        .HasForeignKey("EmployerId");

                    b.HasOne("Restaurants.API.Models.EntityFramework.People", "ModifiedBy")
                        .WithMany()
                        .HasForeignKey("ModifiedByUserId");

                    b.HasOne("Restaurants.API.Models.EntityFramework.RestaurantObjects", "TheRestaurant")
                        .WithMany("TheRestaurantEmployers")
                        .HasForeignKey("RestaurantId");
                });

            modelBuilder.Entity("Restaurants.API.Models.EntityFramework.Guests", b =>
                {
                    b.HasOne("Restaurants.API.Models.EntityFramework.People", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedByUserId");

                    b.HasOne("Restaurants.API.Models.EntityFramework.People", "ModifiedBy")
                        .WithMany()
                        .HasForeignKey("ModifiedByUserId");

                    b.HasOne("Restaurants.API.Models.EntityFramework.People", "TheGuestDetails")
                        .WithOne("ThePersonAsGuest")
                        .HasForeignKey("Restaurants.API.Models.EntityFramework.Guests", "PersonId");
                });

            modelBuilder.Entity("Restaurants.API.Models.EntityFramework.LocationContact", b =>
                {
                    b.HasOne("Restaurants.API.Models.EntityFramework.People", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedByUserId");

                    b.HasOne("Restaurants.API.Models.EntityFramework.LocationPoints", "TheLocationPoint")
                        .WithMany()
                        .HasForeignKey("LocationPointId");

                    b.HasOne("Restaurants.API.Models.EntityFramework.People", "ModifiedBy")
                        .WithMany()
                        .HasForeignKey("ModifiedByUserId");

                    b.HasOne("Restaurants.API.Models.EntityFramework.RestaurantObjects", "TheRestaurantOwningTheSchedule")
                        .WithMany("TheRestaurantLocationAddresses")
                        .HasForeignKey("RestaurantId");
                });

            modelBuilder.Entity("Restaurants.API.Models.EntityFramework.LocationPoints", b =>
                {
                    b.HasOne("Restaurants.API.Models.EntityFramework.People", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedByUserId");

                    b.HasOne("Restaurants.API.Models.EntityFramework.People", "ModifiedBy")
                        .WithMany()
                        .HasForeignKey("ModifiedByUserId");
                });

            modelBuilder.Entity("Restaurants.API.Models.EntityFramework.MenuCategories", b =>
                {
                    b.HasOne("Restaurants.API.Models.EntityFramework.People", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedByUserId");

                    b.HasOne("Restaurants.API.Models.EntityFramework.People", "ModifiedBy")
                        .WithMany()
                        .HasForeignKey("ModifiedByUserId");
                });

            modelBuilder.Entity("Restaurants.API.Models.EntityFramework.MenuCurrencies", b =>
                {
                    b.HasOne("Restaurants.API.Models.EntityFramework.People", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedByUserId");

                    b.HasOne("Restaurants.API.Models.EntityFramework.Currencies", "TheCurrency")
                        .WithMany()
                        .HasForeignKey("CurrencyId");

                    b.HasOne("Restaurants.API.Models.EntityFramework.Menus", "TheMenu")
                        .WithMany()
                        .HasForeignKey("MenuId");

                    b.HasOne("Restaurants.API.Models.EntityFramework.People", "ModifiedBy")
                        .WithMany()
                        .HasForeignKey("ModifiedByUserId");
                });

            modelBuilder.Entity("Restaurants.API.Models.EntityFramework.MenuItemContents", b =>
                {
                    b.HasOne("Restaurants.API.Models.EntityFramework.People", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedByUserId");

                    b.HasOne("Restaurants.API.Models.EntityFramework.MenuItems", "TheMenuItem")
                        .WithMany("TheContent")
                        .HasForeignKey("MenuItemId");

                    b.HasOne("Restaurants.API.Models.EntityFramework.MenuLanguages", "TheMenuLanguage")
                        .WithMany("TheMenuLanguageContents")
                        .HasForeignKey("MenuLanguageId");

                    b.HasOne("Restaurants.API.Models.EntityFramework.People", "ModifiedBy")
                        .WithMany()
                        .HasForeignKey("ModifiedByUserId");
                });

            modelBuilder.Entity("Restaurants.API.Models.EntityFramework.MenuItems", b =>
                {
                    b.HasOne("Restaurants.API.Models.EntityFramework.People", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedByUserId");

                    b.HasOne("Restaurants.API.Models.EntityFramework.MenuCategories", "TheMenuCategory")
                        .WithMany("TheItems")
                        .HasForeignKey("MenuCategoryId");

                    b.HasOne("Restaurants.API.Models.EntityFramework.Menus", "TheMenu")
                        .WithMany("TheMenuItems")
                        .HasForeignKey("MenuId");

                    b.HasOne("Restaurants.API.Models.EntityFramework.People", "ModifiedBy")
                        .WithMany()
                        .HasForeignKey("ModifiedByUserId");
                });

            modelBuilder.Entity("Restaurants.API.Models.EntityFramework.MenuItemValues", b =>
                {
                    b.HasOne("Restaurants.API.Models.EntityFramework.People", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedByUserId");

                    b.HasOne("Restaurants.API.Models.EntityFramework.MenuCurrencies", "TheMenuCurrency")
                        .WithMany("TheMenuCurrencyValues")
                        .HasForeignKey("MenuCurrencyId");

                    b.HasOne("Restaurants.API.Models.EntityFramework.MenuItems", "TheMenuItem")
                        .WithMany()
                        .HasForeignKey("MenuItemId");

                    b.HasOne("Restaurants.API.Models.EntityFramework.People", "ModifiedBy")
                        .WithMany()
                        .HasForeignKey("ModifiedByUserId");
                });

            modelBuilder.Entity("Restaurants.API.Models.EntityFramework.MenuLanguages", b =>
                {
                    b.HasOne("Restaurants.API.Models.EntityFramework.People", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedByUserId");

                    b.HasOne("Restaurants.API.Models.EntityFramework.Languages", "TheLanguage")
                        .WithMany("TheMenuLanguagesAssosiatedWithThisLanguages")
                        .HasForeignKey("LanguageId");

                    b.HasOne("Restaurants.API.Models.EntityFramework.Menus", "TheMenu")
                        .WithMany("TheMenuLanguagesAssosiatedWithThisMenu")
                        .HasForeignKey("MenuId");

                    b.HasOne("Restaurants.API.Models.EntityFramework.People", "ModifiedBy")
                        .WithMany()
                        .HasForeignKey("ModifiedByUserId");
                });

            modelBuilder.Entity("Restaurants.API.Models.EntityFramework.Menus", b =>
                {
                    b.HasOne("Restaurants.API.Models.EntityFramework.People", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedByUserId");

                    b.HasOne("Restaurants.API.Models.EntityFramework.People", "ModifiedBy")
                        .WithMany()
                        .HasForeignKey("ModifiedByUserId");

                    b.HasOne("Restaurants.API.Models.EntityFramework.RestaurantObjects", "TheRestaurantOwningTheNumber")
                        .WithMany("TheRestaurantMenu")
                        .HasForeignKey("RestaurantId");
                });

            modelBuilder.Entity("Restaurants.API.Models.EntityFramework.OpenHoursSchedule", b =>
                {
                    b.HasOne("Restaurants.API.Models.EntityFramework.People", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedByUserId");

                    b.HasOne("Restaurants.API.Models.EntityFramework.People", "ModifiedBy")
                        .WithMany()
                        .HasForeignKey("ModifiedByUserId");

                    b.HasOne("Restaurants.API.Models.EntityFramework.RestaurantObjects", "TheRestaurantOwningTheSchedule")
                        .WithMany("TheRestaurantsOpenHours")
                        .HasForeignKey("RestaurantId");
                });

            modelBuilder.Entity("Restaurants.API.Models.EntityFramework.OrderItems", b =>
                {
                    b.HasOne("Restaurants.API.Models.EntityFramework.People", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedByUserId");

                    b.HasOne("Restaurants.API.Models.EntityFramework.People", "ModifiedBy")
                        .WithMany()
                        .HasForeignKey("ModifiedByUserId");

                    b.HasOne("Restaurants.API.Models.EntityFramework.Orders", "TheOrderForThisOrderItem")
                        .WithMany("TheItemsForThisOrder")
                        .HasForeignKey("OrderId");

                    b.HasOne("Restaurants.API.Models.EntityFramework.OrderItemStatuses", "TheStatusForThisOrderItem")
                        .WithMany()
                        .HasForeignKey("OrderItemStatusId");

                    b.HasOne("Restaurants.API.Models.EntityFramework.MenuItems", "TheOrderedItem")
                        .WithMany()
                        .HasForeignKey("OrderedItemId");
                });

            modelBuilder.Entity("Restaurants.API.Models.EntityFramework.Orders", b =>
                {
                    b.HasOne("Restaurants.API.Models.EntityFramework.People", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedByUserId");

                    b.HasOne("Restaurants.API.Models.EntityFramework.Guests")
                        .WithMany("TheOrdersFromThisGuest")
                        .HasForeignKey("GuestsId");

                    b.HasOne("Restaurants.API.Models.EntityFramework.People", "ModifiedBy")
                        .WithMany()
                        .HasForeignKey("ModifiedByUserId");

                    b.HasOne("Restaurants.API.Models.EntityFramework.OrderStatuses", "TheStatusForThisOrder")
                        .WithMany()
                        .HasForeignKey("OrderStatusId");
                });

            modelBuilder.Entity("Restaurants.API.Models.EntityFramework.OutOfSchedulePeriods", b =>
                {
                    b.HasOne("Restaurants.API.Models.EntityFramework.People", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedByUserId");

                    b.HasOne("Restaurants.API.Models.EntityFramework.People", "ModifiedBy")
                        .WithMany()
                        .HasForeignKey("ModifiedByUserId");

                    b.HasOne("Restaurants.API.Models.EntityFramework.OpenHoursSchedule", "TheRealSchedule")
                        .WithMany("TheScheduleExceptions")
                        .HasForeignKey("OpenHoursScheduleId");
                });

            modelBuilder.Entity("Restaurants.API.Models.EntityFramework.People", b =>
                {
                    b.HasOne("Restaurants.API.Models.EntityFramework.People", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedByUserId");

                    b.HasOne("Restaurants.API.Models.EntityFramework.People", "ModifiedBy")
                        .WithMany()
                        .HasForeignKey("ModifiedByUserId");
                });

            modelBuilder.Entity("Restaurants.API.Models.EntityFramework.PhoneContacts", b =>
                {
                    b.HasOne("Restaurants.API.Models.EntityFramework.People", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedByUserId");

                    b.HasOne("Restaurants.API.Models.EntityFramework.People", "ModifiedBy")
                        .WithMany()
                        .HasForeignKey("ModifiedByUserId");

                    b.HasOne("Restaurants.API.Models.EntityFramework.RestaurantObjects", "TheRestaurantOwningTheNumber")
                        .WithMany("TheRestaurantsContactNumbers")
                        .HasForeignKey("RestaurantId");
                });

            modelBuilder.Entity("Restaurants.API.Models.EntityFramework.RestaurantObjects", b =>
                {
                    b.HasOne("Restaurants.API.Models.EntityFramework.People", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedByUserId");

                    b.HasOne("Restaurants.API.Models.EntityFramework.People", "ModifiedBy")
                        .WithMany()
                        .HasForeignKey("ModifiedByUserId");
                });
        }
    }
}
