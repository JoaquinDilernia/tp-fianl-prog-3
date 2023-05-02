using Entidades;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DAO
{
    public class FacturaDAO
    {
        public FacturaDAO() { }

        public bool GenerarFactura(FacturaEntidad facturas)
        {
            AccesoDatos acceso = new AccesoDatos();
            SqlCommand command = new SqlCommand();
            SqlParameter parameter = new SqlParameter();
            parameter = command.Parameters.Add("@dniUsuario", SqlDbType.VarChar);
            parameter.Value = facturas.Dni_Usuario;
            parameter = command.Parameters.Add("@montoFinal", SqlDbType.Decimal);
            parameter.Value = facturas.Monto_final;
            return Convert.ToBoolean(acceso.ejecutarSP(command, "AgregarFactura"));
        }

        public bool generarDetalleFactura(FacturaDetallesEntidad detalleFactura)
        {
            AccesoDatos acceso = new AccesoDatos();
            SqlCommand command = new SqlCommand();
            ArmarParametrosGenerarDetalleFactura(ref command, detalleFactura);
            return Convert.ToBoolean(acceso.ejecutarSP(command, "AgregarDetalleFacturas"));
        }

        public void ArmarParametrosGenerarDetalleFactura(ref SqlCommand command, FacturaDetallesEntidad detalleFactura)
        {
            SqlParameter parameter = new SqlParameter();
            parameter = command.Parameters.Add("@IdFactura", SqlDbType.Int);
            parameter.Value = detalleFactura.Id_factura;
            parameter = command.Parameters.Add("@IdArticulo", SqlDbType.Int);
            parameter.Value = detalleFactura.Id_articulo;
            parameter = command.Parameters.Add("@PrecioUnitario", SqlDbType.Decimal);
            parameter.Value = detalleFactura.Precio_unitario;
            parameter = command.Parameters.Add("@DescripcionProducto", SqlDbType.VarChar);
            parameter.Value = detalleFactura.DescripcionProducto;
            parameter = command.Parameters.Add("@Cantidad", SqlDbType.Int);
            parameter.Value = detalleFactura.Cantidad;
        }
    }
}
