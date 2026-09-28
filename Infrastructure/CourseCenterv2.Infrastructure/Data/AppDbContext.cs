using CourseCenterv2.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CourseCenterv2.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Student> Students => Set<Student>();
        public DbSet<Course> Courses => Set<Course>();

        public DbSet<Enrollment> Enrollments => Set<Enrollment>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.FirstName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(x => x.LastName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(x => x.Email)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(x => x.Mobile)
                    .HasMaxLength(20);
            });
            modelBuilder.Entity<Course>(entity =>
           {
               entity.HasKey(x => x.Id);
               entity.Property(x => x.Title).IsRequired().HasMaxLength(200);

               entity.Property(x => x.Capacity).IsRequired();

               entity.Property(x => x.Version).IsConcurrencyToken();


           }
           );
            modelBuilder.Entity<Enrollment>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.EnrolledAt).IsRequired();

                entity.HasOne(x => x.Student)
                .WithMany(x => x.Enrollments)
                .HasForeignKey(x => x.StudentId)
                .OnDelete(DeleteBehavior.Cascade);


                entity.HasOne(x => x.Course)
                .WithMany(x => x.Enrollments)
                .HasForeignKey(x => x.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(x => new
                {
                    x.StudentId,
                    x.CourseId

                }).IsUnique();

            });



        }


    }
}