using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MascotasWebApp
{
    public partial class AdminPanel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["tipo"] == null || Session["tipo"].ToString() != "admin")
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                lblBienvenido.Text = "Bienvenido, " + Session["nombre"].ToString();
            }
        }
    }
}