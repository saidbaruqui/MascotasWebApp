using System;
using System.Configuration;
using System.Data.SqlClient;

namespace MascotasWeb
{
    public partial class TestConexion : System.Web.UI.Page
    {
        protected void btnProbarConexion_Click(object sender, EventArgs e)
        {
            string conexion = ConfigurationManager.ConnectionStrings["TiendaPerrosDB"].ConnectionString;

            using (SqlConnection con = new SqlConnection(conexion))
            {
                try
                {
                    con.Open();
                    lblResultado.Text = "✅ Conexión exitosa a TiendaPerrosDB";
                }
                catch (Exception ex)
                {
                    lblResultado.Text = "❌ Error: " + ex.Message;
                }
            }
        }
    }
}
