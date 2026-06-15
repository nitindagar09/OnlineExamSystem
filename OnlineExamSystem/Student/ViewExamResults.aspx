<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewExamResults.aspx.cs" Inherits="OnlineExamSystem.Student.ViewExamResults" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
       <asp:GridView ID="gvExams" runat="server" AutoGenerateColumns="False" OnRowCommand="gvExams_RowCommand">
            <Columns>
                <asp:BoundField DataField="ExamTitle" HeaderText="Exam Title" />
                <asp:BoundField DataField="DateTaken" HeaderText="Date Taken" />
                <asp:ButtonField ButtonType="Button" CommandName="ViewReport" Text="View Report" />
            </Columns>
        </asp:GridView>
    </form>
</body>
</html>
