using ExamMethodLibrary.Student;
using System;
using System.Data;
using System.Linq;


namespace OnlineExamSystem.Student
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int studentId = Convert.ToInt32(Session["StudentID"]);
            LoadPerformanceData(studentId);
          
        }
        private void LoadPerformanceData(int studentId)
        {
            StudentDAL dal = new StudentDAL();
            DataTable dt = dal.GetStudentResults(studentId);

            if (dt.Rows.Count > 0)
            {
                gvMarks.DataSource = dt;
                gvMarks.DataBind();
                int totalScore = Convert.ToInt32(dt.Compute("SUM(Score)", ""));
                int totalPossible = Convert.ToInt32(dt.Compute("SUM(TotalMarks)", ""));
                double percentage = (double)totalScore / totalPossible * 100;

                litPerformanceSummary.Text = $"<p><strong>Total Score:</strong> {totalScore} / {totalPossible} ({percentage:F2}%)</p>";

                // Convert to JSON
                string jsonLabels = Newtonsoft.Json.JsonConvert.SerializeObject(
                    dt.AsEnumerable().Select(r => r["Subject"].ToString())
                );
                string jsonScores = Newtonsoft.Json.JsonConvert.SerializeObject(
                    dt.AsEnumerable().Select(r => Convert.ToInt32(r["Score"]))
                );

                // Inject combined chart + pie chart
                ClientScript.RegisterStartupScript(this.GetType(), "charts", $@"
        <script>
        window.onload = function () {{
            // Bar + Line Chart
            var ctx1 = document.getElementById('performanceChart').getContext('2d');
            new Chart(ctx1, {{
                type: 'bar',
                data: {{
                    labels: {jsonLabels},
                    datasets: [
                        {{
                            type: 'bar',
                            label: 'Score (Bar)',
                            data: {jsonScores},
                            backgroundColor: 'rgba(54, 162, 235, 0.6)',
                            borderRadius: 5
                        }},
                        {{
                            type: 'line',
                            label: 'Score Trend (Line)',
                            data: {jsonScores},
                            fill: false,
                            borderColor: 'rgba(255, 99, 132, 1)',
                            tension: 0.3,
                            pointBackgroundColor: 'rgba(255, 99, 132, 1)',
                            pointRadius: 4
                        }}
                    ]
                }},
                options: {{
                    responsive: true,
                    plugins: {{
                        title: {{
                            display: true,
                            text: 'Student Performance Analysis',
                            font: {{
                                size: 20
                            }}
                        }}
                    }},
                    scales: {{
                        y: {{
                            beginAtZero: true,
                            max: 100
                        }}
                    }}
                }}
            }});

            // Pie Chart
            var ctx2 = document.getElementById('pieChart').getContext('2d');
            new Chart(ctx2, {{
                type: 'pie',
                data: {{
                    labels: {jsonLabels},
                    datasets: [{{
                        label: 'Score Distribution',
                        data: {jsonScores},
                        backgroundColor: [
                            '#4CAF50',
                            '#2196F3',
                            '#FF9800',
                            '#E91E63',
                            '#9C27B0',
                            '#00BCD4',
                            '#CDDC39'
                        ],
                        borderWidth: 1
                    }}]
                }},
                options: {{
                    responsive: true,
                    plugins: {{
                        title: {{
                            display: true,
                            text: 'Score Distribution by Subject',
                            font: {{
                                size: 18
                            }}
                        }}
                    }}
                }}
            }});
        }};
        </script>
        ", false);
            }
            else
            {
                litPerformanceSummary.Text = "<p>No performance data available.</p>";
            }
        }
    }
}