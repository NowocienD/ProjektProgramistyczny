using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GradebookBackend.Model;

namespace GradebookBackend.Repositories
{
    public class GradebookDbContext : DbContext
    {
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        public GradebookDbContext(DbContextOptions<GradebookDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClassSubject>()
                .HasKey(bc => new { bc.ClassId, bc.SubjectId });
            modelBuilder.Entity<ClassSubject>()
                .HasOne(bc => bc.Subject)
                .WithMany(b => b.ClassSubjects)
                .HasForeignKey(bc => bc.SubjectId);
            modelBuilder.Entity<ClassSubject>()
                .HasOne(bc => bc.Subject)
                .WithMany(c => c.ClassSubjects)
                .HasForeignKey(bc => bc.ClassId);

            modelBuilder.Entity<TeacherSubject>()
    .HasKey(bc => new { bc.TeacherId, bc.SubjectId });
            modelBuilder.Entity<TeacherSubject>()
                .HasOne(bc => bc.Subject)
                .WithMany(b => b.TeacherSubjects)
                .HasForeignKey(bc => bc.SubjectId);
            modelBuilder.Entity<TeacherSubject>()
                .HasOne(bc => bc.Subject)
                .WithMany(c => c.TeacherSubjects)
                .HasForeignKey(bc => bc.TeacherId);
        }
    }
}
