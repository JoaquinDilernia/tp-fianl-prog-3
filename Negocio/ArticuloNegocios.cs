using DAO;
using Entidades;
using System;
using System.Data;


namespace Negocio
{
    public class ArticuloNegocios
    {
        public ArticuloNegocios() { }

        public bool AgregarArticulo(ArticulosEntidad articulos)
        {
            ArticulosDatos datos = new ArticulosDatos();
            return datos.AgregarArticulo(articulos);
        }

        public DataTable CargarGvArticulos(string consulta)
        {
            AccesoDatos acc = new AccesoDatos();
            return acc.ObtenerTabla("Articulo", consulta);
        }

        public DataSet CargarListView(string consulta)
        {
            ArticulosDatos articulos = new ArticulosDatos();
            return articulos.DevolverDataSet(consulta);
        }

        public bool BorrarArticulo(int id)
        {
            ArticulosDatos articulos = new ArticulosDatos();
            return articulos.BorrarArticulo(id);
        }

        public bool BajaLogica(int id)
        {
            ArticulosDatos articulos = new ArticulosDatos();
            return articulos.bajaLogica(id);
        }

        public bool EditarArticulo(ArticulosEntidad articulos)
        {
            ArticulosDatos arts = new ArticulosDatos();
            return arts.editarArticulo(articulos);
        }

        public bool VerificarUsoArticulo(int id)
        {
            AccesoDatos acceso = new AccesoDatos();
            return acceso.existe("Select * from DetalleFacturas where CodArticulo = '" + id.ToString() + "'");
        }
    }
}
