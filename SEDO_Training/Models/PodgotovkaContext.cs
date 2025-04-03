using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SEDO_Training.Models;

public partial class PodgotovkaContext : DbContext
{
    public PodgotovkaContext()
    {
    }

    public PodgotovkaContext(DbContextOptions<PodgotovkaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Questions1> Questions1s { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Test> Tests { get; set; }

    public virtual DbSet<Test4> Test4s { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UsersTest> UsersTests { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=podgotovka;Username=postgres;Password=12345");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("courses_pkey");

            entity.ToTable("courses");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Photo)
                .HasMaxLength(255)
                .HasColumnName("photo");
            entity.Property(e => e.Uk).HasColumnName("uk");
        });

        modelBuilder.Entity<Questions1>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("questions1_pkey");

            entity.ToTable("questions1");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Answer1)
                .HasMaxLength(500)
                .HasColumnName("answer1");
            entity.Property(e => e.Answer2)
                .HasMaxLength(500)
                .HasColumnName("answer2");
            entity.Property(e => e.Answer3)
                .HasMaxLength(500)
                .HasColumnName("answer3");
            entity.Property(e => e.Correctanswerindex).HasColumnName("correctanswerindex");
            entity.Property(e => e.Questiontext)
                .HasMaxLength(500)
                .HasColumnName("questiontext");
            entity.Property(e => e.Selectedanswerindex).HasColumnName("selectedanswerindex");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("roles_pkey");

            entity.ToTable("roles");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Role1)
                .HasMaxLength(50)
                .HasColumnName("role");
        });

        modelBuilder.Entity<Test>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("tests_pkey");

            entity.ToTable("tests");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Photo)
                .HasMaxLength(255)
                .HasColumnName("photo");
            entity.Property(e => e.Points).HasColumnName("points");
        });

        modelBuilder.Entity<Test4>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("test4_pkey");

            entity.ToTable("test4");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .HasColumnName("description");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnName("title");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => e.Login, "users_login_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Login)
                .HasMaxLength(50)
                .HasColumnName("login");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Role).HasColumnName("role");

            entity.HasOne(d => d.RoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.Role)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("users_role_fkey");
        });

        modelBuilder.Entity<UsersTest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_tests_pkey");

            entity.ToTable("users_tests");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Points).HasColumnName("points");
            entity.Property(e => e.Tests).HasColumnName("tests");
            entity.Property(e => e.Users).HasColumnName("users");

            entity.HasOne(d => d.TestsNavigation).WithMany(p => p.UsersTests)
                .HasForeignKey(d => d.Tests)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("users_tests_tests_fkey");

            entity.HasOne(d => d.UsersNavigation).WithMany(p => p.UsersTests)
                .HasForeignKey(d => d.Users)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("users_tests_users_fkey");
        });
        modelBuilder.HasSequence("users_id_seq");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
