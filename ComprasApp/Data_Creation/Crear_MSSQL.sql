CREATE DATABASE SistemaCompras;
GO

USE SistemaCompras;
GO

-- Tabla de Tipos de Unidades
CREATE TABLE Tipounidades (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(50) NOT NULL
);

-- Tabla de Productos
CREATE TABLE Productos (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(255),
    Precio DECIMAL(18,2) NOT NULL,
    TipoUnidadId INT NOT NULL,
    FOREIGN KEY (TipoUnidadId) REFERENCES Tipounidades(Id)
);

-- Tabla de Clientes
CREATE TABLE Clientes (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Direccion NVARCHAR(255)
);

-- Tabla de Pedidos
CREATE TABLE Pedidos (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Fecha DATETIME NOT NULL,
    ClienteId INT NOT NULL,
    FOREIGN KEY (ClienteId) REFERENCES Clientes(Id)
);

-- Tabla de Pedido_Productos (relación muchos a muchos)
CREATE TABLE Pedido_Productos (
    Id INT PRIMARY KEY IDENTITY(1,1),
    PedidoId INT NOT NULL,
    ProductoId INT NOT NULL,
    Cantidad INT NOT NULL,
    FOREIGN KEY (PedidoId) REFERENCES Pedidos(Id),
    FOREIGN KEY (ProductoId) REFERENCES Productos(Id)
);
