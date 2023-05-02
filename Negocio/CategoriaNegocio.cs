using DAO;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Negocio
{
    public class CategoriaNegocio
    {
        public DataTable CargarCategorias()
        {
            AccesoDatos acc = new AccesoDatos();
            string consulta = "Select * from Categoria";
            return acc.ObtenerTabla("Categoria", consulta);
        }

        public DataTable CargarCategorias(string consulta)
        {
            AccesoDatos acc = new AccesoDatos();
            return acc.ObtenerTabla("Categoria", consulta);
        }

        public bool BorrarCategoria(int id)
        {
            CategoriaDAO cate = new CategoriaDAO();
            return cate.EliminarCategoria(id);
        }

        public bool EditarCategoria(string id, string nuevaDescripcion)
        {
            AccesoDatos accesoDatos = new AccesoDatos();
            SqlCommand cmd = new SqlCommand();
            SqlParameter param = new SqlParameter();
            param = cmd.Parameters.Add("@IdCat", SqlDbType.Int);
            param.Value = id;
            param = cmd.Parameters.Add("@DescripcionCat", SqlDbType.VarChar);
            param.Value = nuevaDescripcion;
            return Convert.ToBoolean(accesoDatos.ejecutarSP(cmd, "ModificarCategoria"));
        }

        public bool AgrearCategoria(string descr)
        {
            CategoriaDAO cat = new CategoriaDAO();
            return cat.AgregarCategoria(descr);
        }

        public SqlDataReader CargarDdl()
        {
            AccesoDatos acc = new AccesoDatos();
            SqlCommand cmd = new SqlCommand("Select Id, Descripcion FROM Categoria", acc.GetConexion());
            SqlDataReader dr = cmd.ExecuteReader();
            return dr;
        }

        public bool VerificarUsoCategoria(int id)
        {
            AccesoDatos acc = new AccesoDatos();
            string consulta = "Select * from Articulo Where Id_Cat = '" + id.ToString() + "'";
            return acc.existe(consulta);
        }
    }
}
