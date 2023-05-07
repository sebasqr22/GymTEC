-- Instituto Tecnológico de Costa Rica
-- Curso: Bases de Datos
-- Proyecto I - GymTEC
-- Script de creación
-- Estudiante: Eduardo Bolívar Minguet
-- Carné: 2020158103

CREATE DATABASE [GymTEC-DB];

USE [GymTEC-DB];

-- Tabla EMPLEADO
-- Informacion de los empleados del gimnasio.
CREATE TABLE EMPLEADO (
	Cedula INT NOT NULL,
	Nombre NVARCHAR(30) NOT NULL,
	Apellido1 NVARCHAR(30) NOT NULL,
	Apellido2 NVARCHAR(30),
	Distrito NVARCHAR(30),
	Canton NVARCHAR(30),
	Provincia NVARCHAR(30) NOT NULL,
	Correo NVARCHAR(30) NOT NULL,
	Contrasena NVARCHAR(30) NOT NULL,
	Salario FLOAT NOT NULL,
	Id_puesto INT NOT NULL,
	Id_planilla INT NOT NULL,
	Codigo_suc INT,
	PRIMARY KEY (Cedula)
);

-- Tabla SUCURSAL
-- Informacion de las sucursales del gimnasio.
CREATE TABLE SUCURSAL (
	Codigo_sucursal INT IDENTITY(1,1) NOT NULL,
	Nombre NVARCHAR(50) NOT NULL,
	Distrito NVARCHAR(50),
	Canton NVARCHAR(50) NOT NULL,
	Provincia NVARCHAR(50) NOT NULL,
	Fecha_apertura DATE,
	Hora_apertura TIME,
	Hora_cierre TIME,
	Max_capacidad INT,
	Cedula_administrador INT NOT NULL,
	PRIMARY KEY (Codigo_sucursal)
);

-- Tabla CLIENTE
-- Informacion de los usuarios de la aplicacion GymTEC.
CREATE TABLE CLIENTE (
	Cedula INT NOT NULL,
	Nombre NVARCHAR(50) NOT NULL,
	Apellido1 NVARCHAR(50),
	Apellido2 NVARCHAR(50),
	Dia_nacimiento VARCHAR(2) NOT NULL,
	Mes_nacimiento VARCHAR(2) NOT NULL,
	Ano_nacimiento VARCHAR(4) NOT NULL,
	Peso FLOAT NOT NULL,
	Direccion NVARCHAR(50),
	Correo NVARCHAR(50) NOT NULL,
	Contrasena NVARCHAR(50) NOT NULL,
	PRIMARY KEY (Cedula)
);

-- Tabla PUESTO
-- Puestos de trabajo disponibles para los empleados.
CREATE TABLE PUESTO (
	Identificador INT IDENTITY(1,1) NOT NULL,
	Descripcion NVARCHAR(50),
	PRIMARY KEY (Identificador)
);

-- Tabla PLANILLA
-- Planilla con la que reciben el pago los empleados.
CREATE TABLE PLANILLA (
	Identificador INT IDENTITY(1,1) NOT NULL,
	Descripcion NVARCHAR(50),
	PRIMARY KEY (Identificador)
);

-- Tabla SERVICIO
-- Servicios que ofrecen las sucursales del gimnasio.
CREATE TABLE SERVICIO (
	Identificador INT IDENTITY(1,1) NOT NULL,
	Descripcion NVARCHAR(50),
	PRIMARY KEY (Identificador)
);

-- Tabla TRATAMIENTO
-- Tratamientos dados dentro de los spas del gimnasio.
CREATE TABLE TRATAMIENTO (
	Identificador INT IDENTITY(1,1) NOT NULL,
	Nombre NVARCHAR(50),
	PRIMARY KEY (Identificador)
);

-- Tabla TIPO_EQUIPO
-- Tipo de equipo disponible en el gimnasio.
CREATE TABLE TIPO_EQUIPO (
	Identificador INT IDENTITY(1,1) NOT NULL,
	Descripcion NVARCHAR(50),
	PRIMARY KEY (Identificador)
);

-- Tabla INVENTARIO
-- Las maquinas disponibles en el gimnasio.
CREATE TABLE INVENTARIO (
	Numero_serie INT NOT NULL,
	Marca NVARCHAR(50) NOT NULL,
	Tipo INT NOT NULL,
	PRIMARY KEY (Numero_serie)
);

-- Tabla PRODUCTO
-- Informacion de los productos que venden las tiendas.
CREATE TABLE PRODUCTO (
	Codigo_barras INT NOT NULL,
	Nombre NVARCHAR(50) NOT NULL,
	Descripcion NVARCHAR(50),
	Costo FLOAT NOT NULL,
	PRIMARY KEY (Codigo_barras)
);

-- Tabla TIENDA
-- Tiendas dentro de las sucursales.
CREATE TABLE TIENDA (
	Codigo_sucursal INT NOT NULL,
	Estado BIT NOT NULL -- 0: Inactivo. 1: Activo.
	PRIMARY KEY (Codigo_sucursal)
);

-- Tabla SPA
-- Spas dentro de las sucursales.
CREATE TABLE SPA (
	Codigo_sucursal INT NOT NULL,
	Estado BIT NOT NULL -- 0: Inactivo. 1: Activo.
	PRIMARY KEY (Codigo_sucursal)
);

-- Tabla VENTA_PRODUCTO
-- Relaciona las tiendas con los productos que venden.
CREATE TABLE VENTA_PRODUCTO (
	Codigo_sucursal INT NOT NULL,
	Codigo_producto INT NOT NULL,
	PRIMARY KEY (Codigo_sucursal, Codigo_producto)
);

-- Tabla TRATAMIENTO_SPA
-- Relaciona los spas con los tratamientos que ofrecen.
CREATE TABLE TRATAMIENTO_SPA (
	Codigo_sucursal INT NOT NULL,
	Id_tratamiento INT NOT NULL,
	PRIMARY KEY (Codigo_sucursal, Id_tratamiento)
);

-- Tabla INVENTARIO_EN_SUCURSAL
-- Relaciona la sucursal con las maquinas que tiene disponibles.
CREATE TABLE INVENTARIO_EN_SUCURSAL (
	Codigo_sucursal INT NOT NULL,
	Num_serie_maquina INT NOT NULL,
	Costo_sucursal FLOAT NOT NULL,
	PRIMARY KEY (Codigo_sucursal, Num_serie_maquina)
);

-- Tabla SERVICIOS_EN_SUCURSAL
-- Relaciona la sucursal con los servicios que ofrece.
CREATE TABLE SERVICIOS_EN_SUCURSAL (
	Codigo_sucursal INT NOT NULL,
	Id_servicio INT NOT NULL,
	PRIMARY KEY (Codigo_sucursal, Id_servicio)
);

-- Tabla TELEFONO_SUCURSAL
-- Guarda los numeros de telefono de las sucursales
CREATE TABLE TELEFONO_SUCURSAL (
	Codigo_sucursal INT NOT NULL,
	Telefono NVARCHAR(13) NOT NULL,
	PRIMARY KEY (Codigo_sucursal, Telefono)
);

-- Tabla CLASE
-- Informacion de una clase impartida en una sucursal.
CREATE TABLE CLASE (
	Num_clase INT IDENTITY(1,1) NOT NULL,
	Id_servicio INT NOT NULL,
	Fecha DATE,
	Hora_inicio TIME,
	Hora_fin TIME,
	Modalidad NVARCHAR(50),
	Capacidad INT,
	Cedula_instructor INT NOT NULL,
	PRIMARY KEY (Id_servicio, Num_clase)
);

-- Tabla ASISTENCIA_CLASE
-- Relaciona a los clientes con la clase a la que se registran.
CREATE TABLE ASISTENCIA_CLASE (
	Cedula_cliente INT NOT NULL,
	Id_servicio INT NOT NULL,
	Num_clase INT NOT NULL,
	PRIMARY KEY (Cedula_cliente, Id_servicio, Num_clase)
);

ALTER TABLE EMPLEADO
ADD CONSTRAINT FK_PUESTO FOREIGN KEY (Id_puesto) REFERENCES PUESTO(Identificador);

ALTER TABLE EMPLEADO
ADD CONSTRAINT FK_PLANILLA FOREIGN KEY (Id_planilla) REFERENCES PLANILLA(Identificador);

ALTER TABLE EMPLEADO
ADD CONSTRAINT FK_SUCURSAL FOREIGN KEY (Codigo_suc) REFERENCES SUCURSAL(Codigo_sucursal);

ALTER TABLE SUCURSAL
ADD CONSTRAINT FK_ADMIN FOREIGN KEY (Cedula_administrador) REFERENCES EMPLEADO(Cedula);

ALTER TABLE INVENTARIO
ADD CONSTRAINT FK_TIPO FOREIGN KEY (Tipo) REFERENCES TIPO_EQUIPO(Identificador);

ALTER TABLE TIENDA
ADD CONSTRAINT FK_TIENDA FOREIGN KEY (Codigo_sucursal) REFERENCES SUCURSAL(Codigo_sucursal);

ALTER TABLE SPA
ADD CONSTRAINT PKFK_SPA FOREIGN KEY (Codigo_sucursal) REFERENCES SUCURSAL(Codigo_sucursal);

ALTER TABLE VENTA_PRODUCTO
ADD CONSTRAINT FK_VP_Store FOREIGN KEY (Codigo_sucursal) REFERENCES TIENDA(Codigo_sucursal);

ALTER TABLE VENTA_PRODUCTO
ADD CONSTRAINT FK_VP_Product FOREIGN KEY (Codigo_producto) REFERENCES PRODUCTO(Codigo_barras);

ALTER TABLE TRATAMIENTO_SPA
ADD CONSTRAINT FK_TS_Spa FOREIGN KEY (Codigo_sucursal) REFERENCES SPA(Codigo_sucursal);

ALTER TABLE TRATAMIENTO_SPA
ADD CONSTRAINT FK_TS_Treatment FOREIGN KEY (Id_tratamiento) REFERENCES TRATAMIENTO(Identificador);

ALTER TABLE INVENTARIO_EN_SUCURSAL
ADD CONSTRAINT FK_Inventory_Name FOREIGN KEY (Codigo_sucursal) REFERENCES SUCURSAL(Codigo_sucursal);

ALTER TABLE INVENTARIO_EN_SUCURSAL
ADD CONSTRAINT FK_Inventory_Series FOREIGN KEY (Num_serie_maquina) REFERENCES INVENTARIO(Numero_serie);

ALTER TABLE SERVICIOS_EN_SUCURSAL
ADD CONSTRAINT FK_Services_Name FOREIGN KEY (Codigo_sucursal) REFERENCES SUCURSAL(Codigo_sucursal);

ALTER TABLE SERVICIOS_EN_SUCURSAL
ADD CONSTRAINT FK_Services_Service FOREIGN KEY (Id_servicio) REFERENCES SERVICIO(Identificador);

ALTER TABLE TELEFONO_SUCURSAL
ADD CONSTRAINT FK_PHONE FOREIGN KEY (Codigo_sucursal) REFERENCES SUCURSAL(Codigo_sucursal);

ALTER TABLE CLASE
ADD CONSTRAINT FK_Class FOREIGN KEY (Id_servicio) REFERENCES SERVICIO(Identificador);

ALTER TABLE CLASE
ADD CONSTRAINT FK_Instructor FOREIGN KEY (Cedula_instructor) REFERENCES EMPLEADO(Cedula);

ALTER TABLE ASISTENCIA_CLASE
ADD CONSTRAINT FK_Assist_Client FOREIGN KEY (Cedula_cliente) REFERENCES CLIENTE(Cedula);

ALTER TABLE ASISTENCIA_CLASE
ADD CONSTRAINT FK_Assist_Class FOREIGN KEY (Id_servicio, Num_clase) REFERENCES CLASE(Id_servicio, Num_clase);