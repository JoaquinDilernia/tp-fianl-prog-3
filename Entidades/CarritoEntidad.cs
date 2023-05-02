namespace Entidades
{
    public class CarritoEntidad
    {
        private string dni;
        private int id_articulo;
        private string descripcionArticulo;
        private decimal precio;
        private int cantidad;

        public CarritoEntidad()
        {

        }

        public string Dni { get => dni; set => dni = value; }
        public int Id_articulo { get => id_articulo; set => id_articulo = value; }
        public string DescripcionArticulo { get => descripcionArticulo; set => descripcionArticulo = value; }
        public decimal Precio { get => precio; set => precio = value; }
        public int Cantidad { get => cantidad; set => cantidad = value; }
    }
}
