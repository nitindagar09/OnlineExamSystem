<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="OnlineExamSystem.Student.Registration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />
    <%--<link rel="stylesheet" href="../Styles/Registration.css" />--%>
    <link rel="stylesheet" href="../Styles/Registration.css" />


</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="form-container">
                <div class="form-heading">Student Registration</div>

                <!-- Validation summary div -->
                <div id="validationSummary" class="alert alert-danger d-none" role="alert"></div>


                <div class="mb-3">
                    <label for="txtFullName" class="form-label">Full Name</label>
                    <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control" placeholder="Enter full name" />
                </div>

                <div class="mb-3">
                    <label for="txtEmail" class="form-label">Email</label>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" placeholder="Enter email" />
                </div>

                <div class="mb-3">
                    <label for="txtPassword" class="form-label">Password</label>
                    <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Create password" />
                </div>

                <div class="mb-3">
                    <label for="txtConfirmPassword" class="form-label">Confirm Password</label>
                    <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Confirm password" />
                </div>


                <div class="mb-3">
                    <label for="txtContact" class="form-label">Contact Number</label>
                    <asp:TextBox ID="txtContact" runat="server" CssClass="form-control" placeholder="Enter contact number" />
                </div>

                <div class="VerifyOtp">
                    <asp:TextBox ID="txtOTP" runat="server" CssClass="form-control" Placeholder="OTP"></asp:TextBox>
                    <asp:Button ID="btnVerifyOtp" runat="server" CssClass="btn btn-verify w-50" Text="Generate OTP" OnClick="btnRegister_Click" />
                    <button type="button" class="btn btn-secondary" onclick="openCameraPopup()">Capture</button>
                </div>

                <div class="AlreadyExist">
                    <p>Already Exists?</p>
                    <a href="Login.aspx">Sign In</a>
                </div>

                <asp:Button ID="btnRegister" runat="server" Text="Register" CssClass="btn btn-primary w-100" OnClick="btnVerifyOtp_Click" />

                <asp:Label ID="lblMessage" runat="server" CssClass="text-danger mt-3 d-block"></asp:Label>
            </div>
            <asp:HiddenField ID="hiddenImageData" runat="server" />

        </div>

        <div id="cameraModal" class="modal-overlay">
            <div class="modal-box">
                <video id="popupVideo" autoplay muted></video>
                <img id="capturedPhoto" src="" style="display: none;" />
                <div id="initialButtons">
                    <button type="button" class="btn btn-success" onclick="capturePhoto()">Save Photo</button>
                    <button type="button" class="btn btn-danger" onclick="closeCameraPopup()">Cancel</button>
                </div>
                <div id="afterCaptureButtons">
                    <button type="button" class="btn btn-warning" onclick="retakePhoto()">Retake</button>
                    <button type="button" class="btn btn-success" onclick="savePhoto()">Save</button>
                </div>
            </div>
        </div>
    </form>

    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {


            $('#<%= btnRegister.ClientID %>').on('click', function (e) {
                let isValid = true;

                const fullNameBox = $('#<%= txtFullName.ClientID %>');
                const emailBox = $('#<%= txtEmail.ClientID %>');
                const passwordBox = $('#<%= txtPassword.ClientID %>');
                const confirmPasswordBox = $('#<%= txtConfirmPassword.ClientID %>');
                const contactBox = $('#<%= txtContact.ClientID %>');

                const fullName = fullNameBox.val().trim();
                const email = emailBox.val().trim();
                const password = passwordBox.val().trim();
                const confirmPassword = confirmPasswordBox.val().trim();
                const contact = contactBox.val().trim();

                // Reset styles and restore original placeholders
                $('.form-control').removeClass('is-invalid');
                $('.form-control').each(function () {
                    $(this).attr('placeholder', $(this).data('placeholder'));
                });

                // Full Name validation
                if (!fullName) {
                    fullNameBox.addClass('is-invalid').val('').attr('placeholder', 'Full Name is required');
                    isValid = false;
                }

                // Email validation
                const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
                if (!email) {
                    emailBox.addClass('is-invalid').val('').attr('placeholder', 'Email is required');
                    isValid = false;
                } else if (!emailRegex.test(email)) {
                    emailBox.addClass('is-invalid').val('').attr('placeholder', 'Enter a valid email address');
                    isValid = false;
                }

                // Password validation
                const passwordRegex = /^(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?]).{6,15}$/;
                if (!password) {
                    passwordBox.addClass('is-invalid').val('').attr('placeholder', 'Password is required');
                    isValid = false;
                } else if (!passwordRegex.test(password)) {
                    passwordBox.addClass('is-invalid').val('').attr('placeholder', '6-15 chars, 1 uppercase, 1 digit, 1 special char');
                    isValid = false;
                }

                // Confirm Password validation
                if (!confirmPassword) {
                    confirmPasswordBox.addClass('is-invalid').val('').attr('placeholder', 'Please confirm your password');
                    isValid = false;
                } else if (password !== confirmPassword) {
                    confirmPasswordBox.addClass('is-invalid').val('').attr('placeholder', 'Passwords does not match');
                    isValid = false;
                }

                // Contact validation
                if (!contact) {
                    contactBox.addClass('is-invalid').val('').attr('placeholder', 'Contact Number is required');
                    isValid = false;
                }

                // Prevent form submission if any field is invalid
                if (!isValid) {
                    e.preventDefault();
                }
            });

            // Store original placeholders on page load
            $('.form-control').each(function () {
                $(this).data('placeholder', $(this).attr('placeholder'));
            });




        });
    </script>

    <script type="text/javascript">
        $(document).ready(function () {

            $('#<%= txtContact.ClientID %>').on('keypress', function (e) {
                const key = e.which ? e.which : e.keyCode;
                if (key < 48 || key > 57) {
                    e.preventDefault(); // block non-digit input
                }
            });
        });

    </script>
    <script>
        const popupVideo = document.getElementById("popupVideo");
        const capturedPhoto = document.getElementById("capturedPhoto");
        const hfImage = document.getElementById("<%= hiddenImageData.ClientID %>");
        const initialButtons = document.getElementById("initialButtons");
        const afterCaptureButtons = document.getElementById("afterCaptureButtons");
        let imageData;

        function openCameraPopup() {
            document.getElementById("cameraModal").style.display = "flex";

            capturedPhoto.style.display = "none";
            popupVideo.style.display = "block";

            navigator.mediaDevices.getUserMedia({ video: true })
                .then(stream => {
                    popupVideo.srcObject = stream;
                    popupVideo.play(); // Important: starts the video feed
                })
                .catch(err => {
                    alert("Camera access denied or not available.");
                });
        }

        function closeCameraPopup() {
            document.getElementById("cameraModal").style.display = "none";

            if (videoStream) {
                videoStream.getTracks().forEach(track => track.stop());
            }
        }

        function capturePhoto() {
            const canvas = document.createElement("canvas");
            canvas.width = popupVideo.clientWidth;
            canvas.height = popupVideo.clientHeight;

            const ctx = canvas.getContext("2d");
            ctx.drawImage(popupVideo, 0, 0, canvas.width, canvas.height);

            imageData = canvas.toDataURL("image/jpeg");

            popupVideo.style.display = "none";
            capturedPhoto.src = imageData;
            capturedPhoto.style.display = "block";

            initialButtons.style.display = "none";
            afterCaptureButtons.style.display = "flex";
        }

        function retakePhoto() {
            capturedPhoto.src = "";
            capturedPhoto.style.display = "none";
            popupVideo.style.display = "block";

            afterCaptureButtons.style.display = "none";
            initialButtons.style.display = "flex";
        }

        function savePhoto() {
            hfImage.value = imageData.split(",")[1];
            document.getElementById("cameraModal").style.display = "none";
            if (videoPopup.srcObject) {
                let tracks = stream.getTracks;
                tracks.forEach(tracks => tracks.stop());
                videoPopup.srcObject = null;
            }
        }
</script>

</body>
</html>
