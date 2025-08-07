using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace MascotasWebApp
{
    public partial class Admin_Mascotas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["tipo"] == null || Session["tipo"].ToString() != "admin" && Session["tipo"].ToString() != "empleado")
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                CargarMascotas();
            }
        }

        protected void CargarMascotas()
        {
            string connStr = ConfigurationManager.ConnectionStrings["TiendaPerrosDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT m.id, u.nombre AS Cliente, m.nombre, m.tipo, m.raza, m.edad " +
                               "FROM mascotas_cliente m " +
                               "INNER JOIN catalogo_usuarios u ON m.usuario_id = u.id";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvMascotas.DataSource = dt;
                gvMascotas.DataBind();
            }
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            string connStr = ConfigurationManager.ConnectionStrings["TiendaPerrosDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT m.id, u.nombre AS Cliente, m.nombre, m.tipo, m.raza, m.edad " +
                               "FROM mascotas_cliente m " +
                               "INNER JOIN catalogo_usuarios u ON m.usuario_id = u.id " +
                               "WHERE 1=1";

                if (!string.IsNullOrWhiteSpace(txtFiltroUsuario.Text))
                {
                    query += " AND m.usuario_id = @usuario_id";
                }

                SqlCommand cmd = new SqlCommand(query, conn);
                if (!string.IsNullOrWhiteSpace(txtFiltroUsuario.Text))
                {
                    cmd.Parameters.AddWithValue("@usuario_id", txtFiltroUsuario.Text);
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvMascotas.DataSource = dt;
                gvMascotas.DataBind();
            }


        }

        protected void btnVolverAdminPanel_Click(object sender, EventArgs e)
        {

            string tipo = Session["tipo"]?.ToString();
            if (tipo == "admin")
            {
                Response.Redirect("AdminPanel.aspx");
            }
            else if (tipo == "empleado")
            {
                Response.Redirect("EmpleadoPanel.aspx");
            }
            else
            {
                Response.Redirect("Login.aspx");
            }




        }
    }
}
