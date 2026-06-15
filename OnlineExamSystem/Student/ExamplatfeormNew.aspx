<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExamplatfeormNew.aspx.cs" Inherits="OnlineExamSystem.Student.ExamplatfeormNew" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Instructions</title>
    <link rel="stylesheet" href="../Styles/ExamPlatform.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div id="countdownContainer">
            <div id="startDiv" class="box">
                <h1>Your Exam is ready to begin after some time, Please Wait!</h1>
                <div class="CountDown">
                    <p class="time">0<%= Session["timeNow"]+":00" %></p>
                </div>
            </div>

            <div class="exam-instructions">
                <h2 style="color: #007BFF;">📘 Live Exam Instructions</h2>
                <ul style="padding-left: 20px;">
                    <li><strong>Be on time:</strong> The exam will start exactly at the scheduled time.</li>
                    <li><strong>Browser Compatibility:</strong> Use the latest version of Chrome or Edge.</li>
                    <li><strong>Do not refresh:</strong> Avoid refreshing the page.</li>
                    <li><strong>Single attempt:</strong> Attempt only once.</li>
                    <li><strong>Stable internet:</strong> Required.</li>
                    <li><strong>Auto-submit:</strong> When time ends.</li>
                    <li><strong>Navigation:</strong> Use question panel.</li>
                    <li><strong>Do not switch tabs:</strong> 3 warnings = disqualification.</li>
                </ul>
                <p style="margin-top: 20px; font-weight: 600; font-size: 1.3rem; color: #dc3545;">
                    <strong>Note:</strong> Misconduct will cancel your exam.
                </p>
            </div>
        </div>

        <div id="questionSection" style="display:none;">
            <asp:ScriptManager ID="ScriptManager1" runat="server" />
            <div style="margin-bottom: 15px;" id="fixedTimer">
                Time Remaining: <span id="timerLabel"></span>
            </div>

            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div id="violationWarning" style="display: none; background-color: yellow; color: red; padding: 10px; font-weight: bold; text-align: center;">
                        Warning: You have violated the exam rules. Switching tabs is not allowed!
                    </div>

                    <asp:FormView ID="FormView1" runat="server" DataKeyNames="QuestionID" AllowPaging="false"
                        OnPageIndexChanging="FormView1_PageIndexChanging" OnDataBound="FormView1_DataBound">
                        <ItemTemplate>
                            <asp:HiddenField ID="hfQuestionID" runat="server" Value='<%# Eval("QuestionID") %>' />
                            <div style="padding: 20px;" class="question">
                                <h3>Q<%=Session["QuestionNumber"] %>. <%# Eval("QuestionText") %></h3>
                                <asp:RadioButtonList ID="rblOptions" runat="server" />
                                <br />
                                <div class="button">
                                    <asp:Button ID="btnPrevious" runat="server" Text="Previous" OnClick="btnPrevious_Click" />
                                    <asp:Button ID="btnNext" runat="server" Text="Next" OnClick="btnNext_Click" />
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" Visible="false" OnClick="btnSubmit_Click" />
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:FormView>

                    <div id="divReport" runat="server">
                        <asp:GridView ID="gvQuestionReport" runat="server" AutoGenerateColumns="true" CssClass="table table-bordered" />
                        <br />
                        <asp:GridView ID="gvSummaryReport" runat="server" AutoGenerateColumns="true" CssClass="table table-bordered" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <asp:Button ID="btnAutoSubmit" runat="server" OnClick="btnAutoSubmit_Click" style="display:none;" />
    </form>

    <script type="text/javascript">
        var examStarted = false;
        var examAutoSubmitted = false;
        var warningCount = 0;
        var maxWarnings = 3;
        var tabSwitchTriggered = false;

        window.onload = function () {
            startInstructionTimer();
        };

        function startInstructionTimer() {
            const startTime = <%= Session["timeNow"] %>;
            let time = startTime * 60;
            const showTime = document.querySelector(".time");
            const countdownContainer = document.getElementById("countdownContainer");
            const questionSection = document.getElementById("questionSection");

            const timer = setInterval(function () {
                const minutes = String(Math.floor(time / 60)).padStart(2, '0');
                const seconds = String(time % 60).padStart(2, '0');
                showTime.innerHTML = `${minutes}:${seconds}`;
                time--;

                if (time < 0) {
                    clearInterval(timer);
                    countdownContainer.style.display = "none";
                    questionSection.style.display = "block";
                    startExam();
                }
            }, 1000);
        }

        function startExam() {
            examStarted = true;
            localStorage.setItem("examStarted", "true");
            launchFullScreen();
            initTabSwitchDetection();
        }

        function initTabSwitchDetection() {
            document.addEventListener('visibilitychange', function () {
                if (document.hidden && !examAutoSubmitted && !tabSwitchTriggered) {
                    tabSwitchTriggered = true;
                    warningCount++;
                    alert("Warning " + warningCount + ": Do not switch tabs!");
                    if (warningCount >= maxWarnings) {
                        examAutoSubmitted = true;
                        submitExam();
                    }
                    setTimeout(() => {
                        tabSwitchTriggered = false;
                    }, 1000);
                }
            });
        }

        function launchFullScreen() {
            const elem = document.documentElement;
            if (elem.requestFullscreen) {
                elem.requestFullscreen();
            } else if (elem.webkitRequestFullscreen) {
                elem.webkitRequestFullscreen();
            } else if (elem.msRequestFullscreen) {
                elem.msRequestFullscreen();
            }
        }

        function submitExam() {
            __doPostBack('<%= btnAutoSubmit.UniqueID %>', '');
        }

        document.addEventListener("fullscreenchange", () => {
            if (!document.fullscreenElement && examStarted && !examAutoSubmitted) {
                alert("You exited fullscreen. The exam will be auto-submitted.");
                submitExam();
            }
        });

        document.addEventListener("keydown", function (e) {
            if (["F5", "Escape"].includes(e.key) || (e.ctrlKey && e.key.toLowerCase() === "r")) {
                e.preventDefault();
                alert("This key is disabled during the exam.");
            }
        });

        window.addEventListener("load", function () {
            const navType = performance.getEntriesByType("navigation")[0].type;
            if (navType === "reload" && localStorage.getItem("examStarted") === "true") {
                submitExam();
            }
        });
    </script>
</body>
</html>
