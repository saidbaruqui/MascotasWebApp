using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace MascotasWebApp
{
    public partial class Admin_Proveedores : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["TiendaPerrosDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarProveedores();
        }

        private void CargarProveedores()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM proveedores", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvProveedores.DataSource = dt;
                gvProveedores.DataBind();
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd;
                if (string.IsNullOrEmpty(hfProveedorID.Value))
                {
                    cmd = new SqlCommand("sp_crear_proveedor", conn);
                }
                else
                {
                    cmd = new SqlCommand("sp_actualizar_proveedor", conn);
                    cmd.Parameters.AddWithValue("@id", hfProveedorID.Value);
                }

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nombre", txtNombre.Text);
                cmd.Parameters.AddWithValue("@telefono", txtTelefono.Text);
                cmd.Parameters.AddWithValue("@correo", txtCorreo.Text);
                cmd.Parameters.AddWithValue("@direccion", txtDireccion.Text);

                conn.Open();
                cmd.ExecuteNonQuery();

                lblMensaje.Text = string.IsNullOrEmpty(hfProveedorID.Value)
                    ? "Proveedor registrado correctamente."
                    : "Proveedor actualizado correctamente.";

                LimpiarFormulario();
                CargarProveedores();
            }
        }

        protected void gvProveedores_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow fila = gvProveedores.Rows[index];
            int id = Convert.ToInt32(fila.Cells[0].Text);

            if (e.CommandName == "Editar")
            {
                hfProveedorID.Value = id.ToString();
                txtNombre.Text = fila.Cells[1].Text;
                txtTelefono.Text = fila.Cells[2].Text;
                txtCorreo.Text = fila.Cells[3].Text;
                txtDireccion.Text = fila.Cells[4].Text;
            }
            else if (e.CommandName == "Eliminar")
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    SqlCommand cmd = new SqlCommand("sp_eliminar_proveedor", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    lblMensaje.Text = "Proveedor eliminado.";
                    CargarProveedores();
                }
            }
        }

        private void LimpiarFormulario()
        {
            hfProveedorID.Value = "";
            txtNombre.Text = "";
            txtTelefono.Text = "";
            txtCorreo.Text = "";
            txtDireccion.Text = "";
        }

        protected void btnVolverAdminPanel_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminPanel.aspx");
        }

    }
}
