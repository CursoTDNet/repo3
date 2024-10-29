using System;
using System.Collections.Generic;

public class Producto
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public decimal Precio { get; set; }
    public int TipoUnidadId { get; set; }
    public TipoUnidad TipoUnidad { get; set; }
    public ICollection<PedidoProducto> PedidoProductos { get; set; }
}

public class TipoUnidad
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public ICollection<Producto> Productos { get; set; }
}

public class Cliente
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Direccion { get; set; }
    public ICollection<Pedido> Pedidos { get; set; }
}

public class Pedido
{
    public int Id { get; set; }
    public DateTime Fecha { get; set; }
    public int ClienteId { get; set; }
    public Cliente Cliente { get; set; }
    public ICollection<PedidoProducto> PedidoProductos { get; set; }
}

public class PedidoProducto
{
    public int Id { get; set; }
    public int PedidoId { get; set; }
    public Pedido Pedido { get; set; }
    public int ProductoId { get; set; }
    public Producto Producto { get; set; }
    public int Cantidad { get; set; }
}
