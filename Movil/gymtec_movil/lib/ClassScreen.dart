/// These are the libraries use in the application.
import 'dart:convert';
import 'package:crypto/crypto.dart';
import 'package:flutter/material.dart';
import 'package:gymtec_movil/database_handler.dart';
import 'package:gymtec_movil/sqlite_service.dart';
import 'package:intl/intl.dart';
import 'package:validators/validators.dart';
import 'package:http/http.dart' as http;
import 'RegisterClass.dart';
import 'SearchClass.dart';


/// Class Screen Class
class ClassScreen  extends StatefulWidget {
  final String type;
  /// Classes of the filter
  const ClassScreen (this.type,{super.key});
  @override
  _ClassScreenState createState() => _ClassScreenState(type);
}


/// Class Screen State
class _ClassScreenState extends State<ClassScreen>{
  /// Controller for the text field
  late TextEditingController _controller,dateInput, dateInput2;
  String? selectedSucursal = null;
  String? selectedClass = null;
  late SqliteService _sqliteService;
  final String type;
  /// Constructor
  _ClassScreenState (this.type);

  @override
  void initState() {
    super.initState();
    /// Initialize the controller
    _controller = TextEditingController();
    dateInput = TextEditingController();
    dateInput2 = TextEditingController();
    /// Initialize the database
    this._sqliteService= SqliteService();
    this._sqliteService.initializeDB();
    /// Update the database
    _updateClases();
    _updateServicios();
    _updateEmpleados();
  }

  @override
  Widget build(BuildContext context) {
    Widget okButton = TextButton(
        child: Text("OK"),
        onPressed: () { 
          Navigator.of(context).pop(); // dismiss dialog
        },
    );
    AlertDialog alert = AlertDialog(
      title: const Text("Error de búsqueda"),
      content: const Text("Seleccione una opción de búsqueda!"),
      actions: [
        okButton,
      ],
    ); 
    final ButtonStyle style = ElevatedButton.styleFrom(textStyle: const TextStyle(fontSize: 20));
    /// Scaffold to show the screen
    if(type=="1"){
      return Scaffold(
        backgroundColor: Colors.cyan[100],
        body: Center(
            child: Container(
              margin: const EdgeInsets.all(10.0),
              width: 320.0,
              height: 600.0,
              decoration: BoxDecoration(
                borderRadius: BorderRadius.circular(40),
                color: Colors.cyan[100],
                boxShadow: const [
                  BoxShadow(color: Colors.blue, spreadRadius: 3),
                ],
              ),
              child: ListView(
                padding: const EdgeInsets.all(40),
                children: <Widget>[
                  const Expanded(
                    child: Image(image: AssetImage('assets/logoGymTec.png')),
                  ),
                  const Text(
                    "Búsqueda de una clase",
                    style: TextStyle(fontSize: 30),
                  ),
                  DropdownButtonFormField(
                    items: SucursalesItems,
                      onChanged: (String? newValue) {
                          selectedSucursal = newValue;
                      },
                    value: selectedSucursal,
                    hint: const Text("Seleccione la sucursal"),
                    validator: (value) => value == null ? "Seleccione una sucursal" : null,
                    dropdownColor: Colors.blueAccent,
                    decoration: InputDecoration(
                        enabledBorder: OutlineInputBorder(
                        borderSide: const BorderSide(color: Colors.blue, width: 2),
                        borderRadius: BorderRadius.circular(20),
                        ),
                        border: OutlineInputBorder(
                        borderSide: const BorderSide(color: Colors.blue, width: 2),
                        borderRadius: BorderRadius.circular(20),
                        ),
                        filled: true,
                        fillColor: Colors.blueAccent,
                    ),
                  ),
                  ElevatedButton(
                      style: style,
                      onPressed: () {
                        if (selectedSucursal!=null){
                          _verClasesPorSucursal(selectedSucursal.toString());
                        }else{
                          showDialog(
                            context: context,
                            builder: (BuildContext context) {
                              return alert;
                            },
                          );
                        }
                      },
                      child: const Text('Buscar clases'),
                  ),  
                ],
              ),
            ),
          ));
    }else if(type=="2"){
      return Scaffold(
      backgroundColor: Colors.cyan[100],
      body: Center(
          child: Container(
            margin: const EdgeInsets.all(10.0),
            width: 320.0,
            height: 600.0,
            decoration: BoxDecoration(
              borderRadius: BorderRadius.circular(40),
              color: Colors.cyan[100],
              boxShadow: const [
                BoxShadow(color: Colors.blue, spreadRadius: 3),
              ],
            ),
            child: ListView(
              padding: const EdgeInsets.all(20),
              children: <Widget>[
                const Expanded(
                  child: Image(image: AssetImage('assets/logoGymTec.png')),
                ),
                const Text(
                  "Búsqueda de una clase",
                  style: TextStyle(fontSize: 30),
                ),
                DropdownButtonFormField(
                    decoration: InputDecoration(
                      enabledBorder: OutlineInputBorder(
                        borderSide: const BorderSide(color: Colors.blue, width: 2),
                        borderRadius: BorderRadius.circular(20),
                      ),
                      border: OutlineInputBorder(
                        borderSide: const BorderSide(color: Colors.blue, width: 2),
                        borderRadius: BorderRadius.circular(20),
                      ),
                      filled: true,
                      fillColor: Colors.blueAccent,
                    ),
                    hint: const Text("Seleccione tipo de clase"),
                    validator: (value) => value == null ? "Seleccione un tipo de clase" : null,
                    dropdownColor: Colors.blueAccent,
                    value: selectedClass,
                    onChanged: (String? newValue) {
                        selectedClass = newValue;
                        _updateClases();
                    },
                    isDense: true,
                    isExpanded: true,
                  items: ClasesItems),       
                  ElevatedButton(
                  style: style,
                  onPressed: () {
                    if (selectedClass!=null){
                      _verClasesPorTipoClase(selectedClass.toString());
                    }else{
                      showDialog(
                        context: context,
                        builder: (BuildContext context) {
                          return alert;
                        },
                      );
                    }
                  },
                  child: const Text('Buscar clases'),
                ),  
              ],
            ),
          ),
        ));
    }else{
    return Scaffold(
      backgroundColor: Colors.cyan[100],
      body: Center(
          child: Container(
            margin: const EdgeInsets.all(10.0),
            width: 320.0,
            height: 600.0,
            decoration: BoxDecoration(
              borderRadius: BorderRadius.circular(40),
              color: Colors.cyan[100],
              boxShadow: const [
                BoxShadow(color: Colors.blue, spreadRadius: 3),
              ],
            ),
            child: ListView(
              padding: EdgeInsets.all(40),
              children: <Widget>[
                const Expanded(
                  child: Image(image: AssetImage('assets/logoGymTec.png')),
                ),
                const Text(
                  "Búsqueda de una clase",
                  style: TextStyle(fontSize: 30),
                ),
                  TextField(
              controller: dateInput,
              //editing controller of this TextField
              decoration: const InputDecoration(
                  icon: Icon(Icons.calendar_today), //icon of text field
                  labelText: "Fecha de inicio" //label text of field
                  ),
              readOnly: true,
              //set it true, so that user will not able to edit text
              onTap: () async {
                DateTime? pickedDate = await showDatePicker(
                    context: context,
                    initialDate: DateTime.now(),
                    firstDate: DateTime.now(),
                    //DateTime.now(), //- not to allow to choose before today.
                    lastDate: DateTime(2100));
 
                if (pickedDate != null) {
                  print(
                      pickedDate); //pickedDate output format => 2021-03-10 00:00:00.000
                  String formattedDate =
                      DateFormat('yyyy-MM-dd').format(pickedDate);
                  print(
                      formattedDate); //formatted date output using intl package =>  2021-03-16
                  setState(() {
                    dateInput.text =
                        formattedDate; //set output date to TextField value.
                  });
                } else {}
              }),
              TextField(
              controller: dateInput2,
              //editing controller of this TextField
              decoration: const InputDecoration(
                  icon: Icon(Icons.calendar_today), //icon of text field
                  labelText: "Fecha Final" //label text of field
                  ),
              readOnly: true,
              //set it true, so that user will not able to edit text
              onTap: () async {
                DateTime? pickedDate = await showDatePicker(
                    context: context,
                    initialDate: DateTime.parse(dateInput.text),
                    firstDate: DateTime.parse(dateInput.text),
                    //DateTime.now() - not to allow to choose before today.
                    lastDate: DateTime(2100));
 
                if (pickedDate != null) {
                  String formattedDate = DateFormat('yyyy-MM-dd').format(pickedDate);
                  setState(() {
                    dateInput2.text = formattedDate; //set output date to TextField value.
                  });
                } else {}
              }),
                  ElevatedButton(
                  style: style,
                  onPressed: () {
                    if(dateInput.text.isNotEmpty && dateInput2.text.isNotEmpty){
                      _verClasesPorFechas(dateInput.text, dateInput2.text);
                    }else{
                      showDialog(
    context: context,
    builder: (BuildContext context) {
      return alert;
    },
  );
                    }
                  },
                  child: const Text('Buscar clases'),
                ),  
              ],
            ),
          ),
        ));
  }
  }

  List<DropdownMenuItem<String>> get SucursalesItems{
    List<DropdownMenuItem<String>> menuItems = <String>["GymTEC Campus Central Cartago","GymTEC Campus San Carlos","GymTEC Campus San José"].map((item) {
                          return DropdownMenuItem<String>(
                            value: item,
                            child: Container(
                              height: 50,
                              width: 190,
                              child:Text(item,
                                textAlign: TextAlign.center),
                                
                          ));
                        }).toList();
    return menuItems;
  }

   List<DropdownMenuItem<String>> get ClasesItems{
    List<DropdownMenuItem<String>> menuItems = [
      const DropdownMenuItem(child: Text("Indoor Cycling"),value: "Indoor Cycling"),
      const DropdownMenuItem(child: Text("Yoga"),value: "Yoga"),
      const DropdownMenuItem(child: Text("Zumba"),value: "Zumba"),
      const DropdownMenuItem(child: Text("Natación"),value: "Natacion"),
    ];
    return menuItems;
  }


  /// Go to the register screen
  void _navigateToClases(BuildContext context,String clases) {
    Navigator.of(context).push(MaterialPageRoute(builder: (context) => RegisterClass(clases)));
  }

  /// Update the table of the database called EMPLEADOS
  void _updateEmpleados() async{
    final response = (await http.get(Uri.parse("https://dfd2-2803-6000-e001-acc-4cd4-be42-eccc-c067.ngrok-free.app/usuarios/admin/VerEmpleados")));
    final body = utf8.decode(response.bodyBytes);
    final jsonBody = jsonDecode(body);
    for(int i = 0; i<jsonBody.length-1;i++){
      EMPLEADO c = EMPLEADO(cedula: jsonBody[i]["Cedula"],nombre: jsonBody[i]["Nombre"],apellido1: jsonBody[i]["Apellido1"],apellido2: jsonBody[i]["Apellido2"],distrito: jsonBody[i]["Distrito"],canton: jsonBody[i]["Canton"],provincia: jsonBody[i]["Provincia"],correo: jsonBody[i]["Correo"],contrasena: jsonBody[i]["Contrasena"],salario: jsonBody[i]["Salario"],id_puesto: jsonBody[i]["Id_puesto"], id_planilla: jsonBody[i]["Id_planilla"],codigo_suc: jsonBody[i]["Codigo_suc"]);
      this._sqliteService.createEmpleado(c);
    }
  }

  /// Update the table of the database called SERVICIOS
  void _updateServicios() async{
    final response = (await http.get(Uri.parse("https://dfd2-2803-6000-e001-acc-4cd4-be42-eccc-c067.ngrok-free.app/usuarios/admin/VerServicios")));
    final body = utf8.decode(response.bodyBytes);
    final jsonBody = jsonDecode(body);
    for(int i = 0; i<jsonBody.length-1;i++){
      SERVICIO c = SERVICIO(identificador:jsonBody[i]["identificador"],descripcion:jsonBody[i]["descripcion"]);
      this._sqliteService.createServicio(c);
    }
  }

  /// Update the table of the database called CLASES
  void _updateClases() async{
    final response = (await http.get(Uri.parse("https://dfd2-2803-6000-e001-acc-4cd4-be42-eccc-c067.ngrok-free.app/usuarios/admin/VerClases")));
    final body = utf8.decode(response.bodyBytes);
    final jsonBody = jsonDecode(body);
    for(int i = 0; i<jsonBody.length-1;i++){
      CLASE c = CLASE(id_servicio:jsonBody[i]["id_servicio"],num_clase:jsonBody[i]["num_clase"],fecha:jsonBody[i]["fecha"],hora_fin: jsonBody[i]["hora_fin"],hora_inicio: jsonBody[i]["hora_inicio"],capacidad: jsonBody[i]["capacidad"],cedula_instructor: jsonBody[i]["cedula_instructor"]);
      this._sqliteService.createClase(c);
    }
  }


  /// Update the table of the database called SUCURSAL
  void _verClasesPorSucursal(String sucursal) async{
    //final response = await http.get(Uri.parse("https://2185-190-2-222-247.ngrok-free.app/usuarios/admin/VerClases"));
    //final body = utf8.decode(response.bodyBytes);
    //print(jsonDecode(body));
    //final response = this._sqliteService.buscarClasesPorSucursal(sucursal);
    //print(response);
    _navigateToClases(context,"hola1");
  }

  //metodo de ver clases por tipoclase
  void _verClasesPorTipoClase(String tipoClase) async{
    //final response = await http.get(Uri.parse("https://2185-190-2-222-247.ngrok-free.app/usuarios/admin/VerClases"));
    //final body = utf8.decode(response.bodyBytes);
    //print(jsonDecode(body));
    final response = this._sqliteService.buscarClasesPorServicio(tipoClase);
    print(response);
    _navigateToClases(context,"hola2");
  }

  //metodo de ver clases por fechas
  void _verClasesPorFechas(String fechaInicio, String fechaFinal) async{
    //final response = await http.get(Uri.parse("https://2185-190-2-222-247.ngrok-free.app/usuarios/admin/VerClases"));
    //final body = utf8.decode(response.bodyBytes);
    //print(jsonDecode(body));
    final response = this._sqliteService.buscarClasesPorPeriodo(fechaInicio, fechaFinal);
    print(response);
    _navigateToClases(context,"hola3");
  }
}