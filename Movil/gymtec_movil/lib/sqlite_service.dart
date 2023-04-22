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
           "CREATE TABLE CLIENTE (Cedula INT NOT NULL, Nombre NVARCHAR(50) NOT NULL,Apellido1 NVARCHAR(50),"+
           "Apellido2 NVARCHAR(50),Dia_nacimiento VARCHAR(2) NOT NULL,Mes_nacimiento VARCHAR(2) NOT NULL,"+
           "Año_nacimiento VARCHAR(4) NOT NULL,Peso FLOAT NOT NULL,Direccion NVARCHAR(50),"+
           "Correo NVARCHAR(50) NOT NULL,Contraseña NVARCHAR(50) NOT NULL,PRIMARY KEY (Cedula));"+
           "CREATE TABLE CLASE (Id_servicio INT NOT NULL,Num_clase INT NOT NULL,Fecha DATE,"+
           "Hora_inicio TIME,Hora_fin TIME,Modalidad NVARCHAR(50),Capacidad INT,"+
           "Cedula_instructor INT NOT NULL,PRIMARY KEY (Id_servicio, Num_clase));"+
          "CREATE TABLE ASISTENCIA_CLASE (Cedula_cliente INT NOT NULL,Id_servicio INT NOT NULL,"+
          "Num_clase INT NOT NULL,PRIMARY KEY (Cedula_cliente, Id_servicio, Num_clase));",
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






