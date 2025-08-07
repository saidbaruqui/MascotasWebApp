using System;
using System.Configuration;
using System.Data.SqlClient;

namespace MascotasWebApp
{
    public partial class RegistrarMascota : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario_id"] == null)
                Response.Redirect("Login.aspx");
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            string connStr = ConfigurationManager.ConnectionStrings["TiendaPerrosDB"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO mascotas_cliente (usuario_id, nombre, tipo, raza, edad) VALUES (@usuario_id, @nombre, @tipo, @raza, @edad)", conn);
                cmd.Parameters.AddWithValue("@usuario_id", Session["usuario_id"]);
                cmd.Parameters.AddWithValue("@nombre", txtNombre.Text);
                cmd.Parameters.AddWithValue("@tipo", txtTipo.Text);
                cmd.Parameters.AddWithValue("@raza", txtRaza.Text);
                cmd.Parameters.AddWithValue("@edad", int.Parse(txtEdad.Text));

                conn.Open();
                cmd.ExecuteNonQuery();
                lblMensaje.Text = "Mascota registrada con éxito.";
                Response.Redirect("Catalogo.aspx");
            }
        }
    }
}
