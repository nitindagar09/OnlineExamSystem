<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="OnlineExamSystem.AdminLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.7.2/css/all.min.css"
        integrity="sha512-Evv84Mr4kqVGRNSgIGL/F/aIDqQb7xQ2vcrdIwxfjThSH8CSR7PBEakCr51Ck+w+/U6swU2Im1vVX0SVk9ABhg=="
        crossorigin="anonymous" referrerpolicy="no-referrer" />
    <link rel="stylesheet" href="../Styles/Login.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="main-div">
            <div class="image-area">
                <div class="logo btnLogo">
                    <i class="fa-solid fa-chart-simple"></i>
                    <h4>ExamSoft</h4>
                </div>
                <div class="Description">
                    <h4>Admin Center</h4>
                    <p>Where Your Management Powers Begin</p>
                </div>
            </div>
            <div class="temp">
                <div class="StudentNew">
                    Don't have account? &nbsp;<a href="Registration.aspx">New Register</a>
                </div>
                <div class="login-area">

                    <div class="form-container">

                        <div class="form-heading">Login as a Admin</div>
                        <div class="mb-3">
                            <label for="txtEmail" class="form-label">Email</label>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" placeholder="Enter email" />
                        </div>
                        <div class="mb-3">
                            <label for="txtPassword" class="form-label">Password</label>
                            <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Enter password" />
                        </div>

                        <div class="forgot-Register">
                            <a href="ForgotPassword.aspx">Forgot Password</a>

                        </div>

                        <asp:Button ID="btnLogin" runat="server" CssClass="btn btn-primary w-100" Text="LOGIN" OnClick="btnLogin_Click" />

                        <asp:Label ID="lblLogin" runat="server" CssClass="text-danger d-block"></asp:Label>
                    </div>
                    
                </div>
            </div>

        </div>
        
    </form>
    <script>
        document.querySelector('.btnLogo').addEventListener('click', function () {
            // Change the URL to the page you want to navigate to
            window.location.href = '../Home.aspx';// Replace with your target URL
        });
    </script>

    

</body>
</html>
