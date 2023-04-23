using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Data.SqlClient;
using System.Globalization;

using DataBaseManagement;
using funcionesAuxiliares;

namespace Metodos{

    [ApiController]
    [Route("usuarios")]
    public class MetodosAPI: ControllerBase{
      private DatabaseHandler DB_Handler = new DatabaseHandler();  
      AuxiliarFunctions aux = new AuxiliarFunctions();


      [HttpGet]
      [Route("admin/VerTiposEquipo")]
      public dynamic VerTiposEquipo(){
        try{
          return aux.VerTiposEquipo_aux();
        }catch(Exception e){
          Console.WriteLine(e);
          return new { message = "error" };
        }
      }
      
      [HttpPost]
      [Route("admin/AgregarTipoEquipo")]
      public dynamic AgregarTipoEquipo(string descripcion){
        try{
            if(string.IsNullOrEmpty(descripcion)){
              return new { message = "error" };}

            // INSERTAR TIPO DE EQUIPO EN LA BASE DE DATOS
            dynamic existeTipoEquipo = aux.VerificarExistenciaTipoEquipo_aux(descripcion);
            if(existeTipoEquipo){
              return new { message = "Ya existe este tipo de equipo en la BD" };
            }
            DB_Handler.ConectarServer();
            DB_Handler.AbrirConexion();
            string queryInsert = "INSERT INTO TIPO_EQUIPO VALUES (@Descripcion)";
            using (SqlCommand comando = new SqlCommand(queryInsert, DB_Handler.conectarDB)) {
              comando.Parameters.AddWithValue("@Descripcion", descripcion);
              comando.ExecuteNonQuery();
            }
            DB_Handler.CerrarConexion();
            return new { message = "ok" };
          }catch(Exception e){
            Console.WriteLine(e);
            return new { message = "error" };
          }
      }

      [HttpPost]
      [Route("admin/EliminarTipoEquipo")]
      public dynamic EliminarTipoEquipo(string descripcion){
        try{
            if(string.IsNullOrEmpty(descripcion)){
              return new { message = "error" };}

            // ELIMINAR TIPO DE EQUIPO EN LA BASE DE DATOS
            dynamic existeTipoEquipo = aux.VerificarExistenciaTipoEquipo_aux(descripcion);
            if(!existeTipoEquipo){
              return new { message = "No existe este tipo de equipo en la BD" };
            }
            DB_Handler.ConectarServer();
            DB_Handler.AbrirConexion();
            string queryDelete = "DELETE FROM TIPO_EQUIPO WHERE Descripcion = @Descripcion";
            using (SqlCommand comando = new SqlCommand(queryDelete, DB_Handler.conectarDB)) {
              comando.Parameters.AddWithValue("@Descripcion", descripcion);
              comando.ExecuteNonQuery();
            }
            DB_Handler.CerrarConexion();
            return new { message = "ok" };
          }catch(Exception e){
            Console.WriteLine(e);
            return new { message = "error" };
          }
      }
      [HttpGet]
      [Route("admin/VerEmpleados")]
      public dynamic VerEmpleados(){
        try{
          return aux.VerEmpleados_aux();
        }catch(Exception e){
          Console.WriteLine(e);
          return new { message = "error" };
        }
      }

      [HttpPost]
      [Route("admin/AgregarEmpleado")]
      public dynamic AgregarEmpleado(string cedula, string nombre, string apellido1, string apellido2, string distrito, string canton, string provincia, string correo, string contrasena, string salario, int id_puesto, int id_planilla, int nombre_suc){
        try{
          if(string.IsNullOrEmpty(cedula) || cedula.Length != 9 ||string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(apellido1) || string.IsNullOrEmpty(apellido2) || string.IsNullOrEmpty(distrito) || string.IsNullOrEmpty(canton) || string.IsNullOrEmpty(provincia) || string.IsNullOrEmpty(correo) || !correo.Contains('@') || !correo.Contains('.') || string.IsNullOrEmpty(contrasena) || string.IsNullOrEmpty(salario) || id_puesto == 0 || id_planilla == 0 || nombre_suc == 0){
            return new { message = "error" };}

          // INSERTAR EMPLEADO EN LA BASE DE DATOS
          dynamic existeEmpleado = aux.VerificarExistenciaEmpleado_aux(cedula);
          if(existeEmpleado){
            return new { message = "Ya existe este empleado en la BD" };
          }
          DB_Handler.ConectarServer();
          DB_Handler.AbrirConexion();
          string queryInsert = "INSERT INTO EMPLEADO VALUES (@Cedula, @Nombre, @Apellido1, @Apellido2, @Distrito, @Canton, @Provincia, @Correo, @Contrasena, @Salario, @Id_Puesto, @Id_Planilla, @Nombre_Suc)";
          using (SqlCommand comando = new SqlCommand(queryInsert, DB_Handler.conectarDB)) {
            comando.Parameters.AddWithValue("@Cedula", Int64.Parse(cedula));
            comando.Parameters.AddWithValue("@Nombre", nombre);
            comando.Parameters.AddWithValue("@Apellido1", apellido1);
            comando.Parameters.AddWithValue("@Apellido2", apellido2);
            comando.Parameters.AddWithValue("@Distrito", distrito);
            comando.Parameters.AddWithValue("@Canton", canton);
            comando.Parameters.AddWithValue("@Provincia", provincia);
            comando.Parameters.AddWithValue("@Correo", correo);
            comando.Parameters.AddWithValue("@Contrasena", contrasena);
            comando.Parameters.AddWithValue("@Salario", Int64.Parse(salario));
            comando.Parameters.AddWithValue("@Id_Puesto", id_puesto);
            comando.Parameters.AddWithValue("@Id_Planilla", id_planilla);
            comando.Parameters.AddWithValue("@Nombre_Suc", nombre_suc);
            comando.ExecuteNonQuery();
          }
          DB_Handler.CerrarConexion();
          return new { message = "ok" };
        }catch(Exception e){
          Console.WriteLine(e);
          return new { message = "error" };
        }
      }

      [HttpPost]
      [Route("admin/EliminarEmpleado")]
      public dynamic EliminarEmpleado(string cedula){
        try{
          if(string.IsNullOrEmpty(cedula) || cedula.Length != 9){
            return new { message = "error" };}

          // ELIMINAR EMPLEADO EN LA BASE DE DATOS
          dynamic existeEmpleado = aux.VerificarExistenciaEmpleado_aux(cedula);
          if(!existeEmpleado){
            return new { message = "No existe este empleado en la BD" };
          }
          DB_Handler.ConectarServer();
          DB_Handler.AbrirConexion();
          string queryDelete = "DELETE FROM EMPLEADO WHERE Cedula = @Cedula";
          using (SqlCommand comando = new SqlCommand(queryDelete, DB_Handler.conectarDB)) {
            comando.Parameters.AddWithValue("@Cedula", Int64.Parse(cedula));
            comando.ExecuteNonQuery();
          }
          DB_Handler.CerrarConexion();
          return new { message = "ok" };
        }catch(Exception e){
          Console.WriteLine(e);
          return new { message = "error" };
        }
      }
      [HttpGet]
      [Route("admin/VerPlanillas")]
      public dynamic VerPlanillas(){
        try{
          return aux.VerPlanillas_aux();
        }catch(Exception e){
          Console.WriteLine(e);
          return new { message = "error" };
        }
      }
      
      [HttpPost]
      [Route("admin/AgregarPlanilla")]
        public dynamic AgregarPlanilla(string descripcionPlanilla){
            try{

                // VERIFICACION DE DATOS
                if(string.IsNullOrEmpty(descripcionPlanilla)){
                    return new { message = "error" };}

                // INSERTAR PLANILLA EN LA BASE DE DATOS
                dynamic existePlanilla = aux.VerificarExistenciaPlanilla_aux(descripcionPlanilla);
                if(existePlanilla){
                    return new { message = "Ya existe esta planilla en la BD" };
                }

                DB_Handler.ConectarServer();
                DB_Handler.AbrirConexion();
                string queryInsert = "INSERT INTO PLANILLA (Descripcion) VALUES (@Descripcion)";
                using (SqlCommand comando = new SqlCommand(queryInsert, DB_Handler.conectarDB)) {
                    comando.Parameters.AddWithValue("@Descripcion", descripcionPlanilla);
                    comando.ExecuteNonQuery();
                }
                DB_Handler.CerrarConexion();
                return new { message = "ok" };

            }catch(Exception e){
                Console.WriteLine(e);
                return new { message = "error" };
            }

        }

      [HttpPost]
      [Route("admin/EliminarPlanilla")]
      public dynamic EliminarPlanilla(string descripcionPlanilla){
        try{
            // VERIFICACION DE DATOS
            if(string.IsNullOrEmpty(descripcionPlanilla)){
                return new { message = "error" };}

            // ELIMINAR PLANILLA EN LA BASE DE DATOS
            dynamic existePlanilla = aux.VerificarExistenciaPlanilla_aux(descripcionPlanilla);
            if(!existePlanilla){
                return new { message = "No existe esta planilla en la BD" };
            }

            DB_Handler.ConectarServer();
            DB_Handler.AbrirConexion();
            string queryDelete = "DELETE FROM PLANILLA WHERE Descripcion = @Descripcion";
            using (SqlCommand comando = new SqlCommand(queryDelete, DB_Handler.conectarDB)) {
                comando.Parameters.AddWithValue("@Descripcion", descripcionPlanilla);
                comando.ExecuteNonQuery();
            }
            DB_Handler.CerrarConexion();
            return new { message = "ok" };

        }catch(Exception e){
            Console.WriteLine(e);
            return new { message = "error" };
        }
      }

      [HttpGet]
      [Route("admin/VerPuestos")]
      public dynamic VerPuestos(){
        try{
          return aux.VerPuestos_aux();
        }catch(Exception e){
          Console.WriteLine(e);
          return new { message = "error" };
        }
      }

      [HttpPost]
      [Route("admin/AgregarPuesto")]
      public dynamic AgregarPuesto(string descripcionPuesto){
        try{
            if(string.IsNullOrEmpty(descripcionPuesto)){
                return new { message = "error" };}

            dynamic existePuesto = aux.VerificarExistenciaPuesto_aux(descripcionPuesto);
            if (existePuesto) {
                return new { message = "Puesto ya existe en la BD. Error" };}

            DB_Handler.ConectarServer();
            DB_Handler.AbrirConexion();
            string queryInsert = "INSERT INTO PUESTO (Descripcion) VALUES (@Descripcion)";
            using (SqlCommand comando = new SqlCommand(queryInsert, DB_Handler.conectarDB)) {
                comando.Parameters.AddWithValue("@Descripcion", descripcionPuesto);
                comando.ExecuteNonQuery();
                DB_Handler.CerrarConexion();
            }
            return new { message = "ok" };
        }catch(Exception e){
            Console.WriteLine(e);
            return new { message = "error" };
        }
      }

      [HttpPost]
      [Route("admin/EliminarPuesto")]
      public dynamic EliminarPuesto(string descripcionPuesto){
        try{
            if(string.IsNullOrEmpty(descripcionPuesto)){
                return new { message = "error" };}

            // ELIMINAR PUESTO EN LA BASE DE DATOS
            bool existePuesto = aux.VerificarExistenciaPuesto_aux(descripcionPuesto);
            if (!existePuesto) {
                return new { message = "Puesto no existe en la BD. Error" };}

            DB_Handler.ConectarServer();
            DB_Handler.AbrirConexion();
            string queryDelete = "DELETE FROM PUESTO WHERE Descripcion = @Descripcion";
            using (SqlCommand comando = new SqlCommand(queryDelete, DB_Handler.conectarDB)) {
                comando.Parameters.AddWithValue("@Descripcion", descripcionPuesto);
                comando.ExecuteNonQuery();
                DB_Handler.CerrarConexion();
            }
            return new { message = "ok" };
        }catch(Exception e){
            Console.WriteLine(e);
            return new { message = "error" };
        }
      }      
      
      [HttpGet]
      [Route("admin/VerTratamientosSPA")]
      public dynamic VerTratamientosSPA(){
        try{
          return aux.VerTratamientosSPA_aux();
        }catch(Exception e){
          Console.WriteLine(e);
          return new { message = "error" };
        }
      }

      [HttpPost]
      [Route("admin/AgregarTratamientoSPA")]
      public dynamic AgregarTratamientoSPA(string nombreSucursal, int numSpa){
        try{
          // VERIFICACION DE DATOS
          if (string.IsNullOrEmpty(nombreSucursal) || numSpa == 0) {
              return new { message = "error" };}

          // INSERTAR TRATAMIENTO_SPA EN LA BASE DE DATOS
          dynamic existeTratamientoSPA = aux.VerificarExistenciaTratamientoSPA_aux(nombreSucursal, numSpa);
            if (existeTratamientoSPA) {
                return new { message = "Tratamiento ya existe en la BD. Error" };}

            DB_Handler.ConectarServer();
            DB_Handler.AbrirConexion();
            string queryInsert = "INSERT INTO TRATAMIENTO_SPA VALUES (@Nsucursal, @Spa)";
            using (SqlCommand comando = new SqlCommand(queryInsert, DB_Handler.conectarDB)) {
                comando.Parameters.AddWithValue("@Nsucursal", nombreSucursal);
                comando.Parameters.AddWithValue("@Spa", numSpa);
                comando.ExecuteNonQuery();
                DB_Handler.CerrarConexion();
            }

          return new { message = "ok" };

        }catch(Exception e){
          Console.WriteLine(e);
          return new { message = "error" };
        }
    }

      [HttpPost]
      [Route("admin/EliminarTratamientoSPA")]
      public dynamic EliminarTratamientoSPA(string nombreSucursal, int numSpa){
        try{
            // VERIFICACION DE DATOS
            if (string.IsNullOrEmpty(nombreSucursal) || numSpa == 0) {
                return new { message = "error" };}

            // ELIMINAR TRATAMIENTO_SPA EN LA BASE DE DATOS
            dynamic existeTratamientoSPA = aux.VerificarExistenciaTratamientoSPA_aux(nombreSucursal, numSpa);
            if (!existeTratamientoSPA) {
                return new { message = "Tratamiento no existe en la BD. Error" };}

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

            return aux.VerTratamientosSPA_aux();  // JSON
                  
        }catch(Exception e){
          return new { message = "error" };
        }
      }

      [HttpPost]
      [Route("login/SignUpCliente")]
      public dynamic SignUpCliente(string cedula, string nombre, string apellido1, string apellido2, string fechaNacimiento, string peso, string direccion, string correoElectronico, string contrasena) {
          try {              
              // VERIFICACION DE DATOS
              if (string.IsNullOrEmpty(cedula) || string.IsNullOrEmpty(nombre)|| string.IsNullOrEmpty(apellido1) || string.IsNullOrEmpty(apellido2) || string.IsNullOrEmpty(fechaNacimiento)|| string.IsNullOrEmpty(peso) || string.IsNullOrEmpty(direccion) || string.IsNullOrEmpty(correoElectronico)|| string.IsNullOrEmpty(contrasena)) {
                  return new { message = "error" };}
              if (cedula.Length != 9 || cedula[0] == '0' || !correoElectronico.Contains("@") || !correoElectronico.Contains(".")) {
                  return new { message = "error" };}

              // SEPARAR FECHA DE NACIMIENTO
              string[] fechaNSeparada = fechaNacimiento.Split("/");
              string mes = fechaNSeparada[0];
              string dia = fechaNSeparada[1];
              string anio = fechaNSeparada[2];

              // INSERTAR DATOS EN LA BASE DE DATOS
              DB_Handler.ConectarServer();
              DB_Handler.AbrirConexion();
              string queryInsert = "INSERT INTO CLIENTE VALUES (@Cedula, @Nombre, @Apellido1, @Apellido2, @Dia_nacimiento, @Mes_nacimiento, @Año_nacimiento, @Peso, @Direccion, @Correo, @Contraseña)";
              using (SqlCommand comando = new SqlCommand(queryInsert, DB_Handler.conectarDB)) {
                  comando.Parameters.AddWithValue("@Cedula", Int64.Parse(cedula));
                  comando.Parameters.AddWithValue("@Nombre", nombre);
                  comando.Parameters.AddWithValue("@Apellido1", apellido1);
                  comando.Parameters.AddWithValue("@Apellido2", apellido2);
                  comando.Parameters.AddWithValue("@Dia_nacimiento", dia);
                  comando.Parameters.AddWithValue("@Mes_nacimiento", mes);
                  comando.Parameters.AddWithValue("@Año_nacimiento", anio);
                  comando.Parameters.AddWithValue("@Peso", Math.Round(Convert.ToDouble(peso, CultureInfo.InvariantCulture), 2));
                  comando.Parameters.AddWithValue("@Direccion", direccion);
                  comando.Parameters.AddWithValue("@Correo", correoElectronico);
                  comando.Parameters.AddWithValue("@Contraseña", aux.EncriptarContrasenaMD5_aux(contrasena));
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
      [Route("login/LoginCliente")]
      public dynamic LoginCliente(string cedula, string contrasena){
        try{
          // VERIFICACION DE DATOS
          if (string.IsNullOrEmpty(cedula) || string.IsNullOrEmpty(contrasena)) {
              return new { message = "error" };}
          if (cedula.Length != 9 || cedula[0] == '0') {
              return new { message = "error" };}

          // VERIFICAR QUE EL CLIENTE EXISTA EN LA BASE DE DATOS
          DB_Handler.ConectarServer();
          DB_Handler.AbrirConexion();
          string querySelect = "SELECT * FROM CLIENTE WHERE Cedula = @Cedula AND Contraseña = @Contraseña";
          using (SqlCommand comando = new SqlCommand(querySelect, DB_Handler.conectarDB)) {
              comando.Parameters.AddWithValue("@Cedula", Int64.Parse(cedula));
              comando.Parameters.AddWithValue("@Contraseña", aux.EncriptarContrasenaMD5_aux(contrasena));
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
