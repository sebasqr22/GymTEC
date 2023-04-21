-- Instituto Tecnológico de Costa Rica
-- Curso: Bases de Datos
-- Proyecto I - GymTEC
-- Script de populación
-- Estudiante: Eduardo Bolívar Minguet
-- Carné: 2020158103

-- Tratamientos que se ofrecen por default.
INSERT INTO TRATAMIENTO VALUES (1, 'Masaje relajante');
INSERT INTO TRATAMIENTO VALUES (2, 'Masaje descarga muscular');
INSERT INTO TRATAMIENTO VALUES (3, 'Sauna');
INSERT INTO TRATAMIENTO VALUES (4, 'Baño a vapor');

-- Puestos default de GymTEC.
INSERT INTO PUESTO VALUES (1, 'Administrador: Gestiona la sucursal');
INSERT INTO PUESTO VALUES (2, 'Instructor: Se encarga de impartir una clase');
INSERT INTO PUESTO VALUES (3, 'Dependiente Spa: Encargado de gestionar un Spa');
INSERT INTO PUESTO VALUES (4, 'Dependiente Tienda: Encargado de una tienda');

-- Planillas únicas que maneja el gimnasio.
INSERT INTO PLANILLA VALUES (1, 'Pago mensual');
INSERT INTO PLANILLA VALUES (2, 'Pago por horas');
INSERT INTO PLANILLA VALUES (3, 'Pago por clase');

-- Servicios default que ofrece el gimnasio.
INSERT INTO SERVICIO VALUES (1, 'Indoor Cycling');
INSERT INTO SERVICIO VALUES (2, 'Pilates');
INSERT INTO SERVICIO VALUES (3, 'Yoga');
INSERT INTO SERVICIO VALUES (4, 'Zumba');
INSERT INTO SERVICIO VALUES (5, 'Natación');

-- Tipo de equipo por default que maneja el gimnasio.
INSERT INTO TIPO_EQUIPO VALUES (1, 'Cintas de correr');
INSERT INTO TIPO_EQUIPO VALUES (2, 'Bicicletas estacionarias');
INSERT INTO TIPO_EQUIPO VALUES (3, 'Multigimnasios');
INSERT INTO TIPO_EQUIPO VALUES (4, 'Remos');
INSERT INTO TIPO_EQUIPO VALUES (5, 'Pesas');

-- Empleado Administrador General.
INSERT INTO EMPLEADO VALUES ();

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
