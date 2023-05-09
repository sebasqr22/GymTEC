import 'package:gymtec_movil/database_handler.dart';
import 'package:sqflite/sqflite.dart';
import 'package:path/path.dart';

class SqliteService {
  Future<Database> initializeDB() async {
    String path = await getDatabasesPath();
    print("creando base de datos");

    return openDatabase(
      join(path, 'miBases2.db'),
      onCreate: (database, version) async {
         await database.execute( 
                      """
CREATE TABLE IF NOT EXISTS CLIENTE (Cedula INTEGER NOT NULL, Nombre TEXT NOT NULL,Apellido1 TEXT,
Apellido2 TEXT,Dia_nacimiento TEXT NOT NULL,Mes_nacimiento TEXT NOT NULL,
Año_nacimiento TEXT NOT NULL,Peso REAL NOT NULL,Direccion TEXT,
Correo TEXT NOT NULL,Contraseña TEXT NOT NULL,PRIMARY KEY (Cedula));""");
        await database.execute( 
                      """
      CREATE TABLE IF NOT EXISTS SERVICIO (
	Identificador INTEGER NOT NULL,
	Descripcion TEXT,
	PRIMARY KEY (Identificador)
);""");
await database.execute("""
CREATE TABLE IF NOT EXISTS CLASE (Id_servicio INTEGER NOT NULL,Num_clase INTEGER NOT NULL,Fecha DATE,
Hora_inicio TIME,Hora_fin TIME,Modalidad TEXT,Capacidad INTEGER,
Cedula_instructor INTEGER NOT NULL,PRIMARY KEY (Id_servicio, Num_clase));""");
await database.execute("""CREATE TABLE IF NOT EXISTS SUCURSAL (
             Codigo_sucursal INTEGER NOT NULL,
             Nombre TEXT NOT NULL,
             Distrito TEXT,
             Canton TEXT NOT NULL,
             Provincia TEXT NOT NULL,
             Fecha_apertura DATE,
             Hora_apertura TIME,
             Hora_cierre TIME,
             Max_capacidad INTEGER,
             Cedula_administrador INTEGER NOT NULL,
             PRIMARY KEY (Codigo_sucursal));""");
       await database.execute("""
                 CREATE TABLE IF NOT EXISTS EMPLEADO (
            Cedula INTEGER NOT NULL,
          	Nombre TEXT NOT NULL,
	          Apellido1 TEXT NOT NULL,
	          Apellido2 TEXT,
	          Distrito TEXT,
	          Canton TEXT,Provincia TEXT NOT NULL, Correo TEXT NOT NULL,
	          Contrasena TEXT NOT NULL,Salario REAL NOT NULL, Id_puesto INTEGER NOT NULL, Id_planilla INTEGER NOT NULL,
	          Codigo_suc INTEGER,
                   PRIMARY KEY (Cedula),
                 FOREIGN KEY (Codigo_suc) REFERENCES SUCURSAL(Codigo_sucursal));""");
await database.execute("""CREATE TABLE IF NOT EXISTS ASISTENCIA_CLASE (Cedula_cliente INTEGER NOT NULL,Id_servicio INTEGER NOT NULL,
Num_clase INTEGER NOT NULL,PRIMARY KEY (Cedula_cliente, Id_servicio, Num_clase),
 FOREIGN KEY (Id_servicio, Num_clase) REFERENCES CLASE(Id_servicio, Num_clase));
 """,
      );// FALTAN LOS ALTER
     },
     version: 1,
    );
  }

  Future createCliente(CLIENTE cliente) async {
    int result = 0;
    final Database db = await initializeDB();
    final id = await db.insert(
      'CLIENTE', cliente.toMap(), 
      conflictAlgorithm: ConflictAlgorithm.replace); 
    print("CLIENTE CREADO EXITOSAMENTE");  
  }

  Future createServicio(SERVICIO servicio) async {
    int result = 0;
    final Database db = await initializeDB();
    final id = await db.insert(
      'SERVICIO', servicio.toMap(), 
      conflictAlgorithm: ConflictAlgorithm.replace); 
    print("SERVICIO CREADO EXITOSAMENTE");  
  }

  Future createClase(CLASE clase) async {
    int result = 0;
    final Database db = await initializeDB();
    final id = await db.insert(
      'CLASE', clase.toMap(), 
      conflictAlgorithm: ConflictAlgorithm.replace); 
    print("CLASE CREADA EXITOSAMENTE");  
  }

  Future createEmpleado(EMPLEADO emp) async {
    int result = 0;
    final Database db = await initializeDB();
    final id = await db.insert(
      'EMPLEADO', emp.toMap(), 
      conflictAlgorithm: ConflictAlgorithm.replace); 
    print("EMPLEADO CREADO EXITOSAMENTE");  
  }

  Future<bool> obtenerClientePorCedula(double cedula) async {
  final db = await initializeDB();
  List<Map<String, dynamic>> maps = await db.query('CLIENTE',
      where: 'cedula = ?',
      whereArgs: [cedula]);

  if (maps.length > 0) {
    print(maps.first);
    CLIENTE c = CLIENTE.fromMap(maps.first);
    //print(CLIENTE.fromMap(maps.first).Nombre);
    return true;
  }

  return false;
}

  Future<bool> iniciarSesion(String correo, String password) async {
  final db = await initializeDB();
  List<Map<String, dynamic>> maps = await db.query('CLIENTE',
      where: 'Correo = ? AND Contraseña = ?',
      whereArgs: [correo, password]);

  if (maps.length > 0) {
    print("here");
    return true;
  }
  print("here2");
  return false;
}

  Future<List<CLASE>> buscarClasesPorSucursal(String sucursal) async{
    final db = await initializeDB();
  List<Map<String, dynamic>> maps = await db.rawQuery("SELECT fecha, hora_inicio, hora_fin, EMPLEADO.Nombre + ' ' + Apellido1 + ' ' + Apellido2 AS Instructor, Capacidad - COUNT(ASISTENCIA_CLASE.Num_clase) AS Cupos_disponibles"+
                "FROM CLASE LEFT JOIN ASISTENCIA_CLASE ON CLASE.Num_clase = ASISTENCIA_CLASE.Num_clase JOIN EMPLEADO ON Cedula_instructor = Cedula JOIN SUCURSAL ON Nombre_suc = SUCURSAL.Nombre"+
                "WHERE SUCURSAL.Nombre = ?"+
                "GROUP BY Fecha, Hora_inicio, Hora_fin, EMPLEADO.Nombre, Apellido1, Apellido2, Capacidad",[sucursal]);
  if (maps.length > 0) {
    List<CLASE> clases = [];
    for (var i = 0; i < maps.length; i++) {
      clases.add(CLASE.fromMap(maps[i]));
    }
    return clases;
  }
  return [];
  }

  Future<List<CLASE>> buscarClasesPorServicio(String servicio) async{
    final db = await initializeDB();
  List<Map<String, dynamic>> maps = await db.rawQuery("SELECT Fecha, Hora_inicio, Hora_fin, EMPLEADO.Nombre + ' ' + Apellido1 + ' ' + Apellido2 AS Instructor, Capacidad - COUNT(ASISTENCIA_CLASE.Num_clase) AS Cupos_disponibles"+
                "FROM CLASE LEFT JOIN ASISTENCIA_CLASE ON CLASE.Num_clase = ASISTENCIA_CLASE.Num_clase JOIN EMPLEADO ON Cedula_instructor = Cedula JOIN SUCURSAL ON Nombre_suc = SUCURSAL.Nombre"+
                "WHERE CLASE.Id_servicio = ?"+
                "GROUP BY Fecha, Hora_inicio, Hora_fin, EMPLEADO.Nombre, Apellido1, Apellido2, Capacidad",[servicio]);
  if (maps.length > 0) {
    List<CLASE> clases = [];
    for (var i = 0; i < maps.length; i++) {
      clases.add(CLASE.fromMap(maps[i]));
    }
    return clases;
  }
  return [];
  }

  Future<List<CLASE>> buscarClasesPorPeriodo(String fecha_inicio,fecha_fin) async{
    final db = await initializeDB();
  List<Map<String, dynamic>> maps = await db.rawQuery("SELECT Fecha, Hora_inicio, Hora_fin, EMPLEADO.Nombre + ' ' + Apellido1 + ' ' + Apellido2 AS Instructor, Capacidad - COUNT(ASISTENCIA_CLASE.Num_clase) AS Cupos_disponibles"+
                "FROM CLASE LEFT JOIN ASISTENCIA_CLASE ON CLASE.Num_clase = ASISTENCIA_CLASE.Num_clase JOIN EMPLEADO ON Cedula_instructor = Cedula JOIN SUCURSAL ON Nombre_suc = SUCURSAL.Nombre"+
                "WHERE ? <= Fecha AND Fecha <= ?"+
                "GROUP BY Fecha, Hora_inicio, Hora_fin, EMPLEADO.Nombre, Apellido1, Apellido2, Capacidad",[fecha_inicio,fecha_fin]);
  if (maps.length > 0) {
    List<CLASE> clases = [];
    for (var i = 0; i < maps.length; i++) {
      clases.add(CLASE.fromMap(maps[i]));
    }
    return clases;
  }
  return [];
  }

  Future<bool> registrarClase(String cedulaClient, String Num_clase, String Id_servicio, String Cedula_instructor) async{
    final db = await initializeDB();
    List<Map<String, dynamic>> maps = await db.rawQuery("INSERT INTO ASISTENCIA_CLASE (Cedula_cliente, Id_servicio, Num_clase)"+
                "VALUES (?,?,?)",[cedulaClient,Id_servicio,Num_clase]);
    if (maps.length > 0) {
      return true;
    }
    return false;
  }
}






