<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" MasterPageFile="~/Home.Master" Inherits="OnlineExamSystem.Home" %>

<asp:Content ID="home" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="title">

        <h1>ExamSoft - Online Exam System</h1>
    </div>




    <div class="Intro">
        <div class="Intro-description">
            <h1>The Advanced Assessment Platform</h1>
            <p>“We felt there had to be a better way to assess students’ strengths and weaknesses while reducing the potential for cheating. We found it.”</p>
        </div>
        <div class="Intro-img">
            <img src="Images/HomePage_Img_1.jpg" alt="image.." />
        </div>
    </div>




    <div class="features">
        <%--<div class="features-section">
            <h2>Why Choose ExamSoft | Online Exam System </h2>
        </div>--%>
        <div class="features-section">
            <h2>Why Choose ExamSoft | Online Exam System </h2>
            <div class="features-cards">
                <div class="feature-card">
                    <div class="icon">
                        <img src="Images/features1.jpg" class="card-img-top" alt="...">
                    </div>
                    <h3>Live Exams</h3>
                    <p class="card-text">We conduct live tests so every candidate can demonstrate their knowledge in real time.</p>

                </div>

                <div class="feature-card">
                    <div class="icon">
                        <img src="Images/feature4.jpg" class="card-img-top" alt="...">
                    </div>
                    <h3>Analyze Progress</h3>
                    <p class="card-text">Track your progress and view your rank among competitors.</p>

                </div>

                <div class="feature-card">
                    <div class="icon">
                        <img src="Images/feature2.jpg" class="card-img-top" alt="...">
                    </div>
                    <h3>24*7 Support</h3>
                    <p class="card-text">We’ve got your back — always.</p>

                </div>


                <div class="feature-card">
                    <div class="icon">
                        <img src="Images/feature3.jpg" class="card-img-top" alt="...">
                    </div>
                    <h3>Cheating Prevention</h3>
                    <p class="card-text">Administer secure online exams with our AI-based remote proctoring system.</p>

                </div>


            </div>


        </div>

       <%-- <asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">--%>
            <div class="container text-center py-5">
                <h2 class="mb-5" style="color: #0cb197; font-weight: 600;">How Is The ExamSoft Different?</h2>
                <div class="row align-items-center">
                    <!-- Left Features -->
                    <div class="col-md-4 text-end mb-4 mb-md-0">
                        <div class="feature-item mb-4">
                            <span>Low Data Solution</span>
                            <i class="fa fa-tachometer-alt feature-icon", style="color: cornflowerblue;"></i>
                        </div>
                        <div class="feature-item mb-4">
                            <span>Lecturer & Student Support</span>
                            <i class="fa fa-comments feature-icon", , style="color: #ff797b;"></i>
                        </div>
                        <div class="feature-item mb-4">
                            <span>Available on PC or Mobile Devices</span>
                            <i class="fa fa-desktop feature-icon", style="color: #00c69a;"></i>
                        </div>
                        <div class="feature-item">
                            <span>Scalable According to Institution's Needs</span>
                            <i class="fa fa-expand-arrows-alt feature-icon", style="color: #b284e7;"></i>
                        </div>
                    </div>

                    <!-- Center Image -->
                    <div class="col-md-4">
                        <img src="Images/MainHomeComponent3.png" alt="Student" class="img-fluid rounded" />
                    </div>

                    <!-- Right Features -->
                    <div class="col-md-4 text-start mt-4 mt-md-0">
                        <div class="feature-item mb-4">
                            <i class="fa-solid fa-wifi feature-icon", style="color: blue;"></i>
                            <span>Works Offline</span>
                        </div>
                        <div class="feature-item mb-4">
                            <i class="fa fa-dollar-sign feature-icon", style="color: #b284e7;"></i>
                            <span>Low Cost Per Student</span>
                        </div>
                        <div class="feature-item mb-4">
                            <i class="fa fa-mobile-alt feature-icon", style="color: #00c69a"></i>
                            <span>Entry Level Devices</span>
                        </div>
                        <div class="feature-item">
                            <i class="fa fa-graduation-cap feature-icon", style="color: #33a1ff;"></i>
                            <span>In Venue Use</span>
                        </div>
                    </div>
                </div>
            </div>
<%--</asp:Content>--%>

   


</asp:Content>
