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

                            string json_puestosExistentes = JsonSerializer.Serialize(puestosExistentes);
                            return json_puestosExistentes;
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
                                    Spa = reader.GetInt32(1)
                                });
                            }
                            DB_Handler.CerrarConexion();

                            string json_tratamientosExistentes = JsonSerializer.Serialize(tratamientosExistentes);
                            return json_tratamientosExistentes;
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

        public dynamic VerificarExistenciaTratamientoSPA_aux(string nombreSucursal, int numSpa){
            try{
                DB_Handler.ConectarServer();
                DB_Handler.AbrirConexion();
                string querySelect = "SELECT * FROM TRATAMIENTO_SPA WHERE Nsucursal = @Nsucursal AND Spa = @Spa";
                using (SqlCommand comando = new SqlCommand(querySelect, DB_Handler.conectarDB)) {
                    comando.Parameters.AddWithValue("@Nsucursal", nombreSucursal);
                    comando.Parameters.AddWithValue("@Spa", numSpa);
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