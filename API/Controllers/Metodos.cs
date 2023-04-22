using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using DataBaseManagement;
using System.Data.SqlClient;
using System.Globalization;

namespace Metodos{

    [ApiController]
    [Route("usuarios")]
    public class MetodosAPI: ControllerBase{
      private DatabaseHandler DB_Handler = new DatabaseHandler();  
      [HttpPost]
      [Route("cliente/AgregarTratamientoSPA")]
      public dynamic AgregarTratamientoSPA(string nombreSucursal, int numSpa){
        try{
          // VERIFICACION DE DATOS
          if (string.IsNullOrEmpty(nombreSucursal) || numSpa == 0) {
              return new { message = "error" };}
          // VERIFICAR QUE EL TRATAMIENTO NO EXISTA PREVIAMENTE
          DB_Handler.ConectarServer();
          DB_Handler.AbrirConexion();
          string querySelect = "SELECT * FROM TRATAMIENTO_SPA WHERE Nsucursal = @Nsucursal AND Spa = @Spa";
          using (SqlCommand comando = new SqlCommand(querySelect, DB_Handler.conectarDB)) {
              comando.Parameters.AddWithValue("@Nsucursal", nombreSucursal);
              comando.Parameters.AddWithValue("@Spa", numSpa);
              using (SqlDataReader reader = comando.ExecuteReader()) {
                  if (reader.HasRows) {
                      DB_Handler.CerrarConexion();
                      return new { message = "Tratamiento ya existe en la BD. Error" };
                  }
                  else {
                      DB_Handler.CerrarConexion(); // cerrar conexion de la query anterior para abrir otra
                      // INSERTAR TRATAMIENTO_SPA EN LA BASE DE DATOS
                      DB_Handler.ConectarServer();
                      DB_Handler.AbrirConexion();
                      string queryInsert = "INSERT INTO TRATAMIENTO_SPA VALUES (@Nsucursal, @Spa)";
                      using (SqlCommand comando2 = new SqlCommand(queryInsert, DB_Handler.conectarDB)) {
                          comando2.Parameters.AddWithValue("@Nsucursal", nombreSucursal);
                          comando2.Parameters.AddWithValue("@Spa", numSpa);
                          comando2.ExecuteNonQuery();
                          Console.WriteLine("Tratamiento agregado exitosamente");
                          DB_Handler.CerrarConexion();
                      }
                  }
                } 
            }
          // retornar json con todos los tratamientos existentes
          return new { message = "ok" };
        }catch(Exception e){
          Console.WriteLine(e);
          return new { message = "error" };
        }
    }

      [HttpGet]
      [Route("cliente/EliminarTratamientoSPA")]
      public dynamic EliminarTratamientoSPA(string nombreSucursal, int numSpa){
        try{
            // VERIFICACION DE DATOS
            if (string.IsNullOrEmpty(nombreSucursal) || numSpa == 0) {
                return new { message = "error" };}

            // VERIFICAR QUE EL TRATAMIENTO EXISTA PREVIAMENTE
            DB_Handler.ConectarServer();
            DB_Handler.AbrirConexion();
            string querySelect = "SELECT * FROM TRATAMIENTO_SPA WHERE Nsucursal = @Nsucursal AND Spa = @Spa";
            using (SqlCommand comando = new SqlCommand(querySelect, DB_Handler.conectarDB)) {
                comando.Parameters.AddWithValue("@Nsucursal", nombreSucursal);
                comando.Parameters.AddWithValue("@Spa", numSpa);
                using (SqlDataReader reader = comando.ExecuteReader()) {
                    if (!reader.HasRows) {
                        DB_Handler.CerrarConexion();
                        return new { message = "Tratamiento no existe en la BD. Error" };
                    }
                    else {
                        DB_Handler.CerrarConexion(); // cerrar conexion de la query anterior para abrir otra
                        // ELIMINAR TRATAMIENTO_SPA EN LA BASE DE DATOS
                        DB_Handler.ConectarServer();
                        DB_Handler.AbrirConexion();
                        string queryDelete = "DELETE FROM TRATAMIENTO_SPA WHERE Nsucursal = @Nsucursal AND Spa = @Spa";
                        using (SqlCommand comando2 = new SqlCommand(queryDelete, DB_Handler.conectarDB)) {
                            comando2.Parameters.AddWithValue("@Nsucursal", nombreSucursal);
                            comando2.Parameters.AddWithValue("@Spa", numSpa);
                            comando2.ExecuteNonQuery();
                            Console.WriteLine("Tratamiento eliminado exitosamente");
                            DB_Handler.CerrarConexion();
                        }
                    }
                }
            }
            // VER LOS TRATAMIENTOS EXISTENTES
            DB_Handler.ConectarServer();
            DB_Handler.AbrirConexion();
            string querySelect2 = "SELECT * FROM TRATAMIENTO_SPA";
            using (SqlCommand comando3 = new SqlCommand(querySelect2, DB_Handler.conectarDB)) {
                using (SqlDataReader reader2 = comando3.ExecuteReader()) {
                    if (!reader2.HasRows) {
                        DB_Handler.CerrarConexion();
                        return new { message = "No hay tratamientos en la BD. Error" };
                    }
                    else {
                        // ALMACENAR LOS TRATAMIENTOS EN UN JSON
                        // estructura:  {{Nsucursal, Spa}, {Nsucursal, Spa}, ...}  o sea json de json
                        var tratamientosExistentes = new List<dynamic>();
                        while (reader2.Read()) {
                            tratamientosExistentes.Add(new {
                                Nsucursal = reader2.GetString(0),
                                Spa = reader2.GetInt32(1)
                            });
                        }
                        string json_tratamientosExistentes = JsonSerializer.Serialize(tratamientosExistentes);
                        return json_tratamientosExistentes;
                    }
                }
            }        
        }catch(Exception e){
          return new { message = "error" };
        }
      }

      [HttpPost]
      [Route("cliente/SignUpCliente")]
      public dynamic SignUpCliente(string cedula, string nombre, string apellido1, string apellido2, string fechaNacimiento, string peso, string direccion, string correoElectronico, string contrasena) {
          try {

             double pesoNew = Convert.ToDouble(peso, CultureInfo.InvariantCulture);
             double peso2Decimals = Math.Round(pesoNew, 2);
              DB_Handler.ConectarServer();
              string contrasenaEncriptada = "";
              // VERIFICACION DE DATOS
              if (string.IsNullOrEmpty(cedula) || string.IsNullOrEmpty(nombre)|| string.IsNullOrEmpty(apellido1) || string.IsNullOrEmpty(apellido2) || string.IsNullOrEmpty(fechaNacimiento)|| string.IsNullOrEmpty(peso) || string.IsNullOrEmpty(direccion) || string.IsNullOrEmpty(correoElectronico)|| string.IsNullOrEmpty(contrasena)) {
                  return new { message = "error" };}
              if (cedula.Length != 9 || cedula[0] == '0' || !correoElectronico.Contains("@") || !correoElectronico.Contains(".")) {
                  return new { message = "error" };}
              // ENCRIPTAR CONTRASENA CON MD5
              using (MD5 md5 = MD5.Create()) {
                  byte[] inputBytes = Encoding.UTF8.GetBytes(contrasena);
                  byte[] hashBytes = md5.ComputeHash(inputBytes);
                  StringBuilder sb = new StringBuilder();
                  foreach (byte b in hashBytes) {
                      sb.Append(b.ToString("x2"));
                  }
                  contrasenaEncriptada = sb.ToString();
                  Console.WriteLine(sb.ToString()); // borrar luego
              }
              // SEPARAR FECHA DE NACIMIENTO
              string[] fechaNacimientoSeparada = fechaNacimiento.Split("/");
              string mesNacimiento = fechaNacimientoSeparada[0];
              string diaNacimiento = fechaNacimientoSeparada[1];
              string añoNacimiento = fechaNacimientoSeparada[2];
              // INSERTAR DATOS EN LA BASE DE DATOS
              DB_Handler.AbrirConexion();
              string queryInsert = "INSERT INTO CLIENTE VALUES (@Cedula, @Nombre, @Apellido1, @Apellido2, @Dia_nacimiento, @Mes_nacimiento, @Año_nacimiento, @Peso, @Direccion, @Correo, @Contraseña)";
              using (SqlCommand comando = new SqlCommand(queryInsert, DB_Handler.conectarDB)) {
                  comando.Parameters.AddWithValue("@Cedula", Int64.Parse(cedula));
                  comando.Parameters.AddWithValue("@Nombre", nombre);
                  comando.Parameters.AddWithValue("@Apellido1", apellido1);
                  comando.Parameters.AddWithValue("@Apellido2", apellido2);
                  comando.Parameters.AddWithValue("@Dia_nacimiento", diaNacimiento);
                  comando.Parameters.AddWithValue("@Mes_nacimiento", mesNacimiento);
                  comando.Parameters.AddWithValue("@Año_nacimiento", añoNacimiento);
                  comando.Parameters.AddWithValue("@Peso", peso2Decimals);
                  comando.Parameters.AddWithValue("@Direccion", direccion);
                  comando.Parameters.AddWithValue("@Correo", correoElectronico);
                  comando.Parameters.AddWithValue("@Contraseña", contrasenaEncriptada);
                  comando.ExecuteNonQuery();
              }
              DB_Handler.CerrarConexion();
              return new { message = "ok" };
          } catch (Exception e) {
              Console.WriteLine(e);
              return new { message = "error" };
          }
      }


      [HttpPost]
      [Route("cliente/LoginCliente")]
      public dynamic LoginCliente(string cedula, string contrasena){
        try{

          // VERIFICACION DE DATOS
          if (string.IsNullOrEmpty(cedula) || string.IsNullOrEmpty(contrasena)) {
              return new { message = "error" };}
          if (cedula.Length != 9 || cedula[0] == '0') {
              return new { message = "error" };}
          // ENCRIPTAR CONTRASEÑA CON MD5
          string contrasenaEncriptada = "";
          using (MD5 md5 = MD5.Create()) {
              byte[] inputBytes = Encoding.UTF8.GetBytes(contrasena);
              byte[] hashBytes = md5.ComputeHash(inputBytes);
              StringBuilder sb = new StringBuilder();
              foreach (byte b in hashBytes) {
                  sb.Append(b.ToString("x2"));
              }
              contrasenaEncriptada = sb.ToString();
              Console.WriteLine(sb.ToString()); // borrar luego
          }
          // VERIFICAR QUE EL CLIENTE EXISTA EN LA BASE DE DATOS
          DB_Handler.ConectarServer();
          DB_Handler.AbrirConexion();
          string querySelect = "SELECT * FROM CLIENTE WHERE Cedula = @Cedula AND Contraseña = @Contraseña";
          using (SqlCommand comando = new SqlCommand(querySelect, DB_Handler.conectarDB)) {
              comando.Parameters.AddWithValue("@Cedula", Int64.Parse(cedula));
              comando.Parameters.AddWithValue("@Contraseña", contrasenaEncriptada);
              SqlDataReader reader = comando.ExecuteReader();
              if (reader.HasRows) {
                  DB_Handler.CerrarConexion();
                  return new { message = "ok" };
              } else {
                  DB_Handler.CerrarConexion();
                  return new { message = "error" };
              }
          }
          return new { message = "ok" };
        }catch(Exception e){
          return new { message = "error" };
        }
      }


    }
}
