class CLIENTE{
  static final cedulaColumn= "cedula";
  int cedula;
  String Nombre,Apellido1,Apellido2, Dia_nacimiento, Mes_nacimiento, Year, Direccion, Correo, Password;
  double peso;

  
  CLIENTE({this.cedula=0,this.Nombre="", this.Apellido1="",this.Apellido2="",this.Dia_nacimiento="",
  this.Mes_nacimiento="",this.Year="",this.peso=0,this.Direccion="",
  this.Correo="",this.Password=""});

  CLIENTE.fromMap(Map<String,dynamic> item):cedula=item["Cedula"], Nombre=item["Nombre"],
                                            Apellido1=item["Apellido1"], Apellido2=item["Apellido2"],Dia_nacimiento=item["Dia_nacimiento"],
                                            Mes_nacimiento=item["Mes_nacimiento"],Year=item["Año_nacimiento"],peso=item["Peso"],
                                            Direccion=item["Direccion"], Correo=item["Correo"], Password =item["Contraseña"];

  Map<String, Object> toMap(){
    return {'cedula':cedula,"nombre":Nombre, "Apellido1":Apellido1,"Apellido2":Apellido2, "Dia_nacimiento":Dia_nacimiento,
    "Mes_nacimiento":Mes_nacimiento,"Año_nacimiento":Year,"Peso":peso,"Direccion":Direccion, "Correo":Correo, "Contraseña":Password};
  }
}

class CLASE{
  static final id_servicioColumn= "Id_servicio";
  static final num_claseColumn= "Num_clase";
  static final fechaColumn= "Fecha";
  static final hora_inicioColumn= "Hora_inicio";
  static final hora_finColumn= "Hora_fin";
  static final modalidadColumn= "Modalidad";
  static final capacidadColumn= "Capacidad";
  static final cedula_instructorColumn= "Cedula_instructor";
  int id_servicio, num_clase, capacidad, cedula_instructor;
  String fecha, hora_inicio, hora_fin, modalidad;

  CLASE({this.id_servicio=0,this.num_clase=0,this.fecha="",this.hora_inicio="",this.hora_fin="",this.modalidad="",this.capacidad=0,this.cedula_instructor=0});

  CLASE.fromMap(Map<String,dynamic> item):id_servicio=item["Id_servicio"], num_clase=item["Num_clase"],
                                            fecha=item["Fecha"], hora_inicio=item["Hora_inicio"],hora_fin=item["Hora_fin"],
                                            modalidad=item["Modalidad"],capacidad=item["Capacidad"],cedula_instructor=item["Cedula_instructor"];

  Map<String, Object> toMap(){
    return {'Id_servicio':id_servicio,"Num_clase":num_clase, "Fecha":fecha,"Hora_inicio":hora_inicio, "Hora_fin":hora_fin,
    "Modalidad":modalidad,"Capacidad":capacidad,"Cedula_instructor":cedula_instructor};
  }
}

/**
          "CREATE TABLE ASISTENCIA_CLASE (Cedula_cliente INTEGER NOT NULL,Id_servicio INTEGER NOT NULL,"+
          "Num_clase INTEGER NOT NULL,PRIMARY KEY (Cedula_cliente, Id_servicio, Num_clase));",
 */
// escribe la clase anterior
class ASISTENCIA_CLASE{
  static final cedula_clienteColumn= "Cedula_cliente";
  static final id_servicioColumn= "Id_servicio";
  static final num_claseColumn= "Num_clase";
  int cedula_cliente, id_servicio, num_clase;

  ASISTENCIA_CLASE({this.cedula_cliente=0,this.id_servicio=0,this.num_clase=0});

  ASISTENCIA_CLASE.fromMap(Map<String,dynamic> item):cedula_cliente=item["Cedula_cliente"], id_servicio=item["Id_servicio"],
                                            num_clase=item["Num_clase"];

  Map<String, Object> toMap(){
    return {'Cedula_cliente':cedula_cliente,"Id_servicio":id_servicio, "Num_clase":num_clase};
  }
}
/*
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
*/
class EMPLEADO{
  // crea la clase
  static final cedulaColumn= "Cedula";
  static final nombreColumn= "Nombre";
  static final apellido1Column= "Apellido1";
  static final apellido2Column= "Apellido2";
  static final distritoColumn= "Distrito";
  static final cantonColumn= "Canton";
  static final provinciaColumn= "Provincia";
  static final correoColumn= "Correo";
  static final contrasenaColumn= "Contrasena";
  static final salarioColumn= "Salario";
  static final id_puestoColumn= "Id_puesto";
  static final id_planillaColumn= "Id_planilla";
  static final codigo_sucColumn= "Codigo_suc";
  int cedula, id_puesto, id_planilla, codigo_suc;
  String nombre, apellido1, apellido2, distrito, canton, provincia, correo, contrasena;
  double salario;

  EMPLEADO({this.cedula=0,this.nombre="",this.apellido1="",this.apellido2="",this.distrito="",this.canton="",this.provincia="",this.correo="",this.contrasena="",this.salario=0,this.id_puesto=0,this.id_planilla=0,this.codigo_suc=0});

  EMPLEADO.fromMap(Map<String,dynamic> item):cedula=item["Cedula"], nombre=item["Nombre"],
                                            apellido1=item["Apellido1"], apellido2=item["Apellido2"],distrito=item["Distrito"],
                                            canton=item["Canton"],provincia=item["Provincia"],correo=item["Correo"],
                                            contrasena=item["Contrasena"],salario=item["Salario"],id_puesto=item["Id_puesto"],
                                            id_planilla=item["Id_planilla"],codigo_suc=item["Codigo_suc"];

  
  Map<String, Object> toMap(){
    return {'Cedula':cedula,"Nombre":nombre, "Apellido1":apellido1,"Apellido2":apellido2, "Distrito":distrito,
    "Canton":canton,"Provincia":provincia,"Correo":correo,"Contrasena":contrasena,"Salario":salario,"Id_puesto":id_puesto,"Id_planilla":id_planilla,"Codigo_suc":codigo_suc};
  }
}
class SUCURSAL{
  // crea la clase Sucursal
  static final codigo_sucursalColumn= "Codigo_sucursal";
  static final nombreColumn= "Nombre";
  static final distritoColumn= "Distrito";
  static final cantonColumn= "Canton";
  static final provinciaColumn= "Provincia";
  static final fecha_aperturaColumn= "Fecha_apertura";
  static final hora_aperturaColumn= "Hora_apertura";
  static final hora_cierreColumn= "Hora_cierre";
  static final max_capacidadColumn= "Max_capacidad";
  static final cedula_administradorColumn= "Cedula_administrador";
  int codigo_sucursal, max_capacidad, cedula_administrador;
  String nombre, distrito, canton, provincia, fecha_apertura, hora_apertura, hora_cierre;

  SUCURSAL({this.codigo_sucursal=0,this.nombre="",this.distrito="",this.canton="",this.provincia="",this.fecha_apertura="",this.hora_apertura="",this.hora_cierre="",this.max_capacidad=0,this.cedula_administrador=0});

  SUCURSAL.fromMap(Map<String,dynamic> item):codigo_sucursal=item["Codigo_sucursal"], nombre=item["Nombre"],
                                            distrito=item["Distrito"], canton=item["Canton"],provincia=item["Provincia"],
                                            fecha_apertura=item["Fecha_apertura"],hora_apertura=item["Hora_apertura"],
                                            hora_cierre=item["Hora_cierre"],max_capacidad=item["Max_capacidad"],cedula_administrador=item["Cedula_administrador"];

  Map<String, Object> toMap(){
    return {'Codigo_sucursal':codigo_sucursal,"Nombre":nombre, "Distrito":distrito,"Canton":canton, "Provincia":provincia,
    "Fecha_apertura":fecha_apertura,"Hora_apertura":hora_apertura,"Hora_cierre":hora_cierre,"Max_capacidad":max_capacidad,"Cedula_administrador":cedula_administrador};
  }
}

class SERVICIO{
  // Crea la clase segun el texto
  static final identificadorColumn= "Identificador";
  static final descripcionColumn= "Descripcion";
  int identificador;
  String descripcion;

  SERVICIO({this.identificador=0,this.descripcion=""});

  SERVICIO.fromMap(Map<String,dynamic> item):identificador=item["Identificador"], descripcion=item["Descripcion"];

  Map<String, Object> toMap(){
    return {'Identificador':identificador,"Descripcion":descripcion};
  }
}