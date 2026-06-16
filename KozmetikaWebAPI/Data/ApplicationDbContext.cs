using Microsoft.EntityFrameworkCore;
using KozmetikaWebAPI.Models;

namespace KozmetikaWebAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<PaymentTransaction> PaymentTransactions { get; set; }

        public DbSet<WishlistItem> WishlistItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<WishlistItem>()
                .HasIndex(w => new { w.UserId, w.ProductId })
                .IsUnique();

            modelBuilder.Entity<WishlistItem>()
                .HasOne(w => w.User)
                .WithMany(u => u.WishlistItems)
                .HasForeignKey(w => w.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<WishlistItem>()
                .HasOne(w => w.Product)
                .WithMany(p => p.WishlistItems)
                .HasForeignKey(w => w.ProductId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Admin" },
                new Role { Id = 2, Name = "Customer" }, 
                new Role { Id = 3, Name = "Employee" }
            );

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Skincare", Description = "Cleansers, creams, serums and daily face care products" },
                new Category { Id = 2, Name = "Body Care", Description = "Body lotions, shower gels, oils and body care essentials" },
                new Category { Id = 3, Name = "Hair Care", Description = "Shampoos, conditioners, masks and hair treatments" },
                new Category { Id = 4, Name = "Makeup", Description = "Foundation, lipstick, mascara and decorative cosmetics" },
                new Category { Id = 5, Name = "Fragrance", Description = "Perfumes and signature scents" }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 101, Name = "Hydrating Cleanser", Brand = "CeraVe", Description = "Gentle hydrating facial cleanser", Price = 1550, StockQuantity = 20, ImageUrl = "https://www.cerave.rs/-/media/project/loreal/brand-sites/cerave/emea/rs/scx/packshots/hydrating-cleanser/hydrating-cleanser-front-lg.jpg", CategoryId = 1 },
                new Product { Id = 102, Name = "Moisturizing Cream", Brand = "CeraVe", Description = "Rich moisturizer for dry skin", Price = 1800, StockQuantity = 15, ImageUrl = "https://static.beautytocare.com/cdn-cgi/image/f=auto/media/catalog/product/c/e/cerave-moisturizing-cream-dry-to-very-dry-skin-170g_3.jpg", CategoryId = 1 },
                new Product { Id = 103, Name = "Niacinamide Serum", Brand = "The Ordinary", Description = "Serum for blemish-prone skin", Price = 1350, StockQuantity = 25, ImageUrl = "https://metropoliten.rs/upload/catalog/variation/3304/04416_1_66e968f5b08d3.jpg", CategoryId = 1 },
                new Product { Id = 104, Name = "Effaclar Duo+", Brand = "La Roche-Posay", Description = "Corrective care for problematic skin", Price = 2200, StockQuantity = 18, ImageUrl = "https://www.laroche-posay.rs/-/media/project/loreal/brand-sites/lrp/emea/adba/products/effaclar/effaclar-duo-plus-m/larocheposayproductacneeffaclarduom15ml337875863841fws.png", CategoryId = 1 },
                
                new Product { Id = 105, Name = "Body Lotion", Brand = "Neutrogena", Description = "Deeply hydrating body lotion", Price = 1450, StockQuantity = 16, ImageUrl = "https://images.ctfassets.net/b7vjv6cc1lvj/2ZDEKfi7jzTB2dalCyiJOG/3fd420402e8142cba22e16ab5ea04cd2/grlc7-en-gb", CategoryId = 2 },
                new Product { Id = 106, Name = "Shower Oil", Brand = "Bioderma", Description = "Gentle shower oil for sensitive skin", Price = 1900, StockQuantity = 14, ImageUrl = "https://apoteka-apolo.rs/wp-content/uploads/2024/08/37735-Bioderma-atoderm-ulje-za-tusiranje-1l-640x640-1.jpg", CategoryId = 2 },
                new Product { Id = 107, Name = "Body Scrub", Brand = "Tree Hut", Description = "Exfoliating sugar body scrub", Price = 2100, StockQuantity = 12, ImageUrl = "https://www.treehutshea.com/cdn/shop/products/peach-sorbet-front.jpg?v=1675379991", CategoryId = 2 },
                new Product { Id = 108, Name = "Body Oil", Brand = "Nuxe", Description = "Multi-purpose dry oil for body and hair", Price = 3900, StockQuantity = 10, ImageUrl = "https://us.nuxe.com/cdn/shop/files/3264680009761-0A38031-VIEW_2-HP-CLASSIQUE_50ML-PACKSHOT_2000x2000_d65cba9d-0608-4b48-a48f-0eeccea6f94e.jpg?crop=center&height=1200&v=1718290583&width=1200", CategoryId = 2 }, 

                new Product { Id = 109, Name = "Elvive Shampoo", Brand = "L'Oréal Paris", Description = "Repairing shampoo for damaged hair", Price = 850, StockQuantity = 30, ImageUrl = "https://enemmall.com/cdn/shop/files/3312669.jpg?v=1757331037", CategoryId = 3 },
                new Product { Id = 110, Name = "Hair Food Banana Mask", Brand = "Garnier", Description = "Nourishing mask for dry hair", Price = 1150, StockQuantity = 22, ImageUrl = "https://main.thgimages.com/?url=https://static.thcdn.com/productimg/original/12030813-1485131248642276.jpg&format=webp&width=1500&height=1500&fit=cover", CategoryId = 3 },
                new Product { Id = 111, Name = "Bonding Oil", Brand = "Olaplex", Description = "Lightweight oil for shine and repair", Price = 4200, StockQuantity = 9, ImageUrl = "https://m.media-amazon.com/images/I/71ACqtMWo2L._AC_UF1000,1000_QL80_.jpg", CategoryId = 3 },
                new Product { Id = 112, Name = "Genesis Conditioner", Brand = "Kérastase", Description = "Anti-breakage conditioner for weakened hair", Price = 3600, StockQuantity = 11, ImageUrl = "https://numi.rs/wp-content/uploads/2024/09/Kerastase-Genesis-Fondant-Renforcateur-Conditioner-200ml.jpg", CategoryId = 3 },

                new Product { Id = 113, Name = "Double Wear Foundation", Brand = "Estée Lauder", Description = "Long-wear liquid foundation", Price = 6500, StockQuantity = 12, ImageUrl = "https://www.odkshop.com/cdn/shop/files/rgC1jadriv_600x.jpg?v=1726613741", CategoryId = 4 },
                new Product { Id = 114, Name = "Rouge Dior Lipstick", Brand = "Dior", Description = "Luxury satin lipstick", Price = 5900, StockQuantity = 9, ImageUrl = "https://apothecary.rs/27333-large_default/dior-rouge-dior-lipstick-n434-satin-finish-promenade-promenade-a-bold-rosewood-35g.jpg", CategoryId = 4 },
                new Product { Id = 115, Name = "Lip Glow Oil", Brand = "Dior", Description = "Glossy lip oil with nourishing finish", Price = 5200, StockQuantity = 10, ImageUrl = "https://apothecary.rs/18361-large_default/dior-lip-glow-oil-n001-pink-6ml.jpg", CategoryId = 4 },
                new Product { Id = 116, Name = "Fit Me Foundation", Brand = "Maybelline", Description = "Natural finish liquid foundation", Price = 1200, StockQuantity = 22, ImageUrl = "https://www.apotekajankovic.rs/image/cache/wp/lj/Ivory.webp", CategoryId = 4 },
                new Product { Id = 117, Name = "Rare Beauty Blush", Brand = "Rare Beauty", Description = "Liquid blush with soft glowing finish", Price = 4200, StockQuantity = 14, ImageUrl = "https://www.spacenk.com/on/demandware.static/-/Sites-spacenkmastercatalog/default/dw95036ea5/products/RARE/UK200048738_RARE.jpg", CategoryId = 4 },

                new Product { Id = 118, Name = "Miss Dior Parfum", Brand = "Dior", Description = "Elegant floral fragrance", Price = 14500, StockQuantity = 8, ImageUrl = "https://m.media-amazon.com/images/I/61R1+HpKFPL._AC_UF1000,1000_QL80_.jpg", CategoryId = 5 },
                new Product { Id = 119, Name = "Black Opium", Brand = "YSL", Description = "Warm and intense evening fragrance", Price = 13200, StockQuantity = 7, ImageUrl = "https://jasmin.b-cdn.net/media/catalog/product/cache/4456161891bc26600241f10587ca424f/3/3/3365440787858-10ff7fd8-590b-4914-af76-6220761f0543_1.jpg", CategoryId = 5 },
                new Product { Id = 120, Name = "Libre Eau de Parfum", Brand = "YSL", Description = "Bold floral lavender fragrance", Price = 13900, StockQuantity = 6, ImageUrl = "https://www.parfimerijatajna.rs/files/2022/11/3614273776127_1.jpg", CategoryId = 5 },
                new Product { Id = 121, Name = "Good Girl", Brand = "Carolina Herrera", Description = "Iconic sweet and elegant fragrance", Price = 12800, StockQuantity = 9, ImageUrl = "https://parfembox.rs/wp-content/uploads/2020/06/carolina_herrera_good_girl_50_ml.png", CategoryId = 5 }
            );
        }
    }
}