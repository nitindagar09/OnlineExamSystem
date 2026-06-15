<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Report.aspx.cs" MasterPageFile="~/Student/Student.Master" Inherits="OnlineExamSystem.Student.Report" %>


<asp:Content ID="Report" runat="server" ContentPlaceHolderID="StudentMaster">
    <div class="report-container">
        <div class="report-title">📊 Exam Report</div>
        <div class="table-responsive">
        <asp:GridView ID="gvQuestionReport" runat="server" CssClass="table table-bordered report-table" AutoGenerateColumns="true" />
            </div>
            <div class="table-responsive">
        <asp:GridView ID="gvSummaryReport" runat="server" CssClass="table table-bordered report-table" AutoGenerateColumns="true" />
                </div>
        <div class="summary-box">
            Thank you for participating in the exam. Your responses have been recorded successfully.
        </div>
         <div id="btns" >
             <div>
        <asp:Button ID="btnBack" runat="server" Text="← Back" CssClass="btn btn-primary mb-3" Style="margin-top: 10px;" OnClick="btnBack_Click" />
             </div>
             <div>
        <asp:Button ID="btnDownloadPDF" runat="server" CssClass="btn btn-success" Text="Download Report as PDF" OnClick="btnDownloadPDF_Click" />
             </div>
        </div>
    </div>
</asp:Content>

