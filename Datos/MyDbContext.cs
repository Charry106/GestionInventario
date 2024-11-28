using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GestionInventario.Datos
{
    public class MyDbContext : IdentityDbContext<ApplicationUser>
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {    
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<MovimientoInventario> MovimientosInventario { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Producto>()
                .HasOne(p => p.Proveedor)
                .WithMany(p => p.Productos)
                .HasForeignKey(p => p.ProveedorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MovimientoInventario>()
                .HasOne(m => m.Producto)
                .WithMany(p => p.Movimientos)
                .HasForeignKey(m => m.ProductoId)
                .OnDelete(DeleteBehavior.Cascade);
            
            
            base.OnModelCreating(modelBuilder);
        }
    }
}