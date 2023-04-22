-- Instituto Tecnológico de Costa Rica
-- Curso: Bases de Datos
-- Proyecto I - GymTEC
-- Script de populación
-- Estudiante: Eduardo Bolívar Minguet
-- Carné: 2020158103

-- Tratamientos que se ofrecen por default.
INSERT INTO TRATAMIENTO (Nombre) VALUES ('Masaje relajante');
INSERT INTO TRATAMIENTO (Nombre) VALUES ('Masaje descarga muscular');
INSERT INTO TRATAMIENTO (Nombre) VALUES ('Sauna');
INSERT INTO TRATAMIENTO (Nombre) VALUES ('Baño a vapor');

-- Puestos default de GymTEC.
INSERT INTO PUESTO (Descripcion) VALUES ('Administrador: Gestiona la sucursal');
INSERT INTO PUESTO (Descripcion) VALUES ('Instructor: Se encarga de impartir una clase');
INSERT INTO PUESTO (Descripcion) VALUES ('Dependiente Spa: Encargado de gestionar un Spa');
INSERT INTO PUESTO (Descripcion) VALUES ('Dependiente Tienda: Encargado de una tienda');

-- Planillas únicas que maneja el gimnasio.
INSERT INTO PLANILLA (Descripcion) VALUES ('Pago mensual');
INSERT INTO PLANILLA (Descripcion) VALUES ('Pago por horas');
INSERT INTO PLANILLA (Descripcion) VALUES ('Pago por clase');

-- Servicios default que ofrece el gimnasio.
INSERT INTO SERVICIO (Descripcion) VALUES ('Indoor Cycling');
INSERT INTO SERVICIO (Descripcion) VALUES ('Pilates');
INSERT INTO SERVICIO (Descripcion) VALUES ('Yoga');
INSERT INTO SERVICIO (Descripcion) VALUES ('Zumba');
INSERT INTO SERVICIO (Descripcion) VALUES ('Natación');

-- Tipo de equipo por default que maneja el gimnasio.
INSERT INTO TIPO_EQUIPO (Descripcion) VALUES ('Cintas de correr');
INSERT INTO TIPO_EQUIPO (Descripcion) VALUES ('Bicicletas estacionarias');
INSERT INTO TIPO_EQUIPO (Descripcion) VALUES ('Multigimnasios');
INSERT INTO TIPO_EQUIPO (Descripcion) VALUES ('Remos');
INSERT INTO TIPO_EQUIPO (Descripcion) VALUES ('Pesas');

-- Empleado Administrador General.
INSERT INTO EMPLEADO VALUES (123, 'Hector', 'Perez', 'R', 'Luna', 'Sol', 'XD', 'ajdlasj', 'lllllll', 1000000, 1, 1);

-- Sucursales ya existentes en los campus del Tecnológico de Costa Rica.
INSERT INTO SUCURSAL VALUES ('GymTEC Campus Central Cartago', 'Dulce Nombre', 'Cartago', 'Cartago', '2005-03-18', '7:00', '18:00', 40, 123);
INSERT INTO SUCURSAL VALUES ('GymTEC Campus San José', 'Barrio Amón', 'San José', 'San José', '2006-06-20', '7:00', '18:00', 25, 123);
INSERT INTO SUCURSAL VALUES ('GymTEC Campus San Carlos', 'San Carlos', 'San Carlos', 'Alajuela', '2009-04-10', '7:00', '18:00', 20, 123);

-- Se tiene por default un Spa inactivo en cada sucursal.
INSERT INTO SPA VALUES ('GymTEC Campus Central Cartago', 1);
INSERT INTO SPA VALUES ('GymTEC Campus San José', 1);
INSERT INTO SPA VALUES ('GymTEC Campus San Carlos', 1);

-- Se tiene por default una Tienda inactiva en cada sucursal.
INSERT INTO TIENDA VALUES ('GymTEC Campus Central Cartago', 1);
INSERT INTO TIENDA VALUES ('GymTEC Campus San José', 1);
INSERT INTO TIENDA VALUES ('GymTEC Campus San Carlos', 1);

select * from TRATAMIENTO;
select * from PUESTO;
select * from PLANILLA;
select * from SERVICIO;
select * from TIPO_EQUIPO;
