using DAO;
using Entidades;
using System.Data;


namespace Negocio
{
    public class CarritoNegocio
    {
        public CarritoNegocio()
        {

        }

        public bool AgregarArticuloCarrito(CarritoEntidad carro)
        {
            CarritoDAO carroDao = new CarritoDAO();
            return carroDao.agregarArticuloCarrito(carro);
        }

        public DataTable CargarGrv(string Dni)
        {
            AccesoDatos accesoDatos = new AccesoDatos();
            string consulta = "Select dni_Usuario, id_articulo, descripcionArticulo, cantidad, PrecioUnitario, " +
                "(PrecioUnitario * cantidad) AS Total FROM usuariosXcarrito INNER JOIN Articulo ON usuariosXcarrito.id_articulo = Articulo.Id " +
                "WHERE usuariosXcarrito.dni_Usuario = " + Dni;

            return accesoDatos.ObtenerTabla("usuariosXcarrito", consulta);
        }

        public bool BorrarArticulo(string dni, string idArt)
        {
            CarritoDAO carrito = new CarritoDAO();
            return carrito.BorrarArticulo(dni, idArt);
        }

        public bool VerificarSeleccionArticulo(CarritoEntidad carro)
        {
            AccesoDatos acceso = new AccesoDatos();
            string consulta = "Select * from usuariosXcarrito WHERE id_articulo = '" + carro.Id_articulo + "' AND dni_Usuario = '" + carro.Dni + "'";
            return acceso.existe(consulta);
        }

        public bool ModificarCantidad(CarritoEntidad carro)
        {
            CarritoDAO carroDAO = new CarritoDAO();
            return carroDAO.ModificarArticulos(carro);
        }
    }
}
