using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace MascotasWebApp
{
    public partial class Admin_Usuarios : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["TiendaPerrosDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarUsuarios();
            }
        }

        private void CargarUsuarios()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM catalogo_usuarios", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvUsuarios.DataSource = dt;
                gvUsuarios.DataBind();
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                if (string.IsNullOrEmpty(hfIdUsuario.Value)) // Crear
                {
                    cmd.CommandText = "sp_crear_usuario";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nombre", txtNombre.Text);
                    cmd.Parameters.AddWithValue("@apellidos", txtApellidos.Text);
                    cmd.Parameters.AddWithValue("@correo", txtCorreo.Text);
                    cmd.Parameters.AddWithValue("@pwd", EncriptarSHA256(txtPwd.Text)); 
                    cmd.Parameters.AddWithValue("@tipo", ddlTipo.SelectedValue);
                }
                else // Actualizar
                {
                    cmd.CommandText = "sp_actualizar_usuario";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", hfIdUsuario.Value);
                    cmd.Parameters.AddWithValue("@nombre", txtNombre.Text);
                    cmd.Parameters.AddWithValue("@apellidos", txtApellidos.Text);
                    cmd.Parameters.AddWithValue("@correo", txtCorreo.Text);
                    cmd.Parameters.AddWithValue("@tipo", ddlTipo.SelectedValue);
                }

                conn.Open();
                cmd.ExecuteNonQuery();
                CargarUsuarios();
                LimpiarFormulario();
            }
        }

        private string EncriptarSHA256(string texto)
        {
            using(System.Security.Cryptography.SHA256 sha256= System.Security.Cryptography.SHA256.Create())
            {
                byte[] bytes= System.Text.Encoding.UTF8.GetBytes(texto);
                byte[] hash= sha256.ComputeHash(bytes);

                return BitConverter.ToString(hash).Replace("-", "").ToLower();

            }
        }


        protected void gvUsuarios_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            int id = Convert.ToInt32(gvUsuarios.Rows[index].Cells[0].Text);

            if (e.CommandName == "Editar")
            {
                hfIdUsuario.Value = id.ToString();
                txtNombre.Text = gvUsuarios.Rows[index].Cells[1].Text;
                txtApellidos.Text = gvUsuarios.Rows[index].Cells[2].Text;
                txtCorreo.Text = gvUsuarios.Rows[index].Cells[3].Text;
                ddlTipo.SelectedValue = gvUsuarios.Rows[index].Cells[4].Text;
            }
            else if (e.CommandName == "Eliminar")
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    SqlCommand cmd = new SqlCommand("sp_eliminar_usuario", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    CargarUsuarios();
                }
            }
        }

        private void LimpiarFormulario()
        {
            hfIdUsuario.Value = "";
            txtNombre.Text = "";
            txtApellidos.Text = "";
            txtCorreo.Text = "";
            ddlTipo.SelectedValue = "";
            txtPwd.Text = "";

        }
        protected void btnVolverAdminPanel_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminPanel.aspx");
        }

    }
}
