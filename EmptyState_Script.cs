// Script de prueba para la BD creada por Eduardo
using System;
using System.Text;
using System.Data.SqlClient;


namespace ConexionBD{
    class conexion{
        private string pc_eduardo = "DESKTOP-E7FF35K";
        private string pc_jimena = "MENA-LAPTOPHP";
        private string pc_sebas = "";
        private string pc_marco = "";
        private string pc_justin = "";

        string conexionText = "";
        public SqlConnection conectarDB = new SqlConnection();

        public conexion(string pc_name){
            conexionText = "Data Source=" + pc_name + "\\SQLEXPRESS;Initial Catalog=GymTEC-DB; Integrated Security=True;"
            conectarDB.ConnectionString = conexionText; 
        }

        public void abrirDB(){
            try{
                Console.WriteLine("DB abierta correctamente");
            }catch(Exception e){
                Console.WriteLine("Error al abrir DB" + e.Message);
            }
        }

        public void cerrarDB(){
            conectarDB.Close();
        }
    }
}