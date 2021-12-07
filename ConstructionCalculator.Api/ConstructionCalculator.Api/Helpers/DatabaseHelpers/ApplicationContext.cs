using ConstructionCalculator.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ConstructionCalculator.Api.Helpers.DatabaseHelpers
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<CalculateEntity> Calculates { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
