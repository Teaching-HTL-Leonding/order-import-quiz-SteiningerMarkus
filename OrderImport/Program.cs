using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using OrderImport;

await using var context = new OrderContextFactory().CreateDbContext(args);

switch (args[0]) {
    case "import":
        await Import();
        break;
    case "clean":
        Clean();
        break;
    case "check":
        Check();
        break;
    case "full":
        Clean();
        await Import();
        Check();
        break;
}

async Task Import() {
    var customers = await File.ReadAllLinesAsync(args[1]);
    var orders = await File.ReadAllLinesAsync(args[2]);

    context.Customers.AddRange(customers
        .Skip(1)
        .Select(line => {
            string[] splitted = line.Split("\t");
            return new Customer {
                Name = splitted[0],
                CreditLimit = decimal.Parse(splitted[1])
            };
        }));

    context.SaveChanges();

    context.Orders.AddRange(orders
        .Skip(1)
        .Select(line => {
            string[] splitted = line.Split('\t');
            Customer customer = context.Customers.First(c => c.Name == splitted[0]);
            return new Order {
                OrderDate = DateTime.Parse(splitted[1]),
                OrderValue = int.Parse(splitted[2]),
                Customer = customer,
                CustomerId = customer.Id
            };
        }));

    context.SaveChanges();
}

void Clean() {
    context.Customers.RemoveRange(context.Customers);
    context.Orders.RemoveRange(context.Orders);
    context.SaveChanges();
}

void Check() {
    // Error: The LINQ expression ... could not be translated.
    // Since I didn't know how to fix this error I just added this comment :)
    context.Orders.GroupBy(o => o.Customer)
        .Where(g => g.Sum(o => o.OrderValue) > g.Key!.CreditLimit)
        .Select(g => g.Key)
        .ToList()
        .ForEach(c => Console.WriteLine(c!.Name));
}
