class CLIENTE{
  int cedula;
  String Nombre,Apellido1,Apellido2, Dia_nacimiento, Mes_nacimiento, Year, Direccion, Correo, Password;
  double peso;

  CLIENTE({this.cedula=0,this.Nombre="", this.Apellido1="",this.Apellido2="",this.Dia_nacimiento="",
  this.Mes_nacimiento="",this.Year="",this.peso=0,this.Direccion="",
  this.Correo="",this.Password=""});

  CLIENTE.fromMap(Map<String,dynamic> item):cedula=item["cedula"], Nombre=item["Nombre"],
                                            Apellido1=item["Apellido1"], Apellido2=item["Apellido2"],Dia_nacimiento=item["Dia_nacimiento"],
                                            Mes_nacimiento=item["Mes_nacimiento"],Year=item["Año_nacimiento"],peso=item["Peso"],
                                            Direccion=item["Direccion"], Correo=item["Correo"], Password =item["Password"];

  Map<String, Object> toMap(){
    return {'cedula':cedula,"nombre":Nombre, "Apellido1":Apellido1,"Apellido2":Apellido2, "Dia_nacimiento":Dia_nacimiento,
    "Mes_nacimiento":Mes_nacimiento,"Año_nacimiento":Year,"Peso":peso,"Direccion":Direccion, "Correo":Correo, "Contraseña":Password};
  }
}

class CLASE{}


class ASISTENCIA_CLASE{}