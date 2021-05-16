using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ExampleMvcApp.Models.Database
{
    public partial class ExampleDbContext : DbContext
    {
        public ExampleDbContext()
        {
        }

        public ExampleDbContext(DbContextOptions<ExampleDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<SubDepartment> SubDepartments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("Department");

                entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");

                entity.Property(e => e.AddedDate).HasColumnType("datetime");

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.DepartmentName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.AddedDate).HasColumnType("datetime");

                entity.Property(e => e.Bio)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.FbprofileLink)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("FBProfileLink");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProfileImage)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SubDepartmentId).HasColumnName("SubDepartmentID");

                entity.Property(e => e.TwitterProfileLink)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.SubDepartment)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.SubDepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employee_Department");
            });

            modelBuilder.Entity<SubDepartment>(entity =>
            {
                entity.ToTable("SubDepartment");

                entity.Property(e => e.SubDepartmentId).HasColumnName("SubDepartmentID");

                entity.Property(e => e.AddedDate).HasColumnType("datetime");

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");

                entity.Property(e => e.SubDepartmentName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.SubDepartments)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SubDepartment_Department");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
