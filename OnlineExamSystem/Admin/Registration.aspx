<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="OnlineExamSystem.AdminRegistration" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta charset="utf-8" />
    <title>Admin Registration</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="../Styles/Registration.css" />

</head>
<body class="bg-light">
    <form id="form1" runat="server">
        <div class="container">
            <div class="form-container">
                <div class="form-heading">Admin Registration</div>

                <!-- Validation summary div -->
                <div id="validationSummary" class="alert alert-danger d-none"></div>

                <div class="mb-3">
                    <label for="txtFullName" class="form-label">Full Name <span class="text-danger">*</span></label>
                    <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control" placeholder="Enter full name" />
                </div>

                <div class="mb-3">
                    <label for="txtEmail" class="form-label">Email <span class="text-danger">*</span></label>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" placeholder="Enter email" />
                </div>

                <div class="mb-3">
                    <label for="txtPassword" class="form-label">Password <span class="text-danger">*</span></label>
                    <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Create password" />
                </div>
                <div class="mb-3">
                    <label for="txtConfirmPassword" class="form-label">Confirm Password <span class="text-danger">*</span></label>
                    <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Confirm password" />
                </div>

                <div class="mb-3">
                    <label for="txtContact" class="form-label">Contact Number <span class="text-danger">*</span></label>
                    <asp:TextBox ID="txtContact" runat="server" CssClass="form-control" placeholder="Enter contact number" MaxLength="10" TextMode="Phone" />
                </div>

                <div class="AlreadyExist">
                    <p>Already Exists?</p>
                    <a href="Login.aspx">Sign In</a>
                </div>

                <div class="VerifyOtp">
                    <asp:TextBox ID="txtOTP" runat="server" CssClass="form-control" Placeholder="OTP"></asp:TextBox>
                    <asp:Button ID="btnVerifyOtp" runat="server" CssClass="btn btn-verify w-75" Text="Generate OTP" OnClick="btnRegister_Click1" />
                </div>

                <asp:Button ID="btnRegister" runat="server" OnClick="btnVerifyOtp_Click1" Text="Register" CssClass="btn btn-primary w-100" />

                <asp:Label ID="lblMessage" runat="server" CssClass="text-danger mt-3 d-block"></asp:Label>
            </div>
           
        </div>

       
        


    </form>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>

    





    <%--<script type="text/javascript">
    $(document).ready(function () {

        $('#<%= txtContact.ClientID %>').on('keypress', function (e) {
            const key = e.which ? e.which : e.keycode;
            if (key < 48 || key > 57) {
                e.preventdefault(); // block non-digit input
            }
        });

      
        $('#<%= btnRegister.ClientID %>').on('click', function (e) {
            let isvalid = true;

            // element selectors
            const fullnamebox = $('#<%= txtFullName.ClientID %>');
             const emailbox = $('#<%= txtEmail.ClientID %>');
        const passwordbox = $('#<%= txtPassword.ClientID %>');
             const contactbox = $('#<%= txtContact.ClientID %>');

             const fullname = fullnamebox.val().trim();
             const email = emailbox.val().trim();
             const password = passwordbox.val().trim();
             const contact = contactbox.val().trim();

            const emailregex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
            const passwordregex = /^(?=.*[a-z])(?=.*\d)(?=.*[!@#$%^&*()_+\-=[\]{};':"\\|,.<>/?]).{6,15}$/;

             // reset all invalid classes
             $('.form-control').removeclass('is-invalid');

             // full name
             if (!fullname) {
                 fullnamebox.addclass('is-invalid')
                     .attr('placeholder', 'full name is required')
                     .val('');
                 //fullnamebox.addclass('is-invalid');
                 //fullnamebox.attr('placeholder', 'full name is required');
                 //fullnamebox.val('');
                 isvalid = false;
             }

             // email
             if (!email) {
                 emailbox.addclass('is-invalid')
                     .attr('placeholder', 'email is required')
                     .val('');
                 isvalid = false;
             } else if (!emailregex.test(email)) {
                 emailbox.addclass('is-invalid')
                     .val('')
                     .attr('placeholder', 'invalid email format');
                 isvalid = false;
             }

             // password
             if (!password) {
                 passwordbox.addclass('is-invalid')
                     .attr('placeholder', 'password is required')
                     .val('');
                 isvalid = false;
             } else if (!passwordregex.test(password)) {
                 passwordbox.addclass('is-invalid')
                     .val('')
                     .attr('placeholder', 'must be 6-15 chars, 1 uppercase, 1 number, 1 special');
                 isvalid = false;
             }

             // contact
             if (!contact) {
                 contactbox.addclass('is-invalid')
                     .attr('placeholder', 'contact number is required')
                     .val('');
                 isvalid = false;
             }

             // prevent postback if not valid
             if (!isvalid) {
                 e.preventdefault(); // this stops the server-side postback
             }
         });


        const passwordid = '<%= txtPassword.ClientID %>';
        const confirmpasswordid = '<%= txtConfirmPassword.ClientID %>';

        $(`#${confirmpasswordid}`).on('blur', function () {
            const password = $(`#${passwordid}`).val().trim();
            const confirmpassword = $(`#${confirmpasswordid}`).val().trim();

            if (password !== confirmpassword) {
                showinvalid("passwords do not match");
            } else {
                clearinvalid();
            }
        });

        function showinvalid(message) {
            const confirmbox = $(`#${confirmpasswordid}`);
            confirmbox
                .addclass("is-invalid")
                .val("")
                .attr("placeholder", message);
        }

        function clearinvalid() {
            const confirmbox = $(`#${confirmpasswordid}`);
            confirmbox
                .removeclass("is-invalid")
                .attr("placeholder", "confirm password");
        }
    });
 
</script>--%>
</body>
</html>
