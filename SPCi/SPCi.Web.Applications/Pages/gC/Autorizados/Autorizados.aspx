<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/MasterPages/RLSiteMenu.Master" AutoEventWireup="true" CodeBehind="Autorizados.aspx.cs" Inherits="SPCi.Web.Applications.Pages.gC.Autorizados.Autorizados" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="text-align: left; margin-bottom: 20px; position: relative; width: 100%; max-width: 800px; margin-left: auto; margin-right: auto;">
        <div style="display: inline-block; vertical-align: middle;">
            <h1>Sociedad Portuaria de Caldera S.A</h1>
            <h3>Registro de Autorizados</h3>
        </div>
        <img src="../../../Images/logo/logo.png" alt="Logo" style="position: absolute; right: 0; width: 67px; height: auto; top: 0;" />
    </div>

    <hr style="border: 1px solid #000; margin: 20px 0;" />

    <div style="text-align: center; margin-bottom: 20px;">
        <asp:Label ID="lblDescripcion" runat="server" Text="Personas encargadas de realizar trámites de permisos de ingreso a la Terminal de Caldera por sistema digital"></asp:Label>
    </div>

    <!-- Grid de Autorizados -->
    <div style="text-align: center; margin: 0 auto; max-width: 800px;">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <telerik:RadGrid 
            ID="gvAutorizados" runat="server" AutoGenerateColumns="False" AllowPaging="True" AllowSorting="True" GridLines="None" PageSize="10" Width="100%">
            <MasterTableView>
                <Columns>
                    <telerik:GridBoundColumn DataField="IDAutorizado" HeaderText="ID Autorizado" SortExpression="IDAutorizado" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                    <telerik:GridBoundColumn DataField="Cedula" HeaderText="Cédula" SortExpression="Cedula" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                    <telerik:GridBoundColumn DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                    <telerik:GridBoundColumn DataField="PrimerApellido" HeaderText="Primer Apellido" SortExpression="PrimerApellido" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                    <telerik:GridBoundColumn DataField="SegundoApellido" HeaderText="Segundo Apellido" SortExpression="SegundoApellido" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
    </ContentTemplate>
</asp:UpdatePanel>
    </div>

    <hr style="border: 1px solid #000; margin: 20px 0;" />


<!-- contenedor de Pestañas -->
<div style="text-align: left; margin: 0 auto; max-width: 800px;">
    <telerik:RadTabStrip 
        RenderMode="Lightweight" runat="server" ID="RadTabStrip1" MultiPageID="RadMultiPage1" Skin="Office2010Blue" CssClass="custom-telerik-tabstrip" Width="100%">
        <Tabs>
            <telerik:RadTab Text="Agregar Autorizado" Width="200px"></telerik:RadTab>
            <telerik:RadTab Text="Eliminar Autorizado" Width="200px"></telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
</div>


    <!-- Contenedor con borde para el contenido de las pestañas -->
<div style="text-align: left; margin: 0 auto; max-width: 800px; border: 1px solid #000; padding: 10px; border-radius: 5px; ">

    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" Width="100%">
<!-- Página para Agregar -->
<telerik:RadPageView ID="RadPageView1" runat="server">
    <div style="display: flex; justify-content: center; align-items: flex-start; flex-direction: column; padding: 20px; text-align: center;">
        <h3>Agregar Autorizado</h3>
        
        <!-- Contenedor para los Labels y el TextBox -->
        <div style="display: flex; flex-direction: column; align-items: flex-start; width: 100%;">
            <asp:TextBox ID="txtCedula" runat="server" Placeholder="Ingrese Cédula del Usuario" AutoPostBack="True" OnTextChanged="txtCedula_TextChanged" style="width: 100%;"></asp:TextBox>
            <asp:Label ID="lblNombre" runat="server" Text="Nombre:" style="margin-top: 15px; font-size: 14px; font-weight: bold;"></asp:Label>
            <asp:Label ID="lblPrimerApellido" runat="server" Text="Primer Apellido:" style="margin-top: 15px; font-size: 14px; font-weight: bold;"></asp:Label>
            <asp:Label ID="lblSegundoApellido" runat="server" Text="Segundo Apellido:" style="margin-top: 15px; font-size: 14px; font-weight: bold;"></asp:Label>
        </div>

        <!-- Botón de Agregar -->
        <div style="align-self: flex-end; margin-top: 20px;">
            <asp:Button ID="btnAgregar" runat="server" Text="Agregar Usuario" OnClick="btnAgregarAutorizado" 
                        style="background-color: #d6e5f3; border: 1px solid #a5c7e7; color: #2d3e50; padding: 5px 10px; 
                               border-radius: 3px; font-family: Arial, sans-serif; font-size: 14px; cursor: pointer;" />
        </div>
        
        <!-- Mensaje de éxito/agregar -->
        <asp:Label ID="lblAgregar" runat="server" ForeColor="Green" style="margin-top: 10px;"></asp:Label>
    </div>
</telerik:RadPageView>

<!-- Página para Eliminar -->
<telerik:RadPageView ID="RadPageView2" runat="server">
    <div style="display: flex; justify-content: center; align-items: flex-start; flex-direction: column; padding: 20px; text-align: center;">
        <h3>Eliminar Autorizado</h3>
        
        <!-- Contenedor para los Labels y el TextBox -->
        <div style="display: flex; flex-direction: column; align-items: flex-start; width: 100%;">
            <asp:TextBox ID="txtIxCUUsuario" runat="server" Placeholder="Ingrese el ID del Autorizado" AutoPostBack="True" OnTextChanged="txtIxCUUsuario_TextChanged" style="width: 100%;"></asp:TextBox>
            <asp:Label ID="lblCedula" runat="server" Text="Cédula:" style="margin-top: 15px; font-size: 14px; font-weight: bold;"></asp:Label>
        <!-- <asp:Label ID="Label1" runat="server" Text="Nombre:" style="margin-top: 15px; font-size: 14px; font-weight: bold;"></asp:Label> -->
        <!-- <asp:Label ID="Label2" runat="server" Text="Primer Apellido:" style="margin-top: 15px; font-size: 14px; font-weight: bold;"></asp:Label>-->
        <!-- <asp:Label ID="Label3" runat="server" Text="Segundo Apellido:" style="margin-top: 15px; font-size: 14px; font-weight: bold;"></asp:Label> -->

        </div>

        <!-- Botón de Eliminar -->
        <div style="align-self: flex-end; margin-top: 20px;">
            <asp:Button ID="btnEliminar" runat="server" Text="Eliminar Usuario" OnClick="btnEliminarAutorizado" 
                        style="background-color: #d6e5f3; border: 1px solid #a5c7e7; color: #2d3e50; padding: 5px 10px; 
                               border-radius: 3px; font-family: Arial, sans-serif; font-size: 14px; cursor: pointer;" />
        </div>
        
        <!-- Mensaje de eliminación -->
        <asp:Label ID="lblEliminar" runat="server" ForeColor="Red" style="margin-top: 10px;"></asp:Label>
    </div>
</telerik:RadPageView>



        </telerik:RadMultiPage>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="FooterContent" runat="server">
    <hr style="border: 1px solid #ccc; margin: 20px 0;" />
    <div style="text-align: left; padding: 10px 0;">
        <p style="margin: 0;">2006-2023 Sociedad Portuaria de Caldera S.A.</p>
    </div>
</asp:Content>

