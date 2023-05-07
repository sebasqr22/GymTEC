import 'package:gymtec_movil/database_handler.dart';
import 'package:sqflite/sqflite.dart';
import 'package:path/path.dart';

class SqliteService {
  Future<Database> initializeDB() async {
    String path = await getDatabasesPath();


    return openDatabase(
      join(path, 'database.db'),
      onCreate: (database, version) async {
         await database.execute( 
           "CREATE TABLE CLIENTE (Cedula INTEGER NOT NULL, Nombre TEXT NOT NULL,Apellido1 TEXT,"+
           "Apellido2 TEXT,Dia_nacimiento TEXT NOT NULL,Mes_nacimiento TEXT NOT NULL,"+
           "Año_nacimiento TEXT NOT NULL,Peso REAL NOT NULL,Direccion TEXT,"+
           "Correo TEXT NOT NULL,Contraseña TEXT NOT NULL,PRIMARY KEY (Cedula));"+
           "CREATE TABLE CLASE (Id_servicio INTEGER NOT NULL,Num_clase INTEGER NOT NULL,Fecha DATE,"+
           "Hora_inicio TIME,Hora_fin TIME,Modalidad TEXT,Capacidad INTEGER,"+
           "Cedula_instructor INTEGER NOT NULL,PRIMARY KEY (Id_servicio, Num_clase));"+
          "CREATE TABLE ASISTENCIA_CLASE (Cedula_cliente INTEGER NOT NULL,Id_servicio INTEGER NOT NULL,"+
          "Num_clase INTEGER NOT NULL,PRIMARY KEY (Cedula_cliente, Id_servicio, Num_clase));"+
          "ALTER TABLE ASISTENCIA_CLASE"+
          "ADD CONSTRAINT FK_Assist_Class FOREIGN KEY (Id_servicio, Num_clase) REFERENCES CLASE(Id_servicio, Num_clase);",
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

  Future<List<CLASE>> buscarClases() async{
    final db = await initializeDB();
  List<Map<String, dynamic>> maps = await db.rawQuery("SELECT Fecha, Hora_inicio, Hora_fin, EMPLEADO.Nombre + ' ' + Apellido1 + ' ' + Apellido2 AS Instructor, Capacidad - COUNT(ASISTENCIA_CLASE.Num_clase) AS Cupos_disponibles"+
                "FROM CLASE LEFT JOIN ASISTENCIA_CLASE ON CLASE.Num_clase = ASISTENCIA_CLASE.Num_clase JOIN EMPLEADO ON Cedula_instructor = Cedula JOIN SUCURSAL ON Nombre_suc = SUCURSAL.Nombre"+
                "WHERE SUCURSAL.Nombre = @Nombre_sucursal AND CLASE.Id_servicio = Id_servicio AND @fecha_inicio <= Fecha AND Fecha <= fecha_fin"+
                "GROUP BY Fecha, Hora_inicio, Hora_fin, EMPLEADO.Nombre, Apellido1, Apellido2, Capacidad");
  if (maps.length > 0) {
    List<CLASE> clases = [];
    for (var i = 0; i < maps.length; i++) {
      clases.add(CLASE.fromMap(maps[i]));
    }
    return clases;
  }
  return [];
  }
}






