using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExamMethodLibrary.DAL;

namespace OnlineExamSystem.AdminNew
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadStudents();
            }
        }

        private void LoadStudents()
        {
            DataTable students = new DataTable();
            students = AdminDAL.getAllStudents();
            string html = "<table border='1' style='border-collapse:collapse;' class=\'table table-bordered\'>";
            html += "<tr class='head'><th>StudentID</th><th>Name</th><th>Email ID</th><th>Contact Number</th></tr>";

            foreach(DataRow student in students.Rows)
            {
                html += "<tr>";
                html += $"<td>{student["StudentId"]}</td>";
                html += $"<td>{student["FullName"]}</td>";
                html += $"<td>{student["Email"]}</td>";
                html += $"<td>{student["ContactNumber"]}</td>";
                html += "</tr>";
            }
            html += "</table>";

            divStudents.InnerHtml = html;
        }

        protected void gvCandidates_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ViewAnalysis")
            {
                int studentId = Convert.ToInt32(e.CommandArgument);
                // Redirect to student-specific analysis page
                Response.Redirect("~/Student/Home.aspx?studentId=" + studentId);
            }
        }
    }
}