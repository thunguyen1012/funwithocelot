using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Order.Infrastructure.Data;
using System;

namespace Order.WebAPI
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var dbContext = new AppDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>());
            PopulateTestData(dbContext);
        }

        public static void PopulateTestData(AppDbContext dbContext)
        {
            foreach (var item in dbContext.Orders)
            {
                dbContext.Remove(item);
            }
            dbContext.SaveChanges();

            var order1 = Core.Entities.Order.Create();
            order1.Id = Guid.Parse("54C4AA63-6337-41CA-8B70-B8EEB8B77C8F");
            order1.ProductId = Guid.Parse("54C4AA63-6337-41CA-8B71-B8EEB8B77C8F");
            order1.PaymentId = Guid.Parse("54C4AA63-6337-41CA-8B72-B8EEB8B77C8F");
            order1.Status = Core.Entities.OrderStatus.New;

            var order2 = Core.Entities.Order.Create();
            order2.Id = Guid.Parse("54C4AA63-6337-41CA-8B80-B8EEB8B77C8F");
            order2.ProductId = Guid.Parse("54C4AA63-6337-41CA-8B81-B8EEB8B77C8F");
            order2.PaymentId = Guid.Parse("54C4AA63-6337-41CA-8B82-B8EEB8B77C8F");
            order2.Status = Core.Entities.OrderStatus.New;

            dbContext.Orders.Add(order1);
            dbContext.Orders.Add(order2);

            dbContext.SaveChanges();
        }
    }
}