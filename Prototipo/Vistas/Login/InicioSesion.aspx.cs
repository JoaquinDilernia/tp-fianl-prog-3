using Entidades;
using Negocio;
using System;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Web.UI;

namespace Prototipo
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;
            if (!IsPostBack)
            {
                this.Form.Attributes.Add("autocomplete", "off");
            }
            lbMensaje2.Text = "";

            if (Session["Nombre"] != null)
            {
                lblNombreUsuario.Text = Session["Nombre"].ToString();
                lblNombreUsuario.Visible = true;
                btnCerrarSesion.Visible = true;
                txtMailLogin.Enabled = false;
                txtContraLogin.Enabled = false;
                
                    lbMensaje.Visible = true;
                    lbMensaje.ForeColor = Color.Red;
                //lbMensaje.Text = "CIERRE SESION ANTES DE LOGEARSE CON OTRA CUENTA";
                lbMensaje.Text = "";
                    return;
                
            }
            else 
            {
                btnCerrarSesion.Visible = false; 
            }               
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
            UsuarioEntidad usuarioEntidad = new UsuarioEntidad();
            usuarioEntidad.EmailUsuario = txtMailLogin.Text;
            usuarioEntidad.Contra = txtContraLogin.Text;

            if (Session["Nombre"] != null){
                lbMensaje2.ForeColor = Color.Red;
                lbMensaje2.Visible = true;
                lbMensaje2.Text = "DEBERA CERRAR SESION ANTES DE PODER ACCEDER CON OTRA CUENTA";
                return;
            }

            if (!usuarioNegocio.VerificarMail(usuarioEntidad))
            {
                if(Session["Nombre"] != null)
                {
                    lbMensaje2.Text = "DEBERA CERRAR SESION ANTES DE PODER ACCEDER CON OTRA CUENTA";
                    return;
                }
                lbMensaje.ForeColor = Color.Red;
                lbMensaje.Visible = true;
                lbMensaje.Text = "EL MAIL O LA CONTRASEÑA ES INCORRECTO";
                return;
            }

            if(!usuarioNegocio.Logeo(usuarioEntidad))
            {
                if (Session["Nombre"] != null)
                {
                    lbMensaje2.Text = "DEBERA CERRAR SESION ANTES DE PODER ACCEDER CON OTRA CUENTA";
                    return;
                }
                lbMensaje.ForeColor = Color.Red;
                lbMensaje.Visible = true;
                lbMensaje.Text = "EL MAIL O LA CONTRASEÑA ES INCORRECTO";
                return;
            }

            DataTable dt = (DataTable)usuarioNegocio.ObtenerUsuario(usuarioEntidad);
            Session["Dni"] = dt.Rows[0][0].ToString();
            Session["Nombre"] = dt.Rows[0][5].ToString();
            Session["Apellido"] = dt.Rows[0][6].ToString();
            Session["Email"] = dt.Rows[0][2].ToString();
            Session["Rol"] = Convert.ToInt32(dt.Rows[0][1]);
            Session["Direccion"] = dt.Rows[0][7].ToString();
            Session["Contrasenia"] = dt.Rows[0][3].ToString();

            lbMensaje.Visible = true;
            lbMensaje.ForeColor = Color.Green;
            txtMailLogin.Text = "";
            txtMailLogin.Enabled = false;
            txtContraLogin.Enabled = false;
            lbMensaje.Text = "BIENVENIDO " + Session["Nombre"].ToString();
            lbMensaje2.ForeColor = Color.Red;
            lbMensaje2.Visible = true;
            //if (Session["Nombre"] == null)
            //{
            //    lbMensaje2.Text = "DEBERA CERRAR SESION ANTES DE PODER ACCEDER CON OTRA CUENTA";
            //}
            btnCerrarSesion.Visible = true;
            lblNombreUsuario.Visible = true;
            lblNombreUsuario.Text = Session["Nombre"].ToString();
        }

        protected void btnRegistro_Click(object sender, EventArgs e)
        {
            Server.Transfer("Registro.aspx");
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Server.Transfer("/Vistas/Home/Home.aspx");
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            lbMensaje.Visible = true;
            lbMensaje.ForeColor = Color.Red;
            lbMensaje.Text = "SESION CERRADA";
            lbMensaje2.Visible = false;
            btnCerrarSesion.Visible = false;
            lblNombreUsuario.Visible = false;
            txtMailLogin.Enabled = true;
            txtContraLogin.Enabled = true;
        }
    }
}