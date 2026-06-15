<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Exams.aspx.cs" MasterPageFile="~/Student/Student.Master" Inherits="OnlineExamSystem.Student.Exams" %>

<asp:Content ID="StdExam" runat="server" ContentPlaceHolderID="StudentMaster">

    <link rel="stylesheet" href="../Styles/StudentExam.css" />

    <style>
        .ViewReportLink {
            text-decoration: none;
        }
        .ViewReportLink:hover{
            text-decoration: underline;
        }
    </style>

    <div runat="server" class="examsContainer" id="examsContainer">

        <h2>Previous Exams</h2>
        <div id="divExams" runat="server" class="divExams">
        </div>

    </div>
</asp:Content>
