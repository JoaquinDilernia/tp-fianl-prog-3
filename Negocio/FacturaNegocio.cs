using DAO;
using Entidades;
using System.Data;
namespace Negocio
{
    public class FacturaNegocio
    {
        public DataTable CargarGrv(string consulta)
        {
            AccesoDatos acc = new AccesoDatos();
            return acc.ObtenerTabla("Facturas", consulta);
        }

        public bool GenerarFactura(FacturaEntidad factura)
        {
            FacturaDAO fact = new FacturaDAO();
            return fact.GenerarFactura(factura);
        }

        public bool GenerarDetalleFactura(FacturaDetallesEntidad detalleFactura)
        {
            FacturaDAO fact = new FacturaDAO();
            return fact.generarDetalleFactura(detalleFactura);
        }
    }
}
