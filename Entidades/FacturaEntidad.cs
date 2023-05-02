namespace Entidades
{
    public class FacturaEntidad
    {
        private int id_Factura;
        private string dni_Usuario;
        private string fecha_venta;
        private decimal monto_final;

        public FacturaEntidad()
        {

        }

        public int Id_Factura { get => id_Factura; set => id_Factura = value; }
        public string Dni_Usuario { get => dni_Usuario; set => dni_Usuario = value; }
        public string Fecha_venta { get => fecha_venta; set => fecha_venta = value; }
        public decimal Monto_final { get => monto_final; set => monto_final = value; }
    }
}
