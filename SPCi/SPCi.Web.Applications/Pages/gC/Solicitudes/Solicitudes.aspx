<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Default" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sociedad Portuaria de Caldera S.A</title>
    <telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server" />
    <link rel="stylesheet" type="text/css" href="styles/style.css" />
</head>
<body>
    <form id="form1" runat="server">
        <!-- ScriptManager para manejar los scripts de Telerik -->
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server"></telerik:RadScriptManager>

        <!-- Encabezado -->
        <div class="header">
            <img src="../../../Images/logo/logo.png" alt="Logo" />
            <h2>Sociedad Portuaria de Caldera S.A</h2>
            <h3>Solicitudes de empresas pendientes</h3>
        </div>
        
        <!-- Contenedor del formulario -->
        <div class="form-container">
            <table>
                <tr>
                    <td><label for="txtNombre">Nombre o razón social:</label></td>
                    <td>
                        <telerik:RadTextBox ID="txtNombre" runat="server" CssClass="telerik-input" />
                    </td>
                </tr>
                <tr>
                    <td><label for="txtCedula">Cédula:</label></td>
                    <td>
                        <telerik:RadTextBox ID="txtCedula" runat="server" CssClass="telerik-input" />
                    </td>
                </tr>
                <tr>
                    <td><label for="txtDireccion">Dirección:</label></td>
                    <td>
                        <telerik:RadTextBox ID="txtDireccion" runat="server" CssClass="telerik-input" />
                    </td>
                </tr>
                <tr>
                    <td><label for="txtTelefono">Número Teléfono:</label></td>
                    <td>
                        <telerik:RadTextBox ID="txtTelefono" runat="server" CssClass="telerik-input" />
                    </td>
                </tr>
                <tr>
                    <td><label for="txtCorreo">Correo electrónico:</label></td>
                    <td>
                        <telerik:RadTextBox ID="txtCorreo" runat="server" CssClass="telerik-input" />
                    </td>
                </tr>
                <tr>
                    <td><label for="txtRepresentante">Nombre del representante legal:</label></td>
                    <td>
                        <telerik:RadTextBox ID="txtRepresentante" runat="server" CssClass="telerik-input" />
                    </td>
                </tr>
                <tr>
                    <td><label for="txtCedulaRepresentante">Cédula de identidad:</label></td>
                    <td>
                        <telerik:RadTextBox ID="txtCedulaRepresentante" runat="server" CssClass="telerik-input" />
                    </td>
                </tr>
            </table>
            
            <div class="btn-container">
                <telerik:RadButton ID="btnEnviar" runat="server" Text="Enviar" CssClass="btn btn-primary" OnClick="btnEnviar_Click" />
            </div>
        </div>

        <!-- RadWindowManager para manejar ventanas modales -->
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server"></telerik:RadWindowManager>

        <!-- RadWindow para mostrar el mensaje -->
       <telerik:RadWindow ID="RadWindow1" runat="server" Title="Confirmación" Modal="true" Width="400px" Height="200px" VisibleOnPageLoad="false">
    <ContentTemplate>
        <div id="messageContainer">
            <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
        </div>
    </ContentTemplate>
</telerik:RadWindow>
    </form>
</body>
</html>