using System;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data;

public class ApplicationDBContext : DbContext
{
    public ApplicationDBContext(DbContextOptions dbContextOptions)
    : base(dbContextOptions)
    {
        
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Publisher> Publishers { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<BookImage> BookImages { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserAddress> UserAddresses { get; set; }
    public DbSet<ShoppingCart> ShoppingCarts { get; set; }
    public DbSet<ReviewImage> ReviewImages { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // modelBuilder.Entity<ShoppingCart>(b => {
        //     b.HasKey(x => new {x.BookId, x.UserId});
        //     b.HasOne<Book>().WithMany(x => x.ShoppingCarts).HasForeignKey(x => x.BookId).IsRequired();
        //     b.HasOne<User>().WithMany(x => x.ShoppingCarts).HasForeignKey(x => x.UserId).IsRequired();
        // });
        #region "ShoppingCart Entiry"
        modelBuilder.Entity<ShoppingCart>(x => x.HasKey(p => new {p.UserId, p.BookId}));
        modelBuilder.Entity<ShoppingCart>()
                    .HasOne(u => u.User)
                    .WithMany(u => u.ShoppingCarts)
                    .HasForeignKey(s => s.UserId);

        modelBuilder.Entity<ShoppingCart>()
                    .HasOne(b => b.Book)
                    .WithMany(b => b.ShoppingCarts)
                    .HasForeignKey(s => s.BookId);
        #endregion

        #region "Review Entiry"
        modelBuilder.Entity<Review>()
                    .HasOne(b => b.Book)
                    .WithMany(b => b.Reviews)
                    .HasForeignKey(r => r.BookId);

        modelBuilder.Entity<Review>()
                    .HasOne(u => u.User)
                    .WithMany(u => u.Reviews)
                    .HasForeignKey(r => r.UserId);
        #endregion

        #region ""
        modelBuilder.Entity<OrderDetail>(o => o.HasKey(k => new {k.BookId, k.OrderId}));
        modelBuilder.Entity<OrderDetail>()
                    .HasOne(b => b.Book)
                    .WithMany(b => b.OrderDetails)
                    .HasForeignKey(o => o.BookId);
        modelBuilder.Entity<OrderDetail>()
                    .HasOne(o => o.Order)
                    .WithMany(o => o.OrderDetails)
                    .HasForeignKey(od => od.OrderId);
        #endregion        
    }
}