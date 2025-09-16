using EduApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EduApi.Infrastructure.Data
{
    public class EduApiContext : DbContext
    {
        public EduApiContext(DbContextOptions<EduApiContext> options) 
            : base(options) { }

        public DbSet<City> Cities => Set<City>();
        public DbSet<Department> Departments => Set<Department>();
        public DbSet<Student> Students => Set<Student>();
        public DbSet<Teacher> Teachers => Set<Teacher>();
        public DbSet<Subject> Subjects => Set<Subject>();
        public DbSet<StudentSubject> StudentSubjects => Set<StudentSubject>();
        public DbSet<TeacherSubject> TeacherSubjects => Set<TeacherSubject>();

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var now = DateTime.UtcNow;
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                if (entry.State == EntityState.Added) entry.Entity.CreatedDate = now;
                if (entry.State == EntityState.Modified) entry.Entity.LastUpdatedDate = now;
                if (entry.State == EntityState.Deleted) entry.Entity.IsDeleted = true;
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<City>().HasQueryFilter(c => !c.IsDeleted);
            modelBuilder.Entity<Department>().HasQueryFilter(d => !d.IsDeleted);
            modelBuilder.Entity<Student>().HasQueryFilter(s => !s.IsDeleted);

            modelBuilder.Entity<Student>()
                .HasOne(s => s.City)
                .WithMany(s => s.Students)
                .HasForeignKey(s => s.CityId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Student>()
                .HasOne(s => s.Department)
                .WithMany(s => s.Students)
                .HasForeignKey(s => s.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Teacher>()
                .HasOne(t => t.City)
                .WithMany(t => t.Teachers)
                .HasForeignKey(t => t.CityId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Teacher>()
                .HasMany(t => t.Subjects)
                .WithMany(ts => ts.Teachers)
                .UsingEntity<TeacherSubject>();

            modelBuilder.Entity<Subject>()
                .HasMany(s => s.Teachers)
                .WithMany(ts => ts.Subjects)
                .UsingEntity<TeacherSubject>();

            modelBuilder.Entity<City>().HasData(
                new City { Id = 1, Name = "Tashkent" },
                new City { Id = 2, Name = "Bukhara" },
                new City { Id = 3, Name = "Samarkand" },
                new City { Id = 4, Name = "Andijan" },
                new City { Id = 5, Name = "Fergana" },
                new City { Id = 6, Name = "Namangan" },
                new City { Id = 7, Name = "Jizzakh" },
                new City { Id = 8, Name = "Navai" },
                new City { Id = 9, Name = "Kashkadarya" },
                new City { Id = 10, Name = "Surkhandarya" },
                new City { Id = 11, Name = "Sirdarya" },
                new City { Id = 12, Name = "Karakalpakstan" }
            );

            modelBuilder.Entity<Subject>().HasData(
                new Subject { Id = 1, Name = "Mathematics" },
                new Subject { Id = 2, Name = "History" },
                new Subject { Id = 3, Name = "Physics" },
                new Subject { Id = 4, Name = "English Language" },
                new Subject { Id = 5, Name = "Russian Language" },
                new Subject { Id = 6, Name = "Biology" },
                new Subject { Id = 7, Name = "Computer Science" },
                new Subject { Id = 8, Name = "Geography" },
                new Subject { Id = 9, Name = "Literature" },
                new Subject { Id = 10, Name = "Chemistry" }
            );

            modelBuilder.Entity<Department>().HasData(
                new Department { Id = 1, Name = "Department-1" },
                new Department { Id = 2, Name = "Department-2" },
                new Department { Id = 3, Name = "Department-3" },
                new Department { Id = 4, Name = "Department-4" },
                new Department { Id = 5, Name = "Department-5" },
                new Department { Id = 6, Name = "Department-6" },
                new Department { Id = 7, Name = "Department-7" },
                new Department { Id = 8, Name = "Department-8" },
                new Department { Id = 9, Name = "Department-9" },
                new Department { Id = 10, Name = "Department-10" }
            );
        }
    }
}
