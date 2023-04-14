CREATE DATABASE gymtecDB;

USE gymtecDB;

-- //////////////////////////////////////////// TABLAS /////////////////////////////////////////////////////////

-- Tabla EMPLEADO
-- Información de los empleados del gimnasio.
-- Autor: Eduardo Bolívar Minguet
CREATE TABLE EMPLEADO (
	Cédula INT PRIMARY KEY,
	Nombre NVARCHAR(15) NOT NULL,
	Apellido1 NVARCHAR(15) NOT NULL,
	Apellido2 NVARCHAR(15),
	Distrito NVARCHAR(10),
	Cantón NVARCHAR(10),
	Provincia NVARCHAR(10) NOT NULL,
	Correo NVARCHAR(30) NOT NULL,
	Contraseña NVARCHAR(30) NOT NULL,
	Salario DECIMAL(10,2)
);

-- Tabla SUCURSAL
-- Información de las sucursales del gimnasio.
-- Autor: Eduardo Bolívar Minguet
CREATE TABLE SUCURSAL (
	Nombre NVARCHAR(10) PRIMARY KEY,
	Distrito NVARCHAR(10),
	Cantón NVARCHAR(10) NOT NULL,
	Provincia NVARCHAR(10) NOT NULL,
	Fecha_apertura DATE,
	Hora_apertura TIME,
	Hora_cierre TIME,
	Max_capacidad INT,
);

-- Tabla CLIENTE
-- Información de los usuarios de la aplicación GymTEC.
-- Autor: Eduardo Bolívar Minguet
CREATE TABLE CLIENTE (
	Cédula INT PRIMARY KEY,
	Nombre NVARCHAR(15) NOT NULL,
	Apellido1 NVARCHAR(15),
	Apellido2 NVARCHAR(15),
	Día_nacimiento VARCHAR(2) NOT NULL,
	Mes_nacimiento VARCHAR(2) NOT NULL,
	Año_nacimiento VARCHAR(4) NOT NULL,
	Peso DECIMAL(3,2),
	Dirección NVARCHAR(50),
	Correo NVARCHAR(30) NOT NULL,
	Contraseña NVARCHAR(30) NOT NULL,
);

-- Tabla PUESTO
-- Puestos de trabajo disponibles para los empleados.
-- Autor: Eduardo Bolívar Minguet
CREATE TABLE PUESTO (
	Identificador INT PRIMARY KEY,
	Descripción NVARCHAR(50)
);

-- Tabla PLANILLA
-- Planilla de suscripción que pueden escoger los clientes.
-- Autor: Eduardo Bolívar Minguet
CREATE TABLE PLANILLA (
	Identificador INT PRIMARY KEY,
	Descripción NVARCHAR(50)
);

-- Tabla SERVICIO
-- Servicios que ofrecen las sucursales del gimnasio.
-- Autor: Eduardo Bolívar Minguet
CREATE TABLE SERVICIO (
	Identificador INT PRIMARY KEY,
	Descripción NVARCHAR(50)
);

-- Tabla TRATAMIENTO
-- Tratamientos dados dentro de los spas del gimnasio.
-- Autor: Eduardo Bolívar Minguet
CREATE TABLE TRATAMIENTO (
	Identificador INT PRIMARY KEY,
	Nombre NVARCHAR(10)
);

-- Tabla TIPO_EQUIPO
-- Tipo de equipo disponible en el gym.
-- Autor: Eduardo Bolívar Minguet
CREATE TABLE TIPO_EQUIPO (
	Identificador INT PRIMARY KEY,
	Descripción NVARCHAR(50)
);

-- Tabla INVENTARIO
-- Las máquinas para ejercitar que se encuentran en el gym.
-- Autor: Eduardo Bolívar Minguet
CREATE TABLE INVENTARIO (
	Número_serie INT PRIMARY KEY,
	Marca NVARCHAR(10) NOT NULL
);

-- Tabla PRODUCTO
-- Información de los productos que venden las tiendas del gym.
-- Autor: Eduardo Bolívar Minguet
CREATE TABLE PRODUCTO (
	Código_barras INT PRIMARY KEY,
	Nombre NVARCHAR(15) NOT NULL,
	Descripción NVARCHAR(30),
	Costo DECIMAL(10,2)
);

-- Tabla EMPLEADO
-- Información de los empleados del gimnasio.
-- Autor: Eduardo Bolívar Minguet
CREATE TABLE TIENDA (
	Nombre_sucursal NVARCHAR(10) NOT NULL,
	Núm_tienda INT NOT NULL
);

-- Tabla EMPLEADO
-- Información de los empleados del gimnasio.
-- Autor: Eduardo Bolívar Minguet
CREATE TABLE SPA (
	Nombre_sucursal NVARCHAR(10) NOT NULL,
	Núm_spa INT NOT NULL
);

-- Tabla EMPLEADO
-- Información de los empleados del gimnasio.
-- Autor: Eduardo Bolívar Minguet
CREATE TABLE VENTA_PRODUCTO (
	Nombre_sucursal NVARCHAR(10) NOT NULL,
	Tienda INT NOT NULL,
	Código_producto INT NOT NULL
);

-- Tabla EMPLEADO
-- Información de los empleados del gimnasio.
-- Autor: Eduardo Bolívar Minguet
CREATE TABLE TRATAMIENTO_SPA (
	Nombre_sucursal NVARCHAR(10) NOT NULL,
	Spa INT NOT NULL,
	Id_tratamiento INT NOT NULL
);

-- Tabla EMPLEADO
-- Información de los empleados del gimnasio.
-- Autor: Eduardo Bolívar Minguet
CREATE TABLE INVENTARIO_EN_SUCURSAL (
	Nombre_sucursal NVARCHAR(10) NOT NULL,
	Núm_serie_máquina INT NOT NULL
);

-- Tabla EMPLEADO
-- Información de los empleados del gimnasio.
-- Autor: Eduardo Bolívar Minguet
CREATE TABLE SERVICIOS_EN_SUCURSAL (
	Nombre_sucursal NVARCHAR(10) NOT NULL,
	Id_servicio INT NOT NULL
);

-- Tabla EMPLEADO
-- Información de los empleados del gimnasio.
-- Autor: Eduardo Bolívar Minguet
CREATE TABLE TELÉFONO_SUCURSAL (
	Nombre_sucursal NVARCHAR(10) NOT NULL,
	Teléfono NVARCHAR(13) NOT NULL
);

-- Tabla EMPLEADO
-- Información de los empleados del gimnasio.
-- Autor: Eduardo Bolívar Minguet
CREATE TABLE TIPO_DE_MÁQUINA (
	Núm_serie_máquina INT NOT NULL,
	Id_tipo_equipo INT NOT NULL
);

-- Tabla EMPLEADO
-- Información de los empleados del gimnasio.
-- Autor: Eduardo Bolívar Minguet
CREATE TABLE CLASE (
	Id_servicio INT NOT NULL,
	Núm_clase INT NOT NULL,
	Fecha DATE,
	Hora_inicio TIME,
	Hora_fin TIME,
	Modalidad NVARCHAR(10),
	Capacidad INT
);

-- Tabla EMPLEADO
-- Información de los empleados del gimnasio.
-- Autor: Eduardo Bolívar Minguet
CREATE TABLE INSTRUCTOR (
	Cédula_empleado INT NOT NULL,
	Id_servicio INT NOT NULL,
	Núm_clase INT NOT NULL
);

-- Tabla EMPLEADO
-- Información de los empleados del gimnasio.
-- Autor: Eduardo Bolívar Minguet
CREATE TABLE ASISTENCIA_CLASE (
	Cédula_cliente INT NOT NULL,
	Id_servicio INT NOT NULL,
	Núm_clase INT NOT NULL
);

ALTER TABLE EMPLEADO
ADD CONSTRAINT FK_PUESTO
FOREIGN KEY (Id_puesto) REFERENCES PUESTO(Identificador);

ALTER TABLE SUCURSAL
ADD CONSTRAINT FK_ADMIN
FOREIGN KEY (Cédula_administrador) REFERENCES EMPLEADO(Cédula);

ALTER TABLE CLIENTE
ADD CONSTRAINT FK_PLANILLA
FOREIGN KEY (Id_planilla) REFERENCES PLANILLA(Identificador);

ALTER TABLE TIENDA
ADD CONSTRAINT PK_TIENDA
PRIMARY KEY (Nombre_sucursal, Núm_tienda);

ALTER TABLE TIENDA
ADD CONSTRAINT PKFK_TIENDA
FOREIGN KEY (Nombre_sucursal) REFERENCES SUCURSAL(Nombre);

ALTER TABLE SPA
ADD CONSTRAINT PK_SPA
PRIMARY KEY (Nombre_sucursal, Núm_spa);

ALTER TABLE SPA
ADD CONSTRAINT PKFK_SPA
FOREIGN KEY (Nombre_sucursal) REFERENCES SUCURSAL(Nombre);

ALTER TABLE VENTA_PRODUCTO
ADD PRIMARY KEY (Nombre_sucursal, Tienda, Código_producto);

ALTER TABLE VENTA_PRODUCTO
ADD FOREIGN KEY (Nombre_sucursal) REFERENCES TIENDA(Nombre_sucursal);

ALTER TABLE VENTA_PRODUCTO
ADD FOREIGN KEY (Tienda) REFERENCES TIENDA(Núm_tienda);

ALTER TABLE VENTA_PRODUCTO
ADD FOREIGN KEY (Código_producto) REFERENCES PRODUCTO(Código_barras);

ALTER TABLE TRATAMIENTO_SPA
ADD PRIMARY KEY (Nombre_sucursal, Spa, Id_tratamiento);

ALTER TABLE TRATAMIENTO_SPA
ADD FOREIGN KEY (Nombre_sucursal) REFERENCES SPA(Nombre_sucursal);

ALTER TABLE TRATAMIENTO_SPA
ADD FOREIGN KEY (Spa) REFERENCES SPA(Núm_spa);

ALTER TABLE TRATAMIENTO_SPA
ADD FOREIGN KEY (Id_tratamiento) REFERENCES TRATAMIENTO(Identificador);

ALTER TABLE INVENTARIO_EN_SUCURSAL
ADD PRIMARY KEY (Nombre_sucursal, Núm_serie_máquina);

ALTER TABLE INVENTARIO_EN_SUCURSAL
ADD FOREIGN KEY (Nombre_sucursal) REFERENCES SUCURSAL(Nombre);

ALTER TABLE INVENTARIO_EN_SUCURSAL
ADD FOREIGN KEY (Núm_serie_máquina) REFERENCES INVENTARIO(Número_serie);

ALTER TABLE SERVICIOS_EN_SUCURSAL
ADD PRIMARY KEY (Nombre_sucursal, Id_servicio);

ALTER TABLE SERVICIOS_EN_SUCURSAL
ADD FOREIGN KEY (Nombre_sucursal) REFERENCES SUCURSAL(Nombre);

ALTER TABLE SERVICIOS_EN_SUCURSAL
ADD FOREIGN KEY (Id_servicio) REFERENCES SERVICIO(Identificador);

ALTER TABLE TELÉFONO_SUCURSAL
ADD PRIMARY KEY (Nombre_sucursal, Teléfono);

ALTER TABLE TELÉFONO_SUCURSAL
ADD FOREIGN KEY (Nombre_sucursal) REFERENCES SUCURSAL(Nombre);

ALTER TABLE TIPO_DE_MÁQUINA
ADD PRIMARY KEY (Núm_serie_máquina, Id_tipo_equipo);

ALTER TABLE TIPO_DE_MÁQUINA
ADD FOREIGN KEY (Núm_serie_máquina) REFERENCES INVENTARIO(Número_serie);

ALTER TABLE TIPO_DE_MÁQUINA
ADD FOREIGN KEY (Id_tipo_equipo) REFERENCES TIPO_EQUIPO(Identificador);

ALTER TABLE CLASE
ADD PRIMARY KEY (Id_servicio, Núm_clase);

ALTER TABLE CLASE
ADD FOREIGN KEY (Id_servicio) REFERENCES SERVICIO(Identificador);

ALTER TABLE INSTRUCTOR
ADD PRIMARY KEY (Cédula_empleado, Id_servicio, Núm_clase);

ALTER TABLE INSTRUCTOR
ADD FOREIGN KEY (Cédula_empleado) REFERENCES EMPLEADO(Cédula);

ALTER TABLE INSTRUCTOR
ADD FOREIGN KEY (Id_servicio) REFERENCES CLASE(Id_servicio);

ALTER TABLE INSTRUCTOR
ADD FOREIGN KEY (Núm_clase) REFERENCES CLASE(Núm_clase);

ALTER TABLE ASISTENCIA_CLASE
ADD PRIMARY KEY (Cédula_cliente, Id_servicio, Núm_clase);

ALTER TABLE ASISTENCIA_CLASE
ADD FOREIGN KEY (Cédula_cliente) REFERENCES CLIENTE(Cédula);

ALTER TABLE ASISTENCIA_CLASE
ADD FOREIGN KEY (Id_servicio) REFERENCES CLASE(Id_servicio);

ALTER TABLE ASISTENCIA_CLASE
ADD FOREIGN KEY (Núm_clase) REFERENCES CLASE(Núm_clase);