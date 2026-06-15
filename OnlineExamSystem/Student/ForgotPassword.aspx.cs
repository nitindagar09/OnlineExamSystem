using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExamMethodLibrary.Student;

namespace OnlineExamSystem.Student
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnForgotPass_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim(); 
            if (email == "")
            {
                lblForgotPass.Text = "Enter Email for forgot password";
            }
            else
            {
                string password = StudentDAL.getForgotPassword(email);

                if (password == null)
                {
                    lblForgotPass.Text = "This Email is not registered.";
                    //lblForgotPass.CssClass = "text-danger";
                }
                else
                {
                    string ActivationId = StudentDAL.GetActivationId(email);

                    if (ActivationId == null)
                    {
                        lblForgotPass.Text = "There is some problem, Try after some time";
                    }
                    else
                    {
                        string encryptedId = Helper.CryptoHelper.Encrypt(ActivationId);

                        string subject = "ExamSoft - Online Exam System";

                        string body = $"<p>Your Account Password is {password}</p>" +
                                      $"<p>Regards,<br/>ExamSoft Team</p>";

                        bool mailSend = Helper.MailHelper.SendVerificationEmail(email, subject, body);

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