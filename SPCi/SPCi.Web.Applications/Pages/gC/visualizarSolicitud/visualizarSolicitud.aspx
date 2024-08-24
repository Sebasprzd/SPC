<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/MasterPages/RLSiteMenu.Master" AutoEventWireup="true" CodeBehind="visualizarSolicitud.aspx.cs" Inherits="SPCi.Web.Applications.Pages.gC.visualizarSolicitud.visualizarSolicitud" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<style>
    .popup {
        position: fixed;
        top: 50%;
        left: 50%;
        transform: translate(-50%, -50%);
        background-color: white;
        padding: 20px;
        border: 1px solid #ccc;
        box-shadow: 0px 0px 10px #000;
        z-index: 1000;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
       <div style="width: 80%; max-width: 650px; margin-left: auto; margin-right: auto; margin-top: 2%;">
         <header >
            <h1>Sociedad Portuaria de Caldera S.A</h1>
            <h3>Solicitudes de empresas pendientes</h3>
            <img src="../../../Images/logo/logo.png" alt="Logo" style="position: absolute; right: 0; width: 67px; height: auto; top: 0; margin-right:15%; margin-top:-10px " />
        </header>
        <hr />
        <main>
            <div style="width: 70%; margin: 0 auto; padding-top: 2%;" >
                <div>
                    <asp:Label style="margin-top: 10%; padding-right: 21%;" runat="server" Text="ID Cliente:" />
                    <telerik:RadLabel style="width: 200px; border: 1px solid LightBlue;" ID="RadLabelIDCliente" runat="server"></telerik:RadLabel><br /> 
                </div>
                <br />
                <div>
                    <asp:Label style="padding-right: 17.68%;"  runat="server" Text="Razón Social: " />
                    <telerik:RadLabel style="width: 200px; border: 1px solid LightBlue;" ID="RadLabelRazonSocial" runat="server" ></telerik:RadLabel><br />
                </div>
                <br />
                <div>
                    <asp:Label style="padding-right: 16.75%;" runat="server" Text= "Identificación: " />
                    <telerik:RadLabel style="width: 200px; border: 1px solid LightBlue;" ID="RadLabelIdentificacion" runat="server" ></telerik:RadLabel><br />
                </div>
                <br />
                <div>
                     <asp:Label style="padding-right: 21.45%;"  runat="server" Text="Dirección: "/>
                     <telerik:RadLabel style="width: 200px; border: 1px solid LightBlue;" ID="RadLabelDireccion" runat="server" ></telerik:RadLabel><br />
                </div>
                <br />
                <div>
                    <asp:Label style="padding-right: 22.40%;"  runat="server" Text="Teléfono: "/>
                    <telerik:RadLabel style="width: 200px; border: 1px solid LightBlue;" ID="RadLabelTelefono" runat="server" ></telerik:RadLabel><br />
                </div>
                <br />
                <div>
                    <asp:Label style="padding-right: 11.58%;" runat="server" Text="Correo Solicitante: " />
                    <telerik:RadLabel style="width: 200px; border: 1px solid LightBlue;" ID="RadLabelCorreoSolicitante" runat="server" ></telerik:RadLabel><br />
                </div>
                <br />
                <div>
                    <asp:Label style="padding-right: 8.70%;" runat="server" Text="Representante Legal: " />
                    <telerik:RadLabel  style="width: 200px; border: 1px solid LightBlue ;" ID="RadLabelRepresentanteLegal" runat="server" ></telerik:RadLabel><br />
                </div>
                <br />
                <div>
                    <asp:Label style="padding-right: 4.90%;" runat="server" Text="ID Representante Legal:: " />
                    <telerik:RadLabel style="width: 200px; border: 1px solid LightBlue;" ID="RadLabelIDRepresentanteLegal" runat="server" ></telerik:RadLabel><br />
                </div>
                <br />
                <div>
                    <p>¿Desea visualizar los archivos adjuntos?</p>
                    <asp:Button ID="btnVisualizar" runat="server" Text="Visualizar Archivo" OnClick="btnVisualizar_Click" />
                </div>

                <!-- Popup para mostrar el archivo -->
                <asp:Panel ID="pnlPopup" runat="server" CssClass="popup" Style="display: none;">
                    <asp:Label ID="lblFileUrl" runat="server" Text="Abrir archivo:"></asp:Label>
                    <asp:HyperLink ID="lnkFile" runat="server" Text="Ver PDF" Target="_blank" />
                    <asp:Button ID="btnCerrarPopup" runat="server" Text="Cerrar" OnClick="btnCerrarPopup_Click" />
                </asp:Panel>
                <br />
                <br />
                <asp:Button ID="btnAprobar" runat="server" Text="Aprobar" OnClick="btnAprobar_Click" />
                <asp:Button ID="btnRechazar" runat="server" Text="Rechazar" OnClick="btnRechazar_Click" />
            
  
           </div>
        </main>
    </div>  
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterContent" runat="server">
</asp:Content>
