<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/MasterPages/RLSiteMenu.Master" AutoEventWireup="true" CodeBehind="visualizarSolicitud.aspx.cs" Inherits="SPCi.Web.Applications.Pages.gC.visualizarSolicitud.visualizarSolicitud" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="margin-top: 2%">
        <header>
            <h1>Detalles de la Solicitud</h1>
        </header>
        <hr />
        <main>
            <telerik:RadGrid ID="RadGrid2" runat="server" CellSpacing="-1" Culture="es-ES" GridLines="Both" AutoGenerateColumns="False">
                <MasterTableView AutoGenerateColumns="False" DataKeyNames="IxClienteUsuario">
                    <Columns>
                        <telerik:GridBoundColumn DataField="IxClienteUsuario" HeaderText="ID Cliente" SortExpression="IxClienteUsuario" UniqueName="IxClienteUsuario" />
                        <telerik:GridBoundColumn DataField="CorreoSolicitante" HeaderText="Correo Solicitante" SortExpression="CorreoSolicitante" UniqueName="CorreoSolicitante" />
                        <telerik:GridBoundColumn DataField="RazonSocial" HeaderText="Razón Social" SortExpression="RazonSocial" UniqueName="RazonSocial" />
                        <telerik:GridBoundColumn DataField="Identificacion" HeaderText="Identificación" SortExpression="Identificacion" UniqueName="Identificacion" />
                        <telerik:GridBoundColumn DataField="RepresentanteLegal" HeaderText="Representante Legal" SortExpression="RepresentanteLegal" UniqueName="RepresentanteLegal" />
                        <telerik:GridBoundColumn DataField="IdCedulaRepresentanteLegal" HeaderText="ID Cédula Representante Legal" SortExpression="IdCedulaRepresentanteLegal" UniqueName="IdCedulaRepresentanteLegal" />
                        <telerik:GridBoundColumn DataField="CorreoElectronico" HeaderText="Correo Electrónico" SortExpression="CorreoElectronico" UniqueName="CorreoElectronico" />
                        <telerik:GridBoundColumn DataField="Direccion" HeaderText="Dirección" SortExpression="Direccion" UniqueName="Direccion" />
                        <telerik:GridBoundColumn DataField="Telefono" HeaderText="Teléfono" SortExpression="Telefono" UniqueName="Telefono" />
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>

            <br />
            <br />
            <asp:Button ID="btnAprobar" runat="server" Text="Aprobar" OnClick="btnAprobar_Click" CssClass="btn btn-success" />
            <asp:Button ID="btnRechazar" runat="server" Text="Rechazar" OnClick="btnRechazar_Click" CssClass="btn btn-danger" />


        </main>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterContent" runat="server">
</asp:Content>
