using System;
using System.Data.SqlClient;
using System.Web.UI;

namespace SPCi.Web.Applications.Pages.gC.Autorizados
{
    public partial class Autorizados : Page
    {
        private string connectionString = "Data Source=localhost;Initial Catalog=SPCiAutorizadosDB;Integrated Security=True";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarAutorizados();
            }
        }

        private void CargarAutorizados()
        {
            string query = "SELECT Cedula, Nombre + ' ' + PrimerApellido + ' ' + SegundoApellido AS NombreCompleto FROM dbo.Usuarios WHERE Cedula IN (SELECT Cedula FROM dbo.Autorizados)";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    gvAutorizados.DataSource = reader;
                    gvAutorizados.DataBind();
                }
            }
        }

        protected void btnModoAgregar_Click(object sender, EventArgs e)
        {
            pnlFormulario.Visible = true;
            txtCedula.Text = "";
            txtNombre.Text = "";
            txtPrimerApellido.Text = "";
            txtSegundoApellido.Text = "";
            btnGuardar.Text = "Agregar";
            btnBorrar.Visible = false;
            MostrarCampos(false);
        }

        protected void btnModoEditar_Click(object sender, EventArgs e)
        {
            pnlFormulario.Visible = true;
            txtCedula.Text = "";
            btnGuardar.Text = "Guardar";
            btnBorrar.Visible = true;
            MostrarCampos(false);
        }

        private void MostrarCampos(bool visible)
        {
            lblNombre.Visible = visible;
            txtNombre.Visible = visible;
            lblPrimerApellido.Visible = visible;
            txtPrimerApellido.Visible = visible;
            lblSegundoApellido.Visible = visible;
            txtSegundoApellido.Visible = visible;
        }

        protected void txtCedula_TextChanged(object sender, EventArgs e)
        {
            if (txtCedula.Text.Trim() != "")
            {
                string query = "SELECT Nombre, PrimerApellido, SegundoApellido FROM dbo.Usuarios WHERE Cedula = @Cedula";

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Cedula", txtCedula.Text.Trim());
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            txtNombre.Text = reader["Nombre"].ToString();
                            txtPrimerApellido.Text = reader["PrimerApellido"].ToString();
                            txtSegundoApellido.Text = reader["SegundoApellido"].ToString();
                            MostrarCampos(true);
                        }
                        else
                        {
                            txtNombre.Text = "";
                            txtPrimerApellido.Text = "";
                            txtSegundoApellido.Text = "";
                            MostrarCampos(false);
                        }
                    }
                }
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (btnGuardar.Text == "Agregar")
            {
                string query = "INSERT INTO dbo.Autorizados (Cedula) VALUES (@Cedula)";

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Cedula", txtCedula.Text.Trim());
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                CargarAutorizados();
                pnlFormulario.Visible = false;
            }
        }

        protected void btnBorrar_Click(object sender, EventArgs e)
        {
            string query = "DELETE FROM dbo.Autorizados WHERE Cedula = @Cedula";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Cedula", txtCedula.Text.Trim());
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            CargarAutorizados();
            pnlFormulario.Visible = false;
        }
    }
}
