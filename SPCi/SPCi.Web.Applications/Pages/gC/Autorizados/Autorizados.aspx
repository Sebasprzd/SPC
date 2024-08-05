<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/MasterPages/RLSiteMenu.Master" AutoEventWireup="true" CodeBehind="Autorizados.aspx.cs" Inherits="SPCi.Web.Applications.Pages.gC.Autorizados.Autorizados" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="text-align: center; margin-bottom: 20px; position: relative; width: 100%; max-width: 800px; margin-left: auto; margin-right: auto;">
        <div style="display: inline-block; vertical-align: middle;">
            <h3>Sociedad Portuaria de Caldera S.A</h3>
            <h4>Registro de Autorizados</h4>
        </div>
        <img src="../../../Images/logo/logo.png" alt="Logo" style="position: absolute; right: 0; width: 67px; height: auto; top: 0;" />
    </div>

    <!-- Línea -->
    <hr style="border: 1px solid #000; margin: 20px 0;" />

    <!-- Descripción -->
    <div style="text-align: center; margin-bottom: 20px;">
        <asp:Label ID="lblDescripcion" runat="server" Text="Personas encargadas de realizar trámites de permisos de ingreso a la Terminal de Caldera por sistema digital"></asp:Label>
    </div>

    <!-- Grid de Autorizados -->
    <asp:GridView ID="gvAutorizados" runat="server" AutoGenerateColumns="False" Width="100%" CellPadding="4" ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:BoundField DataField="Cedula" HeaderText="Cédula de Identidad" SortExpression="Cedula" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="NombreCompleto" HeaderText="Nombre Completo del Autorizado" SortExpression="NombreCompleto" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
        </Columns>
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#E9E7E2" />
        <SortedAscendingHeaderStyle BackColor="#506C8C" />
        <SortedDescendingCellStyle BackColor="#FFFDF8" />
        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
    </asp:GridView>

    <!-- Contenedor del modo de edición/agregar -->
    <div style="text-align: center; margin-top: 20px;">
        <asp:Button ID="btnModoEditar" runat="server" Text="Eliminar Autorizado" OnClick="btnModoEditar_Click" />
        <asp:Button ID="btnModoAgregar" runat="server" Text="Agregar Autorizado" OnClick="btnModoAgregar_Click" />
    </div>

    <asp:Panel ID="pnlFormulario" runat="server" Visible="false" style="margin-top: 20px; text-align: center;">
        <asp:Label ID="lblCedula" runat="server" Text="Cédula:"></asp:Label>
        <asp:TextBox ID="txtCedula" runat="server" OnTextChanged="txtCedula_TextChanged" AutoPostBack="True"></asp:TextBox><br /><br />

        <asp:Label ID="lblNombre" runat="server" Text="Nombre:" Visible="false"></asp:Label>
        <asp:TextBox ID="txtNombre" runat="server" Visible="false"></asp:TextBox><br /><br />

        <asp:Label ID="lblPrimerApellido" runat="server" Text="Primer Apellido:" Visible="false"></asp:Label>
        <asp:TextBox ID="txtPrimerApellido" runat="server" Visible="false"></asp:TextBox><br /><br />

        <asp:Label ID="lblSegundoApellido" runat="server" Text="Segundo Apellido:" Visible="false"></asp:Label>
        <asp:TextBox ID="txtSegundoApellido" runat="server" Visible="false"></asp:TextBox><br /><br />

        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" />
        <asp:Button ID="btnBorrar" runat="server" Text="Eliminar" OnClick="btnBorrar_Click" Visible="false" />
    </asp:Panel>
</asp:Content>




<asp:Content ID="Content3" ContentPlaceHolderID="FooterContent" runat="server">
    <hr style="border: 1px solid #ccc; margin: 20px 0;" />
    <div style="text-align: left; padding: 10px 0;">
        <p style="margin: 0;">2006-2023 Sociedad Portuaria de Caldera S.A.</p>
    </div>
</asp:Content>

