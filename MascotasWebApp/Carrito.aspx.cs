using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace MascotasWebApp
{
    public partial class Carrito : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario_id"] == null) Response.Redirect("Login.aspx");
            if (!IsPostBack) CargarCarrito();
        }

        private void CargarCarrito()
        {
            if (Session["carrito"] == null) return;

            List<int> carrito = (List<int>)Session["carrito"];
            if (carrito.Count == 0) return;

            string ids = string.Join(",", carrito);
            string connStr = ConfigurationManager.ConnectionStrings["TiendaPerrosDB"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = $"SELECT id, nombre, precio, cantidad FROM catalogo_productos WHERE id IN ({ids})";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvCarrito.DataSource = dt;
                gvCarrito.DataBind();
            }
        }

        protected void gvCarrito_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                List<int> carrito = (List<int>)Session["carrito"];
                carrito.Remove(id);
                Session["carrito"] = carrito;
                CargarCarrito();
            }
        }

        protected void btnCheckout_Click(object sender, EventArgs e)
        {
            Response.Redirect("Checkout.aspx");
        }
    }
}
