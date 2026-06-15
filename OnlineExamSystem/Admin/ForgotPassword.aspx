<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="OnlineExamSystem.ForgotLoginPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="https://fonts.googleapis.com/css2?family=Poppins:wght@400;600;700&display=swap" rel="stylesheet" />
    <title>Get Password</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />
    <%--<link rel="stylesheet" href="../Styles/Login.css" />--%>
    <link rel="stylesheet" href="../Styles/ForgotPassword.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="form-container">
            <div class="form-heading">Forgot Password</div>
            <div class="mb-3">
                <label for="txtEmail" class="form-label">Email</label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" placeholder="Enter email" />
            </div>

            <div class="AlreadyExist">
                <p>Create New Account?</p>
                <a href="Registration.aspx">Register</a>
            </div>
            <asp:Button ID="btnForgotPass" runat="server" CssClass="btn btn-primary w-100" Text="Forgot Password" OnClick="btnForgotPass_Click" />
            <asp:Label ID="lblForgotPass" runat="server" CssClass="text-danger mt-3 d-block"></asp:Label>
        </div>

    </form>
</body>
</html>
