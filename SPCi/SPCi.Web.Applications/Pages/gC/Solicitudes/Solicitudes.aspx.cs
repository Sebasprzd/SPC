using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text.RegularExpressions;
using Telerik.Web.UI;

public partial class Default : System.Web.UI.Page
{
    protected void btnEnviar_Click(object sender, EventArgs e)
    {
        // Validaciones
        if (!Regex.IsMatch(txtNombre.Text, @"^[a-zA-Z\s.,'-]+$"))
        {
            MostrarMensaje("El nombre solo puede contener letras, espacios, puntos, comas, apóstrofos y guiones.");
            return;
        }

        if (!Regex.IsMatch(txtCedula.Text, @"^\d+$"))
        {
            MostrarMensaje("La cédula solo debe contener números.");
            return;
        }

        if (!Regex.IsMatch(txtTelefono.Text, @"^\d+$"))
        {
            MostrarMensaje("El número de teléfono solo debe contener números.");
            return;
        }

        if (!Regex.IsMatch(txtCorreo.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
        {
            MostrarMensaje("El correo electrónico no es válido.");
            return;
        }

        if (!Regex.IsMatch(txtRepresentante.Text, @"^[a-zA-Z\s.,'-]+$"))
        {
            MostrarMensaje("El nombre del representante legal solo puede contener letras, espacios, puntos, comas, apóstrofos y guiones.");
            return;
        }

        if (!Regex.IsMatch(txtCedulaRepresentante.Text, @"^\d+$"))
        {
            MostrarMensaje("La cédula del representante solo debe contener números.");
            return;
        }

        // Si todas las validaciones pasan, se procede a insertar en la base de datos
        try
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["op_SPC"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AT_InsertarClienteUsuario", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@CorreoSolicitante", txtCorreo.Text);
                    cmd.Parameters.AddWithValue("@RazonSocial", txtNombre.Text);
                    cmd.Parameters.AddWithValue("@Identificacion", txtCedula.Text);
                    cmd.Parameters.AddWithValue("@RepresentanteLegal", txtRepresentante.Text);
                    cmd.Parameters.AddWithValue("@IdCedulaRepresentanteLegal", txtCedulaRepresentante.Text);
                    cmd.Parameters.AddWithValue("@CorreoElectronico", txtCorreo.Text);
                    cmd.Parameters.AddWithValue("@Direccion", txtDireccion.Text);
                    cmd.Parameters.AddWithValue("@Telefono", txtTelefono.Text);
                    cmd.Parameters.AddWithValue("@IxCUTipoAutorizacion", 1); // Ejemplo de valor para IxCUTipoAutorizacion
                    cmd.Parameters.AddWithValue("@IxCUEstado", 1); // Ejemplo de valor para IxCUEstado
                    cmd.Parameters.AddWithValue("@FcClienteUsuario", DateTime.Now);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    MostrarMensaje("Los datos se han guardado correctamente. <br />" +
                        "Nombre o razón social: " + txtNombre.Text + "<br />" +
                        "Cédula: " + txtCedula.Text + "<br />" +
                        "Dirección: " + txtDireccion.Text + "<br />" +
                        "Número Teléfono: " + txtTelefono.Text + "<br />" +
                        "Correo electrónico: " + txtCorreo.Text + "<br />" +
                        "Nombre del representante legal: " + txtRepresentante.Text + "<br />" +
                        "Cédula de identidad: " + txtCedulaRepresentante.Text);
                }
            }
        }
        catch (Exception ex)
        {
            MostrarMensaje("Ocurrió un error al guardar los datos: " + ex.Message);
        }
    }

    private void MostrarMensaje(string mensaje)
    {
        lblMensaje.Text = mensaje;
        lblMensaje.Attributes.Add("style", "white-space: pre-line;"); // Para permitir el uso de <br />
        RadWindow1.VisibleOnPageLoad = true; // Asegúrate de que RadWindow1 sea visible
    }
}
