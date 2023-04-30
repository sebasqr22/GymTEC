import 'package:flutter/material.dart';
import 'package:gymtec_movil/database_handler.dart';
import 'package:gymtec_movil/sqlite_service.dart';
import 'package:intl/intl.dart';
import 'package:validators/validators.dart';
import 'package:http/http.dart' as http;

const double font_size = 20;
const double title_size = 32;

void main() => runApp(
  MyApp()
);

class MyApp extends StatelessWidget {
  const MyApp({super.key});
  static const String _title = 'GymTec';
  // This widget is the root of your application.
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: _title,
      theme: ThemeData(
        primarySwatch: Colors.blue,
      ),
      home: MyHomePage(),
    );
  }
}

class MyHomePage extends StatefulWidget {
  const MyHomePage({super.key});
  @override
  _MyHomePageState createState() => _MyHomePageState();
}

class _MyHomePageState extends State<MyHomePage> {
  late SqliteService _sqliteService;
  late TextEditingController _controllerPassword;
  TextEditingController emailAddress = TextEditingController();
  bool texterror = false;
  bool passwordVisible=false;
  

  @override
  void initState() {
    super.initState();
    _controllerPassword = TextEditingController();
    passwordVisible=true;
    emailAddress.text = "";
    this._sqliteService= SqliteService();
    this._sqliteService.initializeDB();
  }

  @override
  Widget build(BuildContext context) {
    final ButtonStyle style =
        ElevatedButton.styleFrom(textStyle: const TextStyle(fontSize: font_size));
    return Scaffold(
        backgroundColor: Colors.cyan[100],
        appBar: AppBar(
            title: Text(
          "Bienvenido a GymTEC",
          style: TextStyle(fontSize: title_size, fontWeight: FontWeight.normal),
        )),
        body: Center(
          child: Container(
            child: Column(
              children: <Widget>[
                Expanded(
                  child: Image(image: AssetImage('assets/logoGymTec.png')),
                ),
                Icon(IconData(0xe043, fontFamily: 'MaterialIcons'),size:60),
                Text(
                  "Correo electrónico: ",
                  style: TextStyle(fontSize: 25),
                ),
                TextField(
                       controller: emailAddress ,
                       decoration: InputDecoration( 
                        hintText: "Correo electrónico",
                         errorText: texterror?"Email no válido":null,
                       )
                     ),
                Text(
                  "Contraseña: ",
                  style: TextStyle(fontSize: 25),
                ),
                 TextField(
                  controller: _controllerPassword,
                  obscureText: passwordVisible,
                  decoration: InputDecoration(
                    border: UnderlineInputBorder(),
                    hintText: "Contraseña",
                    suffixIcon: IconButton(
                      icon: Icon(passwordVisible
                          ? Icons.visibility
                          : Icons.visibility_off),
                      onPressed: () {
                        setState(
                          () {
                            passwordVisible = !passwordVisible;
                          },
                        );
                      },
                    ),
                    alignLabelWithHint: false,
                    filled: true,
                  ),
                  keyboardType: TextInputType.visiblePassword,
                  textInputAction: TextInputAction.done,
                ),
                ElevatedButton(
                  style: style,
                  onPressed: () async {
                    if(isEmail(emailAddress.text) && _controllerPassword.text.isNotEmpty){
                            texterror = false;
                            _verCliente(emailAddress.text,_controllerPassword.text).then((clienteEncontrado) {
                              if(clienteEncontrado){
                                _navigateToWelcome(context);
                              }
                            });
                        }else{
                            texterror = true;
                    }
                  },
                  child: const Text('Iniciar sesión'),
                ),
                ElevatedButton(
                  style: style,
                  onPressed: () {
                    _navigateToRegister(context);
                  },
                  child: const Text('Registrarse'),
                ),
              ],
            ),
            margin: const EdgeInsets.all(10.0),
            width: 320.0,
            height: 600.0,
            decoration: BoxDecoration(
              borderRadius: BorderRadius.circular(40),
              color: Colors.cyan[100],
              boxShadow: [
                BoxShadow(color: Colors.blue, spreadRadius: 3),
              ],
            ),
          ),
        ));
  }

  void _navigateToWelcome(BuildContext context) {
    Navigator.of(context).push(MaterialPageRoute(builder: (context) => ClassScreen()));
  }

  void _navigateToRegister(BuildContext context) {
    Navigator.of(context).push(MaterialPageRoute(builder: (context) => RegisterScreen()));
  }

  Future<bool> _verCliente(String correo, String password) {
    Future<bool> miCliente = _sqliteService.iniciarSesion(correo,password);
    return miCliente;
  }
}


class RegisterScreen  extends StatefulWidget {
  const RegisterScreen ({super.key});
  @override
  _RegisterScreenState createState() => _RegisterScreenState();
}



class _RegisterScreenState extends State<RegisterScreen>{
  late SqliteService _sqliteService;
  late TextEditingController _cedulaController,_nombreController,_primerApellidoController,_segundoApellidoController,_edadController,_pesoController,_imcController,_direccionController,_correoController,_passwordController;
  TextEditingController dateInput = TextEditingController();
  bool passwordVisible=false;

  @override
  void initState() {
    super.initState();
    _cedulaController = TextEditingController();
    _nombreController = TextEditingController();
    _primerApellidoController = TextEditingController();
    _segundoApellidoController = TextEditingController();
    _edadController = TextEditingController();
    _pesoController = TextEditingController();
    _imcController = TextEditingController();
    _direccionController = TextEditingController();
    _correoController = TextEditingController();
    _passwordController = TextEditingController();
    passwordVisible = true;
    this._sqliteService= SqliteService();
    this._sqliteService.initializeDB();
  }

  @override
  Widget build(BuildContext context) {
    final ButtonStyle style =
        ElevatedButton.styleFrom(textStyle: const TextStyle(fontSize: 20));
    return Scaffold(
      appBar: AppBar(title: const Text('Bienvenido a GymTec')),
      body: Center(
          child: Container(
            child: ListView(
              padding: EdgeInsets.all(20),
              children: <Widget>[
                Text(
                  "LOGO",
                  style: TextStyle(fontSize: 25),
                ),
                Icon(IconData(0xe043, fontFamily: 'MaterialIcons'),size:60),
                Text(
                  "Cédula: ",
                  style: TextStyle(fontSize: 25),
                ),
                TextField(
                  keyboardType: TextInputType.number,
                  controller: _cedulaController,
                ),
                Text(
                  "Nombre: ",
                  style: TextStyle(fontSize: 25),
                ),
                TextField(
                  controller: _nombreController,
                  obscureText: true,
                ),
                Text(
                  "Primer Apellido: ",
                  style: TextStyle(fontSize: 25),
                ),
                TextField(
                  controller: _primerApellidoController,
                ),
                Text(
                  "Segundo Apellido: ",
                  style: TextStyle(fontSize: 25),
                ),
                TextField(
                  controller: _segundoApellidoController,
                  obscureText: true,
                ),
                Text(
                  "Edad: ",
                  style: TextStyle(fontSize: 25),
                ),
                TextField(
                  controller: _edadController,
                ),
                Text(
                  "Fecha de Nacimiento: ",
                  style: TextStyle(fontSize: 25),
                ),
                TextField(
              controller: dateInput,
              //editing controller of this TextField
              decoration: InputDecoration(
                  icon: Icon(Icons.calendar_today), //icon of text field
                  labelText: "Ingrese una fecha" //label text of field
                  ),
              readOnly: true,
              //set it true, so that user will not able to edit text
              onTap: () async {
                DateTime? pickedDate = await showDatePicker(
                    context: context,
                    initialDate: DateTime.now(),
                    firstDate: DateTime(1950),
                    //DateTime.now() - not to allow to choose before today.
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
                Text(
                  "Peso: ",
                  style: TextStyle(fontSize: 25),
                ),
                TextField(
                  controller: _pesoController,
                ),
                Text(
                  "IMC: ",
                  style: TextStyle(fontSize: 25),
                ),
                TextField(
                  controller: _imcController,
                ),
                Text(
                  "Dirección: ",
                  style: TextStyle(fontSize: 25),
                ),
                TextField(
                  controller: _direccionController,
                ),
                Text(
                  "Correo Electrónico: ",
                  style: TextStyle(fontSize: 25),
                ),
                TextField(
                  controller: _correoController,
                ),
                Text(
                  "Contraseña: ",
                  style: TextStyle(fontSize: 25),
                ),
                 TextField(
                  controller: _passwordController,
                  obscureText: passwordVisible,
                  decoration: InputDecoration(
                    border: UnderlineInputBorder(),
                    helperStyle:TextStyle(color:Colors.green),
                    suffixIcon: IconButton(
                      icon: Icon(passwordVisible
                          ? Icons.visibility
                          : Icons.visibility_off),
                      onPressed: () {
                        setState(
                          () {
                            passwordVisible = !passwordVisible;
                          },
                        );
                      },
                    ),
                    alignLabelWithHint: false,
                    filled: true,
                  ),
                  keyboardType: TextInputType.visiblePassword,
                  textInputAction: TextInputAction.done,
                ),
                ElevatedButton(
                  style: style,
                  onPressed: () {
                    if(int.parse(_cedulaController.text) !=0 && _nombreController.text != "" && int.parse(_edadController.text) >0 && isDate(dateInput.text) && double.parse(_pesoController.text) != 0 && double.parse(_imcController.text) > 0 &&_direccionController.text != "" && isEmail(_correoController.text) && _passwordController.text != ""){
                      crearCliente(int.parse(_cedulaController.text), _nombreController.text, _primerApellidoController.text, _segundoApellidoController.text, int.parse(_edadController.text), dateInput.text,double.parse(_pesoController.text), double.parse(_imcController.text), _direccionController.text, _correoController.text, _passwordController.text);
                    } 
                    //_navigateToClass(context);
                  },
                  child: const Text('Registrarse'),
                ),
              ],
            ),
            margin: const EdgeInsets.all(10.0),
            width: 320.0,
            height: 600.0,
            decoration: BoxDecoration(
              borderRadius: BorderRadius.circular(40),
              color: Colors.white,
              boxShadow: [
                BoxShadow(color: Colors.blue, spreadRadius: 3),
              ],
            ),
          ),
        ));
  }

  void _navigateToClass(BuildContext context) {
    Navigator.of(context).push(MaterialPageRoute(builder: (context) => ClassScreen()));
  }

  void crearCliente(int cedula, String nombre, String apellido1, String apellido2, int edad,String fecha_nacimiento, double peso, double imc,String direccion, String correo, String password){
  DateTime fecha = DateTime.parse(fecha_nacimiento);
    CLIENTE cliente = CLIENTE(
  cedula: cedula, 
  Nombre: nombre, 
  Apellido1: apellido1, 
  Apellido2: apellido2, 
  Dia_nacimiento: fecha.day.toString(),
  Mes_nacimiento:fecha.month.toString(),
  Year: fecha.year.toString(),
  peso: peso,
  Direccion: direccion,
  Correo: correo,
  Password: password
);
    this._sqliteService.createCliente(cliente);

  }
}

class ClassScreen  extends StatefulWidget {
  const ClassScreen ({super.key});
  // This widget is the home page of your application. It is stateful, meaning
  // that it has a State object (defined below) that contains fields that affect
  // how it looks.
  // This class is the configuration for the state.
  @override
  _ClassScreenState createState() => _ClassScreenState();
}



class _ClassScreenState extends State<ClassScreen>{
  late TextEditingController _controller;
  String? selectedSucursal = null;
  String? selectedClass = null;


  @override
  void initState() {
    super.initState();
    _controller = TextEditingController();
  }

  @override
  Widget build(BuildContext context) {
    final ButtonStyle style =
        ElevatedButton.styleFrom(textStyle: const TextStyle(fontSize: 20));
    return Scaffold(
      body: Center(
          child: Container(
            child: ListView(
              padding: EdgeInsets.all(40),
              children: <Widget>[
                Text(
                  "LOGO",
                  style: TextStyle(fontSize: 25),
                ),
                Text(
                  "Búsqueda de una clase",
                  style: TextStyle(fontSize: 30),
                ),
                  DropdownButtonFormField(
                  items: SucursalesItems,
                    onChanged: (String? newValue) {
                        selectedSucursal = newValue;
                    },
                      value: selectedSucursal,
                  hint: Text("Seleccione la sucursal"),
                  validator: (value) => value == null ? "Seleccione una sucursal" : null,
                  dropdownColor: Colors.blueAccent,
                  decoration: InputDecoration(
                      enabledBorder: OutlineInputBorder(
                      borderSide: BorderSide(color: Colors.blue, width: 2),
                      borderRadius: BorderRadius.circular(20),
                    ),
                      border: OutlineInputBorder(
                      borderSide: BorderSide(color: Colors.blue, width: 2),
                      borderRadius: BorderRadius.circular(20),
                    ),
                      filled: true,
                      fillColor: Colors.blueAccent,
                    ),
                  ),
                  DropdownButtonFormField(
                    decoration: InputDecoration(
                      enabledBorder: OutlineInputBorder(
                        borderSide: BorderSide(color: Colors.blue, width: 2),
                        borderRadius: BorderRadius.circular(20),
                      ),
                      border: OutlineInputBorder(
                        borderSide: BorderSide(color: Colors.blue, width: 2),
                        borderRadius: BorderRadius.circular(20),
                      ),
                      filled: true,
                      fillColor: Colors.blueAccent,
                    ),
                    hint: Text("Seleccione tipo de clase"),
                    validator: (value) => value == null ? "Seleccione un tipo de clase" : null,
                    dropdownColor: Colors.blueAccent,
                    value: selectedClass,
                    onChanged: (String? newValue) {
                        selectedClass = newValue;
                      
                    },
                    isDense: true,
                    isExpanded: true,
                  items: ClasesItems),
                  // Faltan fechas
                  ElevatedButton(
                  style: style,
                  onPressed: () {
                    if(selectedSucursal!=null && selectedClass !=null){
                     _navigateToClases(context);
                    }
                  },
                  child: const Text('Buscar clases'),
                ),  
              ],
            ),
            margin: const EdgeInsets.all(10.0),
            width: 320.0,
            height: 600.0,
            decoration: BoxDecoration(
              borderRadius: BorderRadius.circular(40),
              color: Colors.white,
              boxShadow: [
                BoxShadow(color: Colors.blue, spreadRadius: 3),
              ],
            ),
          ),
        ));
  }

  List<DropdownMenuItem<String>> get SucursalesItems{
    List<DropdownMenuItem<String>> menuItems = <String>["Sucursal 1","Sucursal 2","Sucursal 3","Sucursal 4"].map((item) {
                          return new DropdownMenuItem<String>(
                            child: new Text(item,
                                textAlign: TextAlign.center),
                            value: item, 
                          );
                        }).toList();
    return menuItems;
  }

   List<DropdownMenuItem<String>> get ClasesItems{
    List<DropdownMenuItem<String>> menuItems = [
      DropdownMenuItem(child: Text("Indoor Cycling"),value: "Indoor Cycling"),
      DropdownMenuItem(child: Text("Yoga"),value: "Yoga"),
      DropdownMenuItem(child: Text("Zumba"),value: "Zumba"),
      DropdownMenuItem(child: Text("Natación"),value: "Natación"),
    ];
    return menuItems;
  }

  void _navigateToClases(BuildContext context) {
    Navigator.of(context).push(MaterialPageRoute(builder: (context) => RegisterClass()));
  }
}

class RegisterClass extends StatefulWidget {
  const RegisterClass ({super.key});
  // This widget is the home page of your application. It is stateful, meaning
  // that it has a State object (defined below) that contains fields that affect
  // how it looks.
  // This class is the configuration for the state.
  @override
  _RegisterClassState createState() => _RegisterClassState();
}

class _RegisterClassState extends State<RegisterClass> {
  @override
  void initState() {
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    // This method is rerun every time setState is called, for instance as done
    // by the _incrementCounter method above.
    //
    // The Flutter framework has been optimized to make rerunning build methods
    // fast, so that you can just rebuild anything that needs updating rather
    // than having to individually change instances of widgets.
    final ButtonStyle style =
        ElevatedButton.styleFrom(textStyle: const TextStyle(fontSize: 20));
    return Scaffold(
        body: Center(
          child: Container(
            child: Column(
              children: <Widget>[
                Text(
                  "LOGO",
                  style: TextStyle(fontSize: 25),
                ),
                Text(
                  "Registro de Clase en SUCURSAL NOMBRE: ",
                  style: TextStyle(fontSize: 40),
                ),
                 Card(
                  child: Column(
                    mainAxisSize: MainAxisSize.min,
                    children: <Widget>[
                      const ListTile(
                        leading: Icon(Icons.album),
                        title: Text('The Enchanted Nightingale'),
                        subtitle: Text('Music by Julie Gable. Lyrics by Sidney Stein.'),
                      ),
                      Row(
                        mainAxisAlignment: MainAxisAlignment.end,
                        children: <Widget>[
                          TextButton(
                            child: const Text('BUY TICKETS'),
                            onPressed: () {/* ... */},
                          ),
                          const SizedBox(width: 8),
                          TextButton(
                            child: const Text('LISTEN'),
                            onPressed: () {/* ... */},
                          ),
                          const SizedBox(width: 8),
                        ],
                      ),
                    ],
                  ),
                ),
                ElevatedButton(
                  style: style,
                  onPressed: () {
                    _ShowRegistrationState(context);
                  },
                  child: const Text('Registrar Clase'),
                ),
              ],
            ),
            margin: const EdgeInsets.all(10.0),
            width: 320.0,
            height: 600.0,
            decoration: BoxDecoration(
              borderRadius: BorderRadius.circular(40),
              color: Colors.white,
              boxShadow: [
                BoxShadow(color: Colors.blue, spreadRadius: 3),
              ],
            ),
          ),
        ));
  }

  void _ShowRegistrationState(BuildContext context) {
    Navigator.of(context).push(MaterialPageRoute(builder: (context) => ClassScreen()));
  }
}