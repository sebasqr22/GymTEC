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
      public dynamic AgregarTratamientoSPA(){
        try{
          return new { message = "ok" };
        }catch(Exception e){
          return new { message = "error" };
        }
      }

      [HttpGet]
      [Route("cliente/EliminarTratamientoSPA")]
      public dynamic EliminarTratamientoSPA(){
        try{
          return new { message = "ok" };
        }catch(Exception e){
          return new { message = "error" };
        }
      }

      [HttpPost]
      [Route("cliente/RegistrarCliente")]
      public dynamic RegistrarCliente(string cedula, string nombre, string apellido1, string apellido2, string edad, string fechaNacimiento, string peso, string direccion, string correoElectronico, string contrasena) {
          try {

             double pesoNew = Convert.ToDouble(peso, CultureInfo.InvariantCulture);
             double peso2Decimals = Math.Round(pesoNew, 2);
              DB_Handler.ConectarServer();
              string contrasenaEncriptada = "";
              // VERIFICACION DE DATOS
              if (string.IsNullOrEmpty(cedula) || string.IsNullOrEmpty(nombre)|| string.IsNullOrEmpty(apellido1) || string.IsNullOrEmpty(apellido2)|| string.IsNullOrEmpty(edad) || string.IsNullOrEmpty(fechaNacimiento)|| string.IsNullOrEmpty(peso) || string.IsNullOrEmpty(direccion) || string.IsNullOrEmpty(correoElectronico)|| string.IsNullOrEmpty(contrasena)) {
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
      public dynamic LoginCliente(){
        try{
          

          return new { message = "ok" };
        }catch(Exception e){
          return new { message = "error" };
        }
      }


    }
}
