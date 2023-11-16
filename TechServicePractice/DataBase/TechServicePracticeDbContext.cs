using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TechServicePractice;

public partial class TechServicePracticeDbContext : DbContext
{
    public TechServicePracticeDbContext()
    {
    }

    public TechServicePracticeDbContext(DbContextOptions<TechServicePracticeDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Appeal> Appeals { get; set; }

    public virtual DbSet<Request> Requests { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=TechServicePracticeDB;Integrated Security=True; TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appeal>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Appeal__3214EC0773256756");

            entity.ToTable("Appeal");

            entity.Property(e => e.AppealStatus)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.DateOfAppeal).HasColumnType("date");
            entity.Property(e => e.GadgetType)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ProblemDescription)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.HasOne(d => d.Client).WithMany(p => p.Appeals)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Appeal__ClientId__29572725");
        });

        modelBuilder.Entity<Request>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Request__3214EC07E1CDD9FB");

            entity.ToTable("Request");

            entity.Property(e => e.Commentary)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.CompleatingDate).HasColumnType("date");
            entity.Property(e => e.ProblemType)
                .HasMaxLength(300)
                .IsUnicode(false);

            entity.HasOne(d => d.Appeal).WithMany(p => p.Requests)
                .HasForeignKey(d => d.AppealId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Request__AppealI__2B3F6F97");

            entity.HasOne(d => d.Executor).WithMany(p => p.Requests)
                .HasForeignKey(d => d.ExecutorId)
                .HasConstraintName("FK__Request__Executo__2A4B4B5E");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07BEB57934");

            entity.Property(e => e.DateOfBirth).HasColumnType("date");
            entity.Property(e => e.Fio)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("FIO");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(12)
                .IsUnicode(false);
            entity.Property(e => e.UserPassword)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserRoler)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasDefaultValueSql("('Client')");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
