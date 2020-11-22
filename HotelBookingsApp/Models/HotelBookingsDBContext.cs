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
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=HotelBookingsDB;Integrated Security=True;Pooling=False");
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
