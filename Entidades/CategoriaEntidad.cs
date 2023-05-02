namespace Entidades
{
    class CategoriaEntidad
    {

        private string idCategoria;
        private string descripcionCategoria;

        public CategoriaEntidad()
        {

        }

        public string IdCategoria { get => idCategoria; set => idCategoria = value; }
        public string DescripcionCategoria { get => descripcionCategoria; set => descripcionCategoria = value; }
    }
}
