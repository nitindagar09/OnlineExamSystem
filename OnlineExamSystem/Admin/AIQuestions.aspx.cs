using ExamMethodLibrary.DAL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineExamSystem.Admin
{
    public partial class AIQuestions : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadSubjectsInDropDown();
            }
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            if (ddlSubjects.SelectedValue == "0")
            {
                litResult.Text = "Please select a subject before saving questions.";
                return;
            }

            string numOfQuestions = numQue.Text; 
            string subject = ddlSubjects.SelectedItem.Text;
            string level = examLevel.SelectedValue;
            //string prompt = txtPrompt.Text.Trim();
            //string prompt = "Generate "+ numOfQuestions + " multiple-choice questions of " + subject + " with " + level + " Level. Each question should include 4 options and clearly indicate the correct answer and without heading of question number";
            string prompt = "Generate " + numOfQuestions +  " multiple-choice questions of " + subject + " with " + level + " difficulty." +
                "Each question must be in the following format, with each item on a new line:" +
                "Question text here " +
                "Option A " +
                "Option B " +
                "Option C " +
                "Option D " +
                "Answer: B " +
                "Do not include numbering, headings, explanations, or extra formatting. " +
                "Separate each question with a blank line.";

            if (string.IsNullOrEmpty(prompt))
            {
                litResult.Text = "Please enter a prompt.";
                return;
            }

            string apiKey = ConfigurationManager.AppSettings["OpenAIApiKey"];
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new Exception("OPENAI_API_KEY environment variable not found.");
            }
            string apiUrl = "https://api.openai.com/v1/chat/completions";

            string postData = @"{
        ""model"": ""gpt-4o-mini"",
        ""messages"": [
            { ""role"": ""user"", ""content"": """ + prompt.Replace("\"", "\\\"") + @""" }
        ],
        ""temperature"": 0.7
    }";

            try
            {
                // Create the web request
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiUrl);
                request.Method = "POST";
                request.ContentType = "application/json";
                request.Headers.Add("Authorization", "Bearer " + apiKey);

                // Write the request body
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    streamWriter.Write(postData);
                }

                // Get the response
                string result;
                using (WebResponse response = request.GetResponse())
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    result = reader.ReadToEnd();
                }

                // Parse the JSON
                JObject json = JObject.Parse(result);
                string content = json["choices"][0]["message"]["content"].ToString();




                // Loop through lines
                StringBuilder sb = new StringBuilder();
                sb.Append("<b>Generated Questions:</b><br/><br/>");
                foreach (string line in content.Split('\n'))
                {
                    if (!string.IsNullOrWhiteSpace(line))
                        sb.Append(line + "<br/>");
                }

                //   litResult.Text = sb.ToString();

                List<QuestionItem> questions = ParseQuestions(content);

                // Bind to grid
                GridView1.DataSource = questions;
                GridView1.DataBind();
                //  ViewState["GeneratedQuestions"] = questions;
                ViewState["GeneratedQuestions"] = Newtonsoft.Json.JsonConvert.SerializeObject(questions);

            }
            catch (Exception ex)
            {
                litResult.Text = "Error: " + ex.Message;
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenAIChat", "openAIChat();", true);


        }
        [Serializable]
        public class QuestionItem
        {
            public string Question { get; set; }
            public string[] Options { get; set; }
            public string Answer { get; set; }
        }

        private List<QuestionItem> ParseQuestions(string aiText)
        {
            var questions = new List<QuestionItem>();

            // Split by double newlines - assumes AI separates questions by blank lines
            string[] blocks = aiText.Split(new string[] { "\n\n" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var block in blocks)
            {
                var lines = block.Split('\n');
                if (lines.Length >= 6) // 1 question + 4 options + answer
                {
                    questions.Add(new QuestionItem
                    {
                        Question = lines[0].Trim(),
                        Options = new string[]
                        {
                lines[1].Trim(),
                lines[2].Trim(),
                lines[3].Trim(),
                lines[4].Trim()
                        },
                        Answer = lines[5].Replace("Answer:", "").Trim()
                    });
                }
            }

            return questions;
        }
        protected void btnSaveSelected_Click(object sender, EventArgs e)
        {
            SaveSelectedQuestions();
        }
        private void SaveSelectedQuestions()
        {
            // var questions = ViewState["GeneratedQuestions"] as List<QuestionItem>;
            var questions = Newtonsoft.Json.JsonConvert.DeserializeObject<List<QuestionItem>>(
                ViewState["GeneratedQuestions"].ToString()
                  );

            if (questions == null) return;

            foreach (GridViewRow row in GridView1.Rows)
            {
                var chk = (System.Web.UI.WebControls.CheckBox)row.FindControl("chkSelect");
                if (chk != null && chk.Checked)
                {
                    int index = row.RowIndex;
                    var q = questions[index];

                    // Call your DAL method
                    QuestionDAL.InsertQuestion(
                        examID: Convert.ToInt32(ddlSubjects.SelectedValue),  // set the correct ExamID
                        questionText: q.Question,
                        optionA: q.Options.Length > 0 ? q.Options[0] : "",
                        optionB: q.Options.Length > 1 ? q.Options[1] : "",
                        optionC: q.Options.Length > 2 ? q.Options[2] : "",
                        optionD: q.Options.Length > 3 ? q.Options[3] : "",
                        correctOption: GetCorrectOptionLetter(q.Answer),
                        marks: 10 // default marks, you can change
                    );
                }
            }

            Response.Redirect("~/Admin/Dashboard.aspx");

            //LoadQuestionsByExamID(Convert.ToInt32(ddlSubjects.SelectedValue));
        }

        private char GetCorrectOptionLetter(string answer)
        {
            // If AI provides "Answer: B" or just "B"
            if (string.IsNullOrWhiteSpace(answer))
                return 'A';

            answer = answer.Trim();
            if (answer.StartsWith("Answer", StringComparison.OrdinalIgnoreCase))
                answer = answer.Split(':')[1].Trim();

            return !string.IsNullOrEmpty(answer) ? answer[0] : 'A';
        }
            protected void ddlExams_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadQuestionsByExamID(Convert.ToInt32(ddlSubjects.SelectedValue));
        }
        private void LoadQuestionsByExamID(int examID)
        {
            //DataTable questions = QuestionDAL.GetQuestionsByExamId(examID);
            //GridView1.DataSource = questions;
            //GridView1.DataBind();
        }

        private void LoadSubjectsInDropDown()
        {
            ddlSubjects.Items.Clear();
            DataTable subjects = SubjectDAL.GetSubjects(1, Convert.ToInt32(Session["AdminID"]));
            ddlSubjects.DataSource = subjects;
            ddlSubjects.DataTextField = "SubjectName";
            ddlSubjects.DataValueField = "ExamID";
            ddlSubjects.DataBind();

            ddlSubjects.Items.Insert(0, new ListItem("-- Select Subject --", "0"));
        }
    }
}