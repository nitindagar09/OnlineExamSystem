using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Design;


namespace ExamMethodLibrary.DAL
{
    public class AdminLoginResult
    {
        public int ResultCode { get; set; } // 0: Invalid, 1: Inactive, 2: Valid
        public int? AdminId { get; set; }   // Set only if valid
    }
    public class AdminDAL
    {
        public static bool InsertAdmin(string fullName, string email, string passwordHash, string contactNumber,string activationId)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_InsertAdmin", DBHelper.Instance.GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FullName", fullName);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@PasswordHash", passwordHash);
                    cmd.Parameters.AddWithValue("@ContactNumber", contactNumber);
                    cmd.Parameters.AddWithValue("@ActivationId", activationId);
                    
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

        public DataTable GetAdminByEmail(string email)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_GetAdminByEmail", DBHelper.Instance.GetConnection()))
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

        public static bool ActivateAdminByActivationId(Guid activationId)
        {
            
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_ActivateAdminByActivationId", DBHelper.Instance.GetConnection()))
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


        public static AdminLoginResult IsAdminValid(string email, string password)
        {
            var result = new AdminLoginResult();

            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_VerifyLoginInfo", DBHelper.Instance.GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@input_email", email);
                    cmd.Parameters.AddWithValue("@input_password", password);

                    cmd.Parameters.Add("@Result", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@admin_id", SqlDbType.Int).Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();

                    result.ResultCode = Convert.ToInt32(cmd.Parameters["@Result"].Value);

                    if (result.ResultCode == 2)
                    {
                        result.AdminId = Convert.ToInt32(cmd.Parameters["@admin_id"].Value);
                    }
                }
            }
            catch
            {
                result.ResultCode = 0; // treat as invalid
            }
            finally
            {
                DBHelper.Instance.CloseConnection();
            }

            return result;
        }



        public static string GetActivationId(string email)
        {
            try
            {
                using(SqlCommand cmd = new SqlCommand("sp_ResendEmail", DBHelper.Instance.GetConnection()))
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
                DBHelper.Instance.CloseConnection( );
            }
        }


        public static string getForgotPassword(string email)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_getForgotPassword", DBHelper.Instance.GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.Add("@Password", SqlDbType.NVarChar, 50).Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();

                    string password = (string)cmd.Parameters["@Password"].Value;

                    return password;
                }
            }
            catch(Exception ex) 
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

        public static int checkAdminRegistered(string email)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_checkAdminRegistered", DBHelper.Instance.GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Email", email);

                    var result = cmd.ExecuteScalar();
                    if (result != null)
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



        public static DataTable getAllStudents()
        {
            DataTable dt = new DataTable();
            try
            {
                using(SqlCommand cmd = new SqlCommand("sp_getAllStudents", DBHelper.Instance.GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            catch
            {

            }
            finally
            {
                DBHelper.Instance.CloseConnection();
            }
            return dt;
        }

        public static string getstoredImageBase64String(string email)
        {
                using (SqlCommand cmd = new SqlCommand("sp_GetUserEmbedding", DBHelper.Instance.GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Email", email);
                    var result = cmd.ExecuteScalar();
                    return result?.ToString();
                }
        }
    }

}
