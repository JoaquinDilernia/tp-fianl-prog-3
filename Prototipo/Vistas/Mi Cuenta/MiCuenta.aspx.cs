using Entidades;
using Negocio;
using System;
using System.Drawing;
using System.Web.UI.WebControls;

namespace Prototipo.Vistas.Mi_Cuenta
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Form.Attributes.Add("autocomplete", "off");
            this.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;

            if(Session["Nombre"] != null)
            {
                lbAvisoUsuario.Visible = false;
                btnModificar.Enabled = true;
                lblNombreUsuario.Visible = true;
                lblNombreUsuario.Text = Session["Nombre"].ToString();
                btnCerrarSesion.Visible = true;
                if(!IsPostBack)
                {
                    CargarGrvFacturasUsuario();
                    CargarTxtBoxs();
                }
            }
            else
            {
                btnModificar.Enabled = false;
                btnAdministrar.Enabled = false;
                btnAdministrar.Visible = false;
                lbAvisoUsuario.Visible = true;
                txtNombre.Enabled = false;
                txtApellido.Enabled = false;
                txtDireccion.Enabled = false;
                txtMail.Enabled = false;
                txtContrasenia.Enabled = false;
            }

            if(Convert.ToInt32(Session["Rol"]) == 0)
            {
                btnAdministrar.Visible = true;
            }
        }

        public void CargarTxtBoxs()
        {
            txtNombre.Text = Session["Nombre"].ToString();
            txtApellido.Text = Session["Apellido"].ToString();
            lblDni.Text = Session["Dni"].ToString();
            txtDireccion.Text = Session["Direccion"].ToString();
            txtMail.Text = Session["Email"].ToString();
            txtContrasenia.Text = Session["Contrasenia"].ToString();
        }

        public bool Validacion()
        {
            if(txtDireccion.Text != "" && txtApellido.Text != "" && txtContrasenia.Text != "" && txtMail.Text != "" && txtNombre.Text != "")
            {
                return true;
            }
            return false;
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            if(!Validacion())
            {
                lbValidacion.ForeColor = Color.Red;
                lbValidacion.Text = "NO SE PUEDE FINALIZAR LA MODIFICACION SI UNA CAJA ESTA VACIA";
                return;
            }

            UsuarioEntidad usuario = new UsuarioEntidad();
            usuario.DireccionUsuario = txtDireccion.Text;
            usuario.DniUsuario = Convert.ToInt32(lblDni.Text);
            usuario.NombreUsuario = txtNombre.Text;
            usuario.ApellidoUsuario = txtApellido.Text;
            usuario.Contra = txtContrasenia.Text;
            usuario.EmailUsuario = txtMail.Text;

            UsuarioNegocio usuarioNegocio = new UsuarioNegocio();

            if(usuarioNegocio.EditarUsuario(usuario, true))
            {
                Session["Nombre"] = txtNombre.Text;
                Session["Apellido"] = txtApellido.Text;
                Session["Dni"] = lblDni.Text;
                Session["Direccion"] = txtDireccion.Text;
                Session["Email"] = txtMail.Text;
                Session["Contrasenia"] = txtContrasenia.Text;

                lbValidacion.ForeColor = Color.Green;
                lbValidacion.Text = "REGISTRO CAMBIADO EXITOSAMENTE";
            }
            else
            {
                lbValidacion.ForeColor = Color.Red;
                lbValidacion.Text = "NO SE PUDO MODIFICAR EL REGISTRO";
            }
        }

        public void CargarGrvFacturasUsuario()
        {
            FacturaNegocio factura = new FacturaNegocio();
            string consulta = "Select * from Facturas where Dni_Usuario =" + Session["Dni"].ToString();
            gvFacturas.DataSource = factura.CargarGrv(consulta);
            gvFacturas.DataBind();
        }

        protected void gvFacturas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvFacturas.PageIndex = e.NewPageIndex;
            CargarGrvFacturasUsuario();
        }

        protected void gvFacturas_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            string idFactura = ((Label)gvFacturas.Rows[e.NewSelectedIndex].FindControl("lblIdFactura")).Text;
            FacturaNegocio facturaNegocio = new FacturaNegocio();
            string consulta = "Select DescripcionArticulo AS Producto, PrecioUnitario AS Precio, Cantidad AS Cantidad," +
                " (PrecioUnitario * Cantidad) AS Total FROM DetalleFacturas WHERE Id_Factura = " + idFactura;
            gvDetalleFactura.DataSource = facturaNegocio.CargarGrv(consulta);
            gvDetalleFactura.DataBind();
        }

        protected void btnAdministrar_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Vistas/Administrador/Administrador.aspx");
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            btnCerrarSesion.Visible = false;
            Server.Transfer("/Vistas/Home/Home.aspx");
        }
    }
}