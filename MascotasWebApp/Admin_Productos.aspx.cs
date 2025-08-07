using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Web.UI.WebControls;

namespace MascotasWebApp
{
    public partial class Admin_Productos : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["TiendaPerrosDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarProductos();
            }
        }

        private void CargarProductos()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM catalogo_productos", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvProductos.DataSource = dt;
                gvProductos.DataBind();
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            string rutaRelativa = "";

            if (fuImagen.HasFile)
            {
                string nombreArchivo = Path.GetFileName(fuImagen.FileName);
                string carpeta = Server.MapPath("~/img/productos/");

                // Crear carpeta si no existe
                if (!Directory.Exists(carpeta))
                {
                    Directory.CreateDirectory(carpeta);
                }

                string rutaFisica = Path.Combine(carpeta, nombreArchivo);
                fuImagen.SaveAs(rutaFisica);

                // Ruta relativa para guardar en la base de datos
                rutaRelativa = "~/img/productos/" + nombreArchivo;
            }

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd;
                if (string.IsNullOrEmpty(hfProductoID.Value)) // Nuevo
                {
                    cmd = new SqlCommand("sp_crear_producto", conn);
                }
                else // Editar
                {
                    cmd = new SqlCommand("sp_actualizar_producto", conn);
                    cmd.Parameters.AddWithValue("@id", hfProductoID.Value);
                }

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@nombre", txtNombre.Text);
                cmd.Parameters.AddWithValue("@precio", Convert.ToDecimal(txtPrecio.Text));
                cmd.Parameters.AddWithValue("@cantidad", Convert.ToInt32(txtCantidad.Text));
                cmd.Parameters.AddWithValue("@imagen", rutaRelativa);
                cmd.Parameters.AddWithValue("@descripcion", txtDescripcion.Text);
                cmd.Parameters.AddWithValue("@categoria", ddlCategoria.SelectedValue);

                conn.Open();
                cmd.ExecuteNonQuery();
                CargarProductos();
                lblMensaje.Text = string.IsNullOrEmpty(hfProductoID.Value) ? "Producto guardado." : "Producto actualizado.";
                hfProductoID.Value = "";
                LimpiarFormulario();
            }
        }


        protected void gvProductos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Editar")
            {
                int id = Convert.ToInt32(gvProductos.DataKeys[index].Values["id"]);
                decimal precio = Convert.ToDecimal(gvProductos.DataKeys[index].Values["precio"]);
                string descripcion = gvProductos.DataKeys[index].Values["descripcion"].ToString();
                int cantidad = Convert.ToInt32(gvProductos.DataKeys[index].Values["cantidad"]);

                GridViewRow row = gvProductos.Rows[index];

                hfProductoID.Value = id.ToString();
                txtNombre.Text = row.Cells[1].Text;
                txtPrecio.Text = precio.ToString("0.##");
                txtCantidad.Text = cantidad.ToString();
                txtDescripcion.Text = descripcion;
                ddlCategoria.SelectedValue = row.Cells[5].Text;
            }
            else if (e.CommandName == "Eliminar")
            {
                int id = Convert.ToInt32(gvProductos.DataKeys[index].Values["id"]);

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    SqlCommand cmd = new SqlCommand("sp_eliminar_producto", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                CargarProductos(); // Refresca la tabla
            }
        }






        protected void btnClear_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            lblMensaje.Text = ""; // Clear any previous messages
        }

        private void LimpiarFormulario()
        {
            hfProductoID.Value = "";
            txtNombre.Text = "";
            txtPrecio.Text = "";
            txtCantidad.Text = "";
            txtDescripcion.Text = "";
            ddlCategoria.SelectedIndex = 0; // Resets to the first item
        }


        protected void btnVolverAdminPanel_Click(object sender, EventArgs e)
        {

            string tipo = Session["tipo"]?.ToString();
            if (tipo=="admin")
            {
                Response.Redirect("AdminPanel.aspx");
            }
            else if (tipo=="empleado")
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
