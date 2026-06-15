<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExamPlatform.aspx.cs" Inherits="OnlineExamSystem.Student.ExamPlatform" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div id="questionSection">
            <asp:ScriptManager ID="ScriptManager1" runat="server" />
            <!-- ⏱️ TIMER OUTSIDE UpdatePanel -->
            <div style="margin-bottom: 15px;" id="fixedTimer">
                Time Remaining: <span id="timerLabel"></span>
            </div>

            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div id="violationWarning" style="display: none; background-color: yellow; color: red; padding: 10px; font-weight: bold; text-align: center;">
                        Warning: You have violated the exam rules. Switching tabs is not allowed!
                    </div>


                    <asp:FormView ID="FormView1" runat="server" DataKeyNames="QuestionID" AllowPaging="false"
                        OnPageIndexChanging="FormView1_PageIndexChanging" OnDataBound="FormView1_DataBound">
                        <ItemTemplate>
                            <asp:HiddenField ID="hfQuestionID" runat="server" Value='<%# Eval("QuestionID") %>' />

                            <div style="padding: 20px;" class="question">
                                <h3>Q<%=Session["QuestionNumber"] %>. <%# Eval("QuestionText") %></h3>
                                <asp:RadioButtonList ID="rblOptions" runat="server" />
                                <br />
                                <div class="button">
                                    <asp:Button ID="btnPrevious" runat="server" Text="Previous" OnClick="btnPrevious_Click" />
                                    <asp:Button ID="btnNext" runat="server" Text="Next" OnClick="btnNext_Click" />
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" Visible="false" OnClick="btnSubmit_Click" />
                                </div>

                            </div>
                        </ItemTemplate>
                    </asp:FormView>

                    <%-- <h1>Student report</h1>--%>
                    <div id="divReport" runat="server">
                        <asp:GridView ID="gvQuestionReport" runat="server" AutoGenerateColumns="true" CssClass="table table-bordered" />
                        <br />
                        <asp:GridView ID="gvSummaryReport" runat="server" AutoGenerateColumns="true" CssClass="table table-bordered" />
                    </div>
                </ContentTemplate>

            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
