using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WpfApp.Models;

public partial class FuhrmContext : DbContext
{
    public FuhrmContext()
    {
    }

    public FuhrmContext(DbContextOptions<FuhrmContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<ActivityLog> ActivityLogs { get; set; }

    public virtual DbSet<Attendance> Attendances { get; set; }

    public virtual DbSet<Backup> Backups { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<LeaveRequest> LeaveRequests { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Position> Positions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Salary> Salaries { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server =(local); database = FUHRM;uid=sa;pwd=123;TrustServerCertificate=True;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__Accounts__349DA5867887C247");

            entity.HasIndex(e => e.Username, "UQ__Accounts__536C85E4BAFB75A0").IsUnique();

            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.Username).HasMaxLength(255);

            entity.HasOne(d => d.Role).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Accounts__RoleID__4CA06362");
        });

        modelBuilder.Entity<ActivityLog>(entity =>
        {
            entity.HasKey(e => e.ActivityLogId).HasName("PK__Activity__19A9B78FF3311C09");

            entity.Property(e => e.ActivityLogId).HasColumnName("ActivityLogID");
            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.Action).HasMaxLength(255);
            entity.Property(e => e.Timestamp).HasColumnType("datetime");

            entity.HasOne(d => d.Account).WithMany(p => p.ActivityLogs)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ActivityL__Accou__619B8048");
        });

        modelBuilder.Entity<Attendance>(entity =>
        {
            entity.HasKey(e => e.AttendanceId).HasName("PK__Attendan__8B69263C0FED4ED0");

            entity.Property(e => e.AttendanceId).HasColumnName("AttendanceID");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.Employee).WithMany(p => p.Attendances)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Attendanc__Emplo__5BE2A6F2");
        });

        modelBuilder.Entity<Backup>(entity =>
        {
            entity.HasKey(e => e.BackupId).HasName("PK__Backups__EB9069E2DE9F1035");

            entity.Property(e => e.BackupId).HasColumnName("BackupID");
            entity.Property(e => e.BackupDate).HasColumnType("datetime");
            entity.Property(e => e.BackupFile).HasMaxLength(255);
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("PK__Departme__B2079BCDEE8A23DE");

            entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DepartmentName).HasMaxLength(100);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__7AD04FF1C2FCBB5B");

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");
            entity.Property(e => e.FullName).HasMaxLength(255);
            entity.Property(e => e.Gender).HasMaxLength(50);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            entity.Property(e => e.PositionId).HasColumnName("PositionID");
            entity.Property(e => e.ProfilePicture).HasMaxLength(255);

            entity.HasOne(d => d.Account).WithMany(p => p.Employees)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Employees__Accou__5629CD9C");

            entity.HasOne(d => d.Department).WithMany(p => p.Employees)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Employees__Depar__5441852A");

            entity.HasOne(d => d.Position).WithMany(p => p.Employees)
                .HasForeignKey(d => d.PositionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Employees__Posit__5535A963");
        });

        modelBuilder.Entity<LeaveRequest>(entity =>
        {
            entity.HasKey(e => e.LeaveRequestId).HasName("PK__LeaveReq__6094218E8F3AA229");

            entity.Property(e => e.LeaveRequestId).HasColumnName("LeaveRequestID");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.LeaveType).HasMaxLength(100);
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.Employee).WithMany(p => p.LeaveRequests)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LeaveRequ__Emplo__6477ECF3");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PK__Notifica__20CF2E32506811DD");

            entity.Property(e => e.NotificationId).HasColumnName("NotificationID");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");
            entity.Property(e => e.Title).HasMaxLength(255);

            entity.HasOne(d => d.Department).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Notificat__Depar__5EBF139D");
        });

        modelBuilder.Entity<Position>(entity =>
        {
            entity.HasKey(e => e.PositionId).HasName("PK__Position__60BB9A59D43D703E");

            entity.Property(e => e.PositionId).HasColumnName("PositionID");
            entity.Property(e => e.PositionName).HasMaxLength(100);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE3A02423A75");

            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.RoleName).HasMaxLength(100);
        });

        modelBuilder.Entity<Salary>(entity =>
        {
            entity.HasKey(e => e.SalaryId).HasName("PK__Salaries__4BE204B7CA3B7D07");

            entity.Property(e => e.SalaryId).HasColumnName("SalaryID");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.TotalIncome).HasComputedColumnSql("((([BaseSalary]+[Allowance])+[Bonus])-[Penalty])", true);

            entity.HasOne(d => d.Employee).WithMany(p => p.Salaries)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Salaries__Employ__59063A47");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
