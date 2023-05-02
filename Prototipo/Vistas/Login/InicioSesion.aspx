<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InicioSesion.aspx.cs" Inherits="Prototipo.WebForm3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            width: 433px;
            text-align: right;
        }
        .auto-style3 {
            width: 434px;
            text-align: right;
        }
        .auto-style4 {
            width: 100%;
            height: 8px;
        }
        .auto-style5 {
            width: 260px;
            text-align: center;
        }
        .auto-style6 {
            text-align: center;
        }
        .auto-style7 {
            text-align: center;
            width: 416px;
        }
        .auto-style8 {
            text-align: left;
        }
        .auto-style9 {
            text-align: right;
        }
        .auto-style10 {
            font-size: large;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="auto-style6">
            <div class="auto-style6">
&nbsp;<div class="auto-style9">
                    <asp:Label ID="lblNombreUsuario" runat="server" Visible="False"></asp:Label>
                    <asp:Button ID="btnCerrarSesion" runat="server" OnClick="btnCerrarSesion_Click" Text="Cerrar Sesion" />
                </div>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <br />
&nbsp;<asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="~/Vistas/Home/Home.aspx">HOME</asp:LinkButton>
&nbsp;&nbsp;&nbsp;
                <asp:LinkButton ID="LinkButton2" runat="server" PostBackUrl="~/Vistas/Carrito/Carrito.aspx">CARRITO</asp:LinkButton>
&nbsp;&nbsp;&nbsp;
                <asp:LinkButton ID="LinkButton5" runat="server" PostBackUrl="~/Vistas/Mi Cuenta/MiCuenta.aspx">MI CUENTA</asp:LinkButton>
&nbsp;&nbsp;&nbsp;
                <asp:LinkButton ID="LinkButton3" runat="server" PostBackUrl="~/Vistas/Login/InicioSesion.aspx">LOGIN</asp:LinkButton>
&nbsp;&nbsp;&nbsp;
                <asp:LinkButton ID="LinkButton4" runat="server" PostBackUrl="~/Vistas/Login/Registro.aspx">REGISTRARSE</asp:LinkButton>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <br />
                <br />
            </div>
            <table class="auto-style1">
                <tr>
                    <td class="auto-style2"><strong>Email:</strong></td>
                    <td class="auto-style8">
                        <asp:TextBox ID="txtMailLogin" runat="server" Width="177px"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <div>
                <table class="auto-style4">
                    <tr>
                        <td class="auto-style3"><strong>Contraseña:</strong></td>
                        <td class="auto-style8">
                            <asp:TextBox ID="txtContraLogin" runat="server" Width="174px" TextMode="Password"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div>
            <table class="auto-style1">
                <tr>
                    <td class="auto-style5">
                        <asp:Button ID="btnLogin" runat="server" Text="Login" Width="129px" OnClick="btnLogin_Click" />
                    </td>
                    <td class="auto-style7">
                        <asp:Button ID="btnRegistro" runat="server" Text="Registrarse" Width="129px" OnClick="btnRegistro_Click" />
                    </td>
                    <td class="auto-style6">
                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" Width="129px" OnClick="btnCancelar_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="auto-style6">
            <br />
            <br />
            <asp:Label ID="lbMensaje" runat="server" Text="[MENSAJE ACLARATORIO]" Visible="False" CssClass="auto-style10" ForeColor="Black"></asp:Label>
            <br />
            <asp:Label ID="lbMensaje2" runat="server" Text="[MENSAJE ACLARATORIO 2]" Visible="False" CssClass="auto-style10" ForeColor="Black"></asp:Label>
            <br />
            <br />
        </div>
    </form>
</body>
</html>
