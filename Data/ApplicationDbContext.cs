using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using BarberSalonPrototype.Models;
using System.Text.Json;

namespace BarberSalonPrototype.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<StaffMember> StaffMembers { get; set; }
        public DbSet<GalleryImage> GalleryImages { get; set; }
        public DbSet<ContactMessage> ContactMessages { get; set; }
        public DbSet<StaffSpecialization> StaffSpecializations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Booking entity
            modelBuilder.Entity<Booking>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.PhoneNumber).IsRequired().HasMaxLength(20);
                entity.Property(e => e.AppointmentTime).IsRequired().HasMaxLength(10);
                entity.Property(e => e.SpecialRequests).HasMaxLength(500);
                
                // Navigation properties
                entity.HasOne(e => e.Service)
                    .WithMany()
                    .HasForeignKey(e => e.ServiceId)
                    .OnDelete(DeleteBehavior.Restrict);
                    
                entity.HasOne(e => e.StaffMember)
                    .WithMany()
                    .HasForeignKey(e => e.StaffMemberId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configure Service entity
            modelBuilder.Entity<Service>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).IsRequired().HasMaxLength(500);
                entity.Property(e => e.Price).IsRequired().HasMaxLength(20);
                entity.Property(e => e.ImageUrl).HasMaxLength(500);
            });

            // Configure StaffMember entity
            modelBuilder.Entity<StaffMember>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FullName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Role).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).IsRequired().HasMaxLength(1000);
                entity.Property(e => e.ImageUrl).HasMaxLength(500);
                entity.Property(e => e.InstagramUrl).HasMaxLength(500);
                entity.Property(e => e.FacebookUrl).HasMaxLength(500);
                entity.Property(e => e.TikTokUrl).HasMaxLength(500);
                // Remove JSON conversion for Specializations
                // Configure one-to-many relationship
                entity.HasMany(e => e.Specializations)
                    .WithOne(s => s.StaffMember)
                    .HasForeignKey(s => s.StaffMemberId)
                    .OnDelete(DeleteBehavior.Cascade);
                // Navigation property
                entity.HasMany(e => e.Gallery)
                    .WithOne(g => g.StaffMember)
                    .HasForeignKey(g => g.StaffMemberId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configure GalleryImage entity
            modelBuilder.Entity<GalleryImage>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.ImageUrl).IsRequired().HasMaxLength(500);
                entity.Property(e => e.Caption).HasMaxLength(200);
                entity.HasOne(g => g.StaffMember)
                    .WithMany(s => s.Gallery)
                    .HasForeignKey(g => g.StaffMemberId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configure ContactMessage entity
            modelBuilder.Entity<ContactMessage>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Subject).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Message).IsRequired().HasMaxLength(2000);
                entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            });
        }
    }
}