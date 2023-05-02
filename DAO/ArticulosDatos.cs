using Entidades;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DAO
{
    public class ArticulosDatos
    {
        public bool AgregarArticulo(ArticulosEntidad articulos)
        {
            AccesoDatos datos = new AccesoDatos();
            SqlCommand cmd = new SqlCommand();
            ArmarParametros(ref cmd, articulos, true);
            if (datos.ejecutarSP(cmd, "AgregarArticulo") == 1)
                return true;
            return false;
        }

        public DataSet DevolverDataSet(string consulta)
        {
            AccesoDatos acc = new AccesoDatos();
            return acc.devolverDataSet(consulta, "Articulo");
        }

        public void ArmarParametros(ref SqlCommand cmd, ArticulosEntidad articulos, bool url)
        {
            SqlParameter param = new SqlParameter();
            param = cmd.Parameters.Add("@IdCat", SqlDbType.Int);
            param.Value = articulos.IdCategoria;
            param = cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar);
            param.Value = articulos.DescripcionArticulo1;
            param = cmd.Parameters.Add("@precio", SqlDbType.Decimal);
            param.Value = articulos.PrecioUnitarioArticulo;
            param = cmd.Parameters.Add("@stock", SqlDbType.Int);
            param.Value = articulos.StockDisponibleArticulo;
            if (url)
            {
                param = cmd.Parameters.Add("@urlImg", SqlDbType.VarChar);
                param.Value = articulos.Url_articulo_img;
            }
            else
            {
                param = cmd.Parameters.Add("@id", SqlDbType.VarChar);
                param.Value = articulos.IdArticulo;
            }
        }

        public bool BorrarArticulo(int id)
        {
            AccesoDatos accesoDatos = new AccesoDatos();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = accesoDatos.GetConexion();
            ArmarParamtetrosEliminar(ref cmd, id);
            return Convert.ToBoolean(accesoDatos.ejecutarSP(cmd, "EliminarArticulo"));
        }

        public void ArmarParamtetrosEliminar(ref SqlCommand comando, int id)
        {
            SqlParameter parameter = new SqlParameter();
            parameter = comando.Parameters.Add("@id", SqlDbType.Int);
            parameter.Value = id;
        }

        public bool bajaLogica(int id)
        {
            AccesoDatos accesoDatos = new AccesoDatos();
            SqlCommand command = new SqlCommand();
            command.Connection = accesoDatos.GetConexion();
            ArmarParamtetrosEliminar(ref command, id);
            return Convert.ToBoolean(accesoDatos.ejecutarSP(command, "BajaLogicaArticulo"));
        }

        public bool editarArticulo(ArticulosEntidad articulos)
        {
            AccesoDatos accesoDatos = new AccesoDatos();
            SqlCommand command = new SqlCommand();
            command.Connection = accesoDatos.GetConexion();
            ArmarParametros(ref command, articulos, false);
            return Convert.ToBoolean(accesoDatos.ejecutarSP(command, "modificarArticulo"));
        }
    }
}
