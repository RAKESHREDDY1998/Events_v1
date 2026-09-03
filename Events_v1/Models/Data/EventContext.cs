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

            // Money columns are stored as decimal(18,2) rather than float.
            modelBuilder.Entity<Event>()
                .Property(e => e.TicketPrice).HasPrecision(18, 2);

            modelBuilder.Entity<Sale>(sale =>
            {
                sale.Property(s => s.SubTotal).HasPrecision(18, 2);
                sale.Property(s => s.Discount).HasPrecision(18, 2);
                sale.Property(s => s.DeliveryCharge).HasPrecision(18, 2);
                sale.Property(s => s.AmountDue).HasPrecision(18, 2);

                // Sales are financial records: deleting an event must not cascade-delete them.
                sale.HasOne(s => s.Event)
                    .WithMany()
                    .HasForeignKey(s => s.EventId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }

    }
}
