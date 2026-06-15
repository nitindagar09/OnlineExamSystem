using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamMethodLibrary.DAL
{
  public  class ExamDAL
    {
        public static bool InsertExam( int subjectId, string subjectName, string examTitle, DateTime examDate, TimeSpan startTime, TimeSpan endTime, int durationInMinutes)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_InsertExam", DBHelper.Instance.GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SubjectID", subjectId);
                    cmd.Parameters.AddWithValue("@SubjectName", subjectName);
                    cmd.Parameters.AddWithValue("@ExamTitle", examTitle);
                    cmd.Parameters.AddWithValue("@ExamDate", examDate.Date);
                    cmd.Parameters.AddWithValue("@StartTime", startTime);
                    cmd.Parameters.AddWithValue("@EndTime", endTime);
                    cmd.Parameters.AddWithValue("@DurationInMinutes", durationInMinutes);

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

        public static DataTable GetExams(int statusFilter, int adminId)
        {
            DataTable dtExams = new DataTable();

            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_GetExams", DBHelper.Instance.GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StatusFilter", statusFilter);
                    cmd.Parameters.AddWithValue("@AdminID", adminId);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtExams);
                    }
                }
            }
            catch
            {
                // Optional: log exception
            }
            finally
            {
                DBHelper.Instance.CloseConnection();
            }

            return dtExams;
        }


        public static bool UpdateExam(int examId, int subjectId, string examTitle, DateTime examDate, TimeSpan startTime, TimeSpan endTime, int durationInMinutes)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_UpdateExam", DBHelper.Instance.GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ExamID", examId);
                    cmd.Parameters.AddWithValue("@SubjectID", subjectId);
                    cmd.Parameters.AddWithValue("@ExamTitle", examTitle);
                    cmd.Parameters.AddWithValue("@ExamDate", examDate);
                    cmd.Parameters.AddWithValue("@StartTime", startTime);
                    cmd.Parameters.AddWithValue("@EndTime", endTime);
                    cmd.Parameters.AddWithValue("@DurationInMinutes", durationInMinutes);

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

        public static bool SetExamStatus(int examId, bool isActive)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_SetExamStatus", DBHelper.Instance.GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ExamID", examId);
                    cmd.Parameters.AddWithValue("@IsActive", isActive);

                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch
            {
                // Log exception if needed
                return false;
            }
            finally
            {
                DBHelper.Instance.CloseConnection();
            }
        }




    }
}
