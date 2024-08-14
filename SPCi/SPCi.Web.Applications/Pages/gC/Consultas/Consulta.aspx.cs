﻿using System;
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
                RadGrid1.Rebind(); // Asegura que se dispare el evento OnNeedDataSource
            }
        }

        protected void RadGrid1_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            string connStr = ConfigurationManager.ConnectionStrings["op_SPC"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "AT_SeleccionarDatosSolicitud";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    try
                    {
                        conn.Open();
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            sda.Fill(dt);

                            // Verificar las columnas en el DataTable
                            foreach (DataColumn column in dt.Columns)
                            {
                                System.Diagnostics.Debug.WriteLine("Column: " + column.ColumnName);
                            }

                            // Agregar columna para el estado de la solicitud
                            dt.Columns.Add("Solicitud", typeof(string));

                            foreach (DataRow row in dt.Rows)
                            {
                                int estado = Convert.ToInt32(row["Estado"]);
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

        protected void RadGrid1_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                // Obtener el valor de IxClienteUsuario de la fila seleccionada
                string ixClienteUsuario = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["IxClienteUsuario"].ToString();

                // Redirigir a Pruebas.aspx con IxClienteUsuario como parámetro
                Response.Redirect("~/Pages/gC/visualizarSolicitud/visualizarSolicitud.aspx?IxClienteUsuario=" + ixClienteUsuario);
            }
        }
    }
}