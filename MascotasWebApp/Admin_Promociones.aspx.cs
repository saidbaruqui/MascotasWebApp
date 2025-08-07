using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace MascotasWebApp
{
    public partial class Admin_Promociones : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["TiendaPerrosDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarProductos();
                CargarPromociones();
        }

        private void CargarPromociones()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM catalogo_promos", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvPromociones.DataSource = dt;
                gvPromociones.DataBind();
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            DateTime fechaInicio, fechaFin;
            if (!DateTime.TryParse(txtFechaInicio.Text, out fechaInicio) ||
                !DateTime.TryParse(txtFechaFin.Text, out fechaFin))
            {
                lblMensaje.Text = " Formato de fecha no válido.";
                return;
            }

            if (fechaInicio >= fechaFin)
            {
                lblMensaje.Text = " La fecha de inicio debe ser menor que la fecha de fin.";
                return;
            }

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd;
                int promoId;

                if (string.IsNullOrEmpty(hfPromoID.Value)) // Crear nueva promoción
                {
                    cmd = new SqlCommand("sp_crear_promocion", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@descripcion", txtDescripcion.Text);
                    cmd.Parameters.AddWithValue("@porcentaje", Convert.ToDecimal(txtPorcentaje.Text));
                    cmd.Parameters.AddWithValue("@fecha_inicio", fechaInicio);
                    cmd.Parameters.AddWithValue("@fecha_fin", fechaFin);

                    // Ejecutar y obtener el nuevo ID
                    object result = cmd.ExecuteScalar();
                    promoId = Convert.ToInt32(result);
                }
                else // Actualizar promoción existente
                {
                    promoId = Convert.ToInt32(hfPromoID.Value);
                    cmd = new SqlCommand("sp_actualizar_promocion", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", promoId);
                    cmd.Parameters.AddWithValue("@descripcion", txtDescripcion.Text);
                    cmd.Parameters.AddWithValue("@porcentaje", Convert.ToDecimal(txtPorcentaje.Text));
                    cmd.Parameters.AddWithValue("@fecha_inicio", fechaInicio);
                    cmd.Parameters.AddWithValue("@fecha_fin", fechaFin);

                    cmd.ExecuteNonQuery();

                    // Eliminar relaciones existentes para evitar duplicados
                    SqlCommand deleteOld = new SqlCommand("DELETE FROM promociones_productos WHERE promo_id = @promoId", conn);
                    deleteOld.Parameters.AddWithValue("@promoId", promoId);
                    deleteOld.ExecuteNonQuery();
                }

                // Insertar relaciones con productos seleccionados
                foreach (ListItem item in cblProductos.Items)
                {
                    if (item.Selected)
                    {
                        SqlCommand cmdRelacion = new SqlCommand(
                            "INSERT INTO promociones_productos (promo_id, producto_id) VALUES (@promo, @producto)", conn);
                        cmdRelacion.Parameters.AddWithValue("@promo", promoId);
                        cmdRelacion.Parameters.AddWithValue("@producto", item.Value);
                        cmdRelacion.ExecuteNonQuery();
                    }
                }

                lblMensaje.Text = string.IsNullOrEmpty(hfPromoID.Value)
                    ? "Promoción registrada correctamente."
                    : "Promoción actualizada correctamente.";

                LimpiarFormulario();
                CargarPromociones();
            }
        }   

        protected void gvPromociones_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow fila = gvPromociones.Rows[index];
            int id = Convert.ToInt32(fila.Cells[0].Text);

            if (e.CommandName == "Editar")
            {
                hfPromoID.Value = id.ToString();
                txtDescripcion.Text = fila.Cells[1].Text;
                txtPorcentaje.Text = fila.Cells[2].Text;
                txtFechaInicio.Text = fila.Cells[3].Text;
                txtFechaFin.Text = fila.Cells[4].Text;
            }
            else if (e.CommandName == "Eliminar")
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();

                    // 1. Eliminar relaciones en promociones_productos
                    SqlCommand deleteRelaciones = new SqlCommand("DELETE FROM promociones_productos WHERE promo_id = @id", conn);
                    deleteRelaciones.Parameters.AddWithValue("@id", id);
                    deleteRelaciones.ExecuteNonQuery();

                    // 2. Eliminar la promoción en catalogo_promos
                    SqlCommand cmd = new SqlCommand("sp_eliminar_promocion", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();

                    lblMensaje.Text = "Promoción eliminada.";
                    CargarPromociones();
                }
            }

        }
        private void CargarProductos()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("SELECT id, nombre FROM catalogo_productos", conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                cblProductos.DataSource = reader;
                cblProductos.DataTextField = "nombre";
                cblProductos.DataValueField = "id";
                cblProductos.DataBind();
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



        private void LimpiarFormulario()
        {
            hfPromoID.Value = "";
            txtDescripcion.Text = "";
            txtPorcentaje.Text = "";
            txtFechaInicio.Text = "";
            txtFechaFin.Text = "";
        }
    }
}
