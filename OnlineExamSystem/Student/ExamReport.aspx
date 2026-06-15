<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExamReport.aspx.cs" Inherits="OnlineExamSystem.Student.ExamReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
       <%-- <asp:GridView ID="gvQuestionReport" runat="server" AutoGenerateColumns="true" />
        <br />
        <asp:GridView ID="gvSummaryReport" runat="server" AutoGenerateColumns="true" />--%>

        <div class="container mt-5">
            <h2 class="text-center mb-4">Exam Report</h2>

            <!-- Question Report -->
            <div class="card mb-4">
                <div class="card-header bg-primary text-white">
                    Question-wise Report
                </div>
                <div class="card-body">
                    <asp:GridView ID="gvQuestionReport" runat="server" AutoGenerateColumns="true" CssClass="table table-bordered table-striped" />
                </div>
            </div>

            <!-- Summary Report -->
            <div class="card">
                <div class="card-header bg-success text-white">
                    Summary
                </div>
                <div class="card-body">
                    <asp:GridView ID="gvSummaryReport" runat="server" AutoGenerateColumns="true" CssClass="table table-bordered table-hover" />
                </div>
            </div>

            <!-- Back Button -->
            <div class="mt-4 text-center">
                <a href="ViewExamResults.aspx" class="btn btn-secondary">Back to Results</a>
            </div>
        </div>
    </form>
</body>
</html>
