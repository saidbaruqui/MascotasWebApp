using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace MascotasWebApp // Usa el namespace de tu proyecto
{
    public partial class Login : System.Web.UI.Page
    {
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string correo = txtCorreo.Text;
            string pwd = EncriptarSHA256(txtPwd.Text);

            string connStr = ConfigurationManager.ConnectionStrings["TiendaPerrosDB"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("SELECT id, nombre, tipo FROM catalogo_usuarios WHERE correo=@correo AND pwd=@pwd", conn);
                cmd.Parameters.AddWithValue("@correo", correo);
                cmd.Parameters.AddWithValue("@pwd", pwd);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Session["usuario_id"] = reader["id"];
                    Session["nombre"] = reader["nombre"];
                    Session["tipo"] = reader["tipo"];

                    string tipoUsuario = reader["tipo"].ToString().ToLower();

                  

                    if (tipoUsuario == "admin")
                    {
                        Response.Redirect("AdminPanel.aspx");
                    }
                    else if (tipoUsuario == "empleado")
                    {
                        Response.Redirect("EmpleadoPanel.aspx");
                    }
                    else
                    {
                        Response.Redirect("Catalogo.aspx");
                    }
                }
                else
                {
                    lblMensaje.Text = "❌ Correo o contraseña incorrectos.";
                }
            }
        }

        protected void lnkRegistrar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Registro.aspx");
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
