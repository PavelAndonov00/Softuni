namespace P01_StudentSystem.Data
{
    using Microsoft.EntityFrameworkCore;
    using P01_StudentSystem.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class StudentSystemContext : DbContext
    {
        public StudentSystemContext() { }

        public StudentSystemContext(DbContextOptions options)
            : base(options)
        { }

        public DbSet<Student> Students { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Resource> Resources { get; set; }

        public DbSet<Homework> HomeworkSubmissions { get; set; }

        public DbSet<StudentCourse> StudentCourses { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Config.connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ConfigureHomework(modelBuilder);
            ConfigureResource(modelBuilder);
            ConfigureCourse(modelBuilder);
            ConfigureStudent(modelBuilder);
            CofigureStudentCourse(modelBuilder);
        }

        private static void ConfigureHomework(ModelBuilder modelBuilder)
        {
            modelBuilder
                            .Entity<Homework>(entity =>
                            {
                                entity.HasKey(h => h.HomeworkId);
                            });
        }

        private static void ConfigureResource(ModelBuilder modelBuilder)
        {
            modelBuilder
                            .Entity<Resource>(entity =>
                            {
                                entity.HasKey(r => r.ResourceId);

                                entity
                                .Property(r => r.Name)
                                .HasMaxLength(50)
                                .IsUnicode();
                            });
        }

        private static void ConfigureCourse(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Course>(entity =>
                {
                    entity.HasKey(c => c.CourseId);

                    entity
                    .Property(c => c.Name)
                    .HasMaxLength(80)
                    .IsUnicode();

                    entity
                    .Property(c => c.Description)
                    .IsUnicode();
                });
        }

        private static void ConfigureStudent(ModelBuilder modelBuilder)
        {
            modelBuilder
                            .Entity<Student>(entity =>
                            {
                                entity.HasKey(s => s.StudentId);

                                entity
                                .Property(s => s.Name)
                                .HasMaxLength(100)
                                .IsUnicode();

                                entity
                                .Property(s => s.PhoneNumber)
                                .HasMaxLength(10);
                            });
        }

        private static void CofigureStudentCourse(ModelBuilder modelBuilder)
        {
            modelBuilder
                            .Entity<StudentCourse>(entity =>
                            {
                                entity.HasKey(sc => new { sc.StudentId, sc.CourseId });

                                entity
                                .HasOne(s => s.Student)
                                .WithMany(sc => sc.CourseEnrollments)
                                .HasForeignKey(sc => sc.StudentId);

                                entity
                                .HasOne(c => c.Course)
                                .WithMany(sc => sc.StudentsEnrolled)
                                .HasForeignKey(sc => sc.CourseId);
                            });
        }
    }
}
