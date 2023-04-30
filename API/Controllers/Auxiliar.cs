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

        public dynamic VerificarExistenciaSucursal_aux(string Nombre_sucursal){
            try{
                DB_Handler.ConectarServer();
                DB_Handler.AbrirConexion();
                string querySelect = "SELECT * FROM SUCURSAL WHERE Nombre_sucursal = @Nombre_sucursal";
                using (SqlCommand comando = new SqlCommand(querySelect, DB_Handler.conectarDB)) {
                    comando.Parameters.AddWithValue("@Nombre_sucursal", Nombre_sucursal);
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
                return new { message = "error en VerificarExistenciaSucursal" };
            }
        }


        public dynamic VerInventario_aux(){
            try{
                // VER INVENTARIO EXISTENTE
                DB_Handler.ConectarServer();
                DB_Handler.AbrirConexion();
                string querySelect = "SELECT * FROM INVENTARIO"; 
                using (SqlCommand comando = new SqlCommand(querySelect, DB_Handler.conectarDB)) {
                    using (SqlDataReader reader = comando.ExecuteReader()) {
                        if (reader.HasRows) { // JSON estructura: { "Descripcion": "Gerente" }
                            var inventarioExistentes = new List<dynamic>();
                            while (reader.Read()) {
                                inventarioExistentes.Add(new {
                                });
                            }
                            DB_Handler.CerrarConexion();
                            return new JsonResult(inventarioExistentes);
                        }
                        else {
                            DB_Handler.CerrarConexion();
                            return new { message = "No hay inventario en la BD" };
                        }
                    }
                }
            }catch(Exception e){
                Console.WriteLine(e);
                return new { message = "error en VerInventario_aux" };
            }
        }
        public dynamic VerProductos_aux(){
            try{
                // VER PRODUCTOS EXISTENTES
                DB_Handler.ConectarServer();
                DB_Handler.AbrirConexion();
                string querySelect = "SELECT * FROM PRODUCTO";
                using (SqlCommand comando = new SqlCommand(querySelect, DB_Handler.conectarDB)) {
                    using (SqlDataReader reader = comando.ExecuteReader()) {
                        if (reader.HasRows) { // JSON estructura: { "Descripcion": "Gerente" }
                            var productosExistentes = new List<dynamic>();
                            while (reader.Read()) {
                                productosExistentes.Add(new {
                                    Codigo_barras = reader.GetInt32(0),
                                    Nombre = reader.GetString(1),
                                    Descripcion = reader.GetString(2),
                                    Costo = reader.GetFloat(3),
                                });
                            }
                            DB_Handler.CerrarConexion();
                            return new JsonResult(productosExistentes);
                        }
                        else {
                            DB_Handler.CerrarConexion();
                            return new { message = "No hay productos en la BD" };
                        }
                    }
                }

            }catch(Exception e){
                Console.WriteLine(e);
                return new { message = "error en VerProducto_aux" };
            }
        }

        public dynamic VerificarExistenciaProducto_aux(string codigoBarras){
            // VERIFICAR EXISTENCIA DE PRODUCTO
            try{
                DB_Handler.ConectarServer();
                DB_Handler.AbrirConexion();
                string querySelect = "SELECT * FROM PRODUCTO WHERE Codigo_barras = @Codigo_barras";
                using (SqlCommand comando = new SqlCommand(querySelect, DB_Handler.conectarDB)) {
                    comando.Parameters.AddWithValue("@Codigo_barras", Int64.Parse(codigoBarras));
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
                return new { message = "error en VerificarExistenciaProducto" };
            }
        }

        public dynamic VerClases_aux(){
            // VER CLASES EXISTENTES
            try{
                DB_Handler.ConectarServer();
                DB_Handler.AbrirConexion();
                string querySelect = "SELECT * FROM CLASE";
                using (SqlCommand comando = new SqlCommand(querySelect, DB_Handler.conectarDB)) {
                    using (SqlDataReader reader = comando.ExecuteReader()) {
                        if (reader.HasRows) { 
                            var clasesExistentes = new List<dynamic>();
                            while (reader.Read()) {
                                clasesExistentes.Add(new {
                                    Id_servicio = reader.GetInt32(0),
                                    Num_clase = reader.GetInt32(1),
                                    Fecha = reader.GetDateTime(2),
                                    HoraInicio = reader.GetTimeSpan(3),
                                    HoraFin = reader.GetTimeSpan(4),
                                    Modalidad = reader.GetString(5),
                                    Cedula_instructor = reader.GetInt32(6)
                                });
                            }
                            DB_Handler.CerrarConexion();

                            //string json_tiposEquipoExistentes = JsonSerializer.Serialize(tiposEquipoExistentes);
                            return new JsonResult(clasesExistentes);
                        }
                        else {
                            DB_Handler.CerrarConexion();
                            return new { message = "No hay clases en la BD" };
                        }
                    }
                }
            }catch(Exception e){
                Console.WriteLine(e);
                return new { message = "error en VerClases_aux" };
            }
        }

        public dynamic VerificarExistenciaServicio_aux(string Id_servicio){
            // VERIFICAR EXISTENCIA TIPO DE CLASE 
            try{
                DB_Handler.ConectarServer();
                DB_Handler.AbrirConexion();
                string querySelect = "SELECT * FROM SERVICIO WHERE Id_servicio = @Id_servicio";
                using (SqlCommand comando = new SqlCommand(querySelect, DB_Handler.conectarDB)) {
                    using (SqlDataReader reader = comando.ExecuteReader()) {
                        if (reader.HasRows) { // JSON estructura: { "Descripcion": "Gerente" }
                            var serviciosExistentes = new List<dynamic>();
                            while (reader.Read()) {
                                serviciosExistentes.Add(new {
                                    Identificador = reader.GetInt32(0),
                                    Descripcion = reader.GetString(1)
                                });
                            }
                            DB_Handler.CerrarConexion();

                            //string json_tiposEquipoExistentes = JsonSerializer.Serialize(tiposEquipoExistentes);
                            return new JsonResult(serviciosExistentes);
                        }
                        else {
                            DB_Handler.CerrarConexion();
                            return new { message = "No existe ese servicio en la BD" };
                        }
                    }
                }
            }catch(Exception e){
                Console.WriteLine(e);
                return new { message = "error en VerificarExistenciaServicio_aux" };
            }
        }
        public dynamic VerServicios_aux(){
            // VER TIPOS DE CLASE EXISTENTES
            try{
                DB_Handler.ConectarServer();
                DB_Handler.AbrirConexion();
                string querySelect = "SELECT * FROM SERVICIO";
                using (SqlCommand comando = new SqlCommand(querySelect, DB_Handler.conectarDB)) {
                    using (SqlDataReader reader = comando.ExecuteReader()) {
                        if (reader.HasRows) { 
                            DB_Handler.CerrarConexion();
                            return true;
                        }
                        else {
                            DB_Handler.CerrarConexion();
                            return new { message = "No existen servicios en la BD" };
                        }
                    }
                }
            }catch(Exception e){
                Console.WriteLine(e);
                return new { message = "error en VerServicios_aux" };
            }
        }

        public dynamic VerificarExistenciaClase_aux(string Id_servicio, string cedulaInstructor, string modalidad, string fecha, string horaInicio){            
            try{
                DB_Handler.ConectarServer();
                DB_Handler.AbrirConexion();
                string querySelect = "SELECT * FROM CLASE WHERE Id_servicio = @Id_servicio AND Cedula_instructor = @Cedula_instructor AND Modalidad = @Modalidad AND Fecha = @Fecha AND Hora_inicio = @Hora_inicio";
                using (SqlCommand comando = new SqlCommand(querySelect, DB_Handler.conectarDB)) {
                    comando.Parameters.AddWithValue("@Id_servicio", Int64.Parse(Id_servicio));
                    comando.Parameters.AddWithValue("@Cedula_instructor", Int64.Parse(cedulaInstructor));
                    comando.Parameters.AddWithValue("@Modalidad", modalidad);
                    comando.Parameters.AddWithValue("@Fecha", DateTime.ParseExact(fecha, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture));
                    comando.Parameters.AddWithValue("@Hora_inicio", TimeSpan.ParseExact(horaInicio, @"hh\:mm\:ss", System.Globalization.CultureInfo.InvariantCulture));
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
                return new { message = "error en VerificarExistenciaClase_aux" };
            }
        }

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
                return new { message = "error en VerTiposEquipo_aux" };
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