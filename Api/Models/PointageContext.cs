using Microsoft.EntityFrameworkCore;

namespace Api.Models
{
    public class PointageContext : DbContext
    {
        public PointageContext(DbContextOptions<PointageContext> options) : base(options) { }
        public DbSet<Student> Students { get; set; }
        public DbSet<Filiere> Filieres { get; set; }
        public DbSet<Attendance> Attendances { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Attendance>()
                .HasKey(a => new { a.AttendanceId, a.CIN });

            modelBuilder.Entity<Filiere>()
                .HasIndex(f => f.FiliereName)
                .IsUnique(); 

            modelBuilder.Entity<Attendance>()
                  .HasOne(a => a.Student)
                  .WithMany(s => s.Attendances)
                  .HasForeignKey(a => a.CIN)
                  .HasPrincipalKey(s => s.CIN);

            modelBuilder.Entity<Student>()
               .HasIndex(s => s.CIN)
               .IsUnique();
        }

    }
    }

