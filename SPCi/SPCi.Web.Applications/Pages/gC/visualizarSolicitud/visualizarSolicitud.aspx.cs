using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using Telerik.Web.UI;

namespace SPCi.Web.Applications.Pages.gC.visualizarSolicitud
{
    public partial class visualizarSolicitud : System.Web.UI.Page
    {
        private int ixClienteUsuario; // Cambiado a int para que coincida con el tipo en la base de datos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (int.TryParse(Request.QueryString["IxClienteUsuario"], out ixClienteUsuario))
                {
                    ViewState["IxClienteUsuario"] = ixClienteUsuario;
                    BindGrid(ixClienteUsuario);
                }
                else
                {
                    Response.Write($"IxClienteUsuario no proporcionado o no es un número válido. Valor recibido: {Request.QueryString["IxClienteUsuario"]}");
                    throw new ArgumentException("IxClienteUsuario no proporcionado o no es un número válido.");
                }
            }
            else
            {
                // Recupera el valor de ViewState en postbacks
                ixClienteUsuario = (int?)ViewState["IxClienteUsuario"] ?? 0;
            }
        }



        private void BindGrid(int ixClienteUsuario)
        {
            string connStr = ConfigurationManager.ConnectionStrings["op_SPC"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"SELECT IxClienteUsuario, CorreoSolicitante, RazonSocial, Identificacion, 
                                        RepresentanteLegal, IdCedulaRepresentanteLegal, CorreoElectronico, 
                                        Direccion, Telefono 
                                 FROM vAT_ClienteUsuario 
                                 WHERE IxClienteUsuario = @IxClienteUsuario";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add("@IxClienteUsuario", SqlDbType.Int).Value = ixClienteUsuario;

                    try
                    {
                        conn.Open();
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            sda.Fill(dt);

                            RadGrid2.DataSource = dt;
                            RadGrid2.DataBind();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException("Error al acceder a la base de datos", ex);
                    }
                }
            }
        }

        public void btnAprobar_Click(object sender, EventArgs e)
        {
            ixClienteUsuario = (int?)ViewState["IxClienteUsuario"] ?? 0;

            if (ixClienteUsuario <= 0)
            {
                Response.Write("IxClienteUsuario no válido en btnAprobar_Click");
                return;
            }

            UpdateEstado(2); // Supongamos que 2 representa el estado "Aprobado"
            Response.Write("Se aprobo la solicitud");

        }

        public void btnRechazar_Click(object sender, EventArgs e)
        {
            ixClienteUsuario = (int?)ViewState["IxClienteUsuario"] ?? 0;

            if (ixClienteUsuario <= 0)
            {
                Response.Write("IxClienteUsuario no válido en btnRechazar_Click");
                return;
            }

            UpdateEstado(3); // Supongamos que 3 representa el estado "Rechazado"
            Response.Write("Se rechazo la solicitud");
        }


        private void UpdateEstado(int nuevoEstado)
        {
            // Verifica que ixClienteUsuario no sea cero
            if (ixClienteUsuario <= 0)
            {
                throw new ArgumentException("IxClienteUsuario no proporcionado o no válido.");
            }

            string connStr = ConfigurationManager.ConnectionStrings["op_SPC"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"UPDATE dbo.ClienteUsuario
                                 SET IxCUEstado = @NuevoEstado
                                 WHERE IxClienteUsuario = @IxClienteUsuario";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add("@NuevoEstado", SqlDbType.Int).Value = nuevoEstado;
                    cmd.Parameters.Add("@IxClienteUsuario", SqlDbType.Int).Value = ixClienteUsuario;

                    try
                    {
                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        // Opcional: Manejo del resultado de la operación
                        if (rowsAffected > 0)
                        {
                            // Éxito: Puedes redirigir o mostrar un mensaje
                        }
                        else
                        {
                            // No se encontró ninguna fila para actualizar
                        }
                    }
                    catch (Exception ex)
                    {
                        // Manejo de errores
                        throw new ApplicationException("Error al actualizar el estado de la solicitud", ex);
                    }
                }
            }
        }
    }
}
