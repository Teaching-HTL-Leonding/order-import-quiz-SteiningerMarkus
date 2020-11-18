using Microsoft.EntityFrameworkCore;

#pragma warning disable 8618

namespace OrderImport {
    public class OrderContext : DbContext {
        public OrderContext(DbContextOptions<OrderContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Order> Orders { get; set; }
    }
}
