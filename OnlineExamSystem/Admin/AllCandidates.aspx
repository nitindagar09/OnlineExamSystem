<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AllCandidates.aspx.cs" MasterPageFile="~/Admin/Admin.Master" Inherits="OnlineExamSystem.AdminNew.WebForm1" %>

<asp:Content ID="allStudent" runat="server" ContentPlaceHolderID="ContentPlaceHolderAdmin">
   <div class="student-page">
    <div class="AllCandidate">
        <h2>Students</h2>
        <div id="divStudents" runat="server" class="divStudents">
        </div>
    </div>
       </div>
    <div class="study-image"></div>

    <asp:GridView ID="gvCandidates" runat="server" AutoGenerateColumns="False" OnRowCommand="gvCandidates_RowCommand">
    <Columns>
        <asp:BoundField DataField="StudentID" HeaderText="Student ID" />
        <asp:BoundField DataField="StudentName" HeaderText="Student Name" />
        <asp:TemplateField HeaderText="Actions">
            <ItemTemplate>
                <asp:Button ID="btnViewAnalysis" runat="server" Text="View Analysis"
                    CommandName="ViewAnalysis" CommandArgument='<%# Eval("StudentID") %>' CssClass="btn btn-info btn-sm" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>

</asp:Content>
