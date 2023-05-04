-- Instituto Tecnol�gico de Costa Rica
-- Curso: Bases de Datos
-- Proyecto I - GymTEC
-- Script funcionalidad
-- Estudiante: Eduardo Bol�var Minguet
-- Carn�: 2020158103

CREATE DATABASE [prueba_funcionalidad]
DROP DATABASE prueba_funcionalidad

USE [prueba_funcionalidad];

-- ///////////////////////////////////////// CONFIGURACION DE GIMNASIO /////////////////////////////////////////////////

-- Aqui se agregarian valores a las tablas:

-- TRATAMIENTO_SPA
-- VENTA_PRODUCTO
-- INVENTARIO_EN_SUCURSAL
-- CLASE

-- Respectivamente a como viene en el PDF.

-- ////////////////////////////////////// GENERACION DE PLANILLA ///////////////////////////////////////////////////////

SELECT Nombre_suc AS Sucursal, Cedula, Nombre, Apellido1 AS Primer_apellido, Apellido2 AS Segundo_apellido, 
CASE
	WHEN Id_planilla = 2 THEN CAST(8 AS VARCHAR)
	ELSE 'N/A'
END AS Horas_laboradas,
CASE
	WHEN Id_planilla = 3 THEN CAST(COUNT(Cedula_instructor) AS VARCHAR)
	ELSE 'N/A'
END AS Clases_impartidas,
CASE
	WHEN Id_planilla = 1 THEN Salario
	WHEN Id_planilla = 2 THEN Salario * 8
	WHEN Id_planilla = 3 THEN Salario * COUNT(Cedula_instructor)
END AS Monto_total
FROM EMPLEADO LEFT JOIN CLASE ON Cedula = Cedula_instructor 
GROUP BY Nombre_suc, Cedula, Nombre, Apellido1, Apellido2, Id_planilla, Salario;

-- ///////////////////////////////////////// COPIAR CALENDARIO DE ACTIVIDADES ///////////////////////////////////////////////////////

INSERT INTO CLASE (Id_servicio, Fecha, Hora_inicio, Hora_fin, Modalidad, Capacidad, Cedula_instructor)
SELECT Id_servicio, DATEADD(WEEK, 1, Fecha), Hora_inicio, Hora_fin, Modalidad, Capacidad, Cedula_instructor
FROM CLASE WHERE '2023-04-01' < Fecha AND Fecha < '2023-04-30'
GROUP BY Id_servicio, Num_clase, Fecha, Hora_inicio, Hora_fin, Modalidad, Capacidad, Cedula_instructor

insert into CLASE (Id_servicio, Fecha, Hora_inicio, Hora_fin, Modalidad, Capacidad, Cedula_instructor) values (5, '2023-04-27', '10:00', '11:00', 'Virtual', '50', 123456789)
insert into CLASE (Id_servicio, Fecha, Hora_inicio, Hora_fin, Modalidad, Capacidad, Cedula_instructor) values (1, '2023-04-28', '14:00', '15:50', 'Presencial', '20', 123456789)

select * from clase;

delete from clase
DBCC CHECKIDENT('CLASE', RESEED, 0)

-- //////////////////////////////////////////////// COPIAR GIMNASIO /////////////////////////////////////////////////////////////////

DECLARE @GymNuevo AS VARCHAR = 'GymTEC Campus Heredia'
DECLARE @NumTiendaNueva AS INT = 4
DECLARE @GymCopiado AS VARCHAR = 'GymTEC Campus Central Cartago'

INSERT INTO TRATAMIENTO_SPA
SELECT @GymNuevo, @NumTiendaNueva, Id_tratamiento
FROM TRATAMIENTO_SPA 
WHERE Nsucursal = @GymCopiado

INSERT INTO VENTA_PRODUCTO
SELECT @GymNuevo, @NumTiendaNueva, Codigo_producto 
FROM VENTA_PRODUCTO 
WHERE Nsucursal = @GymCopiado

INSERT INTO CLASE (Id_servicio, Fecha, Hora_inicio, Hora_fin, Modalidad, Capacidad)
SELECT Id_servicio, Fecha, Hora_inicio, Hora_fin, Modalidad, Capacidad
FROM CLASE JOIN EMPLEADO ON Cedula_instructor = Cedula
WHERE Nombre_suc = 'GymTEC Campus Central Cartago'

-- //////////////////////////////////////////////// BUSQUEDA DE UNA CLASE ////////////////////////////////////////////////////////////////

SELECT Fecha, Hora_inicio, Hora_fin, EMPLEADO.Nombre + ' ' + Apellido1 + ' ' + Apellido2 AS Instructor, Capacidad - COUNT(ASISTENCIA_CLASE.Num_clase) AS Cupos_disponibles
FROM CLASE LEFT JOIN ASISTENCIA_CLASE ON CLASE.Num_clase = ASISTENCIA_CLASE.Num_clase JOIN EMPLEADO ON Cedula_instructor = Cedula JOIN SUCURSAL ON Nombre_suc = SUCURSAL.Nombre
WHERE SUCURSAL.Nombre = 'GymTEC Campus Central Cartago' AND CLASE.Id_servicio = 5 AND '2023-04-01' <= Fecha AND Fecha <= '2023-05-31'
GROUP BY Fecha, Hora_inicio, Hora_fin, EMPLEADO.Nombre, Apellido1, Apellido2, Capacidad

SELECT * FROM CLASE;

-- ///////////////////////////////////////////////// REGISTRO EN UNA CLASE ///////////////////////////////////////////////////////////////////

INSERT INTO CLIENTE VALUES (9012, 'N', 'H', 'L', '02', '08', '2009', 60.0, 'Aqui', 'Correo', 'Contra epica xd') --CLIENTES DE PRUEBA
INSERT INTO CLIENTE VALUES (9013, 'N', 'H', 'L', '02', '08', '2009', 60.0, 'Aqui', 'Correo', 'Contra epica xd')

INSERT INTO ASISTENCIA_CLASE VALUES (9012, 5, 1) -- REGISTRO DE PRUEBA
INSERT INTO ASISTENCIA_CLASE VALUES (9012, 5, 4)
INSERT INTO ASISTENCIA_CLASE VALUES (9013, 5, 4)

