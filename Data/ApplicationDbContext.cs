using Microsoft.EntityFrameworkCore;
using parcial.Models;

namespace parcial.Data
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Marca> Marcas { get; set; }

        public virtual DbSet<Vehiculo> Vehiculos { get; set; }

        public virtual DbSet<Venta> Ventas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Puedes agregar aquí la configuración de la base de datos, si es necesario.
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Marca>(entity =>
            {
                entity.HasKey(e => e.MarcaId).HasName("PK_Marca");

                entity.ToTable("Marca");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Vehiculo>(entity =>
            {
                entity.HasKey(e => e.VehiculoId).HasName("PK_Vehiculo");

                entity.ToTable("Vehiculo");

                entity.Property(e => e.VehiculoId).HasColumnName("VehiculoID");
                entity.Property(e => e.MarcaId).HasColumnName("MarcaID");
                entity.Property(e => e.Modelo).HasMaxLength(100).IsRequired(false);

                entity.HasOne(d => d.Marca).WithMany(p => p.Vehiculos)
                    .HasForeignKey(d => d.MarcaId)
                    .HasConstraintName("FK_Vehiculo_Marca");
            });

            modelBuilder.Entity<Venta>(entity =>
            {
                entity.HasKey(e => e.VentaId).HasName("PK_Venta");

                entity.ToTable("Venta");

                entity.Property(e => e.VentaId).HasColumnName("VentaID");
                entity.Property(e => e.TotalVentas).HasColumnType("float");  
                entity.Property(e => e.VehiculoId).HasColumnName("VehiculoID");

                entity.HasOne(d => d.Vehiculo).WithMany(p => p.Venta)
                    .HasForeignKey(d => d.VehiculoId)
                    .HasConstraintName("FK_Venta_Vehiculo");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}