using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.API.Extensions;
using Restaurants.API.Models.EntityFramework;
using Restaurants.API.Models.Enums;
using System.Linq;


namespace Restaurants.API.Models.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<AssignedEmployeeTypes> AssignedEmployeeTypes { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Currencies> Currencies { get; set; }
        public DbSet<Employees> Employees { get; set; }
        public DbSet<EmployeeTypes> EmployeeType { get; set; }
        public DbSet<Employers> Employers { get; set; }
        public DbSet<EmployersRestaurants> EmployersRestaurants { get; set; }
        public DbSet<Guests> Guests { get; set; }
        public DbSet<Languages> Languages { get; set; }
        public DbSet<LocationContact> LocationContact { get; set; }
        public DbSet<LocationPoints> LocationPoints { get; set; }
        public DbSet<MenuCategories> MenuCategories { get; set; }
        public DbSet<MenuCurrencies> MenuCurrencies { get; set; }
        public DbSet<MenuItemContents> MenuItemContents { get; set; }
        public DbSet<MenuItems> MenuItems { get; set; }
        public DbSet<MenuItemValues> MenuItemValues { get; set; }
        public DbSet<MenuLanguages> MenuLanguages { get; set; }
        public DbSet<Menus> Menus { get; set; }
        public DbSet<OpenHoursSchedule> OpenHoursSchedule { get; set; }
        public DbSet<OrderItems> OrderItems { get; set; }
        public DbSet<OrderItemStatuses> OrderItemStatuses { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrderStatuses> OrderStatuses { get; set; }
        public DbSet<OutOfSchedulePeriods> OutOfSchedulePeriods { get; set; }
        public DbSet<People> People { get; set; }
        public DbSet<PhoneContacts> PhoneContacts { get; set; }
        public DbSet<RestaurantObjects> RestaurantObjects { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost;Database=RestaurantsDb;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<People>()
                .HasOne(x => x.ThePersonAsGuest)
                .WithOne(x => x.TheGuestDetails)
                .HasForeignKey<Guests>(x => x.PersonId);

            modelBuilder.Entity<People>()
                .HasOne(x => x.ThePersonAsEmployee)
                .WithOne(x => x.TheEmployeeDetails)
                .HasForeignKey<Employees>(x => x.PersonId);

            modelBuilder.Entity<People>()
                .HasOne(x => x.ThePersonAsEmployer)
                .WithOne(x => x.TheEmployerDetails)
                .HasForeignKey<Employers>(x => x.PersonId);

            modelBuilder.Entity<MenuCurrencies>()
                .HasIndex(x => new { x.MenuId, x.CurrencyId })
                .IsUnique();

            modelBuilder.Entity<MenuLanguages>()
                .HasIndex(x => new { x.MenuId, x.LanguageId })
                .IsUnique();

            modelBuilder.Entity<AssignedEmployeeTypes>()
                .HasIndex(x => new { x.EmployeeId, x.TypeId })
                .IsUnique();

            modelBuilder.Entity<EmployersRestaurants>()
                .HasIndex(x => new { x.EmployerId, x.RestaurantId })
                .IsUnique();

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        public AppDbContext() : base() { }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public static void Seed(IApplicationBuilder app)
        {
            using (var context = app.ApplicationServices.GetRequiredService<AppDbContext>())
            {
                context.Currencies.SeedEnumValues<Currencies, CurrencyEnum>(@enum => @enum);
                context.SaveChanges();

                context.EmployeeType.SeedEnumValues<EmployeeTypes, EmployeeTypeEnum>(@enum => @enum);
                context.SaveChanges();

                context.Languages.SeedEnumValues<Languages, LanguageEnum>(@enum => @enum);
                context.SaveChanges();

                context.OrderItemStatuses.SeedEnumValues<OrderItemStatuses, OrderItemStatusEnum>(@enum => @enum);
                context.SaveChanges();

                context.OrderStatuses.SeedEnumValues<OrderStatuses, OrderStatusEnum>(@enum => @enum);
                context.SaveChanges();
            }
        }
    }
}
