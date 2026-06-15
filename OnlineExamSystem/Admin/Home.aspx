<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" MasterPageFile="~/Admin/Admin.Master" Inherits="OnlineExamSystem.AdminHome" %>

<asp:Content ID="AdminHome" runat="server" ContentPlaceHolderID="ContentPlaceHolderAdmin">
            <div class="container mt-4">
    <!-- Welcome Card -->
    <div class="card p-4 mb-4 mt-5 bg-info text-white dashboard-card">
        <h3>👋 Welcome!</h3>
        <p class="mb-0">Manage exams, track student progress, and stay in control.</p>
    </div>

    <!-- Dashboard Stat Cards -->
    <div class="row">
        <div class="col-md-3 mb-4">
            <div class="card p-3 dashboard-card">
                <div class="d-flex align-items-center">
                    <span class="card-icon text-primary">📘</span>
                    <div>
                        <h5>Total Exams</h5>
                        <h4>12</h4>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-3 mb-4">
            <div class="card p-3 dashboard-card">
                <div class="d-flex align-items-center">
                    <span class="card-icon text-success">👨‍🎓</span>
                    <div>
                        <h5>Total Students</h5>
                        <h4>120</h4>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-3 mb-4">
            <div class="card p-3 dashboard-card">
                <div class="d-flex align-items-center">
                    <span class="card-icon text-warning">🟢</span>
                    <div>
                        <h5>Exams Today</h5>
                        <h4>3</h4>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-3 mb-4">
            <div class="card p-3 dashboard-card">
                <div class="d-flex align-items-center">
                    <span class="card-icon text-danger">🕒</span>
                    <div>
                        <h5>Pending Evaluations</h5>
                        <h4>5</h4>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>



</asp:Content>