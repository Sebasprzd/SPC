using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;
using System.Data.SqlClient;
using Telerik.Web.UI.Skins;

namespace SPCi.Web.Applications.Pages.gC
{
    public partial class Consulta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        private void BindGrid()
        {
            string connStr = ConfigurationManager.ConnectionStrings["op_SPC"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                // Asegúrate de que el nombre de la tabla y el esquema son correctos
                string query = @"SELECT IxClienteUsuario, RazonSocial, FcClienteUsuario, IxCUEstado 
                                 FROM dbo.ClienteUsuario 
                                 ORDER BY IxClienteUsuario";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    try
                    {
                        conn.Open();
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            sda.Fill(dt);

                            // Agregar columna para el estado de la solicitud
                            dt.Columns.Add("Solicitud", typeof(string));

                            foreach (DataRow row in dt.Rows)
                            {
                                int estado = Convert.ToInt32(row["IxCUEstado"]);
                                switch (estado)
                                {
                                    case 1:
                                        row["Solicitud"] = "Pendiente";
                                        break;
                                    case 2:
                                        row["Solicitud"] = "Aprobado";
                                        break;
                                    default:
                                        row["Solicitud"] = "Rechazado";
                                        break;
                                }
                            }

                            RadGrid1.DataSource = dt;
                            RadGrid1.DataBind();
                        }
                    }
                    catch (Exception ex)
                    {
                        // Manejo de errores
                        throw new ApplicationException("Error al acceder a la base de datos", ex);
                    }
                }
            }
        }

        protected void RadGrid1_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                // Obtener el valor de IxClienteUsuario de la fila seleccionada
                string ixClienteUsuario = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["IxClienteUsuario"].ToString();

                // Redirigir a Pruebas.aspx con IxClienteUsuario como parámetro
                Response.Redirect("~/Pages/gC/Solicitudes/Solicitudes.aspx?IxClienteUsuario=" + ixClienteUsuario);
            }
        }
    }
}