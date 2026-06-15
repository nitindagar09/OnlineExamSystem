<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="OnlineExamSystem.Student.Login" Async="true"%>

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
                    <h4>Your Exams Await – Log In to Begin</h4>
                    <p>Safe, Secure and Ease</p>
                </div>

            </div>
            <div class="temp">
                <div class="StudentNew">
                    Don't have account? &nbsp;<a href="/Student/Registration.aspx">New Register</a>
                </div>
                <div class="login-area">

                    <div class="form-container">

                        <div class="form-heading">Login as a student</div>
                        <div class="mb-3">
                            <label for="txtEmail" class="form-label">Email</label>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" placeholder="Enter email" />
                        </div>
                        <div class="mb-3">
                            <label for="txtPassword" class="form-label">Password</label>
                            <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Enter password" />
                        </div>

                        <div class="forgot-Register">
                            <button type="button" class="btn btn-secondary" onclick="openCameraPopup()">Capture</button>
                            <a href="ForgotPassword.aspx">Forgot Password</a>

                        </div>

                        <asp:Button ID="btnLogin" runat="server" CssClass="btn btn-primary w-100" Text="LOGIN" OnClick="btnLogin_Click" />

                        <asp:Label ID="lblLogin" runat="server" CssClass="text-danger d-block"></asp:Label>
                    </div>
                    <asp:HiddenField ID="hiddenImageData" runat="server" />
                </div>
            </div>

        </div>
        <div id="cameraModal" class="modal-overlay">
            <div class="modal-box">
                <video id="popupVideo" autoplay="autoplay"></video>
                <img id="capturedPhoto" src="" style="display: none;"/>
                <div id="initialButtons">
                    <button type="button" class="btn btn-danger" onclick="closeCameraPopup()">Cancel</button>
                    <button type="button" class="btn btn-success" onclick="capturePhoto()">Save Photo</button>
                </div>
                <div id="afterCaptureButtons">
                    <button type="button" class="btn btn-warning" onclick="retakePhoto()">Retake</button>
                    <button type="button" class="btn btn-success" onclick="savePhoto()">Save</button>
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
                    let tracks = stream.getTracks();
                    tracks.forEach(tracks => tracks.stop());
                    videoPopup.srcObject = null;
                }
            }
</script>
</body>
</html>
