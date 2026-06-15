using ExamMethodLibrary.DAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamMethodLibrary.Student
{
    public class StudentDAL
    {
        public static bool InsertStudent(string fullName, string email, string passwordHash, string contactNumber, string activationId, string embeddingJson, string faceImagePath)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_InsertStudent", DBHelper.Instance.GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FullName", fullName);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@PasswordHash", passwordHash);
                    cmd.Parameters.AddWithValue("@ContactNumber", contactNumber);
                    cmd.Parameters.AddWithValue("@ActivationId", activationId);
                    cmd.Parameters.AddWithValue("@FaceEmbedding", embeddingJson);
                    cmd.Parameters.AddWithValue("@FacePath", faceImagePath);
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch
            {
                // Log exception
                return false;
            }
            finally
            {
                DBHelper.Instance.CloseConnection();
            }
        }

        public DataTable GetStudentByEmail(string email)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_GetStudentByEmail", DBHelper.Instance.GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Email", email);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            catch
            {
                // Log exception
            }
            finally
            {
                DBHelper.Instance.CloseConnection();
            }
            return dt;
        }

        public static bool ActivateStudentByActivationId(Guid activationId)
        {
           
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_ActivateStudentByActivationId", DBHelper.Instance.GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ActivationId", activationId);
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception)
            {
                //throw;
                // Log exception
                return false;
            }
            finally
            {
                DBHelper.Instance.CloseConnection();
            }

            //  return rowsAffected > 0;
        }

        // UpdateAdmin() and SetAdminStatus() can be similarly added here


        public static int IsStudentValid(string email, string Password)
        {
            
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_VerifyStudentLoginInfo", DBHelper.Instance.GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@input_email", email);
                    cmd.Parameters.AddWithValue("@input_password", Password);

                    cmd.Parameters.Add("@Result", SqlDbType.Int).Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();

                    int resultValue = (int)cmd.Parameters["@Result"].Value;

                    return resultValue;

                }
            }
            catch
            {
                return 0;
            }
            finally
            {
                DBHelper.Instance.CloseConnection();
            }
        }


        public static string GetActivationId(string email)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_GetStudentActivationId", DBHelper.Instance.GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.Add("@ActivationId", SqlDbType.UniqueIdentifier).Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();

                    string ActivationId = (cmd.Parameters["@ActivationId"].Value).ToString();

                    return ActivationId;
                }
            }
            catch
            {
                return null;
            }
            finally
            {
                DBHelper.Instance.CloseConnection();
            }
        }


        public static string getForgotPassword(string email)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_getStudentForgotPassword", DBHelper.Instance.GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.Add("@Password", SqlDbType.NVarChar, 50).Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();

                    string password = (string)cmd.Parameters["@Password"].Value;

                    return password;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                Console.WriteLine("Stack Trace: " + ex.StackTrace);
                return null;
            }
            finally
            {
                DBHelper.Instance.CloseConnection();
            }
        }


        public static int checkStudentRegistered(string email)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_checkStudentRegistered", DBHelper.Instance.GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Email", email);

                    var result = cmd.ExecuteScalar();
                    if(result != null)
                    {
                        return (int)result;
                    }
                    else
                    {
                        return 0;
                    }
                    
                }
            }
            catch
            {
                return 0;
            }
            finally
            {
                DBHelper.Instance.CloseConnection();
            }
        }

        public static DataTable GetAllExams()
        {
            DataTable examsTable = new DataTable();
            try
            {
                using(SqlCommand cmd = new SqlCommand("sp_GetAllExams", DBHelper.Instance.GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataReader reader = cmd.ExecuteReader();
                    examsTable.Load(reader);
                }
            }
            catch
            {

            }
            finally
            {

            }
            return examsTable;
        }

        public static int CheckStudentExamTaken(int studentId, int examId)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_CheckStudentExamTaken", DBHelper.Instance.GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StudentID", studentId);
                    cmd.Parameters.AddWithValue("@ExamID", examId);

                    SqlParameter resultParam = new SqlParameter("@Result", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(resultParam);

                    cmd.ExecuteNonQuery();

                    return (int)resultParam.Value;
                }
            }
            catch
            {
                return -1; // Error case
            }
            finally
            {
                DBHelper.Instance.CloseConnection();
            }
        }
        public static bool insertStudentAnswer(int StudentID, int ExamID, int QuestionID, char givenAnswerOption)
        {
            try
            {
                using(SqlCommand cmd = new SqlCommand("sp_InsertStudentAnswer", DBHelper.Instance.GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@StudentID", StudentID);
                    cmd.Parameters.AddWithValue("@ExamID", ExamID);
                    cmd.Parameters.AddWithValue("@QuestionID", QuestionID);
                    cmd.Parameters.AddWithValue("@givenAnswerOption", givenAnswerOption);

                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                DBHelper.Instance.CloseConnection();
            }
        }


        public static DataSet GetStudentExamReport(int studentId, int examId)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("GetStudentExamReport", DBHelper.Instance.GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@StudentId", studentId);
                    cmd.Parameters.AddWithValue("@ExamId", examId);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataSet ds = new DataSet();
                        adapter.Fill(ds);
                        return ds;
                    }
                }
            }
            catch
            {
                return null;
            }
            finally
            {
                DBHelper.Instance.CloseConnection();
            }
        }


        public DataTable GetStudentResults(int studentId)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_GetStudentResults", DBHelper.Instance.GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StudentID", studentId);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
            catch (Exception ex)
            {
                // Log exception
                throw new Exception("Error fetching student results", ex);
            }
            finally
            {
                DBHelper.Instance.CloseConnection();

            }
        }
    }
}
