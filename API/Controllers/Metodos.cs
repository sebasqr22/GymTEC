using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Metodos{

    [ApiController]
    [Route("usuarios")]
    public class MetodosAPI: ControllerBase{

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
      public dynamic RegistrarCliente(string cedula, string nombre, string apellido1, string apellido2, string edad, string fechaNacimiento, string peso, string IMC, string direccion, string correoElectronico, string contrasena) {
          try {

              // VERIFICACION DE DATOS
              if (string.IsNullOrEmpty(cedula) || string.IsNullOrEmpty(nombre)
                  || string.IsNullOrEmpty(apellido1) || string.IsNullOrEmpty(apellido2)
                  || string.IsNullOrEmpty(edad) || string.IsNullOrEmpty(fechaNacimiento)
                  || string.IsNullOrEmpty(peso) || string.IsNullOrEmpty(IMC)
                  || string.IsNullOrEmpty(direccion) || string.IsNullOrEmpty(correoElectronico)
                  || string.IsNullOrEmpty(contrasena)) {

                  return new { message = "error" };
              }
              if (cedula.Length != 9 || cedula[0] == '0') {
                  return new { message = "error" };
              }
              if (!correoElectronico.Contains("@") || !correoElectronico.Contains(".")) {
                  return new { message = "error" };
              }


              // ENCRIPTAR CONTRASENA CON MD5
              using (MD5 md5 = MD5.Create()) {
                  byte[] inputBytes = Encoding.UTF8.GetBytes(contrasena);
                  byte[] hashBytes = md5.ComputeHash(inputBytes);
                  StringBuilder sb = new StringBuilder();
                  foreach (byte b in hashBytes) {
                      sb.Append(b.ToString("x2"));
                  }
                  string contrasenaEncriptada = sb.ToString();
                  Console.WriteLine(contrasenaEncriptada); // borrar luego
                  return new { message = "ok" };
              }
          } catch (Exception e) {
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
