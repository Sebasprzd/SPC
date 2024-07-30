using System;
using System.Data;
using System.Web.UI;

namespace SPCi.Web.Applications.Pages.gC.Autorizados
{
    public partial class Autorizados : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Aquí puedes cargar los datos iniciales para el GridView
            }
        }

    }
}
