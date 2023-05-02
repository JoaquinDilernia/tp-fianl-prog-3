<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Prototipo.Vistas.Home.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        .auto-style1 {
            text-align: center;
        }
        .auto-style2 {
            text-align: center;
            width: 297px;
        }
        .auto-style3 {
            width: 226px;
        }
        .auto-style4 {
            text-align: right;
        }
        .auto-style5 {
            color: #66FF66;
            font-size: x-large;
        }
        .auto-style6 {
            font-size: x-large;
        }
        .auto-style7 {
            font-size: large;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="auto-style4">

            <asp:Label ID="lblBienvenido" runat="server" CssClass="auto-style5" Text="BIENVENIDO: " Visible="False"></asp:Label>
&nbsp;&nbsp;&nbsp;

            <asp:Label ID="lblNombreUsuario" runat="server" CssClass="auto-style6"></asp:Label>
            <asp:Button ID="btnCerrarSesion" runat="server" OnClick="btnCerrarSesion_Click" Text="Cerrar Sesion" Visible="False" CssClass="auto-style7" />
            <br />
            <asp:Label ID="lblMensaje" runat="server"></asp:Label>
            <br />

        </div>
        <div class="auto-style1">
            <asp:HyperLink ID="HOME" runat="server" NavigateUrl="~/Vistas/Home/Home.aspx">HOME</asp:HyperLink>
&nbsp;&nbsp;&nbsp;
            <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/Vistas/Carrito/Carrito.aspx">CARRITO</asp:HyperLink>
&nbsp;&nbsp;&nbsp;
            <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/Vistas/Mi Cuenta/MiCuenta.aspx">MI CUENTA</asp:HyperLink>
&nbsp;&nbsp;&nbsp;
            <asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="~/Vistas/Login/InicioSesion.aspx">LOGIN</asp:HyperLink>
&nbsp;&nbsp;&nbsp;
            <asp:HyperLink ID="HyperLink6" runat="server" NavigateUrl="~/Vistas/Login/Registro.aspx">REGISTRARSE</asp:HyperLink>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <br />
            <br />
            <br />
        </div>
        <div class="auto-style1">
              <asp:TextBox ID="txtNombreArticulo" runat="server"></asp:TextBox>
&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnBuscarNombre" runat="server" Text="Buscar Nombre" OnClick="btnBuscarNombre_Click" />
&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="txtPrecioMinimo" runat="server"></asp:TextBox>
&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="txtPrecioMaxim" runat="server"></asp:TextBox>
&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnFiltrarPorPrecio" runat="server" Text="Filtrar" OnClick="btnFiltrarPorPrecio_Click" />
&nbsp;&nbsp;&nbsp;
            <asp:DropDownList ID="ddlCategorias" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCategorias_SelectedIndexChanged">
                <asp:ListItem>--Seleccione Categoria--</asp:ListItem>
            </asp:DropDownList>
&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnQuitarFiltros" runat="server" Text="Quitar Filtros" OnClick="btnQuitarFiltros_Click" />
              <br />
              <br />
              <asp:Label ID="lblArticuloCarrito" runat="server" CssClass="auto-style7"></asp:Label>
              <br />
            <asp:Label ID="lblPreguntaConfirmacion" runat="server" CssClass="auto-style7"></asp:Label>
        </div>
        </div>
        <div>
            <br />
            <br />
            <asp:ListView ID="lvArticulos" runat="server" GroupItemCount="3" OnPagePropertiesChanging="lvArticulos_PagePropertiesChanging">
                <EditItemTemplate>
                    <td runat="server" style="background-color:#008A8C;color: #FFFFFF;">
                        <asp:Label ID="lblArticulo" runat="server" Text='<%# Bind("Id") %>'></asp:Label>
                        <br />
                        <asp:TextBox ID="txtPrecioUnitario" runat="server" Text='<%# Bind("PrecioUnitario") %>'></asp:TextBox>
                        <br />
                        <asp:TextBox ID="txtNombreArticulo" runat="server" Text='<%# Bind("Nombre_Art") %>'></asp:TextBox>
                        <br />
                        <asp:CheckBox ID="cbEstado" runat="server" Checked='<%# Bind("Estado") %>' />
                        <br />
                        <asp:TextBox ID="txtImageUrl" runat="server" Text='<%# Bind("Url_Imagen") %>'></asp:TextBox>
                        <br />
                        <asp:TextBox ID="txtStock" runat="server" Text='<%# Bind("StockArticulo") %>'></asp:TextBox>
                        <br />
                        <asp:Button ID="UpdateButton" runat="server" CommandName="Update" Text="Actualizar" />
                        <br />
                        <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Cancelar" />
                        <br /></td>
                </EditItemTemplate>
                <EmptyDataTemplate>
                    <table runat="server" style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;">
                        <tr>
                            <td>No se han devuelto datos.</td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <EmptyItemTemplate>
                    <td runat="server" />
                </EmptyItemTemplate>
                <GroupTemplate>
                    <tr id="itemPlaceholderContainer" runat="server">
                        <td id="itemPlaceholder" runat="server"></td>
                    </tr>
                </GroupTemplate>
                <InsertItemTemplate>
                    <td runat="server" style="">
                        <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("Id_Cat") %>'></asp:TextBox>
                        <br />
                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("PrecioUnitario") %>'></asp:TextBox>
                        <br />
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Nombre_Art") %>'></asp:TextBox>
                        <br />
                        <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("Estado") %>'></asp:TextBox>
                        <br />
                        <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("Url_Imagen") %>'></asp:TextBox>
                        <br />
                        <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("StockArticulo") %>'></asp:TextBox>
                        <br />&nbsp;<br />
                        <asp:Button ID="InsertButton" runat="server" CommandName="Insert" Text="Insertar" ValidationGroup="Insert" />
                        <br />
                        <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Borrar" />
                        <br /></td>
                </InsertItemTemplate>
                <ItemTemplate>
                    <td runat="server" style="background-color:#DCDCDC;color: #000000;" class="auto-style2">
                        <asp:Label ID="lblNombreArt" runat="server" Text='<%# Bind("Nombre_Art") %>'></asp:Label>
                        <br />
                        Precio:
                        <asp:Label ID="lblPrecioUnitario" runat="server" Text='<%# Bind("PrecioUnitario") %>'></asp:Label>
                        <br />
                        Stock:
                        <asp:Label ID="lblStockArticulo" runat="server" Text='<%# Bind("StockArticulo") %>'></asp:Label>
                        <br />
                        <br />
                        <br />
                        <asp:ImageButton ID="imgbImagenArticulo" runat="server" Height="150px" ImageUrl='<%# Bind("Url_Imagen") %>' Width="150px" Enabled="False" />
                        <br />
                        <br />
                        <br />
                        <br />
                        <asp:Button ID="btnSeleccionar" runat="server" CommandName="Seleccionar" OnCommand="btnSeleccionar_Command" Text="Seleleccionar"  CommandArgument='<%# Eval("Id")+"-"+Eval("Nombre_Art")+"-"+Eval("PrecioUnitario")+"-"+"" %>'/>
                        <br />
                        <br />
                    </td>
                </ItemTemplate>
                <LayoutTemplate>
                    <table runat="server">
                        <tr runat="server">
                            <td runat="server">
                                <table id="groupPlaceholderContainer" runat="server" border="1" style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;font-family: Verdana, Arial, Helvetica, sans-serif;">
                                    <tr id="groupPlaceholder" runat="server">
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr runat="server">
                            <td runat="server" style="text-align: center;background-color: #CCCCCC;font-family: Verdana, Arial, Helvetica, sans-serif;color: #000000;">
                                <asp:DataPager ID="DataPager1" runat="server" PageSize="9">
                                    <Fields>
                                        <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowLastPageButton="True" />    
                                    </Fields>
                                </asp:DataPager>
                            </td>
                        </tr>
                    </table>
                </LayoutTemplate>
                <SelectedItemTemplate>
                    <td runat="server" style="background-color:#008A8C;font-weight: bold;color: #FFFFFF;" class="auto-style3">
                        <asp:Label ID="lblNombre" runat="server" Text='<%# Bind("Nombre_Art") %>'></asp:Label>
                        <br />
                        <asp:Label ID="lblPrecioUnitario" runat="server" Text='<%# Bind("PrecioUnitario") %>'></asp:Label>
                        <br />
                        <asp:Label ID="lblStockArticulo" runat="server" Text='<%# Bind("StockArticulo") %>'></asp:Label>
                        <br />
                        <asp:Label ID="lblUrlImagen" runat="server" Text='<%# Bind("Url_Imagen") %>'></asp:Label>
                        <br />
                        <asp:Label ID="lblIdCategoria" runat="server" Text='<%# Bind("Id_Cat") %>'></asp:Label>
                        <br />
                        <asp:Label ID="lblId" runat="server" Text='<%# Bind("Id") %>'></asp:Label>
                        <br />
                        <asp:CheckBox ID="cbEstado" runat="server" Checked='<%# Bind("Estado") %>' />
                    </td>
                </SelectedItemTemplate>
            </asp:ListView>
            <br />
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Prog3_IntegradorConnectionString %>" SelectCommand="SELECT * FROM [Articulo]"></asp:SqlDataSource>
        </div>
        <div class="auto-style1">
        </div>
    </form>
</body>
</html>
