using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebsiteBanThucAnNhanh18Admin.Data
{
    public partial class QLBHContext : DbContext
    {
        public QLBHContext()
        {
        }

        public QLBHContext(DbContextOptions<QLBHContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Branch> Branches { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Coupon> Coupons { get; set; } = null!;
        public virtual DbSet<CustomerAccount> CustomerAccounts { get; set; } = null!;
        public virtual DbSet<Food> Foods { get; set; } = null!;
        public virtual DbSet<ManagerAccount> ManagerAccounts { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderDetail> OrderDetails { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=QLBH;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Branch>(entity =>
            {
                entity.Property(e => e.BranchId).HasColumnName("branch_id");

                entity.Property(e => e.Address).HasColumnName("address");

                entity.Property(e => e.BranchName).HasColumnName("branch_name");

                entity.Property(e => e.CloseHour)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("close_hour");

                entity.Property(e => e.Ggmap).HasColumnName("ggmap");

                entity.Property(e => e.OpenHour)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("open_hour");

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("phone");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(50)
                    .HasColumnName("category_name");
            });

            modelBuilder.Entity<Coupon>(entity =>
            {
                entity.Property(e => e.CouponId).HasColumnName("coupon_id");

                entity.Property(e => e.CouponCode)
                    .HasMaxLength(20)
                    .HasColumnName("coupon_code");

                entity.Property(e => e.DiscountAmount)
                    .HasColumnType("decimal(15, 0)")
                    .HasColumnName("discount_amount");

                entity.Property(e => e.EndDate)
                    .HasColumnType("datetime")
                    .HasColumnName("end_date");

                entity.Property(e => e.MaxUsage).HasColumnName("max_usage");

                entity.Property(e => e.StartDate)
                    .HasColumnType("datetime")
                    .HasColumnName("start_date");
            });

            modelBuilder.Entity<CustomerAccount>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK_CustomerAccount");

                entity.HasIndex(e => e.Username, "uniq_ca")
                    .IsUnique();

                entity.HasIndex(e => e.Email, "uniq_ca_email")
                    .IsUnique();

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.Address).HasColumnName("address");

                entity.Property(e => e.Bod)
                    .HasColumnType("datetime")
                    .HasColumnName("bod");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Fullname).HasColumnName("fullname");

                entity.Property(e => e.Gender).HasColumnName("gender");

                entity.Property(e => e.Password).HasColumnName("password");

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("phone");

                entity.Property(e => e.Randomkey)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("randomkey");

                entity.Property(e => e.Username)
                    .HasMaxLength(20)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<Food>(entity =>
            {
                entity.Property(e => e.FoodId).HasColumnName("food_id");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Discount)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("discount");

                entity.Property(e => e.FoodName)
                    .HasMaxLength(50)
                    .HasColumnName("food_name");

                entity.Property(e => e.Idx).HasColumnName("idx");

                entity.Property(e => e.Picture)
                    .HasMaxLength(100)
                    .HasColumnName("picture");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(15, 0)")
                    .HasColumnName("price");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Foods)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_Foods_Categories");
            });

            modelBuilder.Entity<ManagerAccount>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK_MgrAccounts");

                entity.HasIndex(e => e.Email, "uique_email")
                    .IsUnique();

                entity.HasIndex(e => e.Username, "uique_username")
                    .IsUnique();

                entity.HasIndex(e => e.Username, "uniq_ma")
                    .IsUnique();

                entity.HasIndex(e => e.Email, "uniq_ma_email")
                    .IsUnique();

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Fullname).HasColumnName("fullname");

                entity.Property(e => e.Password).HasColumnName("password");

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("phone");

                entity.Property(e => e.Randomkey)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("randomkey");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.Username)
                    .HasMaxLength(20)
                    .HasColumnName("username");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.ManagerAccounts)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ManagerAccounts_Roles");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.Address).HasColumnName("address");

                entity.Property(e => e.BranchId).HasColumnName("branch_id");

                entity.Property(e => e.CouponId).HasColumnName("coupon_id");

                entity.Property(e => e.Note).HasColumnName("note");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("datetime")
                    .HasColumnName("order_date");

                entity.Property(e => e.Payment)
                    .HasMaxLength(20)
                    .HasColumnName("payment");

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("phone");

                entity.Property(e => e.Status)
                    .HasMaxLength(20)
                    .HasColumnName("status");

                entity.Property(e => e.Total)
                    .HasColumnType("decimal(12, 0)")
                    .HasColumnName("total");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.BranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_Branches");

                entity.HasOne(d => d.Coupon)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CouponId)
                    .HasConstraintName("FK_Orders_Coupons");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_CustomerAccounts");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.FoodId });

                entity.ToTable("Order_Details");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.FoodId).HasColumnName("food_id");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(12, 0)")
                    .HasColumnName("price");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Food)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.FoodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Details_Foods");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Details_Orders");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.RoleId)
                    .ValueGeneratedNever()
                    .HasColumnName("role_id");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(50)
                    .HasColumnName("role_name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
