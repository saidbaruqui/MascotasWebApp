using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI.WebControls;

namespace MascotasWebApp
{
    public partial class Catalogo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario_id"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            lblUsuario.Text = Session["nombre"]?.ToString() ?? "Invitado";

            if (!IsPostBack)
            {
                CargarCatalogo();
                CargarCarrito();
            }
        }

        private void CargarCatalogo()
        {
            string connStr = ConfigurationManager.ConnectionStrings["TiendaPerrosDB"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"
                    SELECT 
                        p.id,
                        p.nombre,
                        p.precio AS precio_original,
                        p.imagen,
                        pr.porcentaje,
                        pr.id AS promo_id,
                        CASE 
                            WHEN pr.porcentaje IS NOT NULL AND GETDATE() BETWEEN pr.fecha_inicio AND pr.fecha_fin
                            THEN p.precio - (p.precio * pr.porcentaje / 100)
                            ELSE p.precio
                        END AS precio_final
                    FROM catalogo_productos p
                    LEFT JOIN promociones_productos pp ON p.id = pp.producto_id
                    LEFT JOIN catalogo_promos pr ON pr.id = pp.promo_id";

                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Load(reader);

                rptCatalogo.DataSource = dt;
                rptCatalogo.DataBind();
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            var btn = (System.Web.UI.WebControls.Button)sender;
            int productoId = int.Parse(btn.CommandArgument);

            string connStr = ConfigurationManager.ConnectionStrings["TiendaPerrosDB"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"
                    SELECT 
                        p.id,
                        p.nombre,
                        p.precio AS precio_original,
                        pr.porcentaje,
                        pr.id AS promo_id,
                        CASE 
                            WHEN pr.porcentaje IS NOT NULL AND GETDATE() BETWEEN pr.fecha_inicio AND pr.fecha_fin
                            THEN p.precio - (p.precio * pr.porcentaje / 100)
                            ELSE p.precio
                        END AS precio_final
                    FROM catalogo_productos p
                    LEFT JOIN promociones_productos pp ON p.id = pp.producto_id
                    LEFT JOIN catalogo_promos pr ON pr.id = pp.promo_id
                    WHERE p.id = @id";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", productoId);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    DataTable carrito;
                    if (Session["carrito"] == null)
                    {
                        carrito = new DataTable();
                        carrito.Columns.Add("id", typeof(int));
                        carrito.Columns.Add("nombre", typeof(string));
                        carrito.Columns.Add("precio", typeof(decimal));
                        carrito.Columns.Add("cantidad", typeof(int));
                        carrito.Columns.Add("descuento", typeof(decimal));
                        carrito.Columns.Add("subtotal", typeof(decimal));
                        carrito.Columns.Add("promo_id", typeof(int));
                    }
                    else
                    {
                        carrito = (DataTable)Session["carrito"];
                    }

                    DataRow existente = carrito.Select($"id = {productoId}").FirstOrDefault();
                    decimal precioFinal = (decimal)reader["precio_final"];
                    decimal descuento = reader["porcentaje"] != DBNull.Value ? Convert.ToDecimal(reader["porcentaje"]) : 0;
                    object promoIdObj = reader["promo_id"];

                    if (existente != null)
                    {
                        existente["cantidad"] = (int)existente["cantidad"] + 1;
                    }
                    else
                    {
                        DataRow newRow = carrito.NewRow();
                        newRow["id"] = (int)reader["id"];
                        newRow["nombre"] = reader["nombre"].ToString();
                        newRow["precio"] = (decimal)reader["precio_original"];
                        newRow["cantidad"] = 1;
                        newRow["descuento"] = descuento;
                        newRow["subtotal"] = precioFinal;
                        newRow["promo_id"] = promoIdObj != DBNull.Value ? (int)promoIdObj : -1;
                        carrito.Rows.Add(newRow);
                    }

                    Session["carrito"] = carrito;
                    CargarCarrito();
                }
            }
        }

        private void CargarCarrito()
        {
            if (Session["carrito"] != null)
            {
                DataTable carrito = (DataTable)Session["carrito"];
                decimal total = 0;

                // Asegurarse de que las columnas necesarias existan
                if (!carrito.Columns.Contains("descuento"))
                    carrito.Columns.Add("descuento", typeof(decimal));
                if (!carrito.Columns.Contains("subtotal"))
                    carrito.Columns.Add("subtotal", typeof(decimal));
                if (!carrito.Columns.Contains("promo_id"))
                    carrito.Columns.Add("promo_id", typeof(int));

                string connStr = ConfigurationManager.ConnectionStrings["TiendaPerrosDB"].ConnectionString;

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();

                    foreach (DataRow row in carrito.Rows)
                    {
                        int productoId = (int)row["id"];
                        decimal precio = (decimal)row["precio"];
                        int cantidad = (int)row["cantidad"];
                        decimal descuento = 0;
                        int? promoId = null;

                        // Buscar descuento y promo activa
                        SqlCommand cmd = new SqlCommand(@"
                    SELECT TOP 1 cp.porcentaje, cp.id
                    FROM catalogo_promos cp
                    INNER JOIN promociones_productos pp ON cp.id = pp.promo_id
                    WHERE pp.producto_id = @producto_id
                      AND GETDATE() BETWEEN cp.fecha_inicio AND cp.fecha_fin", conn);
                        cmd.Parameters.AddWithValue("@producto_id", productoId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                descuento = reader["porcentaje"] != DBNull.Value ? Convert.ToDecimal(reader["porcentaje"]) : 0;
                                promoId = reader["id"] != DBNull.Value ? Convert.ToInt32(reader["id"]) : (int?)null;
                            }
                        }

                        row["descuento"] = descuento;
                        row["promo_id"] = promoId.HasValue ? promoId.Value : (object)DBNull.Value;

                        decimal subtotal = precio * cantidad;
                        decimal descuentoAplicado = subtotal * (descuento / 100);
                        subtotal -= descuentoAplicado;

                        row["subtotal"] = subtotal;
                        total += subtotal;
                    }
                }

                gvCarrito.DataSource = carrito;
                gvCarrito.DataBind();
                lblTotal.Text = "Total: " + total.ToString("C");
            }
            else
            {
                gvCarrito.DataSource = null;
                gvCarrito.DataBind();
                lblTotal.Text = "Total: $0.00";
            }
        }


        protected void gvCarrito_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                int idProducto = Convert.ToInt32(e.CommandArgument);

                DataTable carrito = Session["carrito"] as DataTable;

                if (carrito != null)
                {
                    // Buscar y eliminar el producto del carrito (DataTable)
                    DataRow rowToDelete = carrito.AsEnumerable()
                                                 .FirstOrDefault(r => Convert.ToInt32(r["id"]) == idProducto);

                    if (rowToDelete != null)
                    {
                        carrito.Rows.Remove(rowToDelete);
                        Session["carrito"] = carrito;
                    }

                    // Volver a enlazar el GridView
                    gvCarrito.DataSource = carrito;
                    gvCarrito.DataBind();
                    ActualizarTotalCarrito();
                }
            }
        }

        private void ActualizarTotalCarrito()
        {
            DataTable carrito = Session["carrito"] as DataTable;
            decimal total = 0;

            if (carrito != null)
            {
                foreach (DataRow row in carrito.Rows)
                {
                    total += Convert.ToDecimal(row["subtotal"]);
                }
            }

            lblTotal.Text = "Total: $" + total.ToString("N2");
        }


        protected void btnCheckout_Click(object sender, EventArgs e)
        {
            if (Session["carrito"] == null || ((DataTable)Session["carrito"]).Rows.Count == 0)
            {
                lblMensaje.Text = "Tu carrito está vacío.";
                return;
            }

            int usuarioId = Convert.ToInt32(Session["usuario_id"]);
            DataTable carrito = (DataTable)Session["carrito"];
            decimal total = carrito.AsEnumerable().Sum(row => Convert.ToDecimal(row["subtotal"]));

            string connStr = ConfigurationManager.ConnectionStrings["TiendaPerrosDB"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();

                try
                {
                    // Insertar la venta
                    SqlCommand cmdVenta = new SqlCommand(
                        "INSERT INTO ventas (usuario_id, total, fecha) OUTPUT INSERTED.id VALUES (@usuario_id, @total, GETDATE())",
                        conn, trans);
                    cmdVenta.Parameters.AddWithValue("@usuario_id", usuarioId);
                    cmdVenta.Parameters.AddWithValue("@total", total);
                    int ventaId = (int)cmdVenta.ExecuteScalar();

                    // Insertar los detalles de la venta con promo_id si aplica
                    foreach (DataRow row in carrito.Rows)
                    {
                        SqlCommand cmdDetalle = new SqlCommand(
                            @"INSERT INTO venta_detalles (venta_id, producto_id, precio, cantidad, promo_id)
                      VALUES (@venta_id, @producto_id, @precio, @cantidad, @promo_id)", conn, trans);

                        cmdDetalle.Parameters.AddWithValue("@venta_id", ventaId);
                        cmdDetalle.Parameters.AddWithValue("@producto_id", (int)row["id"]);
                        cmdDetalle.Parameters.AddWithValue("@precio", (decimal)row["precio"]);
                        cmdDetalle.Parameters.AddWithValue("@cantidad", (int)row["cantidad"]);

                        object promoId = row.Table.Columns.Contains("promo_id") && row["promo_id"] != DBNull.Value
                            ? (object)Convert.ToInt32(row["promo_id"])
                            : DBNull.Value;
                        cmdDetalle.Parameters.AddWithValue("@promo_id", promoId);

                        cmdDetalle.ExecuteNonQuery();
                    }

                    // Registrar en bitácora
                    SqlCommand cmdBitacora = new SqlCommand(
                        "INSERT INTO bitacora_ventas (usuario_id, accion, fecha) VALUES (@usuario_id, @accion, GETDATE())",
                        conn, trans);
                    cmdBitacora.Parameters.AddWithValue("@usuario_id", usuarioId);
                    cmdBitacora.Parameters.AddWithValue("@accion", "Realizó una compra");
                    cmdBitacora.ExecuteNonQuery();

                    trans.Commit();

                    Session["carrito"] = null;
                    CargarCarrito();
                    lblMensaje.Text = "¡Compra realizada con éxito!";
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    lblMensaje.Text = "Error al procesar la compra: " + ex.Message;
                }
            }
        }


        protected void btnRegistrarMascota_Click(object sender, EventArgs e)
        {
            Response.Redirect("RegistrarMascota.aspx");
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Login.aspx");
        }
    }
}
