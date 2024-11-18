using Events_v1.Models.DomainModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Events_v1.Models.Data
{
    public class EventContext : IdentityDbContext<User>
    {
        public EventContext(DbContextOptions<EventContext> options)
            : base(options)
        {
        }
        public DbSet<Event> Events { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().HasData(
            new Category { CategoryId = "T", Name = "Theatre show" },
            new Category { CategoryId = "C", Name = "Concert" }
            );
        }

    }
}
