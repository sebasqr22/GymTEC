INSERT INTO TRATAMIENTO VALUES (1, 'Masaje relajante');
INSERT INTO TRATAMIENTO VALUES (2, 'Masaje descarga muscular');
INSERT INTO TRATAMIENTO VALUES (3, 'Sauna');
INSERT INTO TRATAMIENTO VALUES (4, 'Baño a vapor');

INSERT INTO PUESTO VALUES (1, 'Administrador: Gestiona la sucursal');
INSERT INTO PUESTO VALUES (2, 'Instructor: Se encarga de impartir una clase');
INSERT INTO PUESTO VALUES (3, 'Dependiente Spa: Encargado de gestionar un Spa');
INSERT INTO PUESTO VALUES (4, 'Dependiente Tienda: Encargado de una tienda');

INSERT INTO PLANILLA VALUES (1, 'Pago mensual');
INSERT INTO PLANILLA VALUES (2, 'Pago por horas');
INSERT INTO PLANILLA VALUES (3, 'Pago por clase');

INSERT INTO SERVICIO VALUES (1, 'Indoor Cycling');
INSERT INTO SERVICIO VALUES (2, 'Pilates');
INSERT INTO SERVICIO VALUES (3, 'Yoga');
INSERT INTO SERVICIO VALUES (4, 'Zumba');
INSERT INTO SERVICIO VALUES (5, 'Natación');

INSERT INTO TIPO_EQUIPO VALUES (1, 'Cintas de correr');
INSERT INTO TIPO_EQUIPO VALUES (2, 'Bicicletas estacionarias');
INSERT INTO TIPO_EQUIPO VALUES (3, 'Multigimnasios');
INSERT INTO TIPO_EQUIPO VALUES (4, 'Remos');
INSERT INTO TIPO_EQUIPO VALUES (5, 'Pesas');

INSERT INTO EMPLEADO VALUES (2020158103, 'Eduardo', 'Bolivar', 'Minguet', 'Granadilla', 'Curridabat', 'San José', 'ebolivarminguet@gmail.com', 'maxtercat9', 2000000.00, 1, 1);

INSERT INTO SUCURSAL VALUES ('GymTEC SJ001', 'Granadilla', 'Curridabat', 'San José', '2023-05-06', '9:00', '18:00', 60, 2020158103);

INSERT INTO TIENDA VALUES ('GymTEC SJ001', 1);

INSERT INTO SPA VALUES ('GymTEC SJ001', 1);

INSERT INTO TELEFONO_SUCURSAL VALUES ('GymTEC SJ001', '+506 71505980');
INSERT INTO TELEFONO_SUCURSAL VALUES ('GymTEC SJ001', '+506 88723450');
