<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/MasterPages/RLSiteMenu.Master" AutoEventWireup="true" CodeBehind="Solicitudes.aspx.cs" Inherits="SPCi.Web.Applications.Pages.gC.Solicitudes.Solicitudes" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        Página de SOLICITUDES
    </div>
    <telerik:RadButton ID="btnSolicitar" runat="server" Text="Solicitar" OnClick="btnSolicitar_Click" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterContent" runat="server">
</asp:Content>