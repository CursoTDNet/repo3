using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

class Program
{
    static void Main()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())  // Establecer la ruta base para encontrar el archivo appsettings.json
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        // Obtener la cadena de conexión desde la configuración
        var connectionString = configuration.GetConnectionString("SqlServer");

        // Crear el contexto de la base de datos con la cadena de conexión desde appsettings.json
        var optionsBuilder = new DbContextOptionsBuilder<ComprasDbContext>();
        optionsBuilder.UseSqlServer(connectionString);

        using (var context = new ComprasDbContext(optionsBuilder.Options))
        {
            var salir = false;
            while (!salir)
            {
                Console.WriteLine("===== Menú de Productos =====");
                Console.WriteLine("1. Crear producto");
                Console.WriteLine("2. Consultar productos");
                Console.WriteLine("3. Actualizar producto");
                Console.WriteLine("4. Eliminar producto");
                Console.WriteLine("5. Salir");
                Console.WriteLine("=============================");
                Console.Write("Selecciona una opción: ");

                var opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        CrearProducto();
                        break;
                    case "2":
                        ConsultarProductos();
                        break;
                    case "3":
                        ActualizarProducto();
                        break;
                    case "4":
                        EliminarProducto();
                        break;
                    case "5":
                        salir = true;
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Intente de nuevo.");
                        break;
                }
            }
        }
    }

    static void CrearProducto()
    {
        using (var context = new ComprasDbContext())
        {
            Console.Write("Ingrese el nombre del producto: ");
            var nombre = Console.ReadLine();
            Console.Write("Ingrese la descripción del producto: ");
            var descripcion = Console.ReadLine();
            Console.Write("Ingrese el precio del producto: ");
            var precio = decimal.Parse(Console.ReadLine());
            Console.Write("Ingrese el nombre del tipo de unidad (e.g., Kilogramo): ");
            var nombreUnidad = Console.ReadLine();

            // Buscar o crear el tipo de unidad
            var unidad = context.Tipounidades.FirstOrDefault(u => u.Nombre == nombreUnidad);
            if (unidad == null)
            {
                unidad = new TipoUnidad { Nombre = nombreUnidad };
                context.Tipounidades.Add(unidad);
                context.SaveChanges();
            }

            // Crear el producto
            var producto = new Producto
            {
                Nombre = nombre,
                Descripcion = descripcion,
                Precio = precio,
                TipoUnidadId = unidad.Id
            };
            context.Productos.Add(producto);
            context.SaveChanges();
            Console.WriteLine("Producto creado exitosamente.");
        }
    }

    static void ConsultarProductos()
    {
        using (var context = new ComprasDbContext())
        {
            var productos = context.Productos.Include(p => p.TipoUnidad).ToList();
            Console.WriteLine("===== Lista de productos =====");
            foreach (var p in productos)
            {
                Console.WriteLine($"ID: {p.Id}, Nombre: {p.Nombre}, Descripción: {p.Descripcion}, Precio: ${p.Precio}, Unidad: {p.TipoUnidad.Nombre}");
            }
            Console.WriteLine("==============================");
        }
    }

    static void ActualizarProducto()
    {
        using (var context = new ComprasDbContext())
        {
            Console.Write("Ingrese el ID del producto que desea actualizar: ");
            var id = int.Parse(Console.ReadLine());

            var producto = context.Productos.FirstOrDefault(p => p.Id == id);
            if (producto != null)
            {
                Console.Write("Nuevo nombre (presiona Enter para mantener el actual): ");
                var nuevoNombre = Console.ReadLine();
                Console.Write("Nueva descripción (presiona Enter para mantener la actual): ");
                var nuevaDescripcion = Console.ReadLine();
                Console.Write("Nuevo precio (presiona Enter para mantener el actual): ");
                var nuevoPrecioInput = Console.ReadLine();

                if (!string.IsNullOrEmpty(nuevoNombre))
                    producto.Nombre = nuevoNombre;
                if (!string.IsNullOrEmpty(nuevaDescripcion))
                    producto.Descripcion = nuevaDescripcion;
                if (decimal.TryParse(nuevoPrecioInput, out var nuevoPrecio))
                    producto.Precio = nuevoPrecio;

                context.SaveChanges();
                Console.WriteLine("Producto actualizado exitosamente.");
            }
            else
            {
                Console.WriteLine("Producto no encontrado.");
            }
        }
    }

    static void EliminarProducto()
    {
        using (var context = new ComprasDbContext())
        {
            Console.Write("Ingrese el ID del producto que desea eliminar: ");
            var id = int.Parse(Console.ReadLine());

            var producto = context.Productos.FirstOrDefault(p => p.Id == id);
            if (producto != null)
            {
                context.Productos.Remove(producto);
                context.SaveChanges();
                Console.WriteLine("Producto eliminado exitosamente.");
            }
            else
            {
                Console.WriteLine("Producto no encontrado.");
            }
        }
    }
}
