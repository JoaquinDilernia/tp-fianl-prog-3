<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="Prototipo.Vistas.Login.WebForm1" %>

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
            margin-bottom: 0px;
        }
        .auto-style3 {
            width: 100%;
        }
        .auto-style4 {
            text-align: right;
            width: 386px;
        }
        .auto-style6 {
            width: 386px;
            height: 21px;
            text-align: right;
        }
        .auto-style7 {
            height: 21px;
            text-align: left;
        }
        .auto-style8 {
            text-align: left;
        }
        .auto-style9 {
            color: #FF0000;
        }
        .auto-style10 {
            text-align: right;
        }
        .auto-style11 {
            text-align: right;
            width: 386px;
            height: 32px;
        }
        .auto-style12 {
            text-align: left;
            height: 32px;
        }
        .auto-style13 {
            text-align: right;
            width: 386px;
            height: 30px;
        }
        .auto-style14 {
            text-align: left;
            height: 30px;
        }
        .auto-style15 {
            text-align: right;
            width: 386px;
            height: 34px;
        }
        .auto-style16 {
            text-align: left;
            height: 34px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="auto-style1">
        <div class="auto-style1">
            <div class="auto-style10">

                <asp:Label ID="lblNombreUsuario" runat="server" Visible="False"></asp:Label>
                <asp:Button ID="btnCerrarSesion" runat="server" OnClick="btnCerrarSesion_Click" Text="Cerrar Sesion" Visible="False" />
                <br />

            </div>
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Vistas/Home/Home.aspx">HOME</asp:HyperLink>
&nbsp;&nbsp;&nbsp;
            <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Vistas/Carrito/Carrito.aspx">CARRITO</asp:HyperLink>
&nbsp;&nbsp;<asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="~/Vistas/Mi Cuenta/MiCuenta.aspx">MI CUENTA</asp:HyperLink>
            &nbsp;
            <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/Vistas/Login/InicioSesion.aspx">LOGIN</asp:HyperLink>
&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/Vistas/Login/Registro.aspx">REGISTRARSE</asp:HyperLink>
            <br />
            <br />
        </div>
            <div class="auto-style2">
                <div>
                    <table class="auto-style3">
                        <tr>
                            <td class="auto-style15">Nombre:</td>
                            <td class="auto-style16">
                                <asp:TextBox ID="txtNombreReg" runat="server" Width="185px"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revNombre" runat="server" ControlToValidate="txtNombreReg" CssClass="auto-style9" ErrorMessage="Ingrese solo Letras" ValidationExpression="[A-Za-z ]*"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ControlToValidate="txtNombreReg" ErrorMessage="Ingrese Su Nombre" ForeColor="#CC0000" ValidationGroup="1"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style6">Apellido:</td>
                            <td class="auto-style7">
                                <asp:TextBox ID="txtApellidoReg" runat="server" Width="181px"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revApellido" runat="server" ControlToValidate="txtApellidoReg" CssClass="auto-style9" ErrorMessage="Ingrese solo letras" ValidationExpression="[A-Za-z ]*"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="rfvApellido" runat="server" ControlToValidate="txtApellidoReg" ErrorMessage="Ingrese Su Apellido" ForeColor="#CC0000" ValidationGroup="1"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style13">Dni:</td>
                            <td class="auto-style14">
                                <asp:TextBox ID="txtDniReg" runat="server" Width="183px" TextMode="Number" ValidationGroup="1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvDni" runat="server" ControlToValidate="txtDniReg" ErrorMessage="Ingrese su Dni" ForeColor="Red" ValidationGroup="1"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style4">Email:</td>
                            <td class="auto-style8">
                                <asp:TextBox ID="txtEmailReg" runat="server" Width="179px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmailReg" ErrorMessage="Ingrese su Email" ForeColor="#CC0000" ValidationGroup="1"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style11">Contraseña:</td>
                            <td class="auto-style12">
                                <asp:TextBox ID="txtContraReg" runat="server" Width="182px" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvContraseña" runat="server" ControlToValidate="txtContraReg" ErrorMessage="Ingrese su contraseña" ForeColor="#CC0000" ValidationGroup="1"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style4">Repita Contraseña:</td>
                            <td class="auto-style8">
                                <asp:TextBox ID="txtContraRepeReg" runat="server" Width="177px" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvContraRepe" runat="server" ControlToValidate="txtContraRepeReg" ErrorMessage="Repita la Contraseña" ForeColor="#CC0000" ValidationGroup="1"></asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="cmvContraRepe" runat="server" ControlToCompare="txtContraReg" ControlToValidate="txtContraRepeReg" ErrorMessage="Las contraseñas no coinciden" ForeColor="#CC0000" ValidationGroup="1"></asp:CompareValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style4">Direccion:</td>
                            <td class="auto-style8">
                                <asp:TextBox ID="txtDirecReg" runat="server" Width="178px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvDirec" runat="server" ControlToValidate="txtDirecReg" ErrorMessage="Ingrese su Direccion" ForeColor="#CC0000" ValidationGroup="1"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <br />
            <asp:Button ID="btnRegistrarse" runat="server" Height="36px" OnClick="btnRegistrarse_Click" Text="REGISTRARSE" ValidationGroup="1" />
            <br />
            <br />
            <asp:Label ID="lblMensaje" runat="server"></asp:Label>
            <br />
            <asp:Label ID="lblMensaje2" runat="server" Visible="False"></asp:Label>
        </div>
    </form>
</body>
</html>
