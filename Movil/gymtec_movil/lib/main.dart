import 'package:flutter/material.dart';

void main() => runApp(MyApp());

class MyApp extends StatelessWidget {
  const MyApp({super.key});
  static const String _title = 'Flutter Stateful Clicker Counter';
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
  // This widget is the home page of your application. It is stateful, meaning
  // that it has a State object (defined below) that contains fields that affect
  // how it looks.
  // This class is the configuration for the state.
  @override
  _MyHomePageState createState() => _MyHomePageState();
}

class _MyHomePageState extends State<MyHomePage> {
  late TextEditingController _controller;
  late TextEditingController _controller2;
  bool passwordVisible=false;

  @override
  void initState() {
    super.initState();
    _controller = TextEditingController();
    _controller2 = TextEditingController();
    passwordVisible=true;
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
        appBar: AppBar(
            title: Text(
          "Bienvenido a GymTEC",
          style: TextStyle(fontSize: 32, fontWeight: FontWeight.normal),
        )),
        body: Center(
          child: Container(
            child: Column(
              children: <Widget>[
                Text(
                  "LOGO",
                  style: TextStyle(fontSize: 25),
                ),
                Icon(IconData(0xe043, fontFamily: 'MaterialIcons'),size:60),
                Text(
                  "Correo electrónico: ",
                  style: TextStyle(fontSize: 25),
                ),
                TextField(
                  controller: _controller,
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
                    _navigateToWelcome(context);
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
              color: Colors.white,
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
  late TextEditingController _controller;
  late TextEditingController _controller2;

  @override
  void initState() {
    super.initState();
    _controller = TextEditingController();
    _controller2 = TextEditingController();
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
                  controller: _controller,
                ),
                Text(
                  "Segundo Apellido: ",
                  style: TextStyle(fontSize: 25),
                ),
                TextField(
                  controller: _controller2,
                  obscureText: true,
                ),
                Text(
                  "Edad: ",
                  style: TextStyle(fontSize: 25),
                ),
                TextField(
                  controller: _controller,
                ),
                Text(
                  "Fecha de Nacimiento: ",
                  style: TextStyle(fontSize: 25),
                ),
                TextField(
                  controller: _controller2,
                  obscureText: true,
                ),
                Text(
                  "Peso: ",
                  style: TextStyle(fontSize: 25),
                ),
                TextField(
                  controller: _controller,
                ),
                Text(
                  "IMC: ",
                  style: TextStyle(fontSize: 25),
                ),
                TextField(
                  controller: _controller2,
                  obscureText: true,
                ),
                Text(
                  "Dirección: ",
                  style: TextStyle(fontSize: 25),
                ),
                TextField(
                  controller: _controller,
                ),
                Text(
                  "Correo Electrónico: ",
                  style: TextStyle(fontSize: 25),
                ),
                TextField(
                  controller: _controller2,
                  obscureText: true,
                ),
                Text(
                  "Contraseña: ",
                  style: TextStyle(fontSize: 25),
                ),
                TextField(
                  controller: _controller,
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
              color: Colors.white,
              boxShadow: [
                BoxShadow(color: Colors.blue, spreadRadius: 3),
              ],
            ),
          ),
        ));
  }

  void _navigateToRegister(BuildContext context) {
    Navigator.of(context).push(MaterialPageRoute(builder: (context) => RegisterScreen()));
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
  String? selectedValue = null;
  final _dropdownFormKey = GlobalKey<FormState>();


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
              padding: EdgeInsets.all(20),
              children: <Widget>[
                Text(
                  "LOGO",
                  style: TextStyle(fontSize: 25),
                ),
                Text(
                  "Búsqueda de una clase",
                  style: TextStyle(fontSize: 40),
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
                    validator: (value) => value == null ? "Seleccione una sucursal" : null,
                    dropdownColor: Colors.blueAccent,
                    value: selectedValue,
                    onChanged: (String? newValue) {
                      setState(() {
                        selectedValue = newValue!;
                      });
                    },
                  items: SucursalesItems),
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
                    validator: (value) => value == null ? "Seleccione un tipo de clase" : null,
                    dropdownColor: Colors.blueAccent,
                    value: selectedValue,
                    onChanged: (String? newValue) {
                      setState(() {
                        selectedValue = newValue!;
                      });
                    },
                  items: ClasesItems),
                  // Faltan fechas
                  ElevatedButton(
                  style: style,
                  onPressed: () {
                    _navigateToClases(context);
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
    List<DropdownMenuItem<String>> menuItems = [
      DropdownMenuItem(child: Text("Sucursal 1"),value: "Sucursal 1"),
      DropdownMenuItem(child: Text("Sucursal 2"),value: "Sucursal 2"),
      DropdownMenuItem(child: Text("Sucursal 3"),value: "Sucursal 3"),
      DropdownMenuItem(child: Text("Sucursal 4"),value: "Sucursal 4"),
    ];
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
    Navigator.of(context).push(MaterialPageRoute(builder: (context) => RegisterScreen()));
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