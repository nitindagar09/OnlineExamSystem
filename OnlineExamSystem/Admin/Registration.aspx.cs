using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OnlineExamSystem.Helper;
using ExamMethodLibrary.DAL;
using System.Data.SqlClient;
using ExamMethodLibrary.Student;
using Microsoft.Ajax.Utilities;
using System.IO;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;


namespace OnlineExamSystem
{
    public partial class AdminRegistration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }



        protected void btnVerifyOtp_Click1(object sender, EventArgs e)
        {
            txtFullName.Text = null;
            txtEmail.Text = null;
            txtPassword.Text = null;
            txtContact.Text = null;

            string otp = Session["OTPAdmin"].ToString();

            string otpUser = txtOTP.Text;

            txtOTP.Text = null;

            if (otpUser.IsNullOrWhiteSpace())
            {
                lblMessage.Text = "Please enter otp for activate your account!";
            }
            else if (otpUser == otp)
            {
                Guid activationId = Guid.Parse(Session["AdminId"].ToString());
                bool activated = AdminDAL.ActivateAdminByActivationId(activationId);

                if (activated)
                {
                    lblMessage.Text = "Your account has been successfully activated. You may now log in.";
                    lblMessage.CssClass = " text-success";
                }
                else
                {
                    lblMessage.Text = "Activation failed or link is invalid.";
                    lblMessage.CssClass = " text-danger";
                }
            }
            else
            {
                lblMessage.Text = "Wrong OTP!";
            }
        }

        protected void btnRegister_Click1(object sender, EventArgs e)
        {
            string name = txtFullName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();
            string contact = txtContact.Text.Trim();
         

            

            string activationId = Guid.NewGuid().ToString();

            int isRegistered = AdminDAL.checkAdminRegistered(email);

            if (isRegistered == 1)
            {
                lblMessage.Text = "This email is already registered, you log in.";
                lblMessage.CssClass = "text-success";
            }
            else
            {
                Session["AdminId"] = activationId;

                // Call DAL to insert data (with IsActive = 0 initially)
                bool isInserted = AdminDAL.InsertAdmin(name, email, password, contact, activationId);

                if (isInserted)
                {
                    // Encrypt the activationId
                    string encryptedId = CryptoHelper.Encrypt(activationId); // We’ll create CryptoHelper next

                    // Create verification URL
                    string url = Request.Url.GetLeftPart(UriPartial.Authority) + "/AdminActivation.aspx?key=" + encryptedId;

                    var otp = new Random().Next(100000, 999999).ToString();

                    Session["OTPAdmin"] = otp;

                    string subject = "Admin Account Activation - Online Exam System";
                    string body = $"<p>Hi {name},</p>" +
                                  $"<p>Your OTP for Activate your account: <strong>{otp}</strong></p>" +
                                  $"<p>Regards,<br/>Online Exam System Team</p>";

                    // Send email
                    bool mailSent = MailHelper.SendVerificationEmail(email, subject, body);

                    if (mailSent)
                    {
                        lblMessage.Text = "Registration successful! Please enter otp to activate your account, it sent to your email.";
                        lblMessage.CssClass = "text-success";
                    }
                    else
                    {
                        lblMessage.Text = "Registration successful, but email could not be sent.";
                        lblMessage.CssClass = "text-warning";
                    }
                }
                else
                {
                    lblMessage.Text = "Registration failed. Please try again.";
                    lblMessage.CssClass = "text-danger";
                }
            }
        }

        
    }
}
    