using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamMethodLibrary.DAL
{
  public  class SubjectDAL
    {
        public static int InsertSubject(string subjectName, int CreatedBy)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_InsertSubject", DBHelper.Instance.GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SubjectName", subjectName);
                    cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
                    // cmd.Parameters.AddWithValue("@IsActive", isActive);

                    cmd.ExecuteNonQuery();
                    return 1;
                }
            }
            catch(SqlException ex)
            {
                // You can log exception if needed
                if (ex.Number == 50000)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            }
            finally
            {
                DBHelper.Instance.CloseConnection();
            }
        }


        public static DataTable GetSubjects(int statusFilter,int AdminID)
        {
            DataTable dtSubjects = new DataTable();

            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_GetSubjects", DBHelper.Instance.GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StatusFilter", statusFilter);
                    cmd.Parameters.AddWithValue("@AdminID", AdminID);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtSubjects);
                    }
                }
            }
            catch
            {
                // Optional: log the exception
            }
            finally
            {
                DBHelper.Instance.CloseConnection();
            }

            return dtSubjects;
        }


        public static bool SetSubjectStatus(int subjectId, bool isActive)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_SetSubjectStatus", DBHelper.Instance.GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SubjectID", subjectId);
                    cmd.Parameters.AddWithValue("@IsActive", isActive);
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch
            {
                // You may log error here
                return false;
            }
            finally
            {
                DBHelper.Instance.CloseConnection();
            }
        }


        public static bool UpdateSubject(int subjectId, string newName)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_UpdateSubject", DBHelper.Instance.GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SubjectID", subjectId);
                    cmd.Parameters.AddWithValue("@SubjectName", newName);

                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch
            {
                // Optional: Handle or log the specific error if needed
                return false;
            }
            finally
            {
                DBHelper.Instance.CloseConnection();
            }
        }

        public static void DeleteSubject(int subjectId)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_DeleteSubject", DBHelper.Instance.GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SubjectID", subjectId);
                    cmd.ExecuteNonQuery();
                    

                }
            }
            catch(Exception ex)
            {
                throw new ApplicationException("Error deleting subject", ex);
               
            }
            finally
            {
                DBHelper.Instance.CloseConnection();
            }
           
        }
    }

}
