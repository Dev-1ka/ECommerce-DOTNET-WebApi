using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        private readonly string _adminEmail;
        private readonly string _adminPassword;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration config)  : base(options)
        {
            _adminEmail = config["AdminCredentials:Email"];
            _adminPassword = config["AdminCredentials:Password"];
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductInventory> Inventory { get; set; }
        public DbSet<ShoppingCart> Carts { get; set; }
        public DbSet<ShoppingCartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Invite> Invites { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

        
            var roles = new List<IdentityRole>
            {
                new IdentityRole { Id = "role-admin", Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = "role-user", Name = "User", NormalizedName = "USER" },
                new IdentityRole { Id = "role-pm", Name = "ProductManager", NormalizedName = "PRODUCTMANAGER" },
                new IdentityRole { Id = "role-im", Name = "InventoryManager", NormalizedName = "INVENTORYMANAGER" },
                new IdentityRole { Id = "role-da", Name = "DataAnalyst", NormalizedName = "DATAANALYST" }
            };

            builder.Entity<IdentityRole>().HasData(roles);

            var adminId = "admin-user-id";

            var hasher = new PasswordHasher<ApplicationUser>();

            var adminUser = new ApplicationUser
            {
                Id = adminId,
                UserName = _adminEmail,
                NormalizedUserName = _adminEmail.ToUpper(),
                Email = _adminEmail,
                NormalizedEmail = _adminEmail.ToUpper(),
                FullName = "Administrator",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            adminUser.PasswordHash = hasher.HashPassword(adminUser, _adminPassword);

            builder.Entity<ApplicationUser>().HasData(adminUser);

            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                UserId = adminId,
                RoleId = "role-admin"
            });

      
            builder.Entity<ShoppingCartItem>()
                .HasIndex(x => new { x.CartId, x.ProductId }).IsUnique();

      
            builder.Entity<ShoppingCartItem>()
                .HasOne(i => i.Product)
                .WithMany()
                .HasForeignKey(i => i.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

         
            builder.Entity<ShoppingCartItem>()
                .HasQueryFilter(i => !i.IsDeleted);

      
            builder.Entity<ProductInventory>()
                .HasOne<Product>()
                .WithMany()
                .HasForeignKey(i => i.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}