using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace MascotasWebApp // Usa el mismo namespace que tienes en tu proyecto
{
    public partial class Registro : System.Web.UI.Page
    {
        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            string connStr = ConfigurationManager.ConnectionStrings["TiendaPerrosDB"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("sp_crear_usuario", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@nombre", txtNombre.Text);
                cmd.Parameters.AddWithValue("@apellidos", txtApellidos.Text);
                cmd.Parameters.AddWithValue("@correo", txtCorreo.Text);
                cmd.Parameters.AddWithValue("@pwd", EncriptarSHA256(txtPwd.Text));
                cmd.Parameters.AddWithValue("@tipo", "cliente");

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    
                    Response.Redirect("Login.aspx");
                }
                catch (SqlException ex)
                {
                    lblMensaje.Text = "❌ Error al registrar: " + ex.Message;
                }
            }
        }

        private string EncriptarSHA256(string texto)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(texto));
                StringBuilder builder = new StringBuilder();
                foreach (var b in bytes)
                    builder.Append(b.ToString("x2"));
                return builder.ToString();
            }
        }
    }
}
