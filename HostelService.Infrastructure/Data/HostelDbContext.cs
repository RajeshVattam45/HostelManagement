using HostelService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelService.Infrastructure.Data
{
    public class HostelDbContext : DbContext
    {
        public HostelDbContext ( DbContextOptions<HostelDbContext> options )
            : base ( options )
        {
        }

        public DbSet<Hostel> Hostels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<HostelStudent> HostelStudents { get; set; }

        protected override void OnModelCreating ( ModelBuilder modelBuilder )
        {
            base.OnModelCreating ( modelBuilder );

            modelBuilder.Entity<HostelStudent> ()
                .HasOne ( hs => hs.Room )
                .WithMany ()
                .HasForeignKey ( hs => hs.RoomId )
                .OnDelete ( DeleteBehavior.Restrict ); // FIX HERE
        }

    }
}
