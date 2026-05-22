using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;


namespace Application.Interfaces
{

    public interface IApplicationDbContext
    {
        DbSet<ProductInventory> Inventory { get; }
        DbSet<Product> Products { get; }
        DbSet<ShoppingCart> Carts { get; }
        DbSet<ShoppingCartItem> CartItems { get; }
        DbSet<Order> Orders { get; }
        DbSet<OrderItem> OrderItems { get; }
        DbSet<Invite> Invites { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
