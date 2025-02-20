using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotels.Entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hotels.DataAccess.Context
{
    public class HotelsContext : DbContext
    {
        public HotelsContext(DbContextOptions<HotelsContext> options) : base(options) { }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<EmergencyContact> EmergencyContacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<EmergencyContact>()
                .HasOne(e => e.Booking)
                .WithOne(b => b.EmergencyContact)
                .HasForeignKey<EmergencyContact>(e => e.BookingId);
        }
    }
}
