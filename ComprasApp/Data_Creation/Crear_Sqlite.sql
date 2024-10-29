-- En SQLite no hay una instrucción explícita para crear bases de datos. 
-- Solo necesitas especificar el archivo donde se almacenará la base de datos.

-- Tabla de Tipos de Unidades
CREATE TABLE Tipounidades (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Nombre TEXT NOT NULL
);

-- Tabla de Productos
CREATE TABLE Productos (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Nombre TEXT NOT NULL,
    Descripcion TEXT,
    Precio REAL NOT NULL,
    TipoUnidadId INTEGER NOT NULL,
    FOREIGN KEY (TipoUnidadId) REFERENCES Tipounidades(Id)
);

-- Tabla de Clientes
CREATE TABLE Clientes (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Nombre TEXT NOT NULL,
    Direccion TEXT
);

-- Tabla de Pedidos
CREATE TABLE Pedidos (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Fecha TEXT NOT NULL,
    ClienteId INTEGER NOT NULL,
    FOREIGN KEY (ClienteId) REFERENCES Clientes(Id)
);

-- Tabla de Pedido_Productos (relación muchos a muchos)
CREATE TABLE Pedido_Productos (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    PedidoId INTEGER NOT NULL,
    ProductoId INTEGER NOT NULL,
    Cantidad INTEGER NOT NULL,
    FOREIGN KEY (PedidoId) REFERENCES Pedidos(Id),
    FOREIGN KEY (ProductoId) REFERENCES Productos(Id)
);
