using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineExamSystem
{
    public partial class AdminMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AdminID"] == null)
            {
                Response.Redirect("~/Admin/Login.aspx");
            }
           
        }

        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            if (Session["StudentID"] != null && Convert.ToInt32(Session["StudentID"]) > 0)
            {

            }
           // Response.Redirect("../Home.aspx");
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/Admin/Login.aspx");
        }
    }
}