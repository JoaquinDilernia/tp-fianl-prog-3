<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MiCuenta.aspx.cs" Inherits="Prototipo.Vistas.Mi_Cuenta.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        .auto-style1 {
            text-align: center;
        }
        .auto-style3 {
            width: 100%;
        }
        .auto-style4 {
            width: 466px;
            text-align: right;
        }
        .auto-style5 {
            width: 466px;
            text-align: right;
            height: 23px;
        }
        .auto-style6 {
            height: 23px;
        }
        .auto-style7 {
            height: 352px;
            text-align: center;
            width: 414px;
            margin-left: 319px;
        }
        .auto-style8 {
            margin-left: 438px;
        }
        .auto-style9 {
            height: 143px;
            margin-top: 56px;
        }
        .auto-style10 {
            text-align: right;
        }
        .auto-style11 {
            text-decoration: underline;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="auto-style1">
            <div class="auto-style10">

                <asp:Label ID="lblNombreUsuario" runat="server" Visible="False"></asp:Label>
&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnCerrarSesion" runat="server" OnClick="btnCerrarSesion_Click" Text="Cerrar Sesion" Visible="False" />
                <br />

            </div>
            <div>
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Vistas/Home/Home.aspx">HOME</asp:HyperLink>
&nbsp;&nbsp;&nbsp;
                <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Vistas/Carrito/Carrito.aspx">CARRITO</asp:HyperLink>
&nbsp;&nbsp;&nbsp;
                <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/Vistas/Mi Cuenta/MiCuenta.aspx">MI CUENTA</asp:HyperLink>
&nbsp;&nbsp;&nbsp;
                <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/Vistas/Login/InicioSesion.aspx">LOGIN</asp:HyperLink>
&nbsp;&nbsp;&nbsp;
                <asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="~/Vistas/Login/Registro.aspx">REGISTRARSE</asp:HyperLink>
            </div>
            <br />
            <asp:Label ID="lbAvisoUsuario" runat="server" Text="DEBE INICIAR SESION PARA PODER ACCEDER A ESTA INFORMACION" CssClass="auto-style11" ForeColor="Red"></asp:Label>
            <br />
            <br />
            <asp:Button ID="btnAdministrar" runat="server" Text="Administrar" OnClick="Page_Load" PostBackUrl="~/Vistas/Administrador/Administrador.aspx" Visible="False"/>
            <br />
            <br />
            <asp:Label ID="lbValidacion" runat="server"></asp:Label>
            <br />
        </div>
        <table class="auto-style3">
            <tr>
                <td class="auto-style5">Nombre:</td>
                <td class="auto-style6">
                    <asp:TextBox ID="txtNombre" runat="server" Width="235px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style4">Apellido:</td>
                <td>
                    <asp:TextBox ID="txtApellido" runat="server" Width="237px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style5">Dni:</td>
                <td class="auto-style6">
                    <asp:Label ID="lblDni" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style4">Direccion:</td>
                <td>
                    <asp:TextBox ID="txtDireccion" runat="server" Width="242px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style4">Email:</td>
                <td>
                    <asp:TextBox ID="txtMail" runat="server" Width="241px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style4">Contraseña:</td>
                <td>
                    <asp:TextBox ID="txtContrasenia" runat="server" Width="239px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style4">&nbsp;</td>
                <td>
                    <asp:Button ID="btnModificar" runat="server" OnClick="btnModificar_Click" Text="Modificar" Width="98px" />
                </td>
            </tr>
        </table>
        <div class="auto-style7">
            <br />
            <asp:GridView ID="gvFacturas" runat="server" AllowPaging="True" AutoGenerateColumns="False" AutoGenerateSelectButton="True" BorderStyle="Solid" BorderWidth="4px" CellPadding="4" OnPageIndexChanging="gvFacturas_PageIndexChanging" OnSelectedIndexChanging="gvFacturas_SelectedIndexChanging">
                <Columns>
                    <asp:TemplateField HeaderText="Numero de Factura">
                        <ItemTemplate>
                            <asp:Label ID="lblIdFactura" runat="server" Text='<%# Bind("Id_Factura") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Fecha de Venta">
                        <ItemTemplate>
                            <asp:Label ID="lblFechaVenta" runat="server" Text='<%# Bind("Fecha_Venta") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Total">
                        <ItemTemplate>
                            <asp:Label ID="lblMontoFinal" runat="server" Text='<%# Bind("MontoFinal") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <br />
        </div>
        <div class="auto-style9">
            <br />
            <br />
            <div class="auto-style1">
            <asp:GridView ID="gvDetalleFactura" runat="server" CssClass="auto-style8">
            </asp:GridView>
            </div>
            <br />
            <br />
            <br />
        </div>
    </form>
</body>
</html>
