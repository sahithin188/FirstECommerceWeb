using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SOFSports.wwwroot.Models
{
    public partial class SOF586SportsContext : DbContext
    {
        public SOF586SportsContext()
        {
        }

        public SOF586SportsContext(DbContextOptions<SOF586SportsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Items> Items { get; set; }
        public virtual DbSet<PaymentAddress> PaymentAddress { get; set; }
        public virtual DbSet<PurchaseHistory> PurchaseHistory { get; set; }
        public virtual DbSet<UserInfo> UserInfo { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
               optionsBuilder.UseSqlServer(Configuration.GetConnectionString("SOFSports"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Items>(entity =>
            {
                entity.Property(e => e.ItemsId).HasColumnName("Items_Id");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.Price).HasColumnType("money");
            });

            modelBuilder.Entity<PaymentAddress>(entity =>
            {
                entity.ToTable("Payment_Address");

                entity.Property(e => e.PaymentAddressId).HasColumnName("Payment_Address_Id");

                entity.Property(e => e.AptNo)
                    .HasColumnName("Apt_No")
                    .HasMaxLength(50);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Street).HasMaxLength(100);

                entity.Property(e => e.UserInfoId).HasColumnName("UserInfo_Id");

                entity.HasOne(d => d.UserInfo)
                    .WithMany(p => p.PaymentAddress)
                    .HasForeignKey(d => d.UserInfoId)
                    .HasConstraintName("FK__Payment__UserInf__52593CB8");
            });

            modelBuilder.Entity<PurchaseHistory>(entity =>
            {
                entity.Property(e => e.PurchaseHistoryId).HasColumnName("PurchaseHistory_Id");

                entity.Property(e => e.ItemsId).HasColumnName("Items_Id");

                entity.Property(e => e.UserInfoId).HasColumnName("UserInfo_Id");

                entity.HasOne(d => d.Items)
                    .WithMany(p => p.PurchaseHistory)
                    .HasForeignKey(d => d.ItemsId)
                    .HasConstraintName("FK__PurchaseH__Items__4D94879B");

                entity.HasOne(d => d.UserInfo)
                    .WithMany(p => p.PurchaseHistory)
                    .HasForeignKey(d => d.UserInfoId)
                    .HasConstraintName("FK__PurchaseH__UserI__4E88ABD4");
            });

            modelBuilder.Entity<UserInfo>(entity =>
            {
                entity.Property(e => e.UserInfoId).HasColumnName("UserInfo_Id");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(100);
            });
        }
    }
}
