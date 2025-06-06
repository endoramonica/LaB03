using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Web_BanHang_T6S34.Models;

namespace Web_BanHang_T6S34.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Khai báo các bảng tùy chỉnh trong CSDL
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        /*protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Category
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Điện thoại", Description = "Các loại điện thoại phổ biến" },
                new Category { Id = 2, Name = "Laptop", Description = "Các loại Laptop phổ biến" }
            );

            // Seed Product
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "iPhone 14",
                    Price = 20000000,
                    Description = "Điện thoại Apple cao cấp",
                    ImageUrl = "/images/iphone.jpg", // Đảm bảo ảnh nằm trong wwwroot/images/
                    CategoryId = 1
                },
                new Product
                {
                    Id = 2,
                    Name = "Oppo A57",
                    Price = 50000000,
                    Description = "Điện thoại Oppo phổ thông",
                    ImageUrl = "/images/oppo.jpg",
                    CategoryId = 1
                },
                new Product
                {
                    Id = 3,
                    Name = "Dell XPS 13",
                    Price = 99999999,
                    Description = "Laptop Dell XPS 13 cao cấp",
                    ImageUrl = "/images/lenovo_legion.jpg",
                    CategoryId = 2
                }
            );

            // Seed ProductImage (nếu muốn)
            modelBuilder.Entity<ProductImage>().HasData(
                new ProductImage { Id = 1, Url = "/images/iphone.jpg", ProductId = 1 },
                new ProductImage { Id = 2, Url = "/images/oppo.jpg", ProductId = 2 },
                new ProductImage { Id = 3, Url = "/images/lenovo_legion.jpg", ProductId = 3 }  
            );
        }*/
    }
}