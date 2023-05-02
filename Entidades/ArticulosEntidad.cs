namespace Entidades
{
    public class ArticulosEntidad
    {
        private int idArticulo;
        private int idCategoria;
        private string descripcionArticulo;
        private float precioUnitario;
        private int stockDisponibleArticulo;
        private string url_articulo_img;

        public ArticulosEntidad()
        {
        }

        public string getDescripcion() { return this.descripcionArticulo; }

        public int IdArticulo { get => idArticulo; set => idArticulo = value; }
        public int IdCategoria { get => idCategoria; set => idCategoria = value; }
        public float PrecioUnitarioArticulo { get => precioUnitario; set => precioUnitario = value; }
        public int StockDisponibleArticulo { get => stockDisponibleArticulo; set => stockDisponibleArticulo = value; }
        public string Url_articulo_img { get => url_articulo_img; set => url_articulo_img = value; }
        public string DescripcionArticulo1 { get => descripcionArticulo; set => descripcionArticulo = value; }
    }
}
