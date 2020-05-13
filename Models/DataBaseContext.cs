#region Using
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
#endregion

namespace smart_table.Models.DataBase
{
    public partial class DataBaseContext : DbContext
    {

        public DataBaseContext()
        {
        }

        public DataBaseContext(DbContextOptions<DataBaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bills> Bills { get; set; }
        public virtual DbSet<CustomerTables> CustomerTables { get; set; }
        public virtual DbSet<Discounts> Discounts { get; set; }
        public virtual DbSet<DishCategories> DishCategories { get; set; }
        public virtual DbSet<DishIngredients> DishIngredients { get; set; }
        public virtual DbSet<Dishes> Dishes { get; set; }
        public virtual DbSet<EventType> EventType { get; set; }
        public virtual DbSet<Events> Events { get; set; }
        public virtual DbSet<Ingredients> Ingredients { get; set; }
        public virtual DbSet<MenuDishes> MenuDishes { get; set; }
        public virtual DbSet<Menus> Menus { get; set; }
        public virtual DbSet<OrderDishes> OrderDishes { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<RegisteredUsers> RegisteredUsers { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bills>(entity =>
            {
                entity.ToTable("bills");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.DateTime)
                    .HasColumnName("date_time")
                    .HasDefaultValueSql("CURRENT_DATE");

                entity.Property(e => e.Evaluation)
                    .IsRequired()
                    .HasColumnName("evaluation")
                    .HasMaxLength(255);

                entity.Property(e => e.FkDiscounts).HasColumnName("fk_discounts");

                entity.Property(e => e.IsPaid).HasColumnName("is_paid");

                entity.Property(e => e.Tips).HasColumnName("tips");

                entity.HasOne(d => d.FkDiscountsNavigation)
                    .WithMany(p => p.Bills)
                    .HasForeignKey(d => d.FkDiscounts)
                    .HasConstraintName("fkc_discounts");
            });

            modelBuilder.Entity<CustomerTables>(entity =>
            {
                entity.ToTable("customer_tables");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IsTaken).HasColumnName("is_taken");

                entity.Property(e => e.JoinCode)
                    .HasColumnName("join_code")
                    .HasMaxLength(255);

                entity.Property(e => e.QrCode)
                    .HasColumnName("qr_code")
                    .HasMaxLength(255);

                entity.Property(e => e.SeatsCount).HasColumnName("seats_count");
            });

            modelBuilder.Entity<Discounts>(entity =>
            {
                entity.ToTable("discounts");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DiscountCode)
                    .IsRequired()
                    .HasColumnName("discount_code")
                    .HasMaxLength(255);

                entity.Property(e => e.DiscountProc).HasColumnName("discount_proc");

                entity.Property(e => e.StandUntil).HasColumnName("stand_until");
            });

            modelBuilder.Entity<DishCategories>(entity =>
            {
                entity.ToTable("dish_categories");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<DishIngredients>(entity =>
            {
                entity.HasKey(e => new { e.FkDishes, e.FkIngredients })
                    .HasName("dish_ingredients_pkey");

                entity.ToTable("dish_ingredients");

                entity.Property(e => e.FkDishes).HasColumnName("fk_dishes");

                entity.Property(e => e.FkIngredients).HasColumnName("fk_ingredients");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.FkDishesNavigation)
                    .WithMany(p => p.DishIngredients)
                    .HasForeignKey(d => d.FkDishes)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkc_dishes");

                entity.HasOne(d => d.FkIngredientsNavigation)
                    .WithMany(p => p.DishIngredients)
                    .HasForeignKey(d => d.FkIngredients)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkc_ingredients");
            });

            modelBuilder.Entity<Dishes>(entity =>
            {
                entity.ToTable("dishes");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Calories).HasColumnName("calories");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(255);

                entity.Property(e => e.Discount).HasColumnName("discount");

                entity.Property(e => e.FkDishCategories).HasColumnName("fk_dish_categories");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasMaxLength(255);

                entity.HasOne(d => d.FkDishCategoriesNavigation)
                    .WithMany(p => p.Dishes)
                    .HasForeignKey(d => d.FkDishCategories)
                    .HasConstraintName("fkc_dish_categories");
            });

            modelBuilder.Entity<EventType>(entity =>
            {
                entity.ToTable("event_type");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(12)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Events>(entity =>
            {
                entity.ToTable("events");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.FkOrders).HasColumnName("fk_orders");

                entity.Property(e => e.Type).HasColumnName("type");

                entity.HasOne(d => d.FkOrdersNavigation)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.FkOrders)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkc_orders");

                entity.HasOne(d => d.TypeNavigation)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.Type)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("events_type_fkey");
            });

            modelBuilder.Entity<Ingredients>(entity =>
            {
                entity.ToTable("ingredients");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<MenuDishes>(entity =>
            {
                entity.HasKey(e => new { e.FkDishes, e.FkMenus })
                    .HasName("menu_dishes_pkey");

                entity.ToTable("menu_dishes");

                entity.Property(e => e.FkDishes).HasColumnName("fk_dishes");

                entity.Property(e => e.FkMenus).HasColumnName("fk_menus");

                entity.Property(e => e.DateFrom).HasColumnName("date_from");

                entity.Property(e => e.DateUntil).HasColumnName("date_until");

                entity.HasOne(d => d.FkDishesNavigation)
                    .WithMany(p => p.MenuDishes)
                    .HasForeignKey(d => d.FkDishes)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkc_dishes");

                entity.HasOne(d => d.FkMenusNavigation)
                    .WithMany(p => p.MenuDishes)
                    .HasForeignKey(d => d.FkMenus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkc_menus");
            });

            modelBuilder.Entity<Menus>(entity =>
            {
                entity.ToTable("menus");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DateFrom)
                    .HasColumnName("date_from")
                    .HasColumnType("date");

                entity.Property(e => e.DateUntil)
                    .HasColumnName("date_until")
                    .HasColumnType("date");

                entity.Property(e => e.Fri).HasColumnName("fri");

                entity.Property(e => e.IsActive).HasColumnName("is_active");

                entity.Property(e => e.Mon).HasColumnName("mon");

                entity.Property(e => e.Sat).HasColumnName("sat");

                entity.Property(e => e.Sun).HasColumnName("sun");

                entity.Property(e => e.Thu).HasColumnName("thu");

                entity.Property(e => e.TimeFrom)
                    .HasColumnName("time_from")
                    .HasColumnType("time without time zone");

                entity.Property(e => e.TimeUntil)
                    .HasColumnName("time_until")
                    .HasColumnType("time without time zone");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasMaxLength(255);

                entity.Property(e => e.Tue).HasColumnName("tue");

                entity.Property(e => e.Wed).HasColumnName("wed");
            });

            modelBuilder.Entity<OrderDishes>(entity =>
            {
                entity.HasKey(e => new { e.FkDishes, e.FkOrders })
                    .HasName("order_dishes_pkey");

                entity.ToTable("order_dishes");

                entity.Property(e => e.FkDishes).HasColumnName("fk_dishes");

                entity.Property(e => e.FkOrders).HasColumnName("fk_orders");

                entity.Property(e => e.Comment)
                    .HasColumnName("comment")
                    .HasMaxLength(255);

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.FkDishesNavigation)
                    .WithMany(p => p.OrderDishes)
                    .HasForeignKey(d => d.FkDishes)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkc_dishes");

                entity.HasOne(d => d.FkOrdersNavigation)
                    .WithMany(p => p.OrderDishes)
                    .HasForeignKey(d => d.FkOrders)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkc_orders");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.ToTable("orders");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DateTime)
                    .HasColumnName("date_time")
                    .HasDefaultValueSql("CURRENT_DATE");

                entity.Property(e => e.FkBills).HasColumnName("fk_bills");

                entity.Property(e => e.FkCustomerTables).HasColumnName("fk_customer_tables");

                entity.Property(e => e.FkRegisteredUsers).HasColumnName("fk_registered_users");

                entity.Property(e => e.Served).HasColumnName("served");

                entity.Property(e => e.Submitted).HasColumnName("submitted");

                entity.Property(e => e.Temperature).HasColumnName("temperature");

                entity.HasOne(d => d.FkBillsNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.FkBills)
                    .HasConstraintName("fkc_bills");

                entity.HasOne(d => d.FkCustomerTablesNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.FkCustomerTables)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkc_customer_tables");

                entity.HasOne(d => d.FkRegisteredUsersNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.FkRegisteredUsers)
                    .HasConstraintName("fkc_registered_users");
            });

            modelBuilder.Entity<RegisteredUsers>(entity =>
            {
                entity.ToTable("registered_users");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BirthDate)
                    .HasColumnName("birth_date")
                    .HasColumnType("date");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(255);

                entity.Property(e => e.IsBlocked).HasColumnName("is_blocked");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(255);

                entity.Property(e => e.Phone)
                    .HasColumnName("phone")
                    .HasMaxLength(255);

                entity.Property(e => e.Role).HasColumnName("role");

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasColumnName("surname")
                    .HasMaxLength(255);

                entity.HasOne(d => d.RoleNavigation)
                    .WithMany(p => p.RegisteredUsers)
                    .HasForeignKey(d => d.Role)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("registered_users_role_fkey");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.ToTable("user_role");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(13)
                    .IsFixedLength();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}