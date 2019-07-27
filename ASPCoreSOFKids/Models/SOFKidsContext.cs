using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace ASPCoreSOFKids.Models
{
    public partial class SOFKidsContext : DbContext
    {
        public IConfiguration Configuration;
        public SOFKidsContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public SOFKidsContext(DbContextOptions<SOFKidsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CartToys> CartToys { get; set; }
        public virtual DbSet<CoustmerDetails> CoustmerDetails { get; set; }
        public virtual DbSet<MailingAddress> MailingAddress { get; set; }
        public virtual DbSet<PurchaseHistory> PurchaseHistory { get; set; }
        public virtual DbSet<Toys> Toys { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.GetConnectionString("SOFKids"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<CartToys>(entity =>
            {
                entity.HasKey(e => e.CartToyId)
                    .HasName("PK__CartToys__8104086DDDDE42E3");

                entity.Property(e => e.CartToyId).HasColumnName("CartToy_Id");

                entity.Property(e => e.CoustmerId).HasColumnName("Coustmer_Id");

                entity.Property(e => e.ToyId).HasColumnName("Toy_Id");

                entity.HasOne(d => d.Coustmer)
                    .WithMany(p => p.CartToys)
                    .HasForeignKey(d => d.CoustmerId)
                    .HasConstraintName("FK__CartToys__Coustm__70DDC3D8");

                entity.HasOne(d => d.Toy)
                    .WithMany(p => p.CartToys)
                    .HasForeignKey(d => d.ToyId)
                    .HasConstraintName("FK__CartToys__Toy_Id__6FE99F9F");
            });

            modelBuilder.Entity<CoustmerDetails>(entity =>
            {
                entity.HasKey(e => e.CoustmerId)
                    .HasName("PK__Coustmer__3FAED0D00CBA042F");

                entity.Property(e => e.CoustmerId).HasColumnName("Coustmer_Id");

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

            modelBuilder.Entity<MailingAddress>(entity =>
            {
                entity.Property(e => e.MailingAddressId).HasColumnName("MailingAddress_Id");

                entity.Property(e => e.AptNo)
                    .HasColumnName("Apt_No")
                    .HasMaxLength(50);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CoustmerId).HasColumnName("Coustmer_Id");

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Street).HasMaxLength(100);

                entity.HasOne(d => d.Coustmer)
                    .WithMany(p => p.MailingAddress)
                    .HasForeignKey(d => d.CoustmerId)
                    .HasConstraintName("FK__Payment__Coustme__5DCAEF64");
            });

            modelBuilder.Entity<PurchaseHistory>(entity =>
            {
                entity.Property(e => e.PurchaseHistoryId).HasColumnName("PurchaseHistory_Id");

                entity.Property(e => e.CoustmerId).HasColumnName("Coustmer_Id");

                entity.Property(e => e.ToyId).HasColumnName("Toy_Id");

                entity.HasOne(d => d.Coustmer)
                    .WithMany(p => p.PurchaseHistory)
                    .HasForeignKey(d => d.CoustmerId)
                    .HasConstraintName("FK__PurchaseH__Coust__74AE54BC");

                entity.HasOne(d => d.Toy)
                    .WithMany(p => p.PurchaseHistory)
                    .HasForeignKey(d => d.ToyId)
                    .HasConstraintName("FK__PurchaseH__Toy_I__73BA3083");
            });

            modelBuilder.Entity<Toys>(entity =>
            {
                entity.HasKey(e => e.ToyId)
                    .HasName("PK__Toys__ACC1B16D19C7CBA8");

                entity.Property(e => e.ToyId).HasColumnName("Toy_Id");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.ToyName)
                    .HasColumnName("Toy_Name")
                    .HasMaxLength(100);
            });
        }
    }
}
