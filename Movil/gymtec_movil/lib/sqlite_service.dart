import 'package:gymtec_movil/database_handler.dart';
import 'package:sqflite/sqflite.dart';
import 'package:path/path.dart';

class SqliteService {
  Future<Database> initializeDB() async {
    String path = await getDatabasesPath();
    

    /*
    FALTA ESTO
"CREATE TABLE CLASE (Id_servicio INT NOT NULL,Num_clase INT NOT NULL,Fecha DATE,"+
           "Hora_inicio TIME,Hora_fin TIME,Modalidad NVARCHAR(50),Capacidad INT,"+
           "Cedula_instructor INT NOT NULL,PRIMARY KEY (Id_servicio, Num_clase));"+
          "CREATE TABLE ASISTENCIA_CLASE (Cedula_cliente INT NOT NULL,Id_servicio INT NOT NULL,"+
          "Num_clase INT NOT NULL,PRIMARY KEY (Cedula_cliente, Id_servicio, Num_clase));"
    */
    return openDatabase(
      join(path, 'database.db'),
      onCreate: (database, version) async {
         await database.execute( 
           "CREATE TABLE CLIENTE (Cedula INTEGER NOT NULL, Nombre TEXT NOT NULL,Apellido1 TEXT,"+
           "Apellido2 TEXT,Dia_nacimiento TEXT NOT NULL,Mes_nacimiento TEXT NOT NULL,"+
           "Año_nacimiento TEXT NOT NULL,Peso REAL NOT NULL,Direccion TEXT,"+
           "Correo TEXT NOT NULL,Contraseña TEXT NOT NULL,PRIMARY KEY (Cedula));",
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
      }

  Future<CLIENTE> obtenerClientePorCedula(int cedula) async {
  final db = await initializeDB();
  final List<Map<String, Object?>> queryResult = await db.query(
    'CLIENTES',
    where: '${CLIENTE.cedulaColumn} = ?',
    whereArgs: [cedula],
  );
  if (queryResult.isNotEmpty) {
    return CLIENTE.fromMap(queryResult.first);
  }
  return new CLIENTE();
}

}






