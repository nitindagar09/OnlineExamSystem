using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExamMethodLibrary.DAL;
using Newtonsoft.Json;
using OnlineExamSystem.Helper;

namespace OnlineExamSystem
{
    public partial class AdminLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();
            

            



            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                lblLogin.Text = "Email and Password are required.";
                return;
            }

            AdminLoginResult result = AdminDAL.IsAdminValid(email, password);

            if (result.ResultCode == 0)
            {
                lblLogin.Text = "Invalid email or password.";
            }
            else if (result.ResultCode == 1)
            {
                lblLogin.Text = "Your account is inactive. Please contact admin.";
            }
            else if (result.ResultCode == 2 && result.AdminId.HasValue)
            {
                Session["AdminID"] = result.AdminId.Value;
                Response.Redirect("./Home.aspx");
            }
            else
            {
                lblLogin.Text = "Unexpected error occurred.";
            }
        }

        

    }
}