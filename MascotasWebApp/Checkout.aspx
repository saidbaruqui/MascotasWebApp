using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace MascotasWebApp
{
    public partial class Checkout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario_id"] == null || Session["carrito"] == null)
                Response.Redirect("Login.aspx");
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            List<int> carrito = (List<int>)Session["carrito"];
            if (carrito.Count == 0) return;

            string connStr = ConfigurationManager.ConnectionStrings["TiendaPerrosDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();

                try
                {
                    decimal total = 0;
                    foreach (int id in carrito)
                    {
                        SqlCommand cmdPrecio = new SqlCommand("SELECT precio FROM catalogo_productos WHERE id=@id", conn, trans);
                        cmdPrecio.Parameters.AddWithValue("@id", id);
                        total += Convert.ToDecimal(cmdPrecio.ExecuteScalar());
                    }

                    SqlCommand cmdVenta = new SqlCommand("INSERT INTO ventas (usuario_id, total) OUTPUT INSERTED.id VALUES (@usuario_id, @total)", conn, trans);
                    cmdVenta.Parameters.AddWithValue("@usuario_id", Session["usuario_id"]);
                    cmdVenta.Parameters.AddWithValue("@total", total);
                    int ventaId = (int)cmdVenta.ExecuteScalar();

                    foreach (int id in carrito)
                    {
                        SqlCommand cmdProducto = new SqlCommand("SELECT precio FROM catalogo_productos WHERE id=@id", conn, trans);
                        cmdProducto.Parameters.AddWithValue("@id", id);
                        decimal precio = Convert.ToDecimal(cmdProducto.ExecuteScalar());

                        SqlCommand cmdDetalle = new SqlCommand("INSERT INTO venta_detalles (venta_id, producto_id, precio, cantidad) VALUES (@venta_id, @producto_id, @precio, @cantidad)", conn, trans);
                        cmdDetalle.Parameters.AddWithValue("@venta_id", ventaId);
                        cmdDetalle.Parameters.AddWithValue("@producto_id", id);
                        cmdDetalle.Parameters.AddWithValue("@precio", precio);
                        cmdDetalle.Parameters.AddWithValue("@cantidad", 1);
                        cmdDetalle.ExecuteNonQuery();
                    }

                    SqlCommand cmdBitacora = new SqlCommand("INSERT INTO bitacora_ventas (usuario_id, accion) VALUES (@usuario_id, 'Compra realizada')", conn, trans);
                    cmdBitacora.Parameters.AddWithValue("@usuario_id", Session["usuario_id"]);
                    cmdBitacora.ExecuteNonQuery();

                    trans.Commit();
                    Session["carrito"] = new List<int>();
                    Response.Redirect("Confirmacion.aspx");
                }
                catch
                {
                    trans.Rollback();
                }
            }
        }
    }
}
