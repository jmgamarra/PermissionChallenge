using Microsoft.EntityFrameworkCore;
using PermissionsWeb.Domain;

public class PermissionsDbContext : DbContext
{
    public PermissionsDbContext(DbContextOptions<PermissionsDbContext> options) : base(options)
    {
    }

    public DbSet<Permiso> Permisos { get; set; }
    public DbSet<TipoPermiso> TiposPermiso { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Permiso>()
            .HasOne(p => p.TipoPermiso)
            .WithMany(t => t.Permisos)
            .HasForeignKey(p => p.TipoPermisoId);

        base.OnModelCreating(modelBuilder);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("N5Connection", options =>
                options.MigrationsAssembly("PermissionsWeb.Data"));
        }
    }
}
