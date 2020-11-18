using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace OrderImport {
    public class OrderContextFactory : IDesignTimeDbContextFactory<OrderContext> {
        public OrderContext CreateDbContext(string[] args) {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            var optionsBuilder = new DbContextOptionsBuilder<OrderContext>();
            optionsBuilder.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"]);

            return new OrderContext(optionsBuilder.Options);
        }
    }
}
