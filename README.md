# Online Exam System with AI-Powered Question Generation

## Project Overview

Online Exam System is a web-based examination platform developed as a Final Year Project. The system provides a secure and automated environment for conducting online examinations. It supports role-based access for Administrators and Students, email OTP verification, AI-powered question generation, automated result evaluation, PDF report generation, and anti-cheating mechanisms.

One of the key highlights of this project is the integration of OpenAI, which enables administrators to automatically generate exam questions based on the selected subject, difficulty level, and number of questions.

---

## Key Features

### Secure Authentication

* Email OTP verification during registration.
* Role-based login system for Admin and Student.
* Secure user management.

### AI Integration

* OpenAI-powered automatic question generation.
* Generate questions by selecting:

  * Subject
  * Difficulty Level
  * Number of Questions
* Reduces manual effort in creating examination content.

### Exam Management

* Subject creation and management.
* Exam scheduling and management.
* Manual question creation.
* AI-assisted question generation.

### Student Performance Analytics

* Track student performance.
* View exam results and scores.
* Access detailed exam reports.

### Anti-Cheating System

* Browser tab switching detection.
* Students receive up to 3 warnings.
* Automatic exam submission after exceeding warning limits.

### Automated Evaluation

* Automatic exam submission after timeout.
* Instant result generation.
* Downloadable PDF reports.

---

## Admin Module

### Dashboard Features

* View all registered students.
* Monitor student performance and examination history.
* Create and manage subjects.
* Create and schedule exams.
* Add questions manually.
* Generate questions automatically using OpenAI.
* Analyze examination results.

---

## Student Module

### Dashboard Features

* View upcoming exams.
* Automatically hide expired exams.
* Access exams only within the allowed examination window.
* Exam access starts 5 minutes before the scheduled exam time.

### Examination Features

* Secure online examination environment.
* Automatic countdown timer.
* Tab switching detection.
* Auto-submit after 3 warnings.
* Auto-submit when exam duration expires.

### Result Features

* Instant result generation.
* Detailed performance report.
* Download report in PDF format.
* Access previous exam reports anytime.

---

## Technology Stack

### Frontend

* HTML
* CSS
* JavaScript
* BootStrap
* ASP.NET Web Forms

### Backend

* C#
* ADO.NET
* SQL Server
* Stored Procedures

### External Services

* OpenAI API
* SMTP Email Service (OTP Verification)

---

## Database Setup

1. Open SQL Server Management Studio.
2. Create a new database.
3. Execute the SQL script available in the Database folder.
4. Verify that all tables and stored procedures are created successfully.

---

## Configuration

Before running the project:

1. Open Web.config.
2. Configure the SQL Server connection string.
3. Add your OpenAI API key.
4. Configure SMTP email settings.
5. Build and run the application.

---

## Project Highlights

* AI-Powered Question Generation using OpenAI
* OTP-Based Email Verification
* Anti-Cheating Exam Monitoring
* Automatic Exam Submission
* PDF Report Generation
* Role-Based Authentication and Authorization
* Real-Time Examination Management
* Performance Analytics Dashboard

---

## Author

Nitin Dagar

Final Year B.Tech (Computer Science)
