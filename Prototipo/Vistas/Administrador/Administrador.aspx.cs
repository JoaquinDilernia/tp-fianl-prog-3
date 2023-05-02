using Negocio;
using Entidades;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI.WebControls;

namespace Prototipo.Vistas.Administrador
{
    public partial class Administrador : System.Web.UI.Page
    {
        public int DevolverCantidad(DataTable dt)
        {
            int cont = 0;

            foreach (DataRow row in dt.Rows)
            {
                cont++;
            }

            return cont;
        }

        public void MostrarPreguntaConfirmacion()
        {
            lblPreguntaConfirmacion.Text = "¿ESTÁ SEGURO DE BORRAR ESTE REGISTRO?";

            if (lblPreguntaConfirmacion.Visible)
            {
                lblPreguntaConfirmacion.Visible = false;
                btnAceptar.Visible = false;
                btnCancelar.Visible = false;
                return;
            }

            lblPreguntaConfirmacion.Visible = true;
            btnAceptar.Visible = true;
            btnCancelar.Visible = true;
        }

        public void ReportesPorFecha(DataTable dt, string fechaInicio, string fechaFin)
        {
            int cont = 0;
            decimal aux, suma = 0;
            foreach (DataRow row in dt.Rows)
            {
                suma += Convert.ToDecimal(dt.Rows[cont][3]);
                cont++;
            }

            aux = suma;

            lblRecaudado.Text = "MONTO TOTAL RECAUDADO EN EL PERIODO ESTABLECIDO: " + aux.ToString();

            if(aux == 0)
            {
                lblCategoriaMasVendida.Visible = false;
                lblProductoMasVendido.Visible = false;
                lblMejorCliente.Visible = false;
                lblRecaudado.Visible = false;
                return;
            }

            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            DataTable dataTableCategorias = categoriaNegocio.CargarCategorias();
            ArticuloNegocios articuloNegocios = new ArticuloNegocios();
            DataTable dataTableArticulos = articuloNegocios.CargarGvArticulos("Select * from Articulo");
            FacturaNegocio facturaNegocio = new FacturaNegocio();
            DataTable detalleFactura = facturaNegocio.CargarGrv("Select DetalleFacturas.Id_Factura, " +
                "NumOrden, Id_Articulo, PrecioUnitario, DescripcionArticulo, Cantidad FROM DetalleFacturas" +
                " inner join Facturas on DetalleFacturas.Id_Factura = Facturas.Id_Factura " +
                "WHERE Fecha_Venta between '" + fechaInicio + "' AND '" + fechaFin + "'");

            int cantCategorias = DevolverCantidad(dataTableCategorias);
            int cantArticulos = DevolverCantidad(dataTableArticulos);

            int[] categorias;
            categorias = new int[cantCategorias];

            int[] articulosVendidos;
            articulosVendidos = new int[cantArticulos];

            string consulta = "";

            foreach (DataRow dataRow in detalleFactura.Rows)
            {
                articulosVendidos[Convert.ToInt32(dataRow[2]) - Convert.ToInt32(dataTableArticulos.Rows[0][0])] += Convert.ToInt32(dataRow[5]);
                consulta = "Select Id_Cat From Articulo where Id = " + dataRow[2].ToString();
                DataTable aux2 = articuloNegocios.CargarGvArticulos(consulta);
                categorias[Convert.ToInt32(aux2.Rows[0][0]) - Convert.ToInt32(dataTableCategorias.Rows[0][0])] += Convert.ToInt32(dataRow[5]);
            }

            int mayor = 0, cant = 0;

            for (int i = 0; i < cantArticulos; i++)
            {
                if (cant < articulosVendidos[i] || i == 0)
                {
                    mayor = i + Convert.ToInt32(dataTableArticulos.Rows[0][0]);
                    cant = articulosVendidos[i];
                }
            }

            consulta = "Select * from Articulo where Id = " + mayor.ToString();
            dataTableArticulos = articuloNegocios.CargarGvArticulos(consulta);
            lblProductoMasVendido.Text = "EL ARTICULO MAS VENDIDO FUE: " + dataTableArticulos.Rows[0][2].ToString();

            for (int i = 0; i < cantCategorias; i++)
            {
                if(cant < categorias[i] || i == 0)
                {
                    mayor = i + Convert.ToInt32(dataTableCategorias.Rows[0][0]);
                    cant = categorias[i];
                }
            }

            for (int i = 0; i < cantCategorias; i++)
            {
                if (mayor == Convert.ToInt32(dataTableCategorias.Rows[i][0]))
                {
                    lblCategoriaMasVendida.Text = "LA CATEGORIA MAS VENDIDA FUE " + dataTableCategorias.Rows[i][1].ToString();
                }
            }

            UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
            string mejorCliente = MejorCliente(dt);
            DataTable usuariosDt = usuarioNegocio.CargarGrv("SELECT * FROM Usuario where DniUsuario = '" + mejorCliente + "'");
            lblMejorCliente.Text = "EL MEJOR CLIENTE FUE: " + usuariosDt.Rows[0][1].ToString() + " " + usuariosDt.Rows[0][2].ToString();
        }

        public string MejorCliente(DataTable dtFactura)
        {
            string mejorCliente = "";
            decimal mejorTotal = 0;
            
            foreach (DataRow dataRow in dtFactura.Rows)
            {
                string dni = dataRow[1].ToString();
                decimal total = 0;

                foreach (DataRow row in dtFactura.Rows)
                {
                    if (dataRow[1].ToString() == row[1].ToString())
                        total += Convert.ToDecimal(row[3]);
                }

                if (total > mejorTotal || mejorTotal == 0)
                {
                    mejorCliente = dni;
                    mejorTotal = total;
                }
            }

            return mejorCliente;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;
            this.Form.Attributes.Add("autocomplete", "off");

            if(Session["Nombre"] == null)
            {
                Response.Redirect("/Vistas/Home/Home.aspx");
            }

            if (ddlAdmin.SelectedIndex == 0)
            {
                EsconderControles();
                CargaGrv();
            }

            if (!IsPostBack)
            {
                CargarCategoriasDDl();
            }

            if (Session["Nombre"] != null)
            {
                lblNombreUsuario.Text = Session["Nombre"].ToString();
                btnCerrarSesion.Visible = true;
            }

           // btnCerrarSesion.Visible = false;
        }

        public void FiltrarPorFecha()
        {
            btnReporte.Visible = false;
            string fechaInicio = Calendar1.SelectedDate.ToString();
            string fechaFin = Calendar2.SelectedDate.ToString();
            FacturaNegocio facturaNegocio = new FacturaNegocio();
            string consulta = "Select Id_Factura, Dni_Usuario, Fecha_Venta, MontoFinal, " +
                "(NombreUsuario + '' + ApellidoUsuario) as [NombreCliente] From Facturas" +
                " inner join Usuario on Facturas.Dni_Usuario = Usuario.DniUsuario AND Fecha_Venta between " +
                "'" + fechaInicio + "' AND '" + fechaFin + "'";
            DataTable dt = facturaNegocio.CargarGrv(consulta);
            gvFacturas.DataSource = dt;
            gvFacturas.DataBind();
            MostrarFiltros();
            ReportesPorFecha(dt, fechaInicio, fechaFin);
            gvDetalleFactura.DataSource = null;
            gvDetalleFactura.DataBind();
        }

        public void MostrarFiltros()
        {
            btnFiltrar.Visible = true;
            btnQuitarFiltros.Visible = true;
            btnReporte.Visible = true;
        }
        public void CargarCategoriasDDl()
        {
            CategoriaNegocio categoria = new CategoriaNegocio();
            SqlDataReader data = categoria.CargarDdl();
            ddlCategoriasArticulo.DataSource = data;
            ddlCategoriasArticulo.DataTextField = "Descripcion";
            ddlCategoriasArticulo.DataValueField = "Id";
            ddlCategoriasArticulo.DataBind();
        }

        protected void gvCategorias_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCategorias.PageIndex = e.NewPageIndex;
            CargaGrv();
        }

        public void CargaGrv()
        {
            switch (ddlAdmin.SelectedIndex)
            {
                case 1:
                    GrvCategoriasSeleccionado();
                    CategoriaNegocio catNeg = new CategoriaNegocio();
                    gvCategorias.DataSource = catNeg.CargarCategorias();
                    gvCategorias.DataBind();
                    break;

                case 2:
                    GrvProductosSeleccionado();
                    if (Convert.ToBoolean(Session["FiltrarNombreProducto"]))
                    {
                        FiltrarNombreProducto();
                        return;
                    }
                    if (ddlCategorias.SelectedIndex != 0)
                    {
                        CargarGvArticulosCategorias();
                        return;
                    }
                    if (Convert.ToBoolean(Session["FiltrarNombreProducto"]) == false)
                    {
                        Session["FiltrarNombreProducto"] = false;
                        ArticuloNegocios articuloNegocios = new ArticuloNegocios();
                        string consulta = "Select * from Articulo inner join Categoria on Articulo.Id_Cat = Categoria.Id Where Estado = 1";
                        gvArticulos.DataSource = articuloNegocios.CargarGvArticulos(consulta);
                        gvArticulos.DataBind();
                    }
                    break;

                case 3:
                    FacturasSelect();

                    MostrarReportes();
                    
                    if(VerificarFiltros())
                    {
                        FiltrarPorFecha();
                        return;    
                    }

                    FacturaNegocio factura = new FacturaNegocio();
                    DataTable dt = factura.CargarGrv("Select Id_Factura, Dni_Usuario, Fecha_Venta, MontoFinal," +
                        " (NombreUsuario + '' + ApellidoUsuario) as [NombreCliente] From Facturas " +
                        "inner join Usuario on Facturas.Dni_Usuario = Usuario.DniUsuario");
                    MostrarFiltros();
                    ReportesTotales(dt);
                    gvFacturas.DataSource = dt;
                    gvFacturas.DataBind();
                    gvDetalleFactura.Visible = true;
                    break;

                case 4:
                    if (Convert.ToBoolean(Session["FiltrarNombre"]))
                    {
                        FiltrarNombre();
                        return;
                    }
                    usuariosSelec();
                    UsuarioNegocio aux = new UsuarioNegocio();
                    gvAdmin.DataSource = aux.CargarGrv("SELECT * FROM Usuario where Rol <> 2 AND  Estado = 1");
                    gvAdmin.DataBind();
                    break;
            }
        }

        public void ReportesTotales(DataTable dt)
        {
            int cont = 0;
            decimal aux, suma = 0;

            foreach (DataRow row in dt.Rows)
            {
                suma += Convert.ToDecimal(dt.Rows[cont][3]);
                cont++;
            }

            aux = suma;
            lblRecaudado.Text = "MONTO TOTAL RECAUDADO EN ESTE PERIODO: " + aux.ToString();

            CategoriaNegocio categoria = new CategoriaNegocio();
            DataTable dtCategoria = categoria.CargarCategorias();
            ArticuloNegocios articulo = new ArticuloNegocios();
            DataTable dtArticulo = articulo.CargarGvArticulos("Select * from Articulo");
            FacturaNegocio factura = new FacturaNegocio();
            DataTable dtFactura = factura.CargarGrv("Select * from DetalleFacturas");

            int cantCategorias = DevolverCantidad(dtCategoria);
            int cantArticulos = DevolverCantidad(dtArticulo);

            int[] categorias;
            categorias = new int[cantCategorias];

            int[] articulos;
            articulos = new int[cantArticulos];

            string consulta = "";

            foreach (DataRow dataRow in dtFactura.Rows)
            {
                articulos[Convert.ToInt32(dataRow[2]) - Convert.ToInt32(dtArticulo.Rows[0][0])] += Convert.ToInt32(dataRow[5]);
                consulta = "select Id_Cat from Articulo where Id =" + dataRow[2].ToString();
                DataTable aux2 = articulo.CargarGvArticulos(consulta);
                categorias[Convert.ToInt32(aux2.Rows[0][0]) - Convert.ToInt32(dtCategoria.Rows[0][0])] += Convert.ToInt32(dataRow[5]);
            }

            int mayor = 0, cant = 0;

            for (int i = 0; i < cantArticulos; i++)
            {
                if(cant < articulos[i] || i == 0)
                {
                    mayor = i + Convert.ToInt32(dtArticulo.Rows[0][0]);
                    cant = articulos[i];
                }
            }

            consulta = "select * from Articulo where Id = " + mayor.ToString();
            dtArticulo = articulo.CargarGvArticulos(consulta);
            lblProductoMasVendido.Text = "EL ARTICULO MAS VENDIDO FUE: " + dtArticulo.Rows[0][3].ToString();

            for (int i = 0; i < cantCategorias; i++)
            {
                if (cant < categorias[i] || i == 0)
                {
                    mayor = i + Convert.ToInt32(dtCategoria.Rows[0][0]);
                    cant = categorias[i];
                }
            }

            for (int i = 0; i < cantCategorias; i++)
            {
                if (mayor == Convert.ToInt32(dtCategoria.Rows[i][0]))
                {
                    lblCategoriaMasVendida.Text = "LA CATEGORIA MAS VENDIDA FUE: " + dtCategoria.Rows[i][1].ToString();
                }
            }

            FacturaNegocio facturaNegocio = new FacturaNegocio();
            UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
            string mejorCliente = MejorCliente(facturaNegocio.CargarGrv("Select * from Facturas"));
            DataTable dtUsuarios = usuarioNegocio.CargarGrv("Select * from Usuario Where DniUsuario = " + mejorCliente);
            lblMejorCliente.Text = "EL MEJOR CLIENTE FUE: " + dtUsuarios.Rows[0][5].ToString() + " " + dtUsuarios.Rows[0][6].ToString();
        }

        public void FacturasSelect()
        {
            lblDesde.Visible = true;
            lblHasta.Visible = true;
            Calendar1.Visible = true;
            Calendar2.Visible = true;
            gvAdmin.Visible = false;
            gvCategorias.Visible = false;
            gvFacturas.Visible = true;
            btnFiltrar.Visible = true;
            btnQuitarFiltros.Visible = true;
        }

        public bool VerificarFiltros()
        {
            if (Convert.ToBoolean(Session["FiltrarFecha"]))
            {
                return true;
            }

            return false;
        }

        public void MostrarReportes()
        {
            if (Convert.ToBoolean(Session["MostrarReportes"]))
            {
                lblRecaudado.Visible = true;
                lblProductoMasVendido.Visible = true;
                lblCategoriaMasVendida.Visible = true;
                lblMejorCliente.Visible = true;
            }
            else
            {
                lblRecaudado.Visible = false;
                lblProductoMasVendido.Visible = false;
                lblCategoriaMasVendida.Visible = false;
                lblMejorCliente.Visible = false;
            }
        }

        public void EsconderControles()
        {
            if (ddlAdmin.SelectedValue != "Categoria")
            {
                EsconderCategoriasControles();
            }

            if (ddlAdmin.SelectedValue != "Productos")
            {
                EsconderProductosControles();
            }

            if(ddlAdmin.SelectedValue != "Facturas")
            {
                EsconderControlesFacturas();
            }

            if (ddlAdmin.SelectedValue != "Usuarios")
            {
                txtNombreUsuarioAdmin.Visible = false;
                btnFiltrarNombreUsuarioAdmin.Visible = false;
                btnQuitarFiltrosAdmin.Visible = false;
            }
        }

        private void EsconderControlesFacturas()
        {
            lblDesde.Visible = false;
            lblHasta.Visible = false;
            Calendar1.Visible = false;
            Calendar2.Visible = false;
            lblCategoriaMasVendida.Visible = false;
            lblProductoMasVendido.Visible = false;
            btnFiltrar.Visible = false;
            btnQuitarFiltros.Visible = false;
            lblRecaudado.Visible = false;
            lblProductoMasVendido.Visible = false;
            lblCategoriaMasVendida.Visible = false;
            lblCategoriaMasVendida.Visible = false;
            btnReporte.Visible = false;
            gvFacturas.Visible = false;
            lblProductoMasVendido.Visible = false;
            lblCategoriaMasVendida.Visible = false;
            Session["MostrarReportes"] = false;
            Session["FiltrarFecha"] = false;
        }

        private void EsconderCategoriasControles()
        {
            gvCategorias.Visible = false;
            txtDescripcionCategoria.Visible = false;
            btnAgregarCategoria.Visible = false;
        }

        private void EsconderProductosControles()
        {
            txtDescripcionArticulo.Visible = false;
            txtPrecioUnitario.Visible = false;
            txtStock.Visible = false;
            txtUrlImagen.Visible = false;
            ddlCategorias.Visible = false;
            btnAgregarArticulo.Visible = false;
            txtNombreProducto.Visible = false;
            btnFiltrarNombreProducto.Visible = false;
            ddlCategoriasArticulo.Visible = false;
            btnQuitarFiltrosProducto.Visible = false;
            gvArticulos.Visible = false;
        }

        public void GrvCategoriasSeleccionado()
        {
            gvArticulos.Visible = false;
            gvAdmin.Visible = false;
            gvCategorias.Visible = true;
            txtDescripcionCategoria.Visible = true;
            btnAgregarCategoria.Visible = true;
        }

        public void GrvProductosSeleccionado()
        {
            gvAdmin.Visible = false;
            txtDescripcionArticulo.Visible = true;
            txtStock.Visible = true;
            txtPrecioUnitario.Visible = true;
            txtUrlImagen.Visible = true;
            ddlCategoriasArticulo.Visible = true;
            btnAgregarArticulo.Visible = true;
            gvArticulos.Visible = true;
            txtNombreProducto.Visible = true;
            btnFiltrarNombreProducto.Visible = true;
            ddlCategorias.Visible = true;
            btnQuitarFiltrosProducto.Visible = true;
        }

        public void usuariosSelec()
        {
            gvCategorias.Visible = false;
            gvArticulos.Visible = false;
            txtNombreUsuarioAdmin.Visible = true;
            gvAdmin.Visible = true;
            btnQuitarFiltrosAdmin.Visible = true;
            btnFiltrarNombreUsuarioAdmin.Visible = true;
        }

        public void FiltrarNombreProducto()
        {
            string nombre = txtNombreProducto.Text;
            string consulta = "Select * from Articulo inner join Categoria On Articulo.Id_Cat = Categoria.Id where Estado = 1 And Descripcion LIKE '%" + nombre + "%'";
            ArticuloNegocios articulo = new ArticuloNegocios();
            gvArticulos.DataSource = articulo.CargarGvArticulos(consulta);
            gvArticulos.DataBind();
        }

        // categorias

        protected void gvCategorias_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            MostrarPreguntaConfirmacion();
            int id = Convert.ToInt32(((Label)gvCategorias.Rows[e.RowIndex].FindControl("lblIdCategoria")).Text);
            Session["IdBorrar"] = id;
        }

        protected void gvCategorias_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvCategorias.EditIndex = e.NewEditIndex;

        }

        protected void gvCategorias_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string id = ((Label)gvCategorias.Rows[e.RowIndex].FindControl("lbIdCatEDIT")).Text;
            string nuevaDescripcion = ((TextBox)gvCategorias.Rows[e.RowIndex].FindControl("txtEditDescArticulo")).Text;
            lblAgregar.Text = nuevaDescripcion;

            if (nuevaDescripcion == "")
            {
                lblAgregar.ForeColor = Color.Red;
                lblAgregar.Text = "DEBE COMPLETAR LA DESCRIPCIN PARA QE LOS CAMBIOS SE GUARDEN";
                return;
            }

            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();

            if (categoriaNegocio.EditarCategoria(id, nuevaDescripcion))
            {
                lblAgregar.Text = "";
                gvCategorias.EditIndex = -1;
            }
            CargaGrv();
        }

        protected void btnAgregarCategoria_Click(object sender, EventArgs e)
        {
            string descripcion = txtDescripcionCategoria.Text;
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            if (categoriaNegocio.AgrearCategoria(descripcion))
            {
                txtDescripcionCategoria.Text = "";
            }

            CargaGrv();
        }

        protected void gvCategorias_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvCategorias.EditIndex = -1;
            CargaGrv();
        }

        public void BorrarRegistro()
        {
            int id = Convert.ToInt32(Session["IdBorrar"]);

            switch (ddlAdmin.SelectedIndex)
            {
                case 1:
                    CategoriaNegocio categoria = new CategoriaNegocio();
                    if (categoria.VerificarUsoCategoria(id))
                    {
                        lblAgregar.ForeColor = Color.Red;
                        lblAgregar.Text = "NO SE PUEDE BORRAR YA QUE ESTA SIENDO UTILIZADA POR UN PRODUCTO";
                        return;
                    }

                    if (categoria.BorrarCategoria(id))
                    {
                        CargaGrv();
                    }
                    break;
                case 2:
                    ArticuloNegocios articuloNegocios = new ArticuloNegocios();
                    if (articuloNegocios.VerificarUsoArticulo(id))
                    {
                        if (articuloNegocios.BajaLogica(id))
                        {
                            lblAgregar.ForeColor = Color.Green;
                            lblAgregar.Text = "EL PRODUCTO HA SIDO DE BAJA LOGICA EXITOSAMENTE";
                            CargaGrv();
                            return;
                        }
                    }
                    if (articuloNegocios.BorrarArticulo(id))
                    {
                        lblAgregar.ForeColor = Color.Green;
                        lblAgregar.Text = "EL PRODUCTO SE HA ELIMINADO EXITOSAMENTE DE LA BASE DE DATOS";
                        CargaGrv();
                    }
                    break;
            }


        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            BorrarRegistro();
            MostrarPreguntaConfirmacion();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            MostrarPreguntaConfirmacion();
            lblAgregar.Text = "CANCELADA LA ELIMINACION";
        }

        protected void btnFiltrarNombreProducto_Click(object sender, EventArgs e)
        {
            Session["FiltrarNombreProducto"] = true;
            CargaGrv();
        }

        protected void btnQuitarFiltrosProducto_Click(object sender, EventArgs e)
        {
            Session["FiltrarNombreProducto"] = false;
            txtNombreProducto.Text = "";
            ddlCategorias.SelectedIndex = 0;
            CargaGrv();
        }

        protected void ddlCategorias_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarGvArticulosCategorias();
        }

        public void CargarGvArticulosCategorias()
        {
            ArticuloNegocios articulo = new ArticuloNegocios();
            gvArticulos.DataSource = articulo.CargarGvArticulos("Select * from Articulo inner join Categoria on Articulo.Id_Cat = Categoria.Id WHERE Estado = 1 AND Descripcion = '" + ddlCategorias.SelectedItem.ToString() + "'");
            gvArticulos.DataBind();
        }

        protected void ddlAdmin_SelectedIndexChanged(object sender, EventArgs e)
        {
            EsconderControles();
            if (ddlAdmin.SelectedIndex == 2)
            {
                cargarCategoriasDDLproductos();
            }

            if (ddlAdmin.SelectedIndex != 3)
                 QuitarFiltros();

                CargaGrv();
        }

        public void QuitarFiltros()
        {
            Session["MostrarReportes"] = false;
            btnFiltrar.Visible = false;
            FacturaNegocio facturaNegocio = new FacturaNegocio();
            Session["FiltrarFecha"] = false;
            gvDetalleFactura.DataSource = null;
            gvDetalleFactura.DataBind();
            gvFacturas.SelectedIndex = -1;
            Calendar1.SelectedDates.Clear();
            Calendar2.SelectedDates.Clear();
            lblMejorCliente.Visible = false;
        }

        public void cargarCategoriasDDLproductos()
        {
            CategoriaNegocio categoria = new CategoriaNegocio();
            SqlDataReader data = categoria.CargarDdl();
            ddlCategorias.DataSource = data;
            ddlCategorias.DataTextField = "Descripcion";
            ddlCategorias.DataValueField = "Id";
            ddlCategorias.DataBind();
        }

        protected void gvArticulos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvArticulos.PageIndex = e.NewPageIndex;
            CargaGrv();
        }

        protected void gvArticulos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvArticulos.EditIndex = -1;
            CargaGrv();
        }

        protected void gvArticulos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(((Label)gvArticulos.Rows[e.RowIndex].FindControl("lblIdArticuloGRVArticulos")).Text);
            Session["IdBorrar"] = id;
            MostrarPreguntaConfirmacion();
        }

        protected void gvArticulos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvArticulos.EditIndex = e.NewEditIndex;
            CargaGrv();
        }

        protected void gvArticulos_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            if (!ValidarCamposProducto(e))
            {
                return;
            }

            lblAgregar.ForeColor = Color.Green;
            lblAgregar.Text = "REGISTRO MODIFICADO";

            ArticulosEntidad articulos = new ArticulosEntidad();
            CargarArticuloEditado(ref articulos, e);
            ArticuloNegocios articuloNegocios = new ArticuloNegocios();

            if (articuloNegocios.EditarArticulo(articulos))
            {
                gvArticulos.EditIndex = -1;
                CargaGrv();
            }
        }

        public bool ValidarCamposProducto(GridViewUpdateEventArgs e)
        {
            bool estado = true;

            lblAgregar.Text = "";

            if (((TextBox)gvArticulos.Rows[e.RowIndex].FindControl("txtEditNombArtGrvArt")).Text == "")
            {
                lblAgregar.ForeColor = Color.Red;
                lblAgregar.Text = "COMPLETAR DESCRIPCION, NO PUEDE QUEDAR VACIO \n";
                estado = false;
            }

            if (((TextBox)gvArticulos.Rows[e.RowIndex].FindControl("txtEditPrecioUniGrvArt")).Text == "")
            {
                lblAgregar.ForeColor = Color.Red;
                lblAgregar.Text += "COMPLETAR PRECIO UNITARIO, NO PUEDE QUEDAR VACIO \n";
                estado = false;
            }

            if (((TextBox)gvArticulos.Rows[e.RowIndex].FindControl("txtStockArtgrvArt")).Text == "")
            {
                lblAgregar.ForeColor = Color.Red;
                lblAgregar.Text += "COMPLETAR STOCK, NO PUEDE QUEDAR VACIO";
                estado = false;
            }

            return estado;
        }

        public void CargarArticuloEditado(ref ArticulosEntidad articulos, GridViewUpdateEventArgs e)
        {
            articulos.IdArticulo = Convert.ToInt32(((Label)gvArticulos.Rows[e.RowIndex].FindControl("lblEditIdArtGrvArt")).Text);
            articulos.IdCategoria = Convert.ToInt32(((DropDownList)gvArticulos.Rows[e.RowIndex].FindControl("ddlETcategoriasGrvArticulos")).SelectedValue);
            articulos.DescripcionArticulo1 = ((TextBox)gvArticulos.Rows[e.RowIndex].FindControl("txtEditNombArtGrvArt")).Text;
            articulos.PrecioUnitarioArticulo = float.Parse(((TextBox)gvArticulos.Rows[e.RowIndex].FindControl("txtEditPrecioUniGrvArt")).Text);
            articulos.StockDisponibleArticulo = Convert.ToInt32(((TextBox)gvArticulos.Rows[e.RowIndex].FindControl("txtStockArtgrvArt")).Text);
        }

        protected void btnAgregarArticulo_Click(object sender, EventArgs e)
        {
            ArticuloNegocios articulo = new ArticuloNegocios();
            ArticulosEntidad articulos = new ArticulosEntidad();
            CargarArticuloNuevo(ref articulos);
            if (articulo.AgregarArticulo(articulos))
            {
                txtDescripcionArticulo.Text = "";
                txtPrecioUnitario.Text = "";
                txtStock.Text = "";
                txtUrlImagen.Text = "";
                CargaGrv();
            }
        }

        public void CargarArticuloNuevo(ref ArticulosEntidad articulos)
        {
            articulos.IdCategoria = Convert.ToInt32(ddlCategoriasArticulo.SelectedValue);
            articulos.PrecioUnitarioArticulo = float.Parse(txtPrecioUnitario.Text);
            articulos.StockDisponibleArticulo = Convert.ToInt32(txtStock.Text);
            articulos.Url_articulo_img = txtUrlImagen.Text;
            articulos.DescripcionArticulo1 = txtDescripcionArticulo.Text;
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvAdmin.PageIndex = e.NewPageIndex;
            CargaGrv();
        }

        public void FiltrarNombre()
        {
            usuariosSelec();
            UsuarioNegocio aux = new UsuarioNegocio();
            string consulta = "Select * from Usuario where Rol <> 1 AND Estado = 1 AND (NombreUsuario like  '% " + txtNombreUsuarioAdmin.Text + " %' or ApellidoUsuario like '% " + txtNombreUsuarioAdmin.Text + "%')";
            gvAdmin.DataSource = aux.CargarGrv(consulta);
            gvAdmin.DataBind();
        }

        protected void gvAdmin_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvAdmin.EditIndex = -1;
            CargaGrv();
        }

        protected void gvAdmin_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvAdmin.EditIndex = e.NewEditIndex;
            CargaGrv();
        }

        protected void gvAdmin_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string id = ((Label)gvAdmin.Rows[e.RowIndex].FindControl("lblDniEditGrvAdmin")).Text;
            bool cb = ((CheckBox)gvAdmin.Rows[e.RowIndex].FindControl("cbRolUsuarioAdmin")).Checked;
            UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
            usuarioNegocio.EditarRolUsuario(id, cb);
            gvAdmin.EditIndex = -1;
            CargaGrv();
        }

        protected void btnFiltrarNombreUsuarioAdmin_Click(object sender, EventArgs e)
        {
            Session["FiltrarNombre"] = true;
            CargaGrv();
        }

        protected void btnQuitarFiltrosAdmin_Click(object sender, EventArgs e)
        {
            txtNombreUsuarioAdmin.Text = "";
            Session["FiltrarNombre"] = false;
            CargaGrv();
        }

        protected void btnCerrarSesion_Click1(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            btnCerrarSesion.Visible = false;
            Server.Transfer("/Vistas/Home/Home.aspx");
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            if(Calendar1.SelectedDate > Calendar2.SelectedDate)
            {
                lblComentario.Visible = true;
                lblComentario.Text = "LA FECHA DE INICIO DEBE SER MENOR A LA FECHA FINAL";
                return;
            }
            FiltrarPorFecha();
            Session["FiltrarFecha"] = true;
            btnFiltrar.Visible = false;

        }

        protected void btnQuitarFiltros_Click(object sender, EventArgs e)
        {
            Session["MostrarReportes"] = false;
            btnFiltrar.Visible = false;
            FacturaNegocio facturas = new FacturaNegocio();
            Session["FiltrarFecha"] = false;
            gvDetalleFactura.DataSource = null;
            gvDetalleFactura.DataBind();
            gvFacturas.SelectedIndex = -1;
            Calendar1.SelectedDates.Clear();
            Calendar2.SelectedDates.Clear();
            MostrarFiltros();
            CargaGrv();
        }

        protected void btnReporte_Click(object sender, EventArgs e)
        {
            MostrarFiltros();
            Session["MostrarReportes"] = true;
            CargaGrv();
        }

        protected void gvFacturas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvFacturas.PageIndex = e.NewPageIndex;
            CargaGrv();
        }

        protected void gvFacturas_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            FacturaNegocio facturaNegocio = new FacturaNegocio();
            string id = ((Label)gvFacturas.Rows[e.NewSelectedIndex].FindControl("lblIdFactura")).Text;
            gvDetalleFactura.DataSource = facturaNegocio.CargarGrv("Select * from DetalleFacturas where Id_Factura = '" + id + "'");
            gvDetalleFactura.DataBind();
            MostrarFiltros();
        }
    }
}