CREATE DATABASE SistemaCompras;
USE SistemaCompras;

-- Tabla de Tipos de Unidades
CREATE TABLE Tipounidades (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Nombre VARCHAR(50) NOT NULL
);

-- Tabla de Productos
CREATE TABLE Productos (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Descripcion VARCHAR(255),
    Precio DECIMAL(10, 2) NOT NULL,
    TipoUnidadId INT NOT NULL,
    FOREIGN KEY (TipoUnidadId) REFERENCES Tipounidades(Id)
);

-- Tabla de Clientes
CREATE TABLE Clientes (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Direccion VARCHAR(255)
);

-- Tabla de Pedidos
CREATE TABLE Pedidos (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Fecha DATETIME NOT NULL,
    ClienteId INT NOT NULL,
    FOREIGN KEY (ClienteId) REFERENCES Clientes(Id)
);

-- Tabla de Pedido_Productos (relación muchos a muchos)
CREATE TABLE Pedido_Productos (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    PedidoId INT NOT NULL,
    ProductoId INT NOT NULL,
    Cantidad INT NOT NULL,
    FOREIGN KEY (PedidoId) REFERENCES Pedidos(Id),
    FOREIGN KEY (ProductoId) REFERENCES Productos(Id)
);
