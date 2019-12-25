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
        public DbSet<LessonDAO> Lessons { get; set; }
        public DbSet<SubjectDAO> Subjects { get; set; }
        public DbSet<ClassDAO> Classes { get; set; }
        public DbSet<GradeDAO> Grades { get; set; }
        public DbSet<AttendanceDAO> Attendances { get; set; }
        public DbSet<NoteDAO> Notes { get; set; }
        public DbSet<StudentDAO> Students { get; set; }
        public DbSet<TeacherDAO> Teachers { get; set; }
        public DbSet<AdminDAO> Admins { get; set; }
        public DbSet<UserDAO> Users { get; set; }
        public DbSet<RoleDAO> Roles { get; set; }
        public DbSet<TeacherSubjectDAO> TeachersSubjects { get; set; }
        public DbSet<ClassSubjectDAO> ClassesSubjects { get; set; }

        public GradebookDbContext(DbContextOptions<GradebookDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClassSubjectDAO>()
                .HasKey(bc => new { bc.ClassId, bc.SubjectId });
            modelBuilder.Entity<ClassSubjectDAO>()
                .HasOne(bc => bc.Subject)
                .WithMany(b => b.ClassSubjects)
                .HasForeignKey(bc => bc.SubjectId);
            modelBuilder.Entity<ClassSubjectDAO>()
                .HasOne(bc => bc.Subject)
                .WithMany(c => c.ClassSubjects)
                .HasForeignKey(bc => bc.ClassId);

            modelBuilder.Entity<TeacherSubjectDAO>()
    .HasKey(bc => new { bc.TeacherId, bc.SubjectId });
            modelBuilder.Entity<TeacherSubjectDAO>()
                .HasOne(bc => bc.Subject)
                .WithMany(b => b.TeacherSubjects)
                .HasForeignKey(bc => bc.SubjectId);
            modelBuilder.Entity<TeacherSubjectDAO>()
                .HasOne(bc => bc.Subject)
                .WithMany(c => c.TeacherSubjects)
                .HasForeignKey(bc => bc.TeacherId);
        }
    }
}
