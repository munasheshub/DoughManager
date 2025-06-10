
using DoughManager.Data.BaseEntityModels;
using DoughManager.Data.EntityModels;
using DoughManager.Data.Migrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DoughManager.Data.DbContexts
{
    public class DoughManagerDbContext : DbContext
    {
        public DoughManagerDbContext(DbContextOptions<DoughManagerDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<Customer> Customers { get; set; }

        public virtual DbSet<DailyInventory> DailyInventories { get; set; }

        public virtual DbSet<DamagedLog> DamagedLogs { get; set; }


        public virtual DbSet<Dispatch> Dispatches { get; set; }

        public virtual DbSet<DispatchItem> DispatchItems { get; set; }

        public virtual DbSet<Order> Orders { get; set; }

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<ProductBatch> ProductBatches { get; set; }

        public virtual DbSet<ProductOrder> ProductOrders { get; set; }

        public virtual DbSet<ProductRawMaterial> ProductRawMaterials { get; set; }

        public virtual DbSet<ProductionBatch> ProductionBatches { get; set; }

        public virtual DbSet<ProductBatchRawMaterial> ProductBatchRawMaterials { get; set; }

        public virtual DbSet<RawMaterial> RawMaterials { get; set; }

        public virtual DbSet<RawMaterialInventory> RawMaterialInventories { get; set; }

        public virtual DbSet<ReceivingLog> ReceivingLogs { get; set; }

        public virtual DbSet<UserState> UserStates { get; set; }
        
        public DbSet<StockTake> StockTakes { get; set; }
        public DbSet<StockTakeStatus> StockTakeStatus { get; set; }
        public DbSet<DiscrepancyRecord> DiscrepancyRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
             

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");
            });

            modelBuilder.Entity<DailyInventory>(entity =>
            {
                entity.Property(e => e.Notes).HasMaxLength(500);

                entity.HasOne(d => d.Product).WithMany(p => p.DailyInventories).HasForeignKey(d => d.ProductId);
            });

            modelBuilder.Entity<DamagedLog>(entity =>
            {
                entity.Property(e => e.DamagedQuantity).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.ProductionBatch).WithMany(p => p.DamagedLogs).HasForeignKey(d => d.ProductionBatchId);
            });

            

            modelBuilder.Entity<Dispatch>(entity =>
            {

                entity.HasOne(d => d.Order).WithMany(p => p.Dispatches).HasForeignKey(d => d.OrderId);

                
            });

            modelBuilder.Entity<DispatchItem>(entity =>
            {
                entity.HasOne(d => d.Dispatch).WithMany(p => p.DispatchItems).HasForeignKey(d => d.DispatchId);

                entity.HasOne(d => d.Product).WithMany(p => p.DispatchItems).HasForeignKey(d => d.ProductId);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.Location)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.CategoryId).HasDefaultValue(1);
                entity.Property(e => e.IdealQuantity).HasColumnType("decimal(10, 2)");
                entity.Property(e => e.Name).HasMaxLength(255);
                entity.Property(e => e.SellingPrice).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.Category).WithMany(p => p.Products).HasForeignKey(d => d.CategoryId);
            });

            modelBuilder.Entity<ProductBatch>(entity =>
            {
                entity.HasOne(d => d.Product).WithMany(p => p.ProductBatches).HasForeignKey(d => d.ProductId);

                entity.HasOne(d => d.ProductionBatch).WithMany(p => p.ProductBatches).HasForeignKey(d => d.ProductionBatchId);
            });

            modelBuilder.Entity<ProductOrder>(entity =>
            {
                entity.Property(e => e.Quantity).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.Order).WithMany(p => p.ProductOrders).HasForeignKey(d => d.OrderId);

                entity.HasOne(d => d.Product).WithMany(p => p.ProductOrders).HasForeignKey(d => d.ProductId);

                entity.HasOne(d => d.ProductionBatch).WithMany(p => p.ProductOrders).HasForeignKey(d => d.ProductionBatchId);
            });

            modelBuilder.Entity<ProductRawMaterial>(entity =>
            {
                entity.Property(e => e.QuantityRequired).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.Product).WithMany(p => p.ProductRawMaterials).HasForeignKey(d => d.ProductId);

                entity.HasOne(d => d.RawMaterial).WithMany(p => p.ProductRawMaterials).HasForeignKey(d => d.RawMaterialId);
            });

            modelBuilder.Entity<ProductionBatch>(entity =>
            {
                entity.Property(e => e.Sector).HasMaxLength(50);
                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false);

               
            });

            modelBuilder.Entity<ProductBatchRawMaterial>(entity =>
            {
                entity.Property(e => e.QuantityUsed).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductionBatchRawMaterials)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.Restrict); // Prevent cascading deletes

                entity.HasOne(d => d.ProductBatch)
                    .WithMany(p => p.RawMaterialsUsed)
                    .HasForeignKey(d => d.ProductBatchId)
                    .OnDelete(DeleteBehavior.Restrict); // Prevent cascading deletes

                entity.HasOne(d => d.RawMaterial)
                    .WithMany(p => p.ProductBatchRawMaterials)
                    .HasForeignKey(d => d.RawMaterialId)
                    .OnDelete(DeleteBehavior.Restrict); // Prevent cascading deletes
            });

            modelBuilder.Entity<RawMaterial>(entity =>
            {
                entity.Property(e => e.CostPrice).HasColumnType("decimal(10, 2)");
                entity.Property(e => e.DamagedQuantity).HasColumnType("decimal(10, 2)");
                entity.Property(e => e.IdealQuantity).HasColumnType("decimal(10, 2)");
                entity.Property(e => e.Name).HasMaxLength(255);
                entity.Property(e => e.QuantityOnHand).HasColumnType("decimal(10, 2)");
            });

            modelBuilder.Entity<RawMaterialInventory>(entity =>
            {
                entity.ToTable("RawMaterialInventory");

                entity.HasOne(d => d.RawMaterial).WithMany(p => p.RawMaterialInventories).HasForeignKey(d => d.RawMaterialId);
            });

            modelBuilder.Entity<ReceivingLog>(entity =>
            {
                entity.Property(e => e.Condition)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.QuantityReceived).HasColumnType("decimal(10, 2)");
                entity.Property(e => e.Supplier).HasMaxLength(255);

                entity.HasOne(d => d.RawMaterial).WithMany(p => p.ReceivingLogs).HasForeignKey(d => d.RawMaterialId);
            });
            modelBuilder.Entity<Category>().HasData(_categories());
            
            ConfigureAuditedEntity<Account>(modelBuilder.Entity<Account>());
            ConfigureAuditedEntity<DailyInventory>(modelBuilder.Entity<DailyInventory>());
            ConfigureAuditedEntity<DamagedLog>(modelBuilder.Entity<DamagedLog>());
            ConfigureAuditedEntity<DiscrepancyRecord>(modelBuilder.Entity<DiscrepancyRecord>());
            ConfigureAuditedEntity<Dispatch>(modelBuilder.Entity<Dispatch>());
            ConfigureAuditedEntity<Order>(modelBuilder.Entity<Order>());
            ConfigureAuditedEntity<Product>(modelBuilder.Entity<Product>());
            ConfigureAuditedEntity<ProductRawMaterial>(modelBuilder.Entity<ProductRawMaterial>());
            ConfigureAuditedEntity<ProductionBatch>(modelBuilder.Entity<ProductionBatch>());
            ConfigureAuditedEntity<RawMaterial>(modelBuilder.Entity<RawMaterial>());
            ConfigureAuditedEntity<RawMaterialInventory>(modelBuilder.Entity<RawMaterialInventory>());
            ConfigureAuditedEntity<ReceivingLog>(modelBuilder.Entity<ReceivingLog>());
            
            
        }

        private List<Category> _categories()
        {
            return new List<Category>
                {
                    new Category { Id = 1, Name = "Uncategorized" },
                    new Category { Id = 2, Name = "Pastry" },
                    new Category { Id = 3, Name = "Bread" },
                    new Category { Id = 4, Name = "Speciality" }
                };
        }
        
        public static void ConfigureAuditedEntity<TEntity>(EntityTypeBuilder<TEntity> entity) where TEntity : AuditedEntity
        {
            // Configure the relationship for Creator
            entity.HasOne<Account>("Creator")
                .WithMany()
                .HasForeignKey("CreatorId")
                .OnDelete(DeleteBehavior.Restrict);

            // Configure the relationship for Deleter
            entity.HasOne<Account>("Deleter")
                .WithMany()
                .HasForeignKey("DeleterId")
                .OnDelete(DeleteBehavior.Restrict);

            // If the entity is of type FullAuditedAggregateRoot, configure LastModifierUser
            if (typeof(FullyAuditedEntity).IsAssignableFrom(typeof(TEntity)))
            {
                entity.HasOne<Account>("LastModifier")
                    .WithMany()
                    .HasForeignKey("LastModifierId")
                    .OnDelete(DeleteBehavior.Restrict);
            }
        }
    }
}
