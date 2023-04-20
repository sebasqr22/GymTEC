-- //////////////////////////////////////////// TABLAS /////////////////////////////////////////////////////////
CREATE DATABASE [GymTEC-DB];
USE [GymTEC-DB];

-- Tabla EMPLEADO
-- Informaci n de los empleados del gimnasio.
-- Autor: Eduardo Bol var Minguet
CREATE TABLE EMPLEADO (
	Cedula INT NOT NULL,
	Nombre NVARCHAR(30) NOT NULL,
	Apellido1 NVARCHAR(30) NOT NULL,
	Apellido2 NVARCHAR(30),
	Distrito NVARCHAR(30),
	Canton NVARCHAR(30),
	Provincia NVARCHAR(30) NOT NULL,
	Correo NVARCHAR(30) NOT NULL,
	Contraseña NVARCHAR(30) NOT NULL,
	Salario FLOAT NOT NULL,
	Id_puesto INT NOT NULL,
	Id_planilla INT NOT NULL,
	PRIMARY KEY (Cedula)
);

-- Tabla SUCURSAL
-- Informaci n de las sucursales del gimnasio.
-- Autor: Eduardo Bol var Minguet
CREATE TABLE SUCURSAL (
	Nombre NVARCHAR(50) NOT NULL,
	Distrito NVARCHAR(50),
	Canton NVARCHAR(50) NOT NULL,
	Provincia NVARCHAR(50) NOT NULL,
	Fecha_apertura DATE,
	Hora_apertura TIME,
	Hora_cierre TIME,
	Max_capacidad INT,
	Cedula_administrador INT NOT NULL,
	PRIMARY KEY (Nombre)
);

-- Tabla CLIENTE
-- Informaci n de los usuarios de la aplicaci n GymTEC.
-- Autor: Eduardo Bol var Minguet
CREATE TABLE CLIENTE (
	Cedula INT NOT NULL,
	Nombre NVARCHAR(50) NOT NULL,
	Apellido1 NVARCHAR(50),
	Apellido2 NVARCHAR(50),
	Dia_nacimiento VARCHAR(2) NOT NULL,
	Mes_nacimiento VARCHAR(2) NOT NULL,
	Año_nacimiento VARCHAR(4) NOT NULL,
	Peso FLOAT NOT NULL,
	Direccion NVARCHAR(50),
	Correo NVARCHAR(50) NOT NULL,
	Contraseña NVARCHAR(50) NOT NULL,
	PRIMARY KEY (Cedula)
);

-- Tabla PUESTO
-- Puestos de trabajo disponibles para los empleados.
-- Autor: Eduardo Bol var Minguet
CREATE TABLE PUESTO (
	Identificador INT NOT NULL,
	Descripcion NVARCHAR(50),
	PRIMARY KEY (Identificador)
);

-- Tabla PLANILLA
-- Planilla de suscripci n que pueden escoger los clientes.
-- Autor: Eduardo Bol var Minguet
CREATE TABLE PLANILLA (
	Identificador INT NOT NULL,
	Descripcion NVARCHAR(50),
	PRIMARY KEY (Identificador)
);

-- Tabla SERVICIO
-- Servicios que ofrecen las sucursales del gimnasio.
-- Autor: Eduardo Bol var Minguet
CREATE TABLE SERVICIO (
	Identificador INT NOT NULL,
	Descripcion NVARCHAR(50),
	PRIMARY KEY (Identificador)
);

-- Tabla TRATAMIENTO
-- Tratamientos dados dentro de los spas del gimnasio.
-- Autor: Eduardo Bol var Minguet
CREATE TABLE TRATAMIENTO (
	Identificador INT NOT NULL,
	Nombre NVARCHAR(50),
	PRIMARY KEY (Identificador)
);

-- Tabla TIPO_EQUIPO
-- Tipo de equipo disponible en el gym.
-- Autor: Eduardo Bol var Minguet
CREATE TABLE TIPO_EQUIPO (
	Identificador INT NOT NULL,
	Descripcion NVARCHAR(50),
	PRIMARY KEY (Identificador)
);

-- Tabla INVENTARIO
-- Las m quinas para ejercitar que se encuentran en el gym.
-- Autor: Eduardo Bol var Minguet
CREATE TABLE INVENTARIO (
	Numero_serie INT NOT NULL,
	Marca NVARCHAR(50) NOT NULL,
	PRIMARY KEY (Numero_serie)
);

-- Tabla PRODUCTO
-- Informaci n de los productos que venden las tiendas del gym.
-- Autor: Eduardo Bol var Minguet
CREATE TABLE PRODUCTO (
	Codigo_barras INT NOT NULL,
	Nombre NVARCHAR(50) NOT NULL,
	Descripcion NVARCHAR(50),
	Costo FLOAT NOT NULL,
	PRIMARY KEY (Codigo_barras)
);

-- Tabla EMPLEADO
-- Informaci n de los empleados del gimnasio.
-- Autor: Eduardo Bol var Minguet
CREATE TABLE TIENDA (
	Nombre_sucursal NVARCHAR(50) NOT NULL,
	Num_tienda INT NOT NULL,
	PRIMARY KEY (Nombre_sucursal, Num_tienda)
);

-- Tabla EMPLEADO
-- Informaci n de los empleados del gimnasio.
-- Autor: Eduardo Bol var Minguet
CREATE TABLE SPA (
	Nombre_sucursal NVARCHAR(50) NOT NULL,
	Num_spa INT NOT NULL,
	PRIMARY KEY (Nombre_sucursal, Num_spa)
);

-- Tabla EMPLEADO
-- Informaci n de los empleados del gimnasio.
-- Autor: Eduardo Bol var Minguet
CREATE TABLE VENTA_PRODUCTO (
	Nsucursal NVARCHAR(50) NOT NULL,
	Tienda INT NOT NULL,
	Codigo_producto INT NOT NULL,
	PRIMARY KEY (Nsucursal, Tienda, Codigo_producto)
);

-- Tabla EMPLEADO
-- Informaci n de los empleados del gimnasio.
-- Autor: Eduardo Bol var Minguet
CREATE TABLE TRATAMIENTO_SPA (
	Nsucursal NVARCHAR(50) NOT NULL,
	Spa INT NOT NULL,
	Id_tratamiento INT NOT NULL,
	PRIMARY KEY (Nsucursal, Spa, Id_tratamiento)
);

-- Tabla EMPLEADO
-- Informaci n de los empleados del gimnasio.
-- Autor: Eduardo Bol var Minguet
CREATE TABLE INVENTARIO_EN_SUCURSAL (
	Nombre_sucursal NVARCHAR(50) NOT NULL,
	Num_serie_maquina INT NOT NULL,
	PRIMARY KEY (Nombre_sucursal, Num_serie_maquina)
);

-- Tabla EMPLEADO
-- Informaci n de los empleados del gimnasio.
-- Autor: Eduardo Bol var Minguet
CREATE TABLE SERVICIOS_EN_SUCURSAL (
	Nombre_sucursal NVARCHAR(50) NOT NULL,
	Id_servicio INT NOT NULL,
	PRIMARY KEY (Nombre_sucursal, Id_servicio)
);

-- Tabla EMPLEADO
-- Informaci n de los empleados del gimnasio.
-- Autor: Eduardo Bol var Minguet
CREATE TABLE TELEFONO_SUCURSAL (
	Nombre_sucursal NVARCHAR(50) NOT NULL,
	Telefono NVARCHAR(13) NOT NULL,
	PRIMARY KEY (Nombre_sucursal, Telefono)
);

-- Tabla EMPLEADO
-- Informaci n de los empleados del gimnasio.
-- Autor: Eduardo Bol var Minguet
CREATE TABLE TIPO_DE_MAQUINA (
	Num_serie_maquina INT NOT NULL,
	Id_tipo_equipo INT NOT NULL,
	PRIMARY KEY (Num_serie_maquina, Id_tipo_equipo)
);

-- Tabla EMPLEADO
-- Informaci n de los empleados del gimnasio.
-- Autor: Eduardo Bol var Minguet
CREATE TABLE CLASE (
	Id_servicio INT NOT NULL,
	Num_clase INT NOT NULL,
	Fecha DATE,
	Hora_inicio TIME,
	Hora_fin TIME,
	Modalidad NVARCHAR(50),
	Capacidad INT,
	Cedula_instructor INT NOT NULL,
	PRIMARY KEY (Id_servicio, Num_clase)
);

-- Tabla EMPLEADO
-- Informaci n de los empleados del gimnasio.
-- Autor: Eduardo Bol var Minguet
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

ALTER TABLE SUCURSAL
ADD CONSTRAINT FK_ADMIN FOREIGN KEY (Cedula_administrador) REFERENCES EMPLEADO(Cedula);

ALTER TABLE TIENDA
ADD CONSTRAINT FK_TIENDA FOREIGN KEY (Nombre_sucursal) REFERENCES SUCURSAL(Nombre);

ALTER TABLE SPA
ADD CONSTRAINT PKFK_SPA FOREIGN KEY (Nombre_sucursal) REFERENCES SUCURSAL(Nombre);

ALTER TABLE VENTA_PRODUCTO
ADD CONSTRAINT FK_VP_Store FOREIGN KEY (Nsucursal, Tienda) REFERENCES TIENDA(Nombre_sucursal, Num_tienda);

ALTER TABLE VENTA_PRODUCTO
ADD CONSTRAINT FK_VP_Product FOREIGN KEY (Codigo_producto) REFERENCES PRODUCTO(Codigo_barras);

ALTER TABLE TRATAMIENTO_SPA
ADD CONSTRAINT FK_TS_Spa FOREIGN KEY (Nsucursal, Spa) REFERENCES SPA(Nombre_sucursal, Num_spa);

ALTER TABLE TRATAMIENTO_SPA
ADD CONSTRAINT FK_TS_Treatment FOREIGN KEY (Id_tratamiento) REFERENCES TRATAMIENTO(Identificador);

ALTER TABLE INVENTARIO_EN_SUCURSAL
ADD CONSTRAINT FK_Inventory_Name FOREIGN KEY (Nombre_sucursal) REFERENCES SUCURSAL(Nombre);

ALTER TABLE INVENTARIO_EN_SUCURSAL
ADD CONSTRAINT FK_Inventory_Series FOREIGN KEY (Num_serie_maquina) REFERENCES INVENTARIO(Numero_serie);

ALTER TABLE SERVICIOS_EN_SUCURSAL
ADD CONSTRAINT FK_Services_Name FOREIGN KEY (Nombre_sucursal) REFERENCES SUCURSAL(Nombre);

ALTER TABLE SERVICIOS_EN_SUCURSAL
ADD CONSTRAINT FK_Services_Service FOREIGN KEY (Id_servicio) REFERENCES SERVICIO(Identificador);

ALTER TABLE TELEFONO_SUCURSAL
ADD CONSTRAINT FK_PHONE FOREIGN KEY (Nombre_sucursal) REFERENCES SUCURSAL(Nombre);

ALTER TABLE TIPO_DE_MAQUINA
ADD CONSTRAINT FK_Type_Machine FOREIGN KEY (Num_serie_maquina) REFERENCES INVENTARIO(Numero_serie);

ALTER TABLE TIPO_DE_MAQUINA
ADD CONSTRAINT FK_Type_Equipment FOREIGN KEY (Id_tipo_equipo) REFERENCES TIPO_EQUIPO(Identificador);

ALTER TABLE CLASE
ADD CONSTRAINT FK_Class FOREIGN KEY (Id_servicio) REFERENCES SERVICIO(Identificador);

ALTER TABLE CLASE
ADD CONSTRAINT FK_Instructor FOREIGN KEY (Cedula_instructor) REFERENCES EMPLEADO(Cedula);

ALTER TABLE ASISTENCIA_CLASE
ADD CONSTRAINT FK_Assist_Client FOREIGN KEY (Cedula_cliente) REFERENCES CLIENTE(Cedula);

ALTER TABLE ASISTENCIA_CLASE
ADD CONSTRAINT FK_Assist_Class FOREIGN KEY (Id_servicio, Num_clase) REFERENCES CLASE(Id_servicio, Num_clase);

