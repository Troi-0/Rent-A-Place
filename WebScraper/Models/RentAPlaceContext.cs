using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WebScraper.Models
{
    public partial class RentAPlaceContext : DbContext
    {
        public RentAPlaceContext()
        {
        }

        public RentAPlaceContext(DbContextOptions<RentAPlaceContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRole> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }
        public virtual DbSet<BuildingType> BuildingTypes { get; set; }
        public virtual DbSet<District> Districts { get; set; }
        public virtual DbSet<RealEstate> RealEstates { get; set; }
        public virtual DbSet<RealEstateBooking> RealEstateBookings { get; set; }
        public virtual DbSet<RealEstateTag> RealEstateTags { get; set; }
        public virtual DbSet<RealEstateType> RealEstateTypes { get; set; }
        public virtual DbSet<Setting> Settings { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.;Database=RentAPlace;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.HasIndex(e => e.IsDeleted, "IX_AspNetRoles_IsDeleted");

                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetRoleClaim>(entity =>
            {
                entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.IsDeleted, "IX_AspNetUsers_IsDeleted");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaim>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<AspNetUserRole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId, "IX_AspNetUserRoles_RoleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<BuildingType>(entity =>
            {
                entity.HasIndex(e => e.IsDeleted, "IX_BuildingTypes_IsDeleted");

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<District>(entity =>
            {
                entity.HasIndex(e => e.IsDeleted, "IX_Districts_IsDeleted");

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<RealEstate>(entity =>
            {
                entity.HasIndex(e => e.BuildingTypeId, "IX_RealEstates_BuildingTypeId");

                entity.HasIndex(e => e.DistrictId, "IX_RealEstates_DistrictId");

                entity.HasIndex(e => e.IsDeleted, "IX_RealEstates_IsDeleted");

                entity.HasIndex(e => e.OwnerId, "IX_RealEstates_OwnerId");

                entity.HasIndex(e => e.RealEstateTypeId, "IX_RealEstates_RealEstateTypeId");

                entity.HasOne(d => d.BuildingType)
                    .WithMany(p => p.RealEstates)
                    .HasForeignKey(d => d.BuildingTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.District)
                    .WithMany(p => p.RealEstates)
                    .HasForeignKey(d => d.DistrictId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.RealEstates)
                    .HasForeignKey(d => d.OwnerId);

                entity.HasOne(d => d.RealEstateType)
                    .WithMany(p => p.RealEstates)
                    .HasForeignKey(d => d.RealEstateTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<RealEstateBooking>(entity =>
            {
                entity.HasIndex(e => e.IsDeleted, "IX_RealEstateBookings_IsDeleted");

                entity.HasIndex(e => e.RealEstateId, "IX_RealEstateBookings_RealEstateId");

                entity.HasIndex(e => e.RenterId, "IX_RealEstateBookings_RenterId");

                entity.HasOne(d => d.RealEstate)
                    .WithMany(p => p.RealEstateBookings)
                    .HasForeignKey(d => d.RealEstateId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Renter)
                    .WithMany(p => p.RealEstateBookings)
                    .HasForeignKey(d => d.RenterId);
            });

            modelBuilder.Entity<RealEstateTag>(entity =>
            {
                entity.HasIndex(e => e.IsDeleted, "IX_RealEstateTags_IsDeleted");

                entity.HasIndex(e => e.PropertyId, "IX_RealEstateTags_PropertyId");

                entity.HasIndex(e => e.TagId, "IX_RealEstateTags_TagId");

                entity.HasOne(d => d.Property)
                    .WithMany(p => p.RealEstateTags)
                    .HasForeignKey(d => d.PropertyId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.RealEstateTags)
                    .HasForeignKey(d => d.TagId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<RealEstateType>(entity =>
            {
                entity.HasIndex(e => e.IsDeleted, "IX_RealEstateTypes_IsDeleted");

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Setting>(entity =>
            {
                entity.HasIndex(e => e.IsDeleted, "IX_Settings_IsDeleted");
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.HasIndex(e => e.IsDeleted, "IX_Tags_IsDeleted");

                entity.Property(e => e.Name).IsRequired();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
