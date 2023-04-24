-- Instituto Tecnológico de Costa Rica
-- Curso: Bases de Datos
-- Proyecto I - GymTEC
-- Script funcionalidad
-- Estudiante: Eduardo Bolívar Minguet
-- Carné: 2020158103

-- Configuracion del gimnasio

-- Aqui se agregarian valores a las tablas:

-- TRATAMIENTO_SPA
-- VENTA_PRODUCTO
-- INVENTARIO_EN_SUCURSAL
-- CLASE

-- Respectivamente a como viene en el PDF.

-- Generación de planilla

SELECT Nombre_suc AS Sucursal, Cedula, Nombre, Apellido1 AS Primer_apellido, Apellido2 AS Segundo_apellido, 
CASE
	WHEN Id_planilla = 1 THEN 'N/A'
	WHEN Id_planilla = 2 THEN CAST(8 AS VARCHAR)
	WHEN Id_planilla = 3 THEN 'N/A'
END AS Horas_laboradas,
CASE
	WHEN Id_planilla = 1 THEN 'N/A'
	WHEN Id_planilla = 2 THEN 'N/A'
	WHEN Id_planilla = 3 THEN CAST(COUNT(Cedula_instructor) AS VARCHAR)
END AS Clases_impartidas,
CASE
	WHEN Id_planilla = 1 THEN Salario
	WHEN Id_planilla = 2 THEN Salario * 8
	WHEN Id_planilla = 3 THEN Salario * COUNT(Cedula_instructor)
END AS Monto_total
FROM EMPLEADO LEFT JOIN CLASE ON Cedula = Cedula_instructor GROUP BY Nombre_suc, Cedula, Nombre, Apellido1, Apellido2, Id_planilla, Salario;
