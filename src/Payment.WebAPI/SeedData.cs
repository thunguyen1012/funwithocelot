using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Payment.Infrastructure.Data;
using System;

namespace Payment.WebAPI
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
            foreach (var item in dbContext.Payments)
            {
                dbContext.Remove(item);
            }
            dbContext.SaveChanges();

            var payment1 = Core.Entities.Payment.Create();
            payment1.Id = Guid.Parse("54C4AA63-6337-41CA-8B70-B8EEB8B77C8F");
            payment1.OrderId = Guid.Parse("54C4AA63-6337-41CA-8B71-B8EEB8B77C8F");
            payment1.Status = Core.Entities.PaymentStatus.New;

            dbContext.Payments.Add(payment1);

            dbContext.SaveChanges();
        }
    }
}