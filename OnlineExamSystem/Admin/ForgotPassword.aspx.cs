using ExamMethodLibrary.DAL;
using OnlineExamSystem.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineExamSystem
{
    public partial class ForgotLoginPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnForgotPass_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            if(email == "")
            {
                lblForgotPass.Text = "Enter Email for forgot password";
            }
            else
            {
                string password = AdminDAL.getForgotPassword(email);

                if(password == null)
                {
                    lblForgotPass.Text = "This Email is not registered.";
                    //lblForgotPass.CssClass = "text-danger";
                }
                else
                {
                    string ActivationId = AdminDAL.GetActivationId(email);

                    if (ActivationId == null)
                    {
                        lblForgotPass.Text = "There is some problem, Try after some time";
                    }
                    else
                    {
                        string encryptedId = CryptoHelper.Encrypt(ActivationId);

                        string subject = "Admin Account Activation - Online Exam System";

                        string body = $"<p>Your Account Password is {password}</p>" +
                                      $"<p>Regards,<br/>Online Exam System Team</p>";

                        bool mailSend = MailHelper.SendVerificationEmail(email, subject, body);

                        if (mailSend)
                        {
                            lblForgotPass.Text = "Your password is send to your email id, Please check it";
                            lblForgotPass.CssClass = "text-success";
                        }
                        else
                        {
                            lblForgotPass.Text = "Try again!";
                            lblForgotPass.CssClass = "text-danger";
                        }
                    }
                }
            }
        }
    }
}