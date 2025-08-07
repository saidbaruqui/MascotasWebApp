using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace MascotasWebApp
{
    public partial class Admin_Bitacora : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["tipo"] == null || (Session["tipo"].ToString() != "admin" && Session["tipo"].ToString() != "empleado"))
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                CargarVentas();
            }
        }

        private void CargarVentas()
        {
            string connStr = ConfigurationManager.ConnectionStrings["TiendaPerrosDB"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"
                    SELECT 
                        v.id AS VentaID,
                        u.id AS UsuarioID,
                        u.nombre AS Usuario,
                        ISNULL(promo.id, '-') AS PromoID,
                        ISNULL(CAST(promo.descripcion AS VARCHAR(200)), 'Sin Promoción') AS Promocion,
                        v.fecha AS Fecha,
                        v.total AS Total
                    FROM ventas v
                    INNER JOIN catalogo_usuarios u ON v.usuario_id = u.id
                    LEFT JOIN venta_detalles vd ON vd.venta_id = v.id
                    LEFT JOIN catalogo_promos promo ON vd.promo_id = promo.id
                    GROUP BY 
                        v.id, u.id, u.nombre, promo.id, CAST(promo.descripcion AS VARCHAR(200)), v.fecha, v.total
                    ORDER BY v.fecha DESC;
                    ";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvVentas.DataSource = dt;
                gvVentas.DataBind();

                // Calcular el total de todas las ventas
                decimal total = 0;
                foreach (DataRow row in dt.Rows)
                {
                    total += Convert.ToDecimal(row["Total"]);
                }

                lblTotalVentas.Text = "💰 Total de Ventas: $" + total.ToString("N2");
            }
        }

        protected void btnVolverAdminPanel_Click(object sender, EventArgs e)
        {
            string tipo = Session["tipo"]?.ToString();
            if (tipo == "admin")
                Response.Redirect("AdminPanel.aspx");
            else if (tipo == "empleado")
                Response.Redirect("EmpleadoPanel.aspx");
            else
                Response.Redirect("Login.aspx");
        }
    }
}
