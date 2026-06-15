using ExamMethodLibrary.DAL;
using OnlineExamSystem.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExamMethodLibrary.Student;
using Microsoft.Ajax.Utilities;
using System.IO;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace OnlineExamSystem.Student
{
    public partial class Registration : System.Web.UI.Page
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnVerifyOtp_Click(object sender, EventArgs e)
        {
            txtFullName.Text = null;
            txtEmail.Text = null;
            txtPassword.Text = null;
            txtContact.Text = null;

            string otp = Session["OTP"].ToString();
            
            string otpUser = txtOTP.Text;

            txtOTP.Text = null;

            if (otpUser.IsNullOrWhiteSpace())
            {
                lblMessage.Text = "Please enter otp for activate your account!"; 
            }
            else if(otpUser == otp)
            {
                Guid activationId = Guid.Parse(Session["AId"].ToString());
                bool activated = StudentDAL.ActivateStudentByActivationId(activationId);

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

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string name = txtFullName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();
            string contact = txtContact.Text.Trim();
            string imageBase64 = hiddenImageData.Value;

            //var embedding = GetFaceEmbedding(imageBase64);
            if (imageBase64 == null)
            {
                lblMessage.Text = "Face not detected. Try again.";
                return;
            }
            //string embeddingString = string.Join(",", embedding.Select(x => x.ToString("G")));


            string fileName = email.ToString() + ".jpg";
            string relativePath = "~/FaceImages/" + fileName;
            string fullPath = Server.MapPath(relativePath);
            byte[] imageBytes = Convert.FromBase64String(imageBase64);
            File.WriteAllBytes(fullPath, imageBytes);

            string activationId = Guid.NewGuid().ToString();

            

            int isRegistered = StudentDAL.checkStudentRegistered(email);

            if (isRegistered == 1)
            {
                lblMessage.Text = "This email is already registered, you log in.";
                lblMessage.CssClass = "text-success";
            }
            else
            {
                Session["AId"] = activationId;

                // Call DAL to insert data (with IsActive = 0 initially)
                bool isInserted = StudentDAL.InsertStudent(name, email, password, contact, activationId, imageBase64, relativePath);

                if (isInserted)
                {
                    // Encrypt the activationId
                    string encryptedId = CryptoHelper.Encrypt(activationId); // We’ll create CryptoHelper next

                    // Create verification URL
                    string url = Request.Url.GetLeftPart(UriPartial.Authority) + "/AdminActivation.aspx?key=" + encryptedId;

                    var otp = new Random().Next(100000, 999999).ToString();

                    Session["OTP"] = otp;

                    string subject = "Admin Account Activation - Online Exam System";
                    string body = $"<p>Hi {name},</p>" +
                                  $"<p>Your OTP for Activate your account: <strong>{otp}</strong></p>" +
                                  $"<p>Regards,<br/>Online Exam System Team</p>";

                    // Send email
                    bool mailSent = MailHelper.SendVerificationEmail(email, subject, body);

                    if (mailSent)
                    {
                        lblMessage.Text = "Enter OTP ";
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
        private float[] GetFaceEmbedding(string base64Image)
        {
            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(new { image = base64Image });
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = client.PostAsync("http://127.0.0.1:5000/get-embedding", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseString = response.Content.ReadAsStringAsync().Result;
                    var result = JsonConvert.DeserializeObject<EmbeddingResponse>(responseString);
                    return result.embedding;
                }
            }
            return null;
        }

        class EmbeddingResponse
        {
            public float[] embedding { get; set; }
        }
    }
}