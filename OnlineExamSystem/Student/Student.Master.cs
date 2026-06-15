using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineExamSystem.Student
{
    public partial class Student : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["StudentID"] == null)
            {
                // If no session, redirect to login page
                Response.Redirect("~/Student/Login.aspx");
            }
           
        }

        protected void btnLogOut_Click(object sender, EventArgs e)
        {
           
           // Response.Redirect("../Home.aspx");
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/Student/Login.aspx");

        }
    }
}