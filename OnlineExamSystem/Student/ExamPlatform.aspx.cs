using ExamMethodLibrary.DAL;
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
    public partial class ExamPlatform : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetValidUntilExpires(false);
            Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            if (Request["__EVENTTARGET"] == "AutoSubmit")
            {
                // TODO: Save selected answers to session or DB
                Response.Write("<script>alert('Responses submitted!');</script>");
            }

            if (!IsPostBack)
            {
                Session["QuestionNumber"] = 1;
                divReport.Visible = false;
                if (Session["ExamStartTime"] == null)
                {
                    // Exam duration: 1 hour
                    Session["ExamStartTime"] = DateTime.Now;
                }
                string examId = CryptoHelper.Decrypt((Request.QueryString["key"]).ToString());
                int id = int.Parse(examId);
                ViewState["ExamID"] = id;

                int studentId = Convert.ToInt32(Session["StudentID"]);
                int alreadyTaken = StudentDAL.CheckStudentExamTaken(studentId, id);

                if (alreadyTaken == 1)
                {
                    FormView1.Visible = false;
                    divReport.Visible = true;
                    string encryptedExamId = CryptoHelper.Encrypt(id.ToString());
                    string encryptedStudentId = CryptoHelper.Encrypt(studentId.ToString());
                    string script = $@"
       <script type='text/javascript'>
        if (typeof timerInterval !== 'undefined') {{
                   clearInterval(timerInterval);
               }}
          alert('You have already taken this exam. Redirecting to report...');
               window.location.href = 'Report.aspx?exam={encryptedExamId}&student={encryptedStudentId}';
       </script>";
                    ClientScript.RegisterStartupScript(this.GetType(), "ExamTakenNotice", script);
                    return;
                }
                else
                {
                    FormView1.Visible = true;
                    divReport.Visible = false;
                    LoadQuestions(id);
                }

                string eventTarget = Request["__EventTarget"];
                if (eventTarget == "AutoSubmit")
                {
                    SubmitStudentExam();
                }
            }
        }

        private void LoadQuestions(int examId)
        {
            DataTable questionsData = QuestionDAL.GetQuestionsByExamId(examId);
            FormView1.DataSource = questionsData;
            FormView1.DataBind();

            UpdateButtonsVisibility();
        }
        protected void FormView1_DataBound(object sender, EventArgs e)
        {
            if (FormView1.CurrentMode == FormViewMode.ReadOnly && FormView1.DataItem != null)
            {
                DataRowView row = (DataRowView)FormView1.DataItem;

                RadioButtonList rbl = (RadioButtonList)FormView1.FindControl("rblOptions");

                rbl.Items.Clear();
                rbl.Items.Add(new ListItem(" A. " + row["OptionB"].ToString(), "B"));
                rbl.Items.Add(new ListItem(" B. " + row["OptionC"].ToString(), "C"));
                rbl.Items.Add(new ListItem(" C. " + row["OptionD"].ToString(), "D"));
                rbl.Items.Add(new ListItem(" D. " + row["OptionA"].ToString(), "A"));

                HiddenField hf = (HiddenField)FormView1.FindControl("hfQuestionID");

                if (hf != null && rbl != null)
                {
                    int questionId = Convert.ToInt32(hf.Value);
                    if (SelectedAnswers.ContainsKey(questionId))
                    {
                        rbl.SelectedValue = SelectedAnswers[questionId];
                    }
                }

            }


            UpdateButtonsVisibility();
        }

        private void UpdateButtonsVisibility()
        {
            Button btnSubmit = (Button)FormView1.FindControl("btnSubmit");
            Button btnNext = (Button)FormView1.FindControl("btnNext");

            try
            {
                if (FormView1.PageIndex == FormView1.PageCount - 1)
                {
                    btnNext.Visible = false;
                    btnSubmit.Visible = true;
                }
                else
                {
                    btnNext.Visible = true;
                    btnSubmit.Visible = false;
                }
            }
            catch
            {

            }

        }

        protected void FormView1_PageIndexChanging(object sender, FormViewPageEventArgs e)
        {

            SaveSelectedAnswer();
            FormView1.PageIndex = e.NewPageIndex;

            LoadQuestions(Convert.ToInt32(ViewState["ExamID"]));
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            SaveSelectedAnswer();
            if (FormView1.PageIndex < FormView1.PageCount - 1)
            {
                FormView1.PageIndex++;
                Session["QuestionNumber"] = Convert.ToInt32(Session["QuestionNumber"]) + 1;
                LoadQuestions(Convert.ToInt32(ViewState["ExamID"]));
            }
        }
        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            SaveSelectedAnswer();
            if (FormView1.PageIndex > 0)
            {
                FormView1.PageIndex--;
                Session["QuestionNumber"] = Convert.ToInt32(Session["QuestionNumber"]) - 1;
                LoadQuestions(Convert.ToInt32(ViewState["ExamID"]));
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            SubmitStudentExam();

        }

        private void SubmitStudentExam()
        {
            SaveSelectedAnswer();
            insertAnswers();

            int studentId = Convert.ToInt32(Session["StudentID"]);
            int examId = Convert.ToInt32(ViewState["ExamID"]);
            string encryptedExamId = CryptoHelper.Encrypt(examId.ToString());
            string encryptedStudentId = CryptoHelper.Encrypt(studentId.ToString());

            // Redirect to Report page (exam result)
            Response.Redirect($"Report.aspx?exam={encryptedExamId}&student={encryptedStudentId}");
            Context.ApplicationInstance.CompleteRequest();  // for stop execution as if this line not there then script goes execute after submit of exam
        }

        private Dictionary<int, string> SelectedAnswers
        {
            get
            {
                if (Session["SelectedAnswers"] == null)
                    Session["SelectedAnswers"] = new Dictionary<int, string>();
                return (Dictionary<int, string>)Session["SelectedAnswers"];
            }
        }

        protected void SaveSelectedAnswer()
        {
            HiddenField hf = (HiddenField)FormView1.FindControl("hfQuestionID");
            RadioButtonList rbl = (RadioButtonList)FormView1.FindControl("rblOptions");

            if (hf != null && rbl != null)
            {
                int questionId = Convert.ToInt32(hf.Value);
                string selected = rbl.SelectedValue;

                if (SelectedAnswers.ContainsKey(questionId))
                    SelectedAnswers[questionId] = selected;
                else
                    SelectedAnswers.Add(questionId, selected);
            }
        }

        private void insertAnswers()
        {
            int StudentID = Convert.ToInt32(Session["StudentID"]);
            int ExamID = Convert.ToInt32(ViewState["ExamID"]);

            foreach (KeyValuePair<int, string> entry in SelectedAnswers)
            {
                int questionId = entry.Key;
                string selectedOption = entry.Value;


                if (!string.IsNullOrEmpty(selectedOption))
                {
                    char selectedChar = selectedOption[0]; // Convert string to char
                    bool isInsert = StudentDAL.insertStudentAnswer(StudentID, ExamID, questionId, selectedChar);
                }
                //BindReport(StudentID, ExamID);

            }

        }
    }
}