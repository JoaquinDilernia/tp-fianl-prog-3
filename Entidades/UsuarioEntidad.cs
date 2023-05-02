namespace Entidades
{
    public class UsuarioEntidad
    {
        private string nombreUsuario;
        private string apellidoUsuario;
        private long dniUsuario;
        private string emailUsuario;
        private string direccionUsuario;
        private bool admin = false;
        private string contra;

        public UsuarioEntidad()
        {

        }

        public UsuarioEntidad(string nombreUsuario, string apellidoUsuario, long dniUsuario, string emailUsuario, string direccionUsuario, bool admin)
        {
            this.nombreUsuario = nombreUsuario;
            this.apellidoUsuario = apellidoUsuario;
            this.dniUsuario = dniUsuario;
            this.emailUsuario = emailUsuario;
            this.direccionUsuario = direccionUsuario;
            this.admin = admin;
        }

        public string NombreUsuario { get => nombreUsuario; set => nombreUsuario = value; }
        public string ApellidoUsuario { get => apellidoUsuario; set => apellidoUsuario = value; }
        public long DniUsuario { get => dniUsuario; set => dniUsuario = value; }
        public string EmailUsuario { get => emailUsuario; set => emailUsuario = value; }
        public string DireccionUsuario { get => direccionUsuario; set => direccionUsuario = value; }
        public bool Admin { get => admin; set => admin = value; }
        public string Contra { get => contra; set => contra = value; }
    }
}
