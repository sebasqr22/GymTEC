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