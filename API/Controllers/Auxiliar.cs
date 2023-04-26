using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using DataBaseManagement;
using System.Data.SqlClient;
using System.Globalization;



namespace funcionesAuxiliares{

    public class AuxiliarFunctions{
        private DatabaseHandler DB_Handler = new DatabaseHandler();  

        public dynamic VerTiposEquipo_aux(){
            // VER TIPOS DE EQUIPO EXISTENTES
            try{
                DB_Handler.ConectarServer();
                DB_Handler.AbrirConexion();
                string querySelect = "SELECT * FROM TIPO_EQUIPO";
                using (SqlCommand comando = new SqlCommand(querySelect, DB_Handler.conectarDB)) {
                    using (SqlDataReader reader = comando.ExecuteReader()) {
                        if (reader.HasRows) { // JSON estructura: { "Descripcion": "Gerente" }
                            var tiposEquipoExistentes = new List<dynamic>();
                            while (reader.Read()) {
                                tiposEquipoExistentes.Add(new {
                                    Identificador = reader.GetInt32(0),
                                    Descripcion = reader.GetString(1)
                                });
                            }
                            DB_Handler.CerrarConexion();

                            //string json_tiposEquipoExistentes = JsonSerializer.Serialize(tiposEquipoExistentes);
                            return new JsonResult(tiposEquipoExistentes);
                        }
                        else {
                            DB_Handler.CerrarConexion();
                            return new { message = "No hay tipos de equipo en la BD" };
                        }
                    }
                }
            }catch(Exception e){
                Console.WriteLine(e);
                return new { message = "error en VerTiposEquipo" };
            }
        }


        public dynamic VerTratamientos_aux(){
            try{
                DB_Handler.ConectarServer();
                DB_Handler.AbrirConexion();
                string querySelect = "SELECT * FROM TRATAMIENTO";
                using (SqlCommand comando = new SqlCommand(querySelect, DB_Handler.conectarDB)) {
                    using (SqlDataReader reader = comando.ExecuteReader()) {
                        if (reader.HasRows) { 
                            var tratamientosExistentes = new List<dynamic>();
                            while (reader.Read()) {
                                tratamientosExistentes.Add(new {
                                    Identificador = reader.GetInt32(0),
                                    Nombre = reader.GetString(1),
                                });
                            }
                            DB_Handler.CerrarConexion();

                            //string json_tratamientosExistentes = JsonSerializer.Serialize(tratamientosExistentes);
                            return new JsonResult(tratamientosExistentes);
                        }
                        else {
                            DB_Handler.CerrarConexion();
                            return new { message = "No hay tratamientos en la BD" };
                        }
                    }
                }
            }catch(Exception e){
                Console.WriteLine(e);
                return new { message = "error en VerTratamientos_aux" };
            }
        }

        public dynamic VerificarExistenciaTipoEquipo_aux(string descripcion){
            try{
                DB_Handler.ConectarServer();
                DB_Handler.AbrirConexion();
                string querySelect = "SELECT * FROM TIPO_EQUIPO WHERE Descripcion = @Descripcion";
                using (SqlCommand comando = new SqlCommand(querySelect, DB_Handler.conectarDB)) {
                    comando.Parameters.AddWithValue("@Descripcion", descripcion);
                    using (SqlDataReader reader = comando.ExecuteReader()) {
                        if (reader.HasRows) {
                            DB_Handler.CerrarConexion();
                            return true;
                        }
                    }
                }
            
                DB_Handler.CerrarConexion();
                return false;
            }catch(Exception e){
                Console.WriteLine(e);
                return new { message = "error en VerificarExistenciaTipoEquipo" };
            }
        }
            
        public dynamic VerificarExistenciaEmpleado_aux(string cedula){
            try{
                DB_Handler.ConectarServer();
                DB_Handler.AbrirConexion();
                string querySelect = "SELECT * FROM EMPLEADO WHERE Cedula = @Cedula";
                using (SqlCommand comando = new SqlCommand(querySelect, DB_Handler.conectarDB)) {
                    comando.Parameters.AddWithValue("@Cedula", cedula);
                    using (SqlDataReader reader = comando.ExecuteReader()) {
                        if (reader.HasRows) {
                            DB_Handler.CerrarConexion();
                            return true;
                        }
                    }
                }

                DB_Handler.CerrarConexion();
                return false;

            }catch(Exception e){
                Console.WriteLine(e);
                return new { message = "error en VerificarExistenciaEmpleado" };
            }
        }

        public dynamic VerificarExistenciaPlanilla_aux(string descripcionPlanilla){
            try{
                DB_Handler.ConectarServer();
                DB_Handler.AbrirConexion();
                string querySelect = "SELECT * FROM PLANILLA WHERE Descripcion = @Descripcion";
                using (SqlCommand comando = new SqlCommand(querySelect, DB_Handler.conectarDB)) {
                    comando.Parameters.AddWithValue("@Descripcion", descripcionPlanilla);
                    using (SqlDataReader reader = comando.ExecuteReader()) {
                        if (reader.HasRows) {
                            DB_Handler.CerrarConexion();
                            return true;
                        }
                    }
                }
            
                DB_Handler.CerrarConexion();
                return false;

            }catch(Exception e){
                Console.WriteLine(e);
                return new { message = "error en VerificarExistenciaPlanilla_aux" };
            }
        }
        
        public dynamic VerificarExistenciaTratamiento_aux(string nombreTratamiento)
        {
            try{
                DB_Handler.ConectarServer();
                DB_Handler.AbrirConexion();
                string querySelect = "SELECT * FROM TRATAMIENTO WHERE Nombre = @Nombre";
                using (SqlCommand comando = new SqlCommand(querySelect, DB_Handler.conectarDB)) {
                    comando.Parameters.AddWithValue("@Nombre", nombreTratamiento);
                    using (SqlDataReader reader = comando.ExecuteReader()) {
                        if (reader.HasRows) {
                            DB_Handler.CerrarConexion();
                            return true;
                        }
                    }
                }

                DB_Handler.CerrarConexion();
                return false;

            }catch(Exception e){
                Console.WriteLine(e);
                return new { message = "error en VerificarExistenciaTratamiento_aux" };
            }
        }

        public dynamic VerEmpleados_aux(){
            try{
                DB_Handler.ConectarServer();
                DB_Handler.AbrirConexion();
                string querySelect = "SELECT * FROM EMPLEADO";
                using (SqlCommand comando = new SqlCommand(querySelect, DB_Handler.conectarDB)) {
                    using (SqlDataReader reader = comando.ExecuteReader()) {
                        if (reader.HasRows) { 
                            var empleadosExistentes = new List<dynamic>();
                            while (reader.Read()) {
                                empleadosExistentes.Add(new {
                                    Cedula = reader.GetInt32(0),
                                    Nombre = reader.GetString(1),
                                    Apellido1 = reader.GetString(2),
                                    Apellido2 = reader.GetString(3),
                                    Distrito = reader.GetString(4),
                                    Canton = reader.GetString(5),
                                    Provincia = reader.GetString(6),
                                    Correo = reader.GetString(7),
                                    Contraseña = reader.GetString(8),
                                    Salario = reader.GetDouble(9),
                                    Id_puesto = reader.GetInt32(10),
                                    Id_planilla = reader.GetInt32(11)
                                });
                            }
                            DB_Handler.CerrarConexion();

                            //string json_empleadosExistentes = JsonSerializer.Serialize(empleadosExistentes);
                            return new JsonResult(empleadosExistentes);
                        }
                        else {
                            DB_Handler.CerrarConexion();
                            return new { message = "No hay empleados en la BD" };
                        }
                    }
                }
            }catch(Exception e){
                Console.WriteLine(e);
                return new { message = "error en VerEmpleados" };
            }
        }

        public dynamic VerPlanillas_aux(){
            // VER PLANILLAS EXISTENTES
            try{
                DB_Handler.ConectarServer();
                DB_Handler.AbrirConexion();
                string querySelect = "SELECT * FROM PLANILLA";
                using (SqlCommand comando = new SqlCommand(querySelect, DB_Handler.conectarDB)) {
                    using (SqlDataReader reader = comando.ExecuteReader()) {
                        if (reader.HasRows) { // JSON estructura: { "Descripcion": "Gerente" }
                            var planillasExistentes = new List<dynamic>();
                            while (reader.Read()) {
                                planillasExistentes.Add(new {
                                    Id_planilla = reader.GetInt32(0),
                                    Descripcion = reader.GetString(1)
                                });
                            }
                            DB_Handler.CerrarConexion();

                            //string json_planillasExistentes = JsonSerializer.Serialize(planillasExistentes);
                            return new JsonResult(planillasExistentes);
                        }
                        else {
                            DB_Handler.CerrarConexion();
                            return new { message = "No hay planillas en la BD" };
                        }
                    }
                }
            }catch(Exception e){
                Console.WriteLine(e);
                return new { message = "error en VerPlanillas_aux" };
            }
        }
        
        public dynamic VerPuestos_aux(){
            try{
                DB_Handler.ConectarServer();
                DB_Handler.AbrirConexion();
                string querySelect = "SELECT * FROM PUESTO";
                using (SqlCommand comando = new SqlCommand(querySelect, DB_Handler.conectarDB)) {
                    using (SqlDataReader reader = comando.ExecuteReader()) {
                        if (reader.HasRows) { // JSON estructura: { "Descripcion": "Gerente" }
                            var puestosExistentes = new List<dynamic>();
                            while (reader.Read()) {
                                puestosExistentes.Add(new {
                                    Descripcion = reader.GetString(1)
                                });
                            }
                            DB_Handler.CerrarConexion();

                            //string json_puestosExistentes = JsonSerializer.Serialize(puestosExistentes);
                            return new JsonResult(puestosExistentes);
                        }
                        else {
                            DB_Handler.CerrarConexion();
                            return new { message = "No hay puestos en la BD" };
                        }
                    }
                }
            }catch(Exception e){
                Console.WriteLine(e);
                return new { message = "error en VerPuestos_aux" };
            }
        }
        
        public dynamic VerificarExistenciaPuesto_aux(string descripcionPuesto){
           try{ 
                DB_Handler.ConectarServer();
                DB_Handler.AbrirConexion();
                string querySelect = "SELECT * FROM PUESTO WHERE Descripcion = @Descripcion";
                using (SqlCommand comando = new SqlCommand(querySelect, DB_Handler.conectarDB)) {
                    comando.Parameters.AddWithValue("@Descripcion", descripcionPuesto);
                    using (SqlDataReader reader = comando.ExecuteReader()) {
                        if (reader.HasRows) {
                            DB_Handler.CerrarConexion();
                            return true;
                        }
                    }
                }
                return false;
                }catch(Exception e){
                    Console.WriteLine(e);
                    return new { message = "error en VerificarExistenciaPuesto_aux" };
                }
        }
        
        public dynamic VerTratamientosSPA_aux(){
            try{
                DB_Handler.ConectarServer();
                DB_Handler.AbrirConexion();
                
                string querySelect = "SELECT * FROM TRATAMIENTO_SPA";
                using (SqlCommand comando = new SqlCommand(querySelect, DB_Handler.conectarDB)) {
                    using (SqlDataReader reader = comando.ExecuteReader()) {
                        if (reader.HasRows) { // JSON estructura: { "Nsucursal": "1", "Spa": "1" }
                            var tratamientosExistentes = new List<dynamic>();
                            while (reader.Read()) {
                                tratamientosExistentes.Add(new {
                                    Nsucursal = reader.GetString(0),
                                    idSpa = reader.GetInt32(1),
                                    idTratamiento = reader.GetInt32(2)
                                });
                            }
                            DB_Handler.CerrarConexion();

                            //string json_tratamientosExistentes = JsonSerializer.Serialize(tratamientosExistentes);
                            return new JsonResult(tratamientosExistentes);
                        }
                        else {
                            DB_Handler.CerrarConexion();
                            return new { message = "No hay tratamientos en la BD" };
                        }
                    }
                }
            }catch(Exception e){
                Console.WriteLine(e);
                return new { message = "error en VerTratamientosSPA_aux" };
            }
        }

        public dynamic VerificarExistenciaTratamientoSPA_aux(string nombreSucursal, int numSpa, int idTratamiento){
            try{
                DB_Handler.ConectarServer();
                DB_Handler.AbrirConexion();
                string querySelect = "SELECT * FROM TRATAMIENTO_SPA WHERE Nsucursal = @Nsucursal AND Spa = @Spa AND Id_tratamiento = @Id_tratamiento";
                using (SqlCommand comando = new SqlCommand(querySelect, DB_Handler.conectarDB)) {
                    comando.Parameters.AddWithValue("@Nsucursal", nombreSucursal);
                    comando.Parameters.AddWithValue("@Spa", numSpa);
                    comando.Parameters.AddWithValue("@Id_tratamiento", idTratamiento);
                    using (SqlDataReader reader = comando.ExecuteReader()) {
                        if (reader.HasRows) {
                            DB_Handler.CerrarConexion();
                            return true;
                        }
                    }
                }

                DB_Handler.CerrarConexion();
                return false;

            }catch(Exception e){
                Console.WriteLine(e);
                return new { message = "error en VerificarExistenciaTratamientoSPA_aux" };
            }
        }
    
        public dynamic EncriptarContrasenaMD5_aux(string contrasena){
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
            return contrasenaEncriptada;
        }

    }
}