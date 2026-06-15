using ExamMethodLibrary.Student;
using iTextSharp.text;
using iTextSharp.text.pdf;
using OnlineExamSystem.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineExamSystem.Student
{
    public partial class Report : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["exam"] != null && Session["StudentID"] != null)
            {
                string decryptedExamId = CryptoHelper.Decrypt(HttpUtility.UrlDecode(Request.QueryString["exam"]));
                //string decryptedExamId = Request.QueryString["exam"];
               // string decryptedStudentId = CryptoHelper.Decrypt(Session["StudentID"].ToString());

                int examId = Convert.ToInt32(decryptedExamId);
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

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Exams.aspx");
        }

        protected void btnDownloadPDF_Click(object sender, EventArgs e)
        {
            int studentId = Convert.ToInt32(Session["StudentID"]);
            Console.WriteLine(studentId);
            //int examId = Convert.ToInt32(Request.QueryString["exam"]);
            int examId = Convert.ToInt32(CryptoHelper.Decrypt(Request.QueryString["exam"]));
            Console.WriteLine(examId);
            DataSet ds = StudentDAL.GetStudentExamReport(studentId, examId);

            if (ds == null || ds.Tables.Count == 0) return;

            // Start PDF creation
            Document pdfDoc = new Document(PageSize.A4, 25, 25, 30, 30);
            MemoryStream memoryStream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
            pdfDoc.Open();

            // Add title
            pdfDoc.Add(new Paragraph("Student Exam Report", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18)));
            pdfDoc.Add(new Chunk("\n"));

            // Questions Report
            if (ds.Tables.Count > 0)
            {
                PdfPTable questionTable = new PdfPTable(ds.Tables[0].Columns.Count);
                questionTable.WidthPercentage = 100;

                // Add headers
                foreach (DataColumn column in ds.Tables[0].Columns)
                {
                    questionTable.AddCell(new Phrase(column.ColumnName, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10)));
                }

                // Add data
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    foreach (var item in row.ItemArray)
                    {
                        questionTable.AddCell(item.ToString());
                    }
                }

                pdfDoc.Add(new Paragraph("Detailed Question Report:"));
                pdfDoc.Add(new Paragraph(" "));
                pdfDoc.Add(questionTable);
                pdfDoc.Add(new Chunk("\n"));
            }

            // Summary Report
            if (ds.Tables.Count > 1)
            {
                PdfPTable summaryTable = new PdfPTable(ds.Tables[1].Columns.Count);
                summaryTable.WidthPercentage = 100;

                foreach (DataColumn column in ds.Tables[1].Columns)
                {
                    summaryTable.AddCell(new Phrase(column.ColumnName, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10)));
                }

                foreach (DataRow row in ds.Tables[1].Rows)
                {
                    foreach (var item in row.ItemArray)
                    {
                        summaryTable.AddCell(item.ToString());
                    }
                }

                pdfDoc.Add(new Paragraph("Summary Report:"));
                pdfDoc.Add(new Paragraph(" "));
                pdfDoc.Add(summaryTable);
            }

            pdfDoc.Close();

            // Send to browser
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "attachment; filename=StudentReport.pdf");
            Response.BinaryWrite(memoryStream.ToArray());
            Response.End();
        }
    }
}