using Entidades;
using Negocio;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace Prototipo
{
    public partial class Carrito : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Form.Attributes.Add("autocomplete", "off");
            this.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;

            if(!IsPostBack)
            {
                CargarGrvCarrito();
            }

            if(Session["Nombre"] != null)
            {
                lblNombreUsuario.Visible = true;
                lblNombreUsuario.Text = Session["Nombre"].ToString();
                btnCerrarSesion.Visible = true;

                if(gvCarrito.Rows.Count == 0)
                {
                    lbComentarios.Visible = true;
                    lbComentarios.Text = "NO HAY ARTICULOS AGREGADOS A SU CARRITO";
                }
            }

            else
            {
                lblNombreUsuario.Visible = false;
                lblNombreUsuario.Text = "";
                btnCerrarSesion.Visible = false;
            }
        }

        public void CargarGrvCarrito()
        {
            if(Session["Dni"] == null)
            {
                lblAvisoCuenta.Visible = true;
                btnFinalizarCompra.Visible = false;
                return;
            }

            if(lblAvisoCuenta.Visible)
            {
                lblAvisoCuenta.Visible = false;
                btnFinalizarCompra.Visible = true;
            }

            CarritoNegocio carrito = new CarritoNegocio();
            DataTable tablaCarro = carrito.CargarGrv(Session["Dni"].ToString());
            gvCarrito.DataSource = tablaCarro;
            gvCarrito.DataBind();
            TotalCompra();
        }

        public void TotalCompra()
        {
            decimal total = 0;

            foreach(GridViewRow row in gvCarrito.Rows)
            {
                total += Convert.ToDecimal(((Label)row.FindControl("lblPrecioTotalFila")).Text);
            }

            Session["Total"] = total;
        }

        private void MostrarConfirmacion()
        {
            if(lblPreguntaConfirmacionEliminarProducto.Visible == true)
            {
                lblPreguntaConfirmacionEliminarProducto.Visible = false;
                btnAceparEliminarProducto.Visible = false;
                btnCancelarEliminarProducto.Visible = false;
                return;
            }

            lblPreguntaConfirmacionEliminarProducto.Visible = true;
            btnAceparEliminarProducto.Visible = true;
            btnCancelarEliminarProducto.Visible = true;
        }

        public void EsconderBotones()
        {
           if (lblConfirmacionDeCompra.Visible)
           {
                lblConfirmacionDeCompra.Visible = false;
                btnConfirmarCompra.Visible = false;
                btnCancelarConfirmarCompra.Visible = false;
           }

            lblConfirmacionDeCompra.Visible = true;
            btnConfirmarCompra.Visible = true;
            btnCancelarConfirmarCompra.Visible = true;
        }

        public bool VerificarStock(string idArticulo, ref int cant)
        {
            ArticuloNegocios articulo = new ArticuloNegocios();
            string consulta = "Select StockArticulo from Articulo where Id = " + idArticulo;
            DataTable dt = articulo.CargarGvArticulos(consulta);
            if(cant > Convert.ToInt32(dt.Rows[0][0]))
            {
                cant = Convert.ToInt32(dt.Rows[0][0]);
                return false;
            }

            return true;
        }

        public void BorrarProductoCarrito()
        {
            CarritoNegocio carritoNegocio = new CarritoNegocio();
            if(carritoNegocio.BorrarArticulo(Session["DniIdBorrar"].ToString(), Session["IdBorrar"].ToString()))
            {
                lbComentarios.Visible = true;
                lbComentarios.Text = "EL ARTICULO SE BORRO CON EXITO";
            }
            else
            {
                lbComentarios.Visible = true;
                lbComentarios.Text = "NO SE PUDO ELIMINAR CON EXITO";
            }

            CargarGrvCarrito();
        }

        protected void gvCarrito_DataBound(object sender, EventArgs e)
        {
            int contRow = gvCarrito.Rows.Count;

            if(contRow == 0)
            {
                btnFinalizarCompra.Visible = false;
                return;
            }

            btnFinalizarCompra.Visible = true;
        }

        protected void gvCarrito_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCarrito.PageIndex = e.NewPageIndex;
            CargarGrvCarrito();
        }

        protected void gvCarrito_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvCarrito.EditIndex = -1;
            CargarGrvCarrito();
        }

        protected void gvCarrito_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string dni = Session["Dni"].ToString();
            string idArticulo = ((Label)gvCarrito.Rows[e.RowIndex].FindControl("lblIdArticulo")).Text;
            Session["DniIdBorrar"] = dni;
            Session["IdBorrar"] = idArticulo;
            MostrarConfirmacion();
        }

        protected void gvCarrito_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvCarrito.EditIndex = e.NewEditIndex;
            CargarGrvCarrito();
        }

        protected void gvCarrito_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string idArticulo = ((Label)gvCarrito.Rows[e.RowIndex].FindControl("lblIdArticuloEdit")).Text;
            int cant = Convert.ToInt32(((TextBox)gvCarrito.Rows[e.RowIndex].FindControl("txtCantidad")).Text);

            if(!VerificarStock(idArticulo, ref cant))
            {
                lbComentarios.Visible = true;
                lbComentarios.Text = "NO SE PUEDE INGRESAR UNA CANTIDAD MAYOR AL STOCK ACTUAL ( " + cant + " )";
                return;
            }

            CarritoNegocio carrito = new CarritoNegocio();
            CarritoEntidad carritoEntidad = new CarritoEntidad();
            carritoEntidad.Dni = Session["Dni"].ToString();
            carritoEntidad.Cantidad = cant;
            carritoEntidad.Id_articulo = Convert.ToInt32(idArticulo);

            if(carrito.ModificarCantidad(carritoEntidad))
            {
                lbComentarios.Text = "REGISTRO MODIFICADO CON EXITO";
                gvCarrito.EditIndex = -1;
                CargarGrvCarrito();
                return;
            }

            gvCarrito.EditIndex = -1;
            CargarGrvCarrito();
            lbComentarios.Text = "NO SE PUDO MODIFICAR CON EXITO";
        }

        protected void btnAceparEliminarProducto_Click(object sender, EventArgs e)
        {
            BorrarProductoCarrito();
            MostrarConfirmacion();
        }

        protected void btnCancelarEliminarProducto_Click(object sender, EventArgs e)
        {
            Session["IdBorrar"] = null;
            MostrarConfirmacion();
            lbComentarios.Text = "SE CANCELO LA ELIMINACION DEL ARTICULO DEL CARRITO";
        }

        protected void btnFinalizarCompra_Click(object sender, EventArgs e)
        {
            lblConfirmacionDeCompra.Visible = true;
            EsconderBotones();
        }

        protected void btnCancelarConfirmarCompra_Click(object sender, EventArgs e)
        {
            EsconderBotones();
            lblConfirmacionDeCompra.Visible = false;
            btnConfirmarCompra.Visible = false;
            btnCancelarConfirmarCompra.Visible = false;
            lbComentarios.Text = "COMPRA CANCELADA";
        }

        protected void btnConfirmarCompra_Click(object sender, EventArgs e)
        {
            EsconderBotones();
            btnConfirmarCompra.Visible = false;
            btnCancelarConfirmarCompra.Visible = false;
            lblConfirmacionDeCompra.Visible = false;
            FacturaNegocio factura = new FacturaNegocio();
            FacturaEntidad facturaEntidad = new FacturaEntidad();
            facturaEntidad.Dni_Usuario = Session["Dni"].ToString();
            facturaEntidad.Monto_final = Convert.ToDecimal(Session["Total"]);
            
            if(!factura.GenerarFactura(facturaEntidad))
            {
                lbComentarios.Text = "NO SE PUDO CONFIRMAR LA COMPRA";
                return;
            }

            lbComentarios.Text = "COMPRA CONFIRMADA";

            DataTable dt = new DataTable();

            string consulta = "Select id_Factura from Facturas where Dni_Usuario = " + Session["Dni"].ToString();

            dt = factura.CargarGrv(consulta);

            FacturaDetallesEntidad detallesEntidad = new FacturaDetallesEntidad();

            foreach(DataRow row in dt.Rows)
            {
                detallesEntidad.Id_factura = Convert.ToInt32(row[0]);
            }

            CarritoNegocio carrito = new CarritoNegocio();

            foreach(GridViewRow row in gvCarrito.Rows)
            {
                detallesEntidad.Id_articulo = Convert.ToInt32(((Label)row.FindControl("lblIdArticulo")).Text);
                detallesEntidad.DescripcionProducto = ((Label)row.FindControl("lblNombreProducto")).Text;
                detallesEntidad.Precio_unitario = Convert.ToDecimal(((Label)row.FindControl("lblPrecioUnitario")).Text);
                detallesEntidad.Cantidad = Convert.ToInt32(((Label)row.FindControl("lblCantidad")).Text);
                factura.GenerarDetalleFactura(detallesEntidad);
                carrito.BorrarArticulo(Session["Dni"].ToString(), detallesEntidad.Id_articulo.ToString());
            }

            CargarGrvCarrito();
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