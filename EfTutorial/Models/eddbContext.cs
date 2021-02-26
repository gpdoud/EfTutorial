using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace EfTutorial.Models {
    public partial class eddbContext : DbContext {
        public eddbContext() {
        }

        public eddbContext(DbContextOptions<eddbContext> options)
            : base(options) {
        }

        public virtual DbSet<Assignment> Assignments { get; set; }
        public virtual DbSet<Class> Classes { get; set; }
        public virtual DbSet<ClassGrade> ClassGrades { get; set; }
        public virtual DbSet<Instructor> Instructors { get; set; }
        public virtual DbSet<Major> Majors { get; set; }
        public virtual DbSet<MajorClass> MajorClasses { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<StudentClass> StudentClasses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if(!optionsBuilder.IsConfigured) {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server=localhost\\sqlexpress;database=eddb;trusted_connection=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Assignment>(entity => {
                entity.ToTable("Assignment");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.Assignments)
                    .HasForeignKey(d => d.ClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Assignmen__Class__33D4B598");
            });

            modelBuilder.Entity<Class>(entity => {
                entity.ToTable("Class");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.Subject)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.Instructor)
                    .WithMany(p => p.Classes)
                    .HasForeignKey(d => d.InstructorId)
                    .HasConstraintName("FK__Class__Instructo__30F848ED");
            });

            modelBuilder.Entity<ClassGrade>(entity => {
                entity.HasKey(e => e.Grade)
                    .HasName("PK__ClassGra__DF0ADB7BA13B40B6");

                entity.ToTable("ClassGrade");

                entity.Property(e => e.Grade)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Gpa)
                    .HasColumnType("decimal(4, 2)")
                    .HasColumnName("GPA");
            });

            modelBuilder.Entity<Instructor>(entity => {
                entity.ToTable("Instructor");

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Lastname)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Major>(entity => {
                entity.ToTable("Major");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MinSat).HasColumnName("MinSAT");
            });

            modelBuilder.Entity<MajorClass>(entity => {
                entity.ToTable("MajorClass");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.MajorClasses)
                    .HasForeignKey(d => d.ClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MajorClas__Class__37A5467C");

                entity.HasOne(d => d.Major)
                    .WithMany(p => p.MajorClasses)
                    .HasForeignKey(d => d.MajorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MajorClas__Major__36B12243");
            });

            modelBuilder.Entity<Student>(entity => {
                entity.ToTable("Student");

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Gpa)
                    .HasColumnType("decimal(4, 2)")
                    .HasColumnName("GPA");

                entity.Property(e => e.Lastname)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Sat).HasColumnName("SAT");

                entity.Property(e => e.StateCode)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('OH')");

                entity.HasOne(d => d.Major)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.MajorId)
                    .HasConstraintName("FK__Student__MajorId__2A4B4B5E");
            });

            modelBuilder.Entity<StudentClass>(entity => {
                entity.ToTable("StudentClass");

                entity.Property(e => e.ClassGradeValue)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.HasOne(d => d.ClassGradeValueNavigation)
                    .WithMany(p => p.StudentClasses)
                    .HasForeignKey(d => d.ClassGradeValue)
                    .HasConstraintName("FK__StudentCl__Class__3C69FB99");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.StudentClasses)
                    .HasForeignKey(d => d.ClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__StudentCl__Class__3B75D760");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentClasses)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__StudentCl__Stude__3A81B327");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
