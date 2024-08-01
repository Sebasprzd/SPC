<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/MasterPages/RLSiteMenu.Master" AutoEventWireup="true" CodeBehind="Consulta.aspx.cs" Inherits="SPCi.Web.Applications.Pages.Misc.Pruebas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
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
         <telerik:RadGrid runat="server" CellSpacing="-1" Culture="es-ES" GridLines="Both" >
             <MasterTableView AutoGenerateColumns="False">
                
             </MasterTableView>
             <telerik> </telerik>
         </telerik:RadGrid>

     </main>
 </div>




</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterContent" runat="server">
</asp:Content>
