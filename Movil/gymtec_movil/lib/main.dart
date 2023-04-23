import 'package:flutter/material.dart';
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
                  onPressed: () {
                    if(isEmail(emailAddress.text) && _controllerPassword.text.isNotEmpty){
                            texterror = false;
                            /*
                            var requestBody = {
                              'parametro1': 'valor1',
                              'parametro2': 'valor2',
                            };
                            Future<http.Response> LoginCliente() {
                                return http.get(Uri.parse('this.http.get("https://localhost:7194/usuarios/cliente/LoginCliente'));
                            }
                            */
                            _navigateToWelcome(context);
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
}


class RegisterScreen  extends StatefulWidget {
  const RegisterScreen ({super.key});
  // This widget is the home page of your application. It is stateful, meaning
  // that it has a State object (defined below) that contains fields that affect
  // how it looks.
  // This class is the configuration for the state.
  @override
  _RegisterScreenState createState() => _RegisterScreenState();
}



class _RegisterScreenState extends State<RegisterScreen>{
  late TextEditingController _controller,_controller2,controller3,controller4,controller5,controller6,controller7,controller8,controller9;
  TextEditingController dateInput = TextEditingController();
  bool passwordVisible=false;

  @override
  void initState() {
    super.initState();
    _controller = TextEditingController();
    _controller2 = TextEditingController();
    controller3 = TextEditingController();
    controller4 = TextEditingController();
    controller5 = TextEditingController();
    controller6 = TextEditingController();
    controller7 = TextEditingController();
    controller8 = TextEditingController();
    controller9 = TextEditingController();
    passwordVisible = true;
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
                  controller: _controller,
                ),
                Text(
                  "Nombre: ",
                  style: TextStyle(fontSize: 25),
                ),
                TextField(
                  controller: _controller2,
                  obscureText: true,
                ),
                Text(
                  "Primer Aepllido: ",
                  style: TextStyle(fontSize: 25),
                ),
                TextField(
                  controller: controller3,
                ),
                Text(
                  "Segundo Apellido: ",
                  style: TextStyle(fontSize: 25),
                ),
                TextField(
                  controller: controller4,
                  obscureText: true,
                ),
                Text(
                  "Edad: ",
                  style: TextStyle(fontSize: 25),
                ),
                TextField(
                  controller: controller5,
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
                  controller: controller6,
                ),
                Text(
                  "IMC: ",
                  style: TextStyle(fontSize: 25),
                ),
                TextField(
                  controller: controller7,
                ),
                Text(
                  "Dirección: ",
                  style: TextStyle(fontSize: 25),
                ),
                TextField(
                  controller: controller8,
                ),
                Text(
                  "Correo Electrónico: ",
                  style: TextStyle(fontSize: 25),
                ),
                TextField(
                  controller: controller9,
                ),
                Text(
                  "Contraseña: ",
                  style: TextStyle(fontSize: 25),
                ),
                 TextField(
                  obscureText: passwordVisible,
                  decoration: InputDecoration(
                    border: UnderlineInputBorder(),
                    hintText: "Password",
                    labelText: "Password",
                    helperText:"Password must contain special character",
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
                    _navigateToClass(context);
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