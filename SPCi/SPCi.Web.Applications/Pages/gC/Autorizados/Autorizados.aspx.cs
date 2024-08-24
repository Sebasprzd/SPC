using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace SPCi.Web.Applications.Pages.gC.Autorizados
{
    public partial class Autorizados : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Obtener el IdEntidad desde la sesión del MasterPage
                string idEntidad = (string)Session["IdEntidad"];
                BindGrid(idEntidad);
                                                                                               //ejemplo de como ser veria la url
                string ixClienteUsuarioQueryString = Request.QueryString["IxClienteUsuario"];  ///Pages/gC/Autorizados/Autorizados.aspx?IxClienteUsuario=20
                //sin esto el autorizado no se registra correctamente

                if (!string.IsNullOrEmpty(ixClienteUsuarioQueryString))
                {
                    int ixClienteUsuario;
                    if (int.TryParse(ixClienteUsuarioQueryString, out ixClienteUsuario))
                    {
                        ViewState["IxClienteUsuario"] = ixClienteUsuario;
                    }
                    else
                    {
                        lblAgregar.Text = "ID de cliente no válido en la consulta.";
                    }
                }
            }
        }

        private void BindGrid(string idEntidad = null)
        {
            string connStr = ConfigurationManager.ConnectionStrings["op_SPC"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand("AT_SeleccionarAutorizadoPorIdEntidad", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (!string.IsNullOrEmpty(idEntidad))
                    {
                        cmd.Parameters.AddWithValue("@IdEntidad", idEntidad);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@IdEntidad", DBNull.Value);
                    }

                    try
                    {
                        conn.Open();
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            sda.Fill(dt);

                            gvAutorizados.DataSource = dt;
                            gvAutorizados.DataBind();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException("Error al acceder a la base de datos", ex);
                    }
                }
            }
        }





        protected void btnAgregarAutorizado(object sender, EventArgs e)
        {
            int ixClienteUsuario;
            string cedula = txtCedula.Text.Trim();

            // Obtener IxClienteUsuario desde ViewState
            if (ViewState["IxClienteUsuario"] != null &&
                int.TryParse(ViewState["IxClienteUsuario"].ToString(), out ixClienteUsuario) &&
                !string.IsNullOrEmpty(cedula))
            {
                try
                {
                    // Verificar si ya hay tres autorizados
                    if (ContarAutorizados(ixClienteUsuario) >= 3)
                    {
                        lblAgregar.Text = "No se puede agregar más de 3 autorizados.";
                    }
                    else if (verificarUsuarioRepetido(ixClienteUsuario, cedula))
                    {
                        lblAgregar.Text = "El usuario ya está autorizado.";
                    }
                    else
                    {
                        InsertCuUsuario(ixClienteUsuario, cedula);
                        BindGrid();  // Actualizar la grid
                        lblAgregar.Text = "Usuario agregado exitosamente.";
                    }
                }
                catch (Exception ex)
                {
                    lblAgregar.Text = "Error al agregar usuario: " + ex.Message;
                }
            }
            else
            {
                lblAgregar.Text = "ID del cliente o cédula inválidos.";
            }
        }



        private int ContarAutorizados(int ixClienteUsuario)
        {
            string connStr = ConfigurationManager.ConnectionStrings["op_SPC"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM CUUsuario WHERE IxClienteUsuario = @IxClienteUsuario", conn))
                {
                    cmd.Parameters.AddWithValue("@IxClienteUsuario", ixClienteUsuario);

                    try
                    {
                        conn.Open();
                        return (int)cmd.ExecuteScalar();
                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException("Error al contar los autorizados", ex);
                    }
                }
            }
        }




        private bool verificarUsuarioRepetido(int ixClienteUsuario, string cedula)
        {
            string connStr = ConfigurationManager.ConnectionStrings["op_SPC"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand(
                    @"SELECT COUNT(*) 
              FROM CUUsuario 
              WHERE IxClienteUsuario = @IxClienteUsuario AND Cedula = @Cedula", conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@IxClienteUsuario", ixClienteUsuario);
                    cmd.Parameters.AddWithValue("@Cedula", cedula);

                    try
                    {
                        conn.Open();
                        int count = (int)cmd.ExecuteScalar();
                        return count > 0;
                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException("Error al verificar si el usuario ya está autorizado", ex);
                    }
                }
            }
        }


        private void InsertCuUsuario(int ixClienteUsuario, string cedula)
        {
            string connStr = ConfigurationManager.ConnectionStrings["op_SPC"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand("AT_InsertCuUsuario", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IxClienteUsuario", ixClienteUsuario);
                    cmd.Parameters.AddWithValue("@Cedula", cedula);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();

                        // Registro en la bitácora
                        using (SqlCommand cmdBitacora = new SqlCommand("AT_InsCUUsuarioB", conn))
                        {
                            cmdBitacora.CommandType = CommandType.StoredProcedure;
                            cmdBitacora.Parameters.AddWithValue("@IxClienteUsuario", ixClienteUsuario);
                            cmdBitacora.Parameters.AddWithValue("@IxSession", Session["IxSesion"]);  // Obtener IxSesion desde la sesión

                            cmdBitacora.ExecuteNonQuery();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException("Error al ejecutar el procedimiento almacenado", ex);
                    }
                }
            }
        }





        protected void btnEliminarAutorizado(object sender, EventArgs e)
        {
            int ixCUUsuario;
            if (int.TryParse(txtIxCUUsuario.Text.Trim(), out ixCUUsuario))
            {
                try
                {
                    DeleteCuUsuario(ixCUUsuario);
                    BindGrid();  // Actualizar 

                    lblEliminar.Text = "Usuario eliminado exitosamente.";
                }
                catch (Exception ex)
                {
                    lblEliminar.Text = "Error al eliminar usuario: " + ex.Message;
                }
            }
            else
            {
                lblEliminar.Text = "ID de usuario inválido.";
            }
        }

        private void DeleteCuUsuario(int ixCUUsuario)
        {
            string connStr = ConfigurationManager.ConnectionStrings["op_SPC"].ConnectionString;

            // Verificar si el usuario existe en la tabla CUUsuario
            if (!verificarUsuarioExistente(ixCUUsuario))
            {
                lblEliminar.Text = "El usuario no está en la tabla autorizados.";
                return;
            }

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand("AT_DeleteCuUsuario", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IxCUUsuario", ixCUUsuario);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();

                        // Registro en la bitácora
                        using (SqlCommand cmdBitacora = new SqlCommand("AT_DelCUUsuarioB", conn))
                        {
                            cmdBitacora.CommandType = CommandType.StoredProcedure;
                            cmdBitacora.Parameters.AddWithValue("@IxClienteUsuario", ixCUUsuario);
                            cmdBitacora.Parameters.AddWithValue("@IxSession", Session["IxSesion"]);  // Obtener IxSesion desde la sesión

                            cmdBitacora.ExecuteNonQuery();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException("Error al ejecutar el procedimiento almacenado", ex);
                    }
                }
            }
        }

        private bool verificarUsuarioExistente(int ixCUUsuario)
        {
            string connStr = ConfigurationManager.ConnectionStrings["op_SPC"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand(
                    @"SELECT COUNT(*) 
              FROM CUUsuario 
              WHERE IxCUUsuario = @IxCUUsuario", conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@IxCUUsuario", ixCUUsuario);

                    try
                    {
                        conn.Open();
                        int count = (int)cmd.ExecuteScalar();
                        return count > 0;
                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException("Error al verificar si el usuario existe", ex);
                    }
                }
            }
        }





        //--------------------------------------------------------------------------------------------



        //Textchenged para buscar la info de los usuarios en la pestaña "Agregar Autorizados"

        protected void txtCedula_TextChanged(object sender, EventArgs e)
        {
            string cedula = txtCedula.Text.Trim();
            if (!string.IsNullOrEmpty(cedula))
            {
                try
                {
                    DataRow userInfo = GetUserInfoByCedula(cedula);
                    if (userInfo != null)
                    {
                        // Mostrar la información recuperada en los controles
                        lblNombre.Text = "Nombre: " + userInfo["Nombre"].ToString();
                        lblPrimerApellido.Text = "Primer Apellido: " + userInfo["PrimerApellido"].ToString();
                        lblSegundoApellido.Text = "Segundo Apellido: " + userInfo["SegundoApellido"].ToString();

                    }
                    else
                    {
                        // Mostrar mensaje si no se encuentra el usuario
                        lblNombre.Text = "Nombre: No encontrado";
                        lblPrimerApellido.Text = "Primer Apellido: No encontrado";
                        lblSegundoApellido.Text = "Segundo Apellido: No encontrado";

                    }
                }
                catch (Exception ex)
                {
                    lblAgregar.Text = "Error al buscar usuario: " + ex.Message;
                }
            }
        }


        private DataRow GetUserInfoByCedula(string cedula)
        {
            string connStr = ConfigurationManager.ConnectionStrings["op_SPC"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand(
                    @"SELECT 
                vs.IdCedula,
                vs.Nombre,
                vs.Apellido1 AS PrimerApellido,
                vs.Apellido2 AS SegundoApellido
              FROM 
                VSesion vs
              WHERE 
                vs.IdCedula = @Cedula;", conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Cedula", cedula);

                    try
                    {
                        conn.Open();
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            sda.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {
                                return dt.Rows[0];
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException("Error al ejecutar la consulta SQL", ex);
                    }
                }
            }
            return null;
        }





        //Textchenged para buscar la info de los usuarios en la pestaña "Eliminar Autorizados"


        protected void txtIxCUUsuario_TextChanged(object sender, EventArgs e)
        {
            string ixCUUsuario = txtIxCUUsuario.Text.Trim();
            if (!string.IsNullOrEmpty(ixCUUsuario))
            {
                try
                {
                    DataRow userInfo = GetUserInfoByIxCUUsuario(ixCUUsuario);
                    if (userInfo != null)
                    {
                        lblCedula.Text = "Cédula: " + userInfo["IdCedula"].ToString();
                        lblNombre.Text = "Nombre: " + userInfo["Nombre"].ToString();
                        lblPrimerApellido.Text = "Primer Apellido: " + userInfo["PrimerApellido"].ToString();
                        lblSegundoApellido.Text = "Segundo Apellido: " + userInfo["SegundoApellido"].ToString();
                    }
                    else
                    {
                        lblCedula.Text = "Cédula: No encontrado";
                        lblNombre.Text = "Nombre: No encontrado";
                        lblPrimerApellido.Text = "Primer Apellido: No encontrado";
                        lblSegundoApellido.Text = "Segundo Apellido: No encontrado";
                    }
                }
                catch (Exception ex)
                {
                    lblEliminar.Text = "Error al buscar usuario: " + ex.Message;
                }
            }
        }






        private DataRow GetUserInfoByIxCUUsuario(string ixCUUsuario)
        {
            string connStr = ConfigurationManager.ConnectionStrings["op_SPC"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand(
                    @"SELECT 
                vs.IdCedula,
                vs.Nombre,
                vs.Apellido1 AS PrimerApellido,
                vs.Apellido2 AS SegundoApellido
              FROM 
                CUUsuario cu
              INNER JOIN 
                VSesion vs ON cu.Cedula = vs.IdCedula
              WHERE 
                cu.IxCUUsuario = @IxCUUsuario", conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@IxCUUsuario", ixCUUsuario);

                    try
                    {
                        conn.Open();
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            sda.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {
                                return dt.Rows[0];
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException("Error al ejecutar la consulta SQL", ex);
                    }
                }
            }
            return null;
        }

    }
}


