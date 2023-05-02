using Entidades;
using Negocio;
using System;
using System.Drawing;

namespace Prototipo.Vistas.Login
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Form.Attributes.Add("autocomplete", "off");
            this.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;

            if (Session["Nombre"] != null)
            {
                lblNombreUsuario.Visible = true;
                lblNombreUsuario.Text = Session["Nombre"].ToString();
                btnCerrarSesion.Visible = true;
                txtApellidoReg.Enabled = false;
                txtNombreReg.Enabled = false;
                txtContraReg.Enabled = false;
                txtContraRepeReg.Enabled = false;
                txtDniReg.Enabled = false;
                txtDirecReg.Enabled = false;
                txtEmailReg.Enabled = false;
                lblMensaje.ForeColor = Color.Red;
                btnRegistrarse.Enabled = false;
                lblMensaje.Text = "DEBE CERRAR SESION ANTES DE PODER REGISTRARSE";
            }
        }

        public UsuarioEntidad CargarUsuario()
        {
            UsuarioEntidad usuario = new UsuarioEntidad();
            try
            {
                usuario.NombreUsuario = txtNombreReg.Text;
                usuario.ApellidoUsuario = txtApellidoReg.Text;
                usuario.Admin = false;
                usuario.Contra = txtContraReg.Text;
                usuario.DniUsuario = Convert.ToInt64(txtDniReg.Text);
                usuario.DireccionUsuario = txtDirecReg.Text;
                usuario.EmailUsuario = txtEmailReg.Text;
            }
            catch
            {
                lblMensaje.Visible = true;
                lblMensaje.Text = "HUBO UN ERROR";
            }
            return usuario;
        }

        protected void btnRegistrarse_Click(object sender, EventArgs e)
        {
            lblMensaje.Text = "";

            UsuarioEntidad usuario = new UsuarioEntidad();

            usuario = CargarUsuario();

            UsuarioNegocio usuarioNegocio = new UsuarioNegocio();

            bool existe = true;

            if (usuarioNegocio.VerificarDni(usuario))
            {
                lblMensaje.ForeColor = Color.Red;
                lblMensaje.Text = "EL DNI INGRESADO YA ESTA ASOCIADO A UN USUARIO ";
                existe = false;
            }

            if (usuarioNegocio.VerificarMail(usuario))
            {
                lblMensaje.ForeColor = Color.Red;
                lblMensaje.Text += " EL MAIL INGRESADO YA PERTENECE A UN USUARIO";
                existe = false;
            }

            if (!existe)
            { return; }

            if (usuarioNegocio.AgregarUsuario(usuario))
            {
                lblMensaje2.Visible = true;
                lblMensaje2.ForeColor = Color.Green;
                lblMensaje2.Text = "EL USUARIO SE REGISTRO EXITOSAMENTE";
            }
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            btnCerrarSesion.Visible = false;
            Response.Redirect("/Vistas/Home/Home.aspx");
        }
    }
}