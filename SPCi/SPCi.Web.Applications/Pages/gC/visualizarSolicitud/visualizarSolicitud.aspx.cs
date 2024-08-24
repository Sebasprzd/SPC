using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using Telerik.Web.UI;
using System.IO;
using System.Web.UI.WebControls;
using Telerik.Windows.Documents.Spreadsheet.Expressions.Functions;

namespace SPCi.Web.Applications.Pages.gC.visualizarSolicitud
{
    public partial class visualizarSolicitud : System.Web.UI.Page
    {
        private int idSolicitud;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string IdSolicitudStr = Request.QueryString["IxClienteUsuario"];

                if (!string.IsNullOrEmpty(IdSolicitudStr))
                {
                    if (int.TryParse(IdSolicitudStr, out idSolicitud))
                    {
                        // Guarda idSolicitud en ViewStateS
                        ViewState["idSolicitud"] = idSolicitud;
                        BindLabels(idSolicitud);
                    }
                    else
                    {
                        Response.Write("Parámetro de cliente no válido.");
                    }
                }
                else
                {
                    Response.Write("Parámetro de cliente faltante.");
                }
            }
        }

        private void BindLabels(int clientId)
        {
            string connStr = ConfigurationManager.ConnectionStrings["op_SPC"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "AT_SeleccionarDatosPorIdSolicitud";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IxClienteUsuario", clientId);

                    try
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                RadLabelIDCliente.Text = reader["IxClienteUsuario"].ToString();
                                RadLabelRazonSocial.Text = reader["RazonSocial"].ToString();
                                RadLabelIdentificacion.Text = reader["Identificacion"].ToString();
                                RadLabelDireccion.Text = reader["Direccion"].ToString();
                                RadLabelTelefono.Text = reader["Telefono"].ToString();
                                RadLabelCorreoSolicitante.Text = reader["CorreoSolicitante"].ToString();
                                RadLabelRepresentanteLegal.Text = reader["RepresentanteLegal"].ToString();
                                RadLabelIDRepresentanteLegal.Text = reader["IdCedulaRepresentanteLegal"].ToString();
                            }
                            else
                            {
                                Response.Write("Cliente no encontrado.");
                            }
                        }
                    }
                    catch (SqlException sqlEx)
                    {
                        Response.Write("Error SQL: " + sqlEx.Message);
                    }
                    catch (Exception ex)
                    {
                        Response.Write("Error: " + ex.Message);
                    }
                }
            }
        }

        protected void btnVisualizar_Click(object sender, EventArgs e)
        {
            // Verificar si el idSolicitud está almacenado en ViewState
            if (ViewState["idSolicitud"] != null)
            {
                // Recuperar el idSolicitud desde ViewState
                idSolicitud = (int)ViewState["idSolicitud"];


                // Obtener la cadena de conexión desde el archivo de configuración
                string connStr = ConfigurationManager.ConnectionStrings["op_SPC"].ConnectionString;

                // Usar la conexión a la base de datos
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    // Definir el procedimiento almacenado a ejecutar
                    string query = "AT_GetCarpetaArchivoWeb";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Agregar el parámetro necesario para la consulta
                        cmd.Parameters.AddWithValue("@IxClienteUsuario", idSolicitud);

                        try
                        {
                            // Abrir la conexión a la base de datos
                            conn.Open();

                            // Ejecutar la consulta y leer los resultados
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                // Verificar si se obtuvo un resultado
                                if (reader.Read())
                                {
                                    // Obtener la URL del archivo desde la base de datos
                                    string rutaBasica = @"C:\PDFs\";
                                    string rutaArmada = Path.Combine(rutaBasica, idSolicitud.ToString());

                                    if (Directory.Exists(rutaArmada))
                                    {
                                        string[] archivosPDF = Directory.GetFiles(rutaArmada, "*.pdf");

                                        if (archivosPDF.Length > 0)
                                        {
                                            pnlPopup.Controls.Clear();

                                            foreach (string archivo in archivosPDF)
                                            {
                                                string nombreArchivo = Path.GetFileName(archivo);

                                                // Crear la ruta file:// para abrir en una nueva pestaña
                                                string urlArchivo = "file://" + archivo.Replace("\\", "/");

                                                HyperLink lnkArchivo = new HyperLink
                                                {
                                                    Text = nombreArchivo,
                                                    NavigateUrl = urlArchivo,
                                                    Target = "blank" // Abrir en una nueva pestaña
                                                };
                                               
                                                pnlPopup.Controls.Add(lnkArchivo);
                                                pnlPopup.Controls.Add(new Literal { Text = "<br />" });
                                            }

                                            pnlPopup.Style["display"] = "block";
                                        }
                                        else
                                        {
                                            Response.Write("No se encontraron archivos PDF en la ruta especificada.");
                                        }
                                    }
                                    else
                                    {
                                        Response.Write("La ruta no existe.");
                                    }
                                }
                                else
                                {
                                    // Mostrar un mensaje si el cliente no fue encontrado
                                    Response.Write("Cliente no encontrado.");
                                }
                            }
                        }
                        catch (SqlException sqlEx)
                        {
                            // Manejar errores de SQL
                            Response.Write("Error SQL: " + sqlEx.Message);
                        }
                        catch (Exception ex)
                        {
                            // Manejar cualquier otro tipo de error
                            Response.Write("Error: " + ex.Message);
                        }
                    }
                }
            }
            else
            {
                // Manejar el caso en que el idSolicitud no esté disponible en ViewState
                Response.Write("Parámetro de cliente faltante para aprobar.");
            }
        }



        protected void btnCerrarPopup_Click(object sender, EventArgs e)
        {
            // Ocultar el panel popup
            pnlPopup.Style["display"] = "none";
        }



        protected void btnAprobar_Click(object sender, EventArgs e)
        {
            if (ViewState["idSolicitud"] != null)
            {
                idSolicitud = (int)ViewState["idSolicitud"];
                ActualizarEstadoCliente(idSolicitud, 2); // 2 "Aprobar"
            }
            else
            {
                Response.Write("Parámetro de cliente faltante para aprobar");
            }
        }

        protected void btnRechazar_Click(object sender, EventArgs e)
        {
            if (ViewState["idSolicitud"] != null)
            {
                idSolicitud = (int)ViewState["idSolicitud"];
                ActualizarEstadoCliente(idSolicitud, 3); // 3 "Rechazar"
                
            }
            else
            {
                Response.Write("Parámetro de cliente faltante para rechazar");
            }
        }

        private void ActualizarEstadoCliente(int num, int nuevoEstado)
        {
            string connStr = ConfigurationManager.ConnectionStrings["op_SPC"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand("AT_ModificarEstadoClienteUsuario", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IxClienteUsuario", num); // Usa idSolicitud
                    cmd.Parameters.AddWithValue("@NuevoEstado", nuevoEstado);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();

                        // Registrar la acción en la bitácora
                        RegistrarEnBitacora(nuevoEstado);

                        Response.Write("<script>alert('Estado actualizado exitosamente');</script>");
                    }
                    catch (SqlException sqlEx)
                    {
                        Response.Write("Error SQL: " + sqlEx.Message);
                    }
                    catch (Exception ex)
                    {
                        Response.Write("Error: " + ex.Message);
                    }
                }
            }
        }



        private void RegistrarEnBitacora(int nuevoEstado)
        {
            string connStr = ConfigurationManager.ConnectionStrings["op_SPC"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand("AT_UpdClienteUsuarioB", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Obtener el valor de la sesión como entero
                    string sessionValue = Session["IxSesion"] as string;

                    int ixSesion;
                    if (!int.TryParse(sessionValue, out ixSesion))
                    {
                        Response.Write("Error: El valor de IxSesion no es válido.");
                        return;
                    }

                    cmd.Parameters.AddWithValue("@IxClienteUsuario", idSolicitud); // Usa idSolicitud que ya es un entero
                    cmd.Parameters.AddWithValue("@IxCUEstado", nuevoEstado);
                    cmd.Parameters.AddWithValue("@IxSession", ixSesion);
                   
                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (SqlException sqlEx)
                    {
                        Response.Write("Error SQL al registrar en bitácora: " + sqlEx.Message);
                    }
                    catch (Exception ex)
                    {
                        Response.Write("Error al registrar en bitácora: " + ex.Message);
                    }
                }
            }
        }
    }
}
