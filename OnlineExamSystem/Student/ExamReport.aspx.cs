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
    public partial class ExamReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int examId = Convert.ToInt32(Request.QueryString["examId"]);
                int studentId = Convert.ToInt32(Session["StudentID"]);
                BindReport(studentId, examId);
            }
        }
            private void BindReport(int studentId, int examId)
        {
            DataSet ds = StudentDAL.GetStudentExamReport(studentId, examId);

            if (ds != null && ds.Tables.Count > 0)
            {
                gvQuestionReport.DataSource = ds.Tables[0];
                gvQuestionReport.DataBind();

                if (ds.Tables.Count > 1)
                {
                    gvSummaryReport.DataSource = ds.Tables[1];
                    gvSummaryReport.DataBind();
                }
            }
        }
    }
}