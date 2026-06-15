<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AIQuestions.aspx.cs" Inherits="OnlineExamSystem.Admin.AIQuestions" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Generate Questions</title>
    <link href="../Styles/AdminDashBoard.css" rel="stylesheet" />
</head>

<body>
    <form id="form1" runat="server">
        <%-- <div id="aiChatModal" style="display:none; bottom:0; right:20px; width:60%; height:50%; background:#f7f7f7; border-left:2px solid #ccc; padding:20px; box-shadow:-2px 0 5px rgba(0,0,0,0.3); overflow:auto; z-index:1000;">
                    <div style="display:flex; justify-content:space-between; align-items:center;">--%>
        <h3>AI Question Generator</h3>
        <%-- <button onclick="closeAIChat()" style="background:red; color:white; border:none; padding:5px 10px; cursor:pointer;">X</button>
                    </div>--%>
        <hr />
        <table class="question-form-table">
            <tr>
                <td>
                    <asp:DropDownList ID="ddlSubjects" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                        <asp:ListItem Text="-- Select Subject --" Value="" />
                    </asp:DropDownList>

                </td>
            </tr>
            <tr>
                <td>
                    <asp:DropDownList ID="examLevel" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                        <asp:ListItem Text="-- Select Level --" Value="" />
                        <asp:ListItem Text="Very Easy" Value="Very Easy"></asp:ListItem>
                        <asp:ListItem Text="Easy" Value="Easy"></asp:ListItem>
                        <asp:ListItem Text="Medium" Value="Medium"></asp:ListItem>
                        <asp:ListItem Text="Hard" Value="Hard"></asp:ListItem>
                        <asp:ListItem Text="Very Hard" Value="Very Hard"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <%--<asp:Label ID="lblNumQue" runat="server">Enter Number of question</asp:Label>--%>
        <asp:TextBox ID="numQue" runat="server" PlaceHolder="Enter number of questions" TextMode="Number"></asp:TextBox><br />
        <asp:TextBox ID="txtPrompt" runat="server" Width="400" Height="80" Visible="false"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="btnGenerate" runat="server" Text="Generate" OnClick="btnGenerate_Click" />
        <br />
        <br />
        <asp:Literal ID="litResult" runat="server"></asp:Literal>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered">
            <Columns>
                <asp:TemplateField HeaderText="Select">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkSelect" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Question" HeaderText="Question" />
                <asp:TemplateField HeaderText="Options">
                    <ItemTemplate>
                        <%# string.Join("<br/>", (string[])Eval("Options")) %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Answer" HeaderText="Correct Answer" />
            </Columns>
        </asp:GridView>

        <br />
        <asp:Button ID="btnSaveSelected" runat="server" Text="Save Selected" OnClick="btnSaveSelected_Click" />
    </form>
</body>
</html>
