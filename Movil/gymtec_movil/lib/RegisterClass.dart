/// These are the libraries use in the application.
import 'dart:convert';
import 'package:crypto/crypto.dart';
import 'package:flutter/material.dart';
import 'package:gymtec_movil/database_handler.dart';
import 'package:gymtec_movil/sqlite_service.dart';
import 'package:intl/intl.dart';
import 'package:validators/validators.dart';
import 'package:http/http.dart' as http;
import 'SearchClass.dart';

/// Register Class with the given class according to the search filters.
class RegisterClass extends StatefulWidget {
  /// Classes of the filter
  final String clases;
  /// Constructor
  const RegisterClass (this.clases,{super.key});

  /// Method to create the state of the class
  @override
  _RegisterClassState createState() => _RegisterClassState(clases); 
}

/// Register Class State
class _RegisterClassState extends State<RegisterClass> {
  // has parameter clases
  final String clases;
  /// Constructor
  _RegisterClassState(this.clases);
  
  /// Method to initialize the state of the class
  @override
  void initState() {
    super.initState();
  }

  /// Method to return to the Search Screen
  @override
  Widget build(BuildContext context) {
    /// Style for the ElevatedButton
    final ButtonStyle style = ElevatedButton.styleFrom(textStyle: const TextStyle(fontSize: 20));
    /// Alert Dialog to show the registration message
    Widget okButton = TextButton(
      child: Text("OK"),
      onPressed: () { 
        Navigator.of(context).pop(); // dismiss dialog
      },
    );
    /// Alert Dialog to show the registration message
    AlertDialog alert = AlertDialog(
      title: Text("Mensaje de Registro"),
      content: Text("Registro exitoso!"),
      actions: [
        okButton,
      ],
    ); 

    /// Scaffold to show the screen
    return Scaffold(
      backgroundColor: Colors.cyan[100],
        body: Center(
          child: Container(
            margin: const EdgeInsets.all(10.0),
            width: 320.0,
            height: 700.0,
            decoration: BoxDecoration(
              borderRadius: BorderRadius.circular(40),
              color: Colors.cyan[100],
              boxShadow: const [
                BoxShadow(color: Colors.blue, spreadRadius: 3),
              ],
            ),
            child: Column(
              children: <Widget>[
                const Expanded(
                  child: Image(image: AssetImage('assets/logoGymTec.png')),
                ),
                const Text(
                  "Registro de Clase: ",
                  style: TextStyle(fontSize: 40),
                ),
                Card(
                  child: Column(
                    mainAxisSize: MainAxisSize.min,
                    children: <Widget>[
                      const ListTile(
                        title: Text('Clase 1'),
                        subtitle: Text("Fecha de Inicio: 8-5-2023\nFecha de Finalización: 8-5-2023\nCapacidad: 20\nInstructor: Justin Fernández\n"),
                      ),
                      Row(
                        mainAxisAlignment: MainAxisAlignment.end,
                        children: <Widget>[
                          TextButton(
                            child: const Text('Registrar clase'),
                            onPressed: () {
                              showDialog(
                                context: context,
                                builder: (BuildContext context) {
                                  return alert;
                                },);
                              },
                            ),
                            const SizedBox(width: 8),
                          ],
                      ),
                    ],
                  ),
                ),
                Card(
                  child: Column(
                    mainAxisSize: MainAxisSize.min,
                    children: <Widget>[
                      const ListTile(
                        title: Text('Clase 2'),
                        subtitle: Text("Fecha de Inicio: 8-5-2023\nFecha de Finalización: 8-5-2023\nCapacidad: 20\nInstructor: Justin Fernández\n"),
                      ),
                      Row(
                        mainAxisAlignment: MainAxisAlignment.end,
                        children: <Widget>[
                          TextButton(
                            child: const Text('Registrar clase'),
                            onPressed: () {
                              showDialog(
                        context: context,
                        builder: (BuildContext context) {
                          return alert;
                        },
                      );
                            },
                          ),
                          const SizedBox(width: 8),
                        ],
                      ),
                    ],
                  ),
                ),
                /// Button to return to the Search Screen
                ElevatedButton(
                  style: style,
                  onPressed: () {
                    _ShowRegistrationState(context);
                  },
                  child: const Text('Regresar'),
                ),
              ],
            ),
          ),
        ));
  }

  /// Method to return to the Search Screen
  void _ShowRegistrationState(BuildContext context) {
    Navigator.of(context).push(MaterialPageRoute(builder: (context) => SearchClassScreen()));
  }
}