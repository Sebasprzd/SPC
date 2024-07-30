using System;
using System.Data;
using System.Web.UI;

namespace SPCi.Web.Applications.Pages.gC.Solicitudes
{
    public partial class Solicitudes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnSolicitar_Click(object sender, EventArgs e)
        {
            // Muestra un mensaje cuando el botón es clickeado
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('estás solicitando xd');", true);
        }
    }
}