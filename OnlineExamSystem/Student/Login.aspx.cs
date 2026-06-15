using ExamMethodLibrary.DAL;
using OnlineExamSystem.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExamMethodLibrary.Student;
using static System.Net.WebRequestMethods;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace OnlineExamSystem.Student
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected async void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            string password = txtPassword.Text;
            string imageBase64 = hiddenImageData.Value;

            string storedImageBase64 = AdminDAL.getstoredImageBase64String(email);

            if (storedImageBase64 == null)
            {
                lblLogin.Text = "Invalid Credentials";
                return;
            }

            // float[] storedEmbedding = storedEmbeddingString.Split(',').Select(s => float.Parse(s)).ToArray();

            //float[] liveEmbedding = GetFaceEmbedding(imageBase64);

            //if (liveEmbedding == null)
            //{
            //    lblLogin.Text = "Error processing face image.";
            //    return;
            //}

            if (string.IsNullOrEmpty(email) && string.IsNullOrEmpty(password) && string.IsNullOrEmpty(imageBase64))  
            {
                lblLogin.Text = "Required Credentials";
            }
            else
            {
                int result = StudentDAL.IsStudentValid(email, password);
                if (result != 0)
                {
                    if (await VerifyFace(imageBase64, storedImageBase64))
                    {
                        Session["StudentID"] = result;
                        Response.Redirect("~/Student/Home.aspx", false);
                }
                else
                {
                    lblLogin.Text = "Face Not Matched.";
                }
            }
                else
                {
                    lblLogin.Text = "Wrong Email and Password";
                }
            }
               
        }


        public async Task<bool> VerifyFace(string base64Image1, string base64Image2)
        {
            try
            {
                string apiUrl = "http://localhost:5000/compare-faces";

                using (var client = new HttpClient())
                {
                    using (var content = new MultipartFormDataContent())
                    {
                        byte[] imageBytes1 = Convert.FromBase64String(base64Image1);
                        byte[] imageBytes2 = Convert.FromBase64String(base64Image2);

                        var byteContent1 = new ByteArrayContent(imageBytes1);
                        byteContent1.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");

                        var byteContent2 = new ByteArrayContent(imageBytes2);
                        byteContent2.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");

                        content.Add(byteContent1, "img1", "img1.jpg");
                        content.Add(byteContent2, "img2", "img2.jpg");

                        HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                        if (response.IsSuccessStatusCode)
                        {
                            string jsonString = await response.Content.ReadAsStringAsync();
                            dynamic result = JsonConvert.DeserializeObject(jsonString);

                            return result.verified == true;
                        }
                        else
                        {
                            // Log error or show a message
                            return false;
                        }
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            
        }


        class EmbeddingResponse
        {
            public float[] embedding { get; set; }
        }
    }
}