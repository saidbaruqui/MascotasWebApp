using System;

namespace MascotasWebApp
{
    public partial class Confirmacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario_id"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            Session["carrito"] = null;
        }
    }
}
