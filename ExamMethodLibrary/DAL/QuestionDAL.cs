using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamMethodLibrary.DAL
{
   public  class QuestionDAL
    {
        public static bool InsertQuestion(int examID, string questionText, string optionA, string optionB, string optionC, string optionD, char correctOption, int marks)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_InsertQuestion", DBHelper.Instance.GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ExamID", examID);
                    cmd.Parameters.AddWithValue("@QuestionText", questionText);
                    cmd.Parameters.AddWithValue("@OptionA", optionA);
                    cmd.Parameters.AddWithValue("@OptionB", optionB);
                    cmd.Parameters.AddWithValue("@OptionC", optionC);
                    cmd.Parameters.AddWithValue("@OptionD", optionD);
                    cmd.Parameters.AddWithValue("@CorrectOption", correctOption);
                    cmd.Parameters.AddWithValue("@Marks", marks);

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

        public static bool UpdateQuestion(int questionID, int examID, string questionText, string optionA, string optionB, string optionC, string optionD, char correctOption, int marks)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_UpdateQuestion", DBHelper.Instance.GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@QuestionID", questionID);
                    cmd.Parameters.AddWithValue("@ExamID", examID);
                    cmd.Parameters.AddWithValue("@QuestionText", questionText);
                    cmd.Parameters.AddWithValue("@OptionA", optionA);
                    cmd.Parameters.AddWithValue("@OptionB", optionB);
                    cmd.Parameters.AddWithValue("@OptionC", optionC);
                    cmd.Parameters.AddWithValue("@OptionD", optionD);
                    cmd.Parameters.AddWithValue("@CorrectOption", correctOption);
                    cmd.Parameters.AddWithValue("@Marks", marks);

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

        public static bool DeleteQuestion(int questionID)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_DeleteQuestion", DBHelper.Instance.GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@QuestionID", questionID);

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

        public static DataTable GetQuestionsByExamId(int examID)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_GetQuestionsByExamId", DBHelper.Instance.GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ExamID", examID);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
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
    }
}
