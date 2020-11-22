using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace HotelBookingsApp
{
    public partial class HotelBookingsDBContext : DbContext
    {
        public HotelBookingsDBContext()
        {
        }

        public HotelBookingsDBContext(DbContextOptions<HotelBookingsDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BookedRoom> BookedRooms { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//You need to change the path in the next line (the AttachDBFilename part) to the local path of wherever the HotelBookingsDB.mdf file is located on your machine
                optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\jonat\\source\\repos\\HotelBookingsApp\\HotelBookingsApp\\App_Data\\HotelBookingsDB.mdf;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookedRoom>(entity =>
            {
                entity.Property(e => e.BookingDate)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.GuestName).HasMaxLength(50);

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.BookedRooms)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BookedRooms_Rooms");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
