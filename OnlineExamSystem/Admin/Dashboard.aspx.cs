using ExamMethodLibrary.DAL;
using Newtonsoft.Json.Linq;
using OnlineExamSystem.Helper;
using System;
using System.Collections.Generic;
//using System.Web.Script.Services;
using System.Data; // adjust this to your actual DAL namespace
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace OnlineExamSystem
{
    public partial class AdminDashboard : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadSubjects();
                LoadSubjectsInDropDown();
                LoadExams();

                LoadExamsinDropdown();

                int examId = Convert.ToInt32(ddlExams.SelectedValue);
                LoadQuestionsByExamID(examId);
            }
        }

        //protected void btnShowExam_Click(object sender, EventArgs e)
        //{
        //    // Load exam data from DB here and bind it to GridView, Repeater, etc.
        //    //  if (!IsPostBack)
        //    LoadSubjectsInDropDown();
        //}


        //protected void btnShowQue_Click(object sender, EventArgs e)
        //{
        //    LoadExamsinDropdown();
        //}

        #region Subject
        private void LoadSubjects()
        {
            DataTable dt = SubjectDAL.GetSubjects(-1,Convert.ToInt32(Session["AdminID"])); // -1 means all
            gvSubjects.DataSource = dt;
            gvSubjects.DataBind();
        }

        protected void btnAddSubject_Click(object sender, EventArgs e)
        {
           
                string subjectName = txtSubjectName.Text.Trim();
                if (!string.IsNullOrEmpty(subjectName))
                {
                    int success = SubjectDAL.InsertSubject(subjectName, Convert.ToInt32(Session["AdminID"]));
                    if(success==1)
                    {
                        lblMessage.Text = "Subject added successfully.";
                    }
                    else if(success !=1)
                    {
                        lblMessage.Text = "Failed to add subject.";
                    }
                    else {
                        lblMessage.Text = "This subject is already exist.";
                        lblMessage.CssClass = "text-danger";
                    }
                    
                    txtSubjectName.Text = "";
                    LoadSubjects();
                LoadSubjectsInDropDown();
               
                }
           
        }

        protected void gvSubjects_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ToggleStatus")
            {
                string[] args = e.CommandArgument.ToString().Split('|');
                int subjectId = Convert.ToInt32(args[0]);
                bool currentStatus = Convert.ToBoolean(args[1]);
                bool newStatus = !currentStatus;

                SubjectDAL.SetSubjectStatus(subjectId, newStatus);
               
            }
            else if (e.CommandName == "DeleteSubject")
            {
                int subjectId = Convert.ToInt32(e.CommandArgument);
                SubjectDAL.DeleteSubject(subjectId);
                LoadExams();
                LoadSubjectsInDropDown();
                LoadExamsinDropdown();
                int examId = Convert.ToInt32(ddlExams.SelectedValue);
                LoadQuestionsByExamID(examId);

            }
            LoadSubjects();
        }

        protected void gvSubjects_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvSubjects.EditIndex = e.NewEditIndex;
            LoadSubjects();
        }

        protected void gvSubjects_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvSubjects.EditIndex = -1;
            LoadSubjects();
        }

        protected void gvSubjects_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int subjectId = Convert.ToInt32(gvSubjects.DataKeys[e.RowIndex].Value);

            GridViewRow row = gvSubjects.Rows[e.RowIndex];
            string updatedName = ((TextBox)row.Cells[1].Controls[0]).Text.Trim();

            if (!string.IsNullOrEmpty(updatedName))
            {
                bool updated = SubjectDAL.UpdateSubject(subjectId, updatedName);
                if (updated)
                {
                    gvSubjects.EditIndex = -1;
                    LoadSubjects();
                }
                else
                {
                    // Optional: Show error message
                }
            }
        }

        #endregion

        #region Exam
        private void LoadSubjectsInDropDown()
        {
            ddlSubjects.Items.Clear();
            DataTable subjects = SubjectDAL.GetSubjects(1,Convert.ToInt32(Session["AdminID"]));
            ddlSubjects.DataSource = subjects;
            ddlSubjects.DataTextField = "SubjectName";
            ddlSubjects.DataValueField = "SubjectID";
            ddlSubjects.DataBind();

            ddlSubjects.Items.Insert(0, new ListItem("-- Select Subject --", ""));
        }

        //This method returns the all list of the updated Subjects from the database by ajax call

        //[System.Web.Services.WebMethod]
        //public static List<string> GetSubjects()
        //{
        //    List<string> subjects = new List<string>();

        //    // Assuming SubjectDAL.GetSubjects(1) returns a DataTable with SubjectName column
            
        //    DataTable dtSubjects = SubjectDAL.GetSubjects(1, Convert.ToInt32(Session["AdminID"]));

        //    if (dtSubjects != null && dtSubjects.Rows.Count > 0)
        //    {
        //        foreach (DataRow row in dtSubjects.Rows)
        //        {
        //            if (row["SubjectName"] != DBNull.Value)
        //            {
        //                subjects.Add(row["SubjectName"].ToString());
        //            }
        //        }
        //    }

        //    return subjects;
        //}



        protected void LoadExams()
        {
            gvExams.DataSource = ExamDAL.GetExams(1, Convert.ToInt32(Session["AdminID"]));
            gvExams.DataBind();

            if (gvExams.EditIndex >= 0)
            {
                GridViewRow editRow = gvExams.Rows[gvExams.EditIndex];
                DropDownList ddlSubjectEdit = (DropDownList)editRow.FindControl("ddlSubjectEdit");

                ddlSubjectEdit.DataSource = SubjectDAL.GetSubjects(1, Convert.ToInt32(Session["AdminID"]));
                ddlSubjectEdit.DataTextField = "SubjectName";
                ddlSubjectEdit.DataValueField = "SubjectID";
                ddlSubjectEdit.DataBind();

                int selectedSubjectId = Convert.ToInt32(DataBinder.Eval(editRow.DataItem, "SubjectID"));
                ddlSubjectEdit.SelectedValue = selectedSubjectId.ToString();
            }
        }


        protected void btnAddExam_Click(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(ddlSubjects.SelectedValue))
            //{
            //    // Optional: Show message to select subject
            //    return;
            //}

            string subjectName = ddlSubjects.SelectedItem.Text;
            
            int subjectId = Convert.ToInt32(ddlSubjects.SelectedValue);
            string title = txtExamTitle.Text.Trim();
            DateTime examDate = Convert.ToDateTime(txtExamDate.Text);
            TimeSpan startTime = TimeSpan.Parse(txtStartTime.Text);
            TimeSpan endTime = TimeSpan.Parse(txtEndTime.Text);
            int duration = int.Parse(txtDuration.Text);

            bool success = ExamDAL.InsertExam(subjectId, subjectName, title, examDate, startTime, endTime, duration);

            if (success)
            {
                // Optionally clear input fields
                txtExamTitle.Text = txtExamDate.Text = txtStartTime.Text = txtEndTime.Text = txtDuration.Text = "";
                LoadExams();
                LoadExamsinDropdown();
            }
        }

        protected void gvExams_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvExams.EditIndex = e.NewEditIndex;
            LoadExams(); // Reload exams in edit mode
        }

        protected void gvExams_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvExams.EditIndex = -1;
            LoadExams(); // Reload normal view
        }

        protected void gvExams_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gvExams.Rows[e.RowIndex];

            int examId = Convert.ToInt32(gvExams.DataKeys[e.RowIndex].Value);
            int subjectId = Convert.ToInt32(((DropDownList)row.FindControl("ddlSubjectEdit")).SelectedValue);
            string examTitle = ((TextBox)row.FindControl("txtExamTitleEdit")).Text;
            DateTime examDate = Convert.ToDateTime(((TextBox)row.FindControl("txtExamDateEdit")).Text);
            TimeSpan startTime = TimeSpan.Parse(((TextBox)row.FindControl("txtStartTimeEdit")).Text);
            TimeSpan endTime = TimeSpan.Parse(((TextBox)row.FindControl("txtEndTimeEdit")).Text);
            int duration = Convert.ToInt32(((TextBox)row.FindControl("txtDurationEdit")).Text);

            bool updated = ExamDAL.UpdateExam(examId, subjectId, examTitle, examDate, startTime, endTime, duration);

            gvExams.EditIndex = -1;
            LoadExams();
        }


        #endregion


        #region Questions
        private void LoadExamsinDropdown()
        {
            DataTable dt = ExamDAL.GetExams(1, Convert.ToInt32(Session["AdminID"])); ; // assumes you already created this method

            ddlExams.DataSource = dt;
            ddlExams.DataTextField = "ExamTitle";
            ddlExams.DataValueField = "ExamID";
            ddlExams.DataBind();

            ddlExams.Items.Insert(0, new ListItem("-- Select Exam --", "0"));
        }

        protected void btnSaveQuestion_Click(object sender, EventArgs e)
        {
            int examId = Convert.ToInt32(ddlExams.SelectedValue);
            string question = txtQuestion.Text.Trim();
            string a = txtOptionA.Text.Trim();
            string b = txtOptionB.Text.Trim();
            string c = txtOptionC.Text.Trim();
            string d = txtOptionD.Text.Trim();
            string correct = txtCorrectOption.Text.Trim().ToUpper();
            int marks = Convert.ToInt32(txtMarks.Text.Trim());

            bool result = QuestionDAL.InsertQuestion(examId, question, a, b, c, d, Convert.ToChar(correct), marks);

            if (result)
            {
                // clear form
                ddlExams.SelectedIndex = 0;
                txtQuestion.Text = txtOptionA.Text = txtOptionB.Text = txtOptionC.Text = txtOptionD.Text = txtCorrectOption.Text = txtMarks.Text = "";

                LoadQuestionsByExamID(examId); // reload grid
            }
        }
        private void LoadQuestionsByExamID(int examID)
        {
            DataTable dt = QuestionDAL.GetQuestionsByExamId(examID); // returns all questions with ExamTitle if needed
            gvQuestions.DataSource = dt;
            gvQuestions.DataBind();
        }

        protected void ddlExams_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedExamId = Convert.ToInt32(ddlExams.SelectedValue);
            if (selectedExamId > 0)
            {
                LoadQuestionsByExamID(selectedExamId);
            }
            else
            {
                gvQuestions.DataSource = null;
                gvQuestions.DataBind();
            }
        }

        protected void gvQuestions_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvQuestions.EditIndex = e.NewEditIndex;
            int selectedExamId = Convert.ToInt32(ddlExams.SelectedValue);
            LoadQuestionsByExamID(selectedExamId);
        }

        protected void gvQuestions_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvQuestions.EditIndex = -1;
            int selectedExamId = Convert.ToInt32(ddlExams.SelectedValue);
            LoadQuestionsByExamID(selectedExamId);
        }

        protected void gvQuestions_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int selectedExamId = Convert.ToInt32(ddlExams.SelectedValue);
            int questionId = Convert.ToInt32(gvQuestions.DataKeys[e.RowIndex].Value);

            GridViewRow row = gvQuestions.Rows[e.RowIndex];

            string questionText = ((TextBox)row.FindControl("txtQuestionText")).Text.Trim();
            string a = ((TextBox)row.FindControl("txtOptionA")).Text.Trim();
            string b = ((TextBox)row.FindControl("txtOptionB")).Text.Trim();
            string c = ((TextBox)row.FindControl("txtOptionC")).Text.Trim();
            string d = ((TextBox)row.FindControl("txtOptionD")).Text.Trim();
            string correct = ((TextBox)row.FindControl("txtCorrect")).Text.Trim();
            int marks = Convert.ToInt32(((TextBox)row.FindControl("txtMarks")).Text.Trim());

            QuestionDAL.UpdateQuestion(questionId, selectedExamId, questionText, a, b, c, d, Convert.ToChar(correct), marks);

            gvQuestions.EditIndex = -1;
            LoadQuestionsByExamID(selectedExamId);
        }

        protected void gvQuestions_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int questionId = Convert.ToInt32(gvQuestions.DataKeys[e.RowIndex].Value);
            bool isDeleted = QuestionDAL.DeleteQuestion(questionId); // Make sure this method exists

            if (isDeleted)
            {
                int selectedExamId = Convert.ToInt32(ddlExams.SelectedValue);
                LoadQuestionsByExamID(selectedExamId);
            }
            else
            {
                // Optional: Show error message if deletion fails
            }
        }

        protected void btnOpenAI_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Admin/AIQuestions.aspx");
        }



        #endregion





    }


}