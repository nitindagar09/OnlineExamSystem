using ExamMethodLibrary.Student;
using OnlineExamSystem.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineExamSystem.Student
{
    public partial class Exams : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadExams();
        }

        private void LoadExams()
        {
            try
            {
                DataTable examsData = StudentDAL.GetAllExams();

                if (examsData.Rows.Count > 0)
                {

                    string divContent = "<table border='1' style='border-collapse:collapse;' class='table table-bordered\'>";
                    divContent += "<tr class='head'> <th>Subject</th><th>Title</th><th>Exam Date</th><th></th> </tr>";

                    foreach (DataRow row in examsData.Rows)
                    {
                        string ExamID = row["ExamID"].ToString();
                        string SubjectName = row["SubjectName"].ToString();
                        string ExamTitle = row["ExamTitle"].ToString();
                        string ExamDate = row["ExamDate"].ToString();


                        string EncryptedExamID = CryptoHelper.Encrypt(ExamID);
                        string EncryptedStudentID = CryptoHelper.Encrypt(Session["StudentID"].ToString());
                        string url1 = $"Response.aspx?key={EncryptedExamID}";
                        string url = $"Report.aspx?exam={EncryptedExamID}&student={EncryptedStudentID}";
                        // Always allow access - remove validations
                        string disableAttribute = $"onclick=\"window.location.href='{url}'\"";



                        if (DateTime.Parse(ExamDate) < DateTime.Now.Date)
                        {
                            divContent += "<tr>";
                            divContent += $"<td>{SubjectName}</td>";
                            divContent += $"<td>{ExamTitle}</td>";
                            divContent += $"<td>{ExamDate.Split(' ')[0]}</td>";
                            //divContent += $"<td><button type='button' class='btn' {disableAttribute}>View Report</button></td>";
                            divContent += $"<td><a class='ViewReportLink' href='{url}'>View Report</button></td>";
                            divContent += "</tr>";

                        }

                    }
                    divExams.InnerHtml = divContent + "</table>";
                }
                else
                {
                    divExams.InnerHtml = "<p>No Previous exam available.</p>";
                }
            }
            catch
            {
                // Log or handle error if needed
            }
        }
    }
}