/// These are the libraries use in the application.
import 'dart:convert';
import 'package:crypto/crypto.dart';
import 'package:flutter/material.dart';
import 'package:gymtec_movil/database_handler.dart';
import 'package:gymtec_movil/sqlite_service.dart';
import 'package:intl/intl.dart';
import 'package:validators/validators.dart';
import 'package:http/http.dart' as http;
import 'ClassScreen.dart';
import 'main.dart';


/// Search Class with the given class according to the search filters.
class SearchClassScreen  extends StatefulWidget {
  /// Classes of the filter
  const SearchClassScreen ({super.key});
  @override
  _SearchClassScreenState createState() => _SearchClassScreenState();
}

/// Search Class State
class _SearchClassScreenState extends State<SearchClassScreen>{
  @override
  void initState() {
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    /// Style for the ElevatedButton
    final ButtonStyle style = ElevatedButton.styleFrom(textStyle: const TextStyle(fontSize: 20));
    /// Scaffold to show the screen
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
                    "Búsqueda de clases",
                    style: TextStyle(fontSize: 30),
                ),
                ElevatedButton(
                    style: style,
                    onPressed: () {
                      _navigateToSearch(context,"1");
                    },
                    child: const Text('Por Sucursal'),
                ), 
                ElevatedButton(
                    style: style,
                    onPressed: () {
                      _navigateToSearch(context,"2");
                    },
                    child: const Text('Por Servicio'),
                ),  
                ElevatedButton(
                    style: style,
                    onPressed: () {
                      _navigateToSearch(context,"3");
                    },
                    child: const Text('Por Períodos'),
                ), 
              ],
            ),
          ),
        ));
  }

  /// Method to navigate to the Search Screen
  void _navigateToSearch(BuildContext context,String type) {
    Navigator.of(context).push(MaterialPageRoute(builder: (context) => ClassScreen(type)));
  }
}