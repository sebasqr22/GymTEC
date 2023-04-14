CREATE DATABASE gymtecDB;

USE gymtecDB;

-- //////////////////////////////////////////// TABLAS /////////////////////////////////////////////////////////

-- Tabla EMPLEADO
-- Informaci�n de los empleados del gimnasio.
-- Autor: Eduardo Bol�var Minguet
CREATE TABLE EMPLEADO (
	C�dula INT PRIMARY KEY,
	Nombre NVARCHAR(15) NOT NULL,
	Apellido1 NVARCHAR(15) NOT NULL,
	Apellido2 NVARCHAR(15),
	Distrito NVARCHAR(10),
	Cant�n NVARCHAR(10),
	Provincia NVARCHAR(10) NOT NULL,
	Correo NVARCHAR(30) NOT NULL,
	Contrase�a NVARCHAR(30) NOT NULL,
	Salario DECIMAL(10,2)
);

-- Tabla SUCURSAL
-- Informaci�n de las sucursales del gimnasio.
-- Autor: Eduardo Bol�var Minguet
CREATE TABLE SUCURSAL (
	Nombre NVARCHAR(10) PRIMARY KEY,
	Distrito NVARCHAR(10),
	Cant�n NVARCHAR(10) NOT NULL,
	Provincia NVARCHAR(10) NOT NULL,
	Fecha_apertura DATE,
	Hora_apertura TIME,
	Hora_cierre TIME,
	Max_capacidad INT,
);

-- Tabla CLIENTE
-- Informaci�n de los usuarios de la aplicaci�n GymTEC.
-- Autor: Eduardo Bol�var Minguet
CREATE TABLE CLIENTE (
	C�dula INT PRIMARY KEY,
	Nombre NVARCHAR(15) NOT NULL,
	Apellido1 NVARCHAR(15),
	Apellido2 NVARCHAR(15),
	D�a_nacimiento VARCHAR(2) NOT NULL,
	Mes_nacimiento VARCHAR(2) NOT NULL,
	A�o_nacimiento VARCHAR(4) NOT NULL,
	Peso DECIMAL(3,2),
	Direcci�n NVARCHAR(50),
	Correo NVARCHAR(30) NOT NULL,
	Contrase�a NVARCHAR(30) NOT NULL,
);

-- Tabla PUESTO
-- Puestos de trabajo disponibles para los empleados.
-- Autor: Eduardo Bol�var Minguet
CREATE TABLE PUESTO (
	Identificador INT PRIMARY KEY,
	Descripci�n NVARCHAR(50)
);

-- Tabla PLANILLA
-- Planilla de suscripci�n que pueden escoger los clientes.
-- Autor: Eduardo Bol�var Minguet
CREATE TABLE PLANILLA (
	Identificador INT PRIMARY KEY,
	Descripci�n NVARCHAR(50)
);

-- Tabla SERVICIO
-- Servicios que ofrecen las sucursales del gimnasio.
-- Autor: Eduardo Bol�var Minguet
CREATE TABLE SERVICIO (
	Identificador INT PRIMARY KEY,
	Descripci�n NVARCHAR(50)
);

-- Tabla TRATAMIENTO
-- Tratamientos dados dentro de los spas del gimnasio.
-- Autor: Eduardo Bol�var Minguet
CREATE TABLE TRATAMIENTO (
	Identificador INT PRIMARY KEY,
	Nombre NVARCHAR(10)
);

-- Tabla TIPO_EQUIPO
-- Tipo de equipo disponible en el gym.
-- Autor: Eduardo Bol�var Minguet
CREATE TABLE TIPO_EQUIPO (
	Identificador INT PRIMARY KEY,
	Descripci�n NVARCHAR(50)
);

-- Tabla INVENTARIO
-- Las m�quinas para ejercitar que se encuentran en el gym.
-- Autor: Eduardo Bol�var Minguet
CREATE TABLE INVENTARIO (
	N�mero_serie INT PRIMARY KEY,
	Marca NVARCHAR(10) NOT NULL
);

-- Tabla PRODUCTO
-- Informaci�n de los productos que venden las tiendas del gym.
-- Autor: Eduardo Bol�var Minguet
CREATE TABLE PRODUCTO (
	C�digo_barras INT PRIMARY KEY,
	Nombre NVARCHAR(15) NOT NULL,
	Descripci�n NVARCHAR(30),
	Costo DECIMAL(10,2)
);

-- Tabla EMPLEADO
-- Informaci�n de los empleados del gimnasio.
-- Autor: Eduardo Bol�var Minguet
CREATE TABLE TIENDA (
	Nombre_sucursal NVARCHAR(10) NOT NULL,
	N�m_tienda INT NOT NULL
);

-- Tabla EMPLEADO
-- Informaci�n de los empleados del gimnasio.
-- Autor: Eduardo Bol�var Minguet
CREATE TABLE SPA (
	Nombre_sucursal NVARCHAR(10) NOT NULL,
	N�m_spa INT NOT NULL
);

-- Tabla EMPLEADO
-- Informaci�n de los empleados del gimnasio.
-- Autor: Eduardo Bol�var Minguet
CREATE TABLE VENTA_PRODUCTO (
	Nombre_sucursal NVARCHAR(10) NOT NULL,
	Tienda INT NOT NULL,
	C�digo_producto INT NOT NULL
);

-- Tabla EMPLEADO
-- Informaci�n de los empleados del gimnasio.
-- Autor: Eduardo Bol�var Minguet
CREATE TABLE TRATAMIENTO_SPA (
	Nombre_sucursal NVARCHAR(10) NOT NULL,
	Spa INT NOT NULL,
	Id_tratamiento INT NOT NULL
);

-- Tabla EMPLEADO
-- Informaci�n de los empleados del gimnasio.
-- Autor: Eduardo Bol�var Minguet
CREATE TABLE INVENTARIO_EN_SUCURSAL (
	Nombre_sucursal NVARCHAR(10) NOT NULL,
	N�m_serie_m�quina INT NOT NULL
);

-- Tabla EMPLEADO
-- Informaci�n de los empleados del gimnasio.
-- Autor: Eduardo Bol�var Minguet
CREATE TABLE SERVICIOS_EN_SUCURSAL (
	Nombre_sucursal NVARCHAR(10) NOT NULL,
	Id_servicio INT NOT NULL
);

-- Tabla EMPLEADO
-- Informaci�n de los empleados del gimnasio.
-- Autor: Eduardo Bol�var Minguet
CREATE TABLE TEL�FONO_SUCURSAL (
	Nombre_sucursal NVARCHAR(10) NOT NULL,
	Tel�fono NVARCHAR(13) NOT NULL
);

-- Tabla EMPLEADO
-- Informaci�n de los empleados del gimnasio.
-- Autor: Eduardo Bol�var Minguet
CREATE TABLE TIPO_DE_M�QUINA (
	N�m_serie_m�quina INT NOT NULL,
	Id_tipo_equipo INT NOT NULL
);

-- Tabla EMPLEADO
-- Informaci�n de los empleados del gimnasio.
-- Autor: Eduardo Bol�var Minguet
CREATE TABLE CLASE (
	Id_servicio INT NOT NULL,
	N�m_clase INT NOT NULL,
	Fecha DATE,
	Hora_inicio TIME,
	Hora_fin TIME,
	Modalidad NVARCHAR(10),
	Capacidad INT
);

-- Tabla EMPLEADO
-- Informaci�n de los empleados del gimnasio.
-- Autor: Eduardo Bol�var Minguet
CREATE TABLE INSTRUCTOR (
	C�dula_empleado INT NOT NULL,
	Id_servicio INT NOT NULL,
	N�m_clase INT NOT NULL
);

-- Tabla EMPLEADO
-- Informaci�n de los empleados del gimnasio.
-- Autor: Eduardo Bol�var Minguet
CREATE TABLE ASISTENCIA_CLASE (
	C�dula_cliente INT NOT NULL,
	Id_servicio INT NOT NULL,
	N�m_clase INT NOT NULL
);

ALTER TABLE EMPLEADO
ADD CONSTRAINT FK_PUESTO
FOREIGN KEY (Id_puesto) REFERENCES PUESTO(Identificador);

ALTER TABLE SUCURSAL
ADD CONSTRAINT FK_ADMIN
FOREIGN KEY (C�dula_administrador) REFERENCES EMPLEADO(C�dula);

ALTER TABLE CLIENTE
ADD CONSTRAINT FK_PLANILLA
FOREIGN KEY (Id_planilla) REFERENCES PLANILLA(Identificador);

ALTER TABLE TIENDA
ADD CONSTRAINT PK_TIENDA
PRIMARY KEY (Nombre_sucursal, N�m_tienda);

ALTER TABLE TIENDA
ADD CONSTRAINT PKFK_TIENDA
FOREIGN KEY (Nombre_sucursal) REFERENCES SUCURSAL(Nombre);

ALTER TABLE SPA
ADD CONSTRAINT PK_SPA
PRIMARY KEY (Nombre_sucursal, N�m_spa);

ALTER TABLE SPA
ADD CONSTRAINT PKFK_SPA
FOREIGN KEY (Nombre_sucursal) REFERENCES SUCURSAL(Nombre);

ALTER TABLE VENTA_PRODUCTO
ADD PRIMARY KEY (Nombre_sucursal, Tienda, C�digo_producto);

ALTER TABLE VENTA_PRODUCTO
ADD FOREIGN KEY (Nombre_sucursal) REFERENCES TIENDA(Nombre_sucursal);

ALTER TABLE VENTA_PRODUCTO
ADD FOREIGN KEY (Tienda) REFERENCES TIENDA(N�m_tienda);

ALTER TABLE VENTA_PRODUCTO
ADD FOREIGN KEY (C�digo_producto) REFERENCES PRODUCTO(C�digo_barras);

ALTER TABLE TRATAMIENTO_SPA
ADD PRIMARY KEY (Nombre_sucursal, Spa, Id_tratamiento);

ALTER TABLE TRATAMIENTO_SPA
ADD FOREIGN KEY (Nombre_sucursal) REFERENCES SPA(Nombre_sucursal);

ALTER TABLE TRATAMIENTO_SPA
ADD FOREIGN KEY (Spa) REFERENCES SPA(N�m_spa);

ALTER TABLE TRATAMIENTO_SPA
ADD FOREIGN KEY (Id_tratamiento) REFERENCES TRATAMIENTO(Identificador);

ALTER TABLE INVENTARIO_EN_SUCURSAL
ADD PRIMARY KEY (Nombre_sucursal, N�m_serie_m�quina);

ALTER TABLE INVENTARIO_EN_SUCURSAL
ADD FOREIGN KEY (Nombre_sucursal) REFERENCES SUCURSAL(Nombre);

ALTER TABLE INVENTARIO_EN_SUCURSAL
ADD FOREIGN KEY (N�m_serie_m�quina) REFERENCES INVENTARIO(N�mero_serie);

ALTER TABLE SERVICIOS_EN_SUCURSAL
ADD PRIMARY KEY (Nombre_sucursal, Id_servicio);

ALTER TABLE SERVICIOS_EN_SUCURSAL
ADD FOREIGN KEY (Nombre_sucursal) REFERENCES SUCURSAL(Nombre);

ALTER TABLE SERVICIOS_EN_SUCURSAL
ADD FOREIGN KEY (Id_servicio) REFERENCES SERVICIO(Identificador);

ALTER TABLE TEL�FONO_SUCURSAL
ADD PRIMARY KEY (Nombre_sucursal, Tel�fono);

ALTER TABLE TEL�FONO_SUCURSAL
ADD FOREIGN KEY (Nombre_sucursal) REFERENCES SUCURSAL(Nombre);

ALTER TABLE TIPO_DE_M�QUINA
ADD PRIMARY KEY (N�m_serie_m�quina, Id_tipo_equipo);

ALTER TABLE TIPO_DE_M�QUINA
ADD FOREIGN KEY (N�m_serie_m�quina) REFERENCES INVENTARIO(N�mero_serie);

ALTER TABLE TIPO_DE_M�QUINA
ADD FOREIGN KEY (Id_tipo_equipo) REFERENCES TIPO_EQUIPO(Identificador);

ALTER TABLE CLASE
ADD PRIMARY KEY (Id_servicio, N�m_clase);

ALTER TABLE CLASE
ADD FOREIGN KEY (Id_servicio) REFERENCES SERVICIO(Identificador);

ALTER TABLE INSTRUCTOR
ADD PRIMARY KEY (C�dula_empleado, Id_servicio, N�m_clase);

ALTER TABLE INSTRUCTOR
ADD FOREIGN KEY (C�dula_empleado) REFERENCES EMPLEADO(C�dula);

ALTER TABLE INSTRUCTOR
ADD FOREIGN KEY (Id_servicio) REFERENCES CLASE(Id_servicio);

ALTER TABLE INSTRUCTOR
ADD FOREIGN KEY (N�m_clase) REFERENCES CLASE(N�m_clase);

ALTER TABLE ASISTENCIA_CLASE
ADD PRIMARY KEY (C�dula_cliente, Id_servicio, N�m_clase);

ALTER TABLE ASISTENCIA_CLASE
ADD FOREIGN KEY (C�dula_cliente) REFERENCES CLIENTE(C�dula);

ALTER TABLE ASISTENCIA_CLASE
ADD FOREIGN KEY (Id_servicio) REFERENCES CLASE(Id_servicio);

ALTER TABLE ASISTENCIA_CLASE
ADD FOREIGN KEY (N�m_clase) REFERENCES CLASE(N�m_clase);