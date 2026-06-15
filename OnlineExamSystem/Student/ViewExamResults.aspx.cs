using ExamMethodLibrary.Student;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineExamSystem.Student
{
    public partial class ViewExamResults : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCompletedExams();
            }
        }
        private void LoadCompletedExams()
        {
            int studentId = Convert.ToInt32(Session["StudentID"]);
            DataTable dt = StudentDAL.GetCompletedExamsByStudent(studentId);

            gvExams.DataSource = dt;
            gvExams.DataBind();
        }
        protected void gvExams_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ViewReport")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                string examId = gvExams.DataKeys[rowIndex].Value.ToString();
                Response.Redirect("ExamReport.aspx?examId=" + examId);
            }
        }
    }
}