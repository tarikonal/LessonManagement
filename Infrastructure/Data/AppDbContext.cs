using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Reflection.Emit;


namespace Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Family> Families { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Session> Sessions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Ensure this call is present

            modelBuilder.Entity<Family>()
                .HasMany(f => f.Members)
                .WithOne(p => p.Family)
                .HasForeignKey(p => p.FamilyId);

            modelBuilder.Entity<Student>()
                .HasMany(p => p.Sessions)
                .WithOne(s => s.Student)
                .HasForeignKey(s => s.StudentId);

            modelBuilder.Entity<Teacher>()
                .HasMany(t => t.Sessions)
                .WithOne(s => s.Teacher)
                .HasForeignKey(s => s.TeacherId);

            modelBuilder.Entity<Lesson>()
                .HasMany(l => l.Sessions)
                .WithOne(s => s.Lesson)
                .HasForeignKey(s => s.LessonId);

            modelBuilder.Entity<Session>()
                .Property(s => s.HourlyPrice)
                .HasColumnType("decimal(18,2)");
        }
    }
}

