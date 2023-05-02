using Entidades;
using System.Data;
using System.Data.SqlClient;

namespace DAO
{
    public class CarritoDAO
    {
        public CarritoDAO()
        {
        }

        public bool agregarArticuloCarrito(CarritoEntidad carro)
        {
            AccesoDatos acceso = new AccesoDatos();
            SqlCommand command = new SqlCommand();
            ArmarParametrosAgregar(ref command, carro);
            if (acceso.ejecutarSP(command, "AgregarArticuloCarrito") == 1)
                return true;
            return false;
        }

        public void ArmarParametrosAgregar(ref SqlCommand command, CarritoEntidad carrito)
        {
            SqlParameter sqlParameter = new SqlParameter();
            sqlParameter = command.Parameters.Add("@dniUsuario", SqlDbType.VarChar);
            sqlParameter.Value = carrito.Dni;
            sqlParameter = command.Parameters.Add("@idArticulo", SqlDbType.Int);
            sqlParameter.Value = carrito.Id_articulo;
            sqlParameter = command.Parameters.Add("@descripcion", SqlDbType.VarChar);
            sqlParameter.Value = carrito.DescripcionArticulo;
        }

        public bool BorrarArticulo(string dni, string idArt)
        {
            AccesoDatos acceso = new AccesoDatos();
            SqlCommand cmd = new SqlCommand();
            SqlParameter param = new SqlParameter();
            param = cmd.Parameters.Add("@dni", SqlDbType.VarChar);
            param.Value = dni;
            param = cmd.Parameters.Add("@idArticulo", SqlDbType.Int);
            param.Value = idArt;

            if (acceso.ejecutarSP(cmd, "eliminarArticuloXcarrito") == 1)
                return true;
            return false;
        }

        public bool ModificarArticulos(CarritoEntidad carro)
        {
            AccesoDatos acceso = new AccesoDatos();
            SqlCommand cmd = new SqlCommand();
            SqlParameter param = new SqlParameter();
            param = cmd.Parameters.Add("@dni", SqlDbType.VarChar);
            param.Value = carro.Dni;
            param = cmd.Parameters.Add("@idArticulo", SqlDbType.Int);
            param.Value = carro.Id_articulo;
            param = cmd.Parameters.Add("@cantidad", SqlDbType.Int);
            param.Value = carro.Cantidad;
            if (acceso.ejecutarSP(cmd, "ModificarCantidadCarrito") == 1)
                return true;
            return false;
        }
    }
}
