using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GestionInventario.Datos
{
    public class GestionInventarioDbContext : IdentityDbContext<Usuario>
    {
        public GestionInventarioDbContext(DbContextOptions<GestionInventarioDbContext> options) : base(options) { }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Movimiento> Movimientos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar precisión y escala para la propiedad PrecioUnitario en Producto
            modelBuilder.Entity<Producto>()
                .Property(p => p.PrecioUnitario)
                .HasPrecision(18, 2); // Establece precisión total de 18 y 2 decimales

             // Asegúrate de que CantidadDisponible se actualiza con los movimientos
            modelBuilder.Entity<Producto>()
                .Property(p => p.CantidadDisponible)
                .HasDefaultValue(0); // Valor por defecto para nuevos productos
        }

        }
    }
