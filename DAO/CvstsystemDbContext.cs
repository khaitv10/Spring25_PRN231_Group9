using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BOs.Models;

public partial class CvstsystemDbContext : DbContext
{
    public CvstsystemDbContext()
    {
    }

    public CvstsystemDbContext(DbContextOptions<CvstsystemDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<AppointmentService> AppointmentServices { get; set; }

    public virtual DbSet<Child> Children { get; set; }

    public virtual DbSet<DoseRecord> DoseRecords { get; set; }

    public virtual DbSet<DoseSchedule> DoseSchedules { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<ServiceVaccine> ServiceVaccines { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Vaccine> Vaccines { get; set; }

    public virtual DbSet<VaccineStock> VaccineStocks { get; set; }

    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
    //        => optionsBuilder.UseSqlServer("Server=(local);Database=CVSTSystemDB;Uid=sa;Pwd=123456;TrustServerCertificate=true");

    private string GetConnectionString()
    {
        IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true).Build();
        return configuration["ConnectionStrings:DefaultConnectionString"];
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(GetConnectionString());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Appointm__3214EC071D535606");

            entity.ToTable("Appointment");

            entity.Property(e => e.AppointmentDate).HasColumnType("datetime");
            entity.Property(e => e.PaymentStatus)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TotalPrice).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Child).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.ChildId)
                .HasConstraintName("FK__Appointme__Child__59063A47");

            entity.HasOne(d => d.Parent).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.ParentId)
                .HasConstraintName("FK__Appointme__Paren__59FA5E80");
        });

        modelBuilder.Entity<AppointmentService>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Appointm__3214EC0798B5904A");

            entity.ToTable("AppointmentService");

            entity.Property(e => e.DoseDate).HasColumnType("datetime");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Appointment).WithMany(p => p.AppointmentServices)
                .HasForeignKey(d => d.AppointmentId)
                .HasConstraintName("FK__Appointme__Appoi__5AEE82B9");

            entity.HasOne(d => d.Service).WithMany(p => p.AppointmentServices)
                .HasForeignKey(d => d.ServiceId)
                .HasConstraintName("FK__Appointme__Servi__5BE2A6F2");
        });

        modelBuilder.Entity<Child>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Child__3214EC075A02603A");

            entity.ToTable("Child");

            entity.Property(e => e.Dob).HasColumnName("DOB");
            entity.Property(e => e.FullName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Note).HasColumnType("text");

            entity.HasOne(d => d.Parent).WithMany(p => p.Children)
                .HasForeignKey(d => d.ParentId)
                .HasConstraintName("FK__Child__ParentId__5CD6CB2B");
        });

        modelBuilder.Entity<DoseRecord>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DoseReco__3214EC07789998E8");

            entity.ToTable("DoseRecord");

            entity.Property(e => e.CreateAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Child).WithMany(p => p.DoseRecords)
                .HasForeignKey(d => d.ChildId)
                .HasConstraintName("FK__DoseRecor__Child__5DCAEF64");

            entity.HasOne(d => d.Service).WithMany(p => p.DoseRecords)
                .HasForeignKey(d => d.ServiceId)
                .HasConstraintName("FK__DoseRecor__Servi__5EBF139D");

            entity.HasOne(d => d.Vaccine).WithMany(p => p.DoseRecords)
                .HasForeignKey(d => d.VaccineId)
                .HasConstraintName("FK__DoseRecor__Vacci__5FB337D6");
        });

        modelBuilder.Entity<DoseSchedule>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DoseSche__3214EC07047871F9");

            entity.ToTable("DoseSchedule");

            entity.Property(e => e.CreateAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Child).WithMany(p => p.DoseSchedules)
                .HasForeignKey(d => d.ChildId)
                .HasConstraintName("FK__DoseSched__Child__60A75C0F");

            entity.HasOne(d => d.Service).WithMany(p => p.DoseSchedules)
                .HasForeignKey(d => d.ServiceId)
                .HasConstraintName("FK__DoseSched__Servi__619B8048");

            entity.HasOne(d => d.Vaccine).WithMany(p => p.DoseSchedules)
                .HasForeignKey(d => d.VaccineId)
                .HasConstraintName("FK__DoseSched__Vacci__628FA481");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Feedback__3214EC0731993B14");

            entity.ToTable("Feedback");

            entity.Property(e => e.Comment).HasColumnType("text");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Appointment).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.AppointmentId)
                .HasConstraintName("FK__Feedback__Appoin__6383C8BA");

            entity.HasOne(d => d.Parent).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.ParentId)
                .HasConstraintName("FK__Feedback__Parent__6477ECF3");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PK__Notifica__20CF2E12AAFB9F15");

            entity.ToTable("Notification");

            entity.Property(e => e.CreateAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Message).HasColumnType("text");

            entity.HasOne(d => d.User).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Notificat__UserI__656C112C");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Payment__3214EC07B26BE8E1");

            entity.ToTable("Payment");

            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PaidAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PaymentStatus)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TransactionCode)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Appointment).WithMany(p => p.Payments)
                .HasForeignKey(d => d.AppointmentId)
                .HasConstraintName("FK__Payment__Appoint__66603565");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Service__3214EC0713ACA610");

            entity.ToTable("Service");

            entity.Property(e => e.CreateAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Status).HasDefaultValue(true);
            entity.Property(e => e.UpdateAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<ServiceVaccine>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ServiceV__3214EC079926E7A4");

            entity.ToTable("ServiceVaccine");

            entity.HasOne(d => d.Service).WithMany(p => p.ServiceVaccines)
                .HasForeignKey(d => d.ServiceId)
                .HasConstraintName("FK__ServiceVa__Servi__6754599E");

            entity.HasOne(d => d.Vaccine).WithMany(p => p.ServiceVaccines)
                .HasForeignKey(d => d.VaccineId)
                .HasConstraintName("FK__ServiceVa__Vacci__68487DD7");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3214EC07A68A44DD");

            entity.ToTable("User");

            entity.HasIndex(e => e.Email, "UQ__User__A9D10534E3B264EC").IsUnique();

            entity.Property(e => e.CreateAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FullName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Status).HasDefaultValue(true);
            entity.Property(e => e.UpdateAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<Vaccine>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Vaccine__3214EC07DA4763A9");

            entity.ToTable("Vaccine");

            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Origin)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Status).HasDefaultValue(true);
        });

        modelBuilder.Entity<VaccineStock>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__VaccineS__3214EC07F4B6E47D");

            entity.ToTable("VaccineStock");

            entity.Property(e => e.UpdateAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Vaccine).WithMany(p => p.VaccineStocks)
                .HasForeignKey(d => d.VaccineId)
                .HasConstraintName("FK__VaccineSt__Vacci__693CA210");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
