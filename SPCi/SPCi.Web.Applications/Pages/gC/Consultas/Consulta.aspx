<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/MasterPages/RLSiteMenu.Master" AutoEventWireup="true" CodeBehind="Consulta.aspx.cs" Inherits="SPCi.Web.Applications.Pages.gC.Consulta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Hola</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">



 <div style="margin-top: 2% " >
    <header >
        <h1>Sociedad Portuaria de Caldera S.A</h1>
        <h3>Solicitudes de empresas pendientes</h3>
        <img src="../../../Images/logo/logo.png" alt="Logo" style="position: absolute; right: 0; width: 67px; height: auto; top: 0; margin-right:5%; margin-top:-10px " />
    </header>
     <br />
    <hr />
        <main>
            <telerik:RadGrid ID="RadGrid1" runat="server" CellSpacing="-1" Culture="es-ES" GridLines="Both" AutoGenerateColumns="False" OnItemCommand="RadGrid1_ItemCommand">
                <MasterTableView AutoGenerateColumns="False" DataKeyNames="IxClienteUsuario">
                    <Columns>
                        <telerik:GridBoundColumn DataField="IxClienteUsuario" HeaderText="ID Cliente" SortExpression="IxClienteUsuario" UniqueName="IxClienteUsuario" />
                        <telerik:GridBoundColumn DataField="RazonSocial" HeaderText="Razón Social" SortExpression="RazonSocial" UniqueName="RazonSocial" />
                        <telerik:GridBoundColumn DataField="FcClienteUsuario" HeaderText="Fecha" SortExpression="FcClienteUsuario" UniqueName="FcClienteUsuario" />
                        <telerik:GridBoundColumn DataField="Solicitud" HeaderText="Estado" SortExpression="Solicitud" UniqueName="Solicitud" />
                        <telerik:GridTemplateColumn UniqueName="SelectColumn">
                            <ItemTemplate>
                                <asp:LinkButton ID="SelectButton" runat="server" Text="Seleccionar" CommandName="Select" CommandArgument='<%# Eval("IxClienteUsuario") %>'></asp:LinkButton>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
        </main>
 </div>




</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterContent" runat="server">
</asp:Content>
