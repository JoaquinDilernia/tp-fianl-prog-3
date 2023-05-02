using System;
using System.Data;
using System.Data.SqlClient;

namespace DAO
{
    public class AccesoDatos
    {
        public string ruta = @"Data Source=DESKTOP-8RDB3AB\SQLEXPRESS;Initial Catalog = Prog3_Integrador; Integrated Security = True";

        public SqlConnection GetConexion()
        {
            SqlConnection conexion = new SqlConnection(ruta);
            try
            {
                conexion.Open();
                return conexion;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public SqlDataAdapter GetSqlDataAdapter(String consultaSql, SqlConnection conexion)
        {
            SqlDataAdapter adapter = new SqlDataAdapter(consultaSql, conexion);
            try
            {
                return adapter;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataTable ObtenerTabla(String NombreTabla, String Sql)
        {
            DataSet ds = new DataSet();
            SqlConnection Conexion = GetConexion();
            SqlDataAdapter adp = GetSqlDataAdapter(Sql, Conexion);
            adp.Fill(ds, NombreTabla);
            Conexion.Close();
            return ds.Tables[NombreTabla];
        }

        public DataSet devolverDataSet(string consulta, string nombre)
        {
            DataSet ds = new DataSet();
            SqlConnection conexion = GetConexion();
            SqlDataAdapter adapter = new SqlDataAdapter(consulta, conexion);
            adapter.Fill(ds, nombre);
            conexion.Close();
            return ds;
        }

        public bool existe(String consulta)
        {
            bool estado = false;
            SqlConnection Conexion = GetConexion();
            SqlCommand cmd = new SqlCommand(consulta, Conexion);
            SqlDataReader datos = cmd.ExecuteReader();
            if (datos.Read())
            {
                estado = true;
            }
            return estado;
        }

        public int ejecutarSP(SqlCommand command, string nombreProcedimiento)
        {
            SqlConnection connection = GetConexion();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
           // command.Parameters.Add()
            command.CommandText = nombreProcedimiento;
            int res = command.ExecuteNonQuery();
            connection.Close();
            return res;
        }
    }
}
