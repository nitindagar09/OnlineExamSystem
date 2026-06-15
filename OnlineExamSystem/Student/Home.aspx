<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" MasterPageFile="~/Student/Student.Master" Inherits="OnlineExamSystem.Student.Home" %>

<asp:Content ID="StdHome" runat="server" ContentPlaceHolderID="StudentMaster">

    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
    <link rel="stylesheet" href="../Styles/StudentHome.css" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css" rel="stylesheet">

    <div class="main-content">
    <asp:GridView ID="gvMarks" runat="server" CssClass="table table-bordered mt-4 mb-4" AutoGenerateColumns="false">
    <Columns>
        <asp:BoundField DataField="TestName" HeaderText="Test" />
        <asp:BoundField DataField="Subject" HeaderText="Subject" />
        <asp:BoundField DataField="Score" HeaderText="Score" />
        <asp:BoundField DataField="TotalMarks" HeaderText="Total Marks" />
        <asp:BoundField DataField="Percentage" HeaderText="Percentage (%)" DataFormatString="{0:F2}" />
        <asp:BoundField DataField="DateTaken" HeaderText="Date Taken" DataFormatString="{0:dd-MMM-yyyy}" />
    </Columns>
</asp:GridView>
        </div>
    <div id="cont" class="container mt-4">
         <div class="chart-container">
    <h3> 📊 Performance Analysis</h3>
    <asp:Literal ID="litPerformanceSummary" runat="server" />
    <canvas id="performanceChart" width="500" height="500"></canvas>
             </div>
        <hr />

         <div class="chart-container">
         <h4>📈 Score Distribution (Pie Chart)</h4>
        <canvas id="pieChart" width="500" height="450"></canvas>
             </div>
</div>

   <%-- <div id="cont">
    <!-- Performance Chart -->
    <div class="chart-container">
        <h4 class="chart-title"><i class="fas fa-chart-line"></i> 📊 Performance Analysis</h4>
        <canvas id="performanceChart" width="1300" height="800"></canvas>
    </div>
    
    <!-- Pie Chart -->
     <div class="chart-container">
        <h4 class="chart-title"><i class="fas fa-chart-pie"></i> 📈 Score Distribution</h4>
        <canvas id="pieChart" width="700" height="700"></canvas>
    </div>
</div>--%>



</asp:Content>
