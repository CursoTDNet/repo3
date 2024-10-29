using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

public class ComprasDbContext : DbContext
{
    private readonly IConfiguration _configuration;
    
    public DbSet<Producto> Productos { get; set; }
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Pedido> Pedidos { get; set; }
    public DbSet<PedidoProducto> PedidoProductos { get; set; }
    public DbSet<TipoUnidad> Tipounidades { get; set; }

    // Constructor que usa la inyección de dependencias
    public ComprasDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    // Constructor que recibe DbContextOptions
    public ComprasDbContext(DbContextOptions<ComprasDbContext> options)
        : base(options)
    {
    }

    // Constructor sin parámetros para permitir el uso de Entity Framework Tools
    public ComprasDbContext() { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var databaseProvider = _configuration?["DatabaseProvider"] ?? "SqlServer";
            var connectionString = string.Empty;

            switch (databaseProvider)
            {
                case "SqlServer":
                    connectionString = _configuration.GetConnectionString("SqlServer");
                    optionsBuilder.UseSqlServer(connectionString);
                    break;

                case "MySql":
                    connectionString = _configuration.GetConnectionString("MySql");
                    optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 21)));
                    break;

                case "Sqlite":
                    connectionString = _configuration.GetConnectionString("Sqlite");
                    optionsBuilder.UseSqlite(connectionString);
                    break;

                default:
                    throw new Exception("Database provider not supported");
            }
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configuración para las relaciones muchos a muchos
        modelBuilder.Entity<PedidoProducto>()
            .HasKey(pp => new { pp.PedidoId, pp.ProductoId });

        modelBuilder.Entity<PedidoProducto>()
            .HasOne(pp => pp.Pedido)
            .WithMany(p => p.PedidoProductos)
            .HasForeignKey(pp => pp.PedidoId);

        modelBuilder.Entity<PedidoProducto>()
            .HasOne(pp => pp.Producto)
            .WithMany(p => p.PedidoProductos)
            .HasForeignKey(pp => pp.ProductoId);
    }
}
