/// These are the libraries use in the application.
import 'dart:convert';
import 'package:crypto/crypto.dart';
import 'package:flutter/material.dart';
import 'package:gymtec_movil/database_handler.dart';
import 'package:gymtec_movil/sqlite_service.dart';
import 'package:intl/intl.dart';
import 'package:validators/validators.dart';
import 'package:http/http.dart' as http;

import 'HomePage.dart';
import 'RegisterClass.dart';
import 'SearchClass.dart';
import 'SignUpScreen.dart';

// Global variables for the components of the application.
const double font_size = 20;
const double title_size = 32;
/// This is the main method that run the application.
void main() => runApp(
  MyApp()
);

/// This is the class that contains the main widget of the application.
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