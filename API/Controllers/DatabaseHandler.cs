using System;
using System.Text;
using System.Data.SqlClient;

namespace DataBaseManagement{
  public class DatabaseHandler {
      public string nombreBD = "GymTEC-DB";
      string conexionText = "";
      public SqlConnection conectarDB = new SqlConnection();

      public void ConectarServer(){
          conexionText = "Data Source=.\\SQLEXPRESS;Database=" + nombreBD + ";Trusted_Connection=True;Trusted_Connection=True;";
          conectarDB.ConnectionString = conexionText;
      }

      public void AbrirConexion() {
        try{
          conectarDB.Open();
        }catch(Exception e){
          Console.WriteLine(e);
        }
      }

      public void CerrarConexion() {
          conectarDB.Close();
      }

      public void ExecuteNonQueryH(string sql) {
          using (SqlCommand command = new SqlCommand(sql, conectarDB)) {
              command.ExecuteNonQuery();
          }
      }

      public SqlDataReader ExecuteReaderH(string sql) {
          SqlCommand command = new SqlCommand(sql, conectarDB);
          return command.ExecuteReader();
      }
  }
}
