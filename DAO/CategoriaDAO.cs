using System;
using System.Data;
using System.Data.SqlClient;


namespace DAO
{
    public class CategoriaDAO
    {
        public bool AgregarCategoria(string descripcion)
        {
            AccesoDatos datos = new AccesoDatos();
            SqlCommand comando = new SqlCommand();
            SqlParameter parameter = new SqlParameter();
            parameter = comando.Parameters.Add("@Descripcion", SqlDbType.VarChar);
            parameter.Value = descripcion;
            return Convert.ToBoolean(datos.ejecutarSP(comando, "AgregarCategoria"));
        }

        public bool EliminarCategoria(int id)
        {
            AccesoDatos accesoDatos = new AccesoDatos();
            SqlCommand command = new SqlCommand();
            SqlParameter parameter = new SqlParameter();
            parameter = command.Parameters.Add("@Id", SqlDbType.Int);
            parameter.Value = id;
            return Convert.ToBoolean(accesoDatos.ejecutarSP(command, "EliminarCategoria"));
        }
    }
}
