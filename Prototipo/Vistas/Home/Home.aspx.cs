using Entidades;
using Negocio;
using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI.WebControls;

namespace Prototipo.Vistas.Home
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Form.Attributes.Add("autocomplete", "off");
            this.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;

            if(!IsPostBack)
            {
                CargarListView();
                CargarCategoriasDDL();
            }

            if(Session["Nombre"] != null)
            {
                lblNombreUsuario.Text = Session["Nombre"].ToString();
                btnCerrarSesion.Visible = true;
                lblBienvenido.Visible = true;
            }
        }

        public void CargarListView()
        {
            string consulta = "";
            lblArticuloCarrito.Text = "";
            lblPreguntaConfirmacion.Text = "";

            bool nombre = Convert.ToBoolean(Session["FiltrarArticuloNombre"]);
            bool categoria = Convert.ToBoolean(Session["Categorias"]);
            bool precio = Convert.ToBoolean(Session["PrecioArticulo"]);

            if(nombre)
            {
                consulta = "Select * from Articulo where Estado = 1 AND StockArticulo > 0 AND Nombre_Art like '%" + txtNombreArticulo.Text + "%'";
            }

            if(categoria)
            {
                if(consulta == "")
                {
                    consulta = "Select * from Articulo where Estado = 1 AND StockArticulo > 0 AND Id_Cat = '" + ddlCategorias.SelectedValue.ToString() + "'";
                }
                else
                {
                    consulta += "AND Id_Cat = '" + ddlCategorias.SelectedValue.ToString() + "'";
                }
            }

            if(precio)
            {
                if (consulta == "")
                    consulta = "Select * from Articulo WHERE Estado = 1 AND StockArticulo > 0 AND PrecioUnitario between '" + txtPrecioMinimo.Text + "' AND '" + txtPrecioMaxim.Text + "' ";
                else
                    consulta += "And PrecioUnitario between '" + txtPrecioMinimo.Text + "' AND '" + txtPrecioMaxim.Text + "' ";
            }

            if(consulta != "")
            {
                CargarLVGenerico(consulta);
                return;
            }

            CargarLVGenerico("Select * from Articulo Where Estado = 1 AND StockArticulo > 0");
        }

        public void CargarLVGenerico(string consulta)
        {
            ArticuloNegocios articulo = new ArticuloNegocios();
            lvArticulos.DataSource = articulo.CargarListView(consulta);
            lvArticulos.DataBind();
        }

        public void CargarCategoriasDDL()
        {
            CategoriaNegocio categoria = new CategoriaNegocio();
            SqlDataReader dr = categoria.CargarDdl();
            ddlCategorias.DataSource = dr;
            ddlCategorias.DataTextField = "Descripcion";
            ddlCategorias.DataValueField = "Id";
            ddlCategorias.DataBind();
        }

        public bool VerificarTxt()
        {
            lblPreguntaConfirmacion.Text = "";

            bool estado = true;

            if(txtPrecioMinimo.Text == "" || txtPrecioMaxim.Text == "")
            {
                estado = false;
                lblPreguntaConfirmacion.ForeColor = Color.Red;
                Session["PrecioArticulo"] = false;
                lblPreguntaConfirmacion.Visible = true;
                lblPreguntaConfirmacion.Text = "TANTO EL PRECIO MINIMO COMO MAXIMO DEBEN ESTAR COMPLETOS \n";
                return estado;
            }

            if (Convert.ToInt32(txtPrecioMinimo.Text) < 0 || Convert.ToInt32(txtPrecioMaxim.Text) < 0)
            {
                estado = false;
                lblPreguntaConfirmacion.ForeColor = Color.Red;
                lblPreguntaConfirmacion.Visible = true;
                lblPreguntaConfirmacion.Text = "DEBE INGRESAR VALORES IGUAL O MAYORES A 0";
                return estado;
            }

            if(Convert.ToInt32(txtPrecioMinimo.Text) > Convert.ToInt32(txtPrecioMaxim.Text))
            {
                estado = false;
                lblPreguntaConfirmacion.ForeColor = Color.Red;
                lblPreguntaConfirmacion.Visible = true;
                lblPreguntaConfirmacion.Text = "EL PRECIO MINIMO NO PUEDE SER MAYOR AL PRECIO MAXIMO \n";
            }

            return estado;
        }

        protected void btnBuscarNombre_Click(object sender, EventArgs e)
        {
            Session["FiltrarArticuloNombre"] = true;
            CargarListView();
        }

        protected void btnQuitarFiltros_Click(object sender, EventArgs e)
        {
            Session["FiltrarArticuloNombre"] = false;
            Session["PrecioArticulo"] = false;
            Session["Categorias"] = false;
            CargarListView();
            txtNombreArticulo.Text = "";
            txtPrecioMinimo.Text = "";
            txtPrecioMaxim.Text = "";
            ddlCategorias.SelectedIndex = 0;
        }

        protected void ddlCategorias_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["Categorias"] = true;
            CargarListView();
        }

        protected void btnFiltrarPorPrecio_Click(object sender, EventArgs e)
        {
            if (!VerificarTxt())
                return;
            Session["PrecioArticulo"] = true;
            CargarListView();
        }


        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            lblNombreUsuario.Visible = false;
            lblMensaje.ForeColor = Color.Red;
            lblMensaje.Text = "LA SESION HA SIDO CERRADA";
            btnCerrarSesion.Visible = false;
            lblBienvenido.Visible = false;
        }

        protected void lvArticulos_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            (lvArticulos.FindControl("DataPager1") as DataPager).SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            this.CargarListView();
        }

        protected void btnSeleccionar_Command(object sender, CommandEventArgs e)
        {
            if (Session["Nombre"] == null)
            {
                lblArticuloCarrito.Visible = true;
                lblArticuloCarrito.ForeColor = Color.Red;
                lblArticuloCarrito.Text = "DEBE INICIAR SESION PARA PODER SELECCIONAR UN ARTICULO";
                return;
            }

            CarritoEntidad carrito = new CarritoEntidad();
            carrito.Id_articulo = Convert.ToInt32(e.CommandArgument.ToString().Split('-')[0]);

            lblPreguntaConfirmacion.Text = "";

            carrito.Dni = Session["Dni"].ToString();

            CarritoNegocio carritoNegocio = new CarritoNegocio();

            if (carritoNegocio.VerificarSeleccionArticulo(carrito))
            {
                lblPreguntaConfirmacion.Visible = true;
                lblPreguntaConfirmacion.ForeColor = Color.Red;
                lblPreguntaConfirmacion.Text = "ESTE PRODUCTO YA SE ENCUENTRA EN SU CARRITO";
                return;
            }

            if (e.CommandName == "Seleccionar")
            {
                carrito.DescripcionArticulo = e.CommandArgument.ToString().Split('-')[1];
                CarritoNegocio carritoNegocio1 = new CarritoNegocio();
                carritoNegocio1.AgregarArticuloCarrito(carrito);
                lblArticuloCarrito.Visible = true;
                lblArticuloCarrito.ForeColor = Color.Green;
                lblArticuloCarrito.Text = "EL ARTICULO SE AGREGO AL CARRITO";
            }
        }
    }
}