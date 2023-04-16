using System;
using System.Text;
using System.Data.SqlClient;

namespace DataBaseManagement{
  public class DatabaseHandler {
      private static string connectionString = "Data Source=nombre_servidor;Initial Catalog=nombre_base_datos;User ID=usuario;Password=contraseña;";
      private static SqlConnection connection;

      private string pc_eduardo = "DESKTOP-E7FF35K";
      private string pc_jimena = "MENA-LAPTOPHP";
      private string pc_sebas = "";
      private string pc_marco = "";
      private string pc_justin = "";
      string conexionText = "";
      public SqlConnection conectarDB = new SqlConnection();

      public conexion(string pc_name){
          conexionText = "Data Source=" + pc_name + "\\SQLEXPRESS;Initial Catalog=GymTEC-DB; Integrated Security=True;";
          conectarDB.ConnectionString = conexionText;
      }

      public static void OpenConnection() {
          connection = new SqlConnection(connectionString);
          connection.Open();
      }

      public static void CloseConnection() {
          connection.Close();
      }

      public static void ExecuteNonQuery(string sql) {
          using (SqlCommand command = new SqlCommand(sql, connection)) {
              command.ExecuteNonQuery();
          }
      }

      public static SqlDataReader ExecuteReader(string sql) {
          SqlCommand command = new SqlCommand(sql, connection);
          return command.ExecuteReader();
      }
  }
}
