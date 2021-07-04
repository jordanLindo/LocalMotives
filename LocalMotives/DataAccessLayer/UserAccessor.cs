using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObject;

namespace DataAccessLayer
{
    public class UserAccessor : IUserAccessor
    {
        public User AuthenticateUser(string username, string passwordHash)
        {
            User result = null;

            var conn = DBConnection.GetConnection();

            var cmd = new SqlCommand("sp_authenticate_user");
            cmd.Connection = conn;

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 250);
            cmd.Parameters.Add("@PasswordHash", SqlDbType.NVarChar, 100);

            cmd.Parameters["@Email"].Value = username;
            cmd.Parameters["@PasswordHash"].Value = passwordHash;

            try
            {
                conn.Open();
               
                if (1 == Convert.ToInt32(cmd.ExecuteScalar()))
                {
                    result = GetUserByEmail(username);
                }
                else
                {
                    throw new ApplicationException("User not found.");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }

        public User GetUserByEmail(string email)
        {
            User user = null;

            var conn = DBConnection.GetConnection();
            var cmd1 = new SqlCommand("sp_select_employee_by_email");
            var cmd2 = new SqlCommand("sp_select_roles_by_employeeID");

            cmd1.Connection = conn;
            cmd2.Connection = conn;

            cmd1.CommandType = CommandType.StoredProcedure;
            cmd2.CommandType = CommandType.StoredProcedure;

            cmd1.Parameters.Add("@Email", SqlDbType.NVarChar, 250);
            cmd1.Parameters["@Email"].Value = email;

            cmd2.Parameters.Add("@EmployeeID", SqlDbType.Int);

            try
            {
                conn.Open();

                var reader1 = cmd1.ExecuteReader();

                if(reader1.Read())
                {
                    user = new User();
                    user.EmployeeID = reader1.GetInt32(0);
                    user.FirstName = reader1.GetString(1);
                    user.LastName = reader1.GetString(2);
                    user.Email = reader1.GetString(3);
                    user.PhoneNumber = reader1.GetString(4);
                    user.Active = reader1.GetBoolean(5);
                    user.PasswordHash = reader1.GetString(6);
                }
                else
                {
                    throw new ApplicationException("User not found.");
                }
                reader1.Close();
                cmd2.Parameters["@EmployeeID"].Value = user.EmployeeID;

                var reader2 = cmd2.ExecuteReader();
                List<string> roles = new List<string>();

                while (reader2.Read())
                {
                    string role = reader2.GetString(0);
                    roles.Add(role);
                }
                user.Roles = roles;
            }
            catch (SqlException ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return user;
        }

        public List<User> SelectUsersByActive(bool active = true)
        {
            List<User> users = new List<User>();
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_employee_by_active");
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Active", SqlDbType.Bit);
            cmd.Parameters["@Active"].Value = active;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while(reader.Read())
                    {
                        var user = new User();
                        user.EmployeeID = reader.GetInt32(0);
                        user.FirstName = reader.GetString(1);
                        user.LastName = reader.GetString(2);
                        user.Email = reader.GetString(3);
                        user.PhoneNumber = reader.GetString(4);
                        user.Active = reader.GetBoolean(5);
                        user.PasswordHash = reader.GetString(6);
                        users.Add(user);
                    }
                }
                reader.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return users;
        }

        public bool UpdatePasswordHash(int employeeID, string oldPassworHash, string newPasswordHash)
        {
            bool updateSuccess = false;

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_update_password");

            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@EmployeeID", SqlDbType.Int);
            cmd.Parameters.Add("@OldPasswordHash", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@NewPasswordHash", SqlDbType.NVarChar, 100);

            cmd.Parameters["@EmployeeID"].Value = employeeID;
            cmd.Parameters["@OldPasswordHash"].Value = oldPassworHash;
            cmd.Parameters["@NewPasswordHash"].Value = newPasswordHash;

            try
            {
                conn.Open();
                int rows = cmd.ExecuteNonQuery();

                updateSuccess = (rows == 1);
                
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();

            }
            return updateSuccess;
        }

        public int UpdateUser(User oldUser, User newUser)
        {
            int rows = 0;

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_update_employee", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmployeeID", oldUser.EmployeeID);
            cmd.Parameters.AddWithValue("@OldFirstName", oldUser.FirstName);
            cmd.Parameters.AddWithValue("@OldLastName", oldUser.LastName);
            cmd.Parameters.AddWithValue("@OldEmail", oldUser.Email);
            cmd.Parameters.AddWithValue("@OldPhoneNumber", oldUser.PhoneNumber);
            cmd.Parameters.AddWithValue("@NewFirstName", newUser.FirstName);
            cmd.Parameters.AddWithValue("@NewLastName", newUser.LastName);
            cmd.Parameters.AddWithValue("@NewEmail", newUser.Email);
            cmd.Parameters.AddWithValue("@NewPhoneNumber", newUser.PhoneNumber);

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }


            return rows;
        }

        public int InsertUser(User user)
        {
            int rows = 0;

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_insert_employee",conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
            cmd.Parameters.AddWithValue("@LastName", user.LastName);
            cmd.Parameters.AddWithValue("@Email", user.Email);
            cmd.Parameters.AddWithValue("@PhoneNumber", user.PhoneNumber);

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }


            return rows;
        }

        public int InsertUserRole(int userID, string roleID)
        {
            int rows = 0;
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_insert_employeerole", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmployeeID", userID);
            cmd.Parameters.AddWithValue("@RoleID", roleID);

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return rows;
        }

        public List<string> SelectRolesByEmployeeID(int employeeID)
        {
            List<string> roles = new List<string>();
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_roles_by_employeeid", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmployeeID", employeeID);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        roles.Add(reader.GetString(0));
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }


            return roles;
        }

        public List<string> SelectAllRoles()
        {
            List<string> roles = new List<string>();
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_all_roles", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        roles.Add(reader.GetString(0));
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }


            return roles;
        }

        public int SetUserActive(bool active,int userID)
        {
            int rows = 0;
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_set_employee_active", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmployeeID", userID);
            cmd.Parameters.AddWithValue("@Active", active);

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }


            return rows;
        }

        public int CountAdmin()
        {
            int count = 0;
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_count_admin", conn);
            try
            {
                conn.Open();
                count = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return count;

        }

        public int DeleteEmployeeRole(int employeeID, string role)
        {
            int rows = 0;
            var conn = DBConnection.GetConnection();
            if (role == "Admin")
            {
                var cmd = new SqlCommand("sp_count_admin", conn);
                try
                {
                    conn.Open();
                    if (1 < Convert.ToInt32(cmd.ExecuteScalar()))
                    {
                        var cmd1 = new SqlCommand("sp_delete_employeerole", conn);
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.AddWithValue("@EmployeeID", employeeID);
                        cmd1.Parameters.AddWithValue("@RoleID", role);
                        try
                        {
                            rows = cmd1.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {

                            throw ex;
                        }
                    }
                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
            else
            {
                
                var cmd1 = new SqlCommand("sp_delete_employeerole", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@EmployeeID", employeeID);
                cmd1.Parameters.AddWithValue("@RoleID", role);
                try
                {
                    conn.Open();
                    rows = cmd1.ExecuteNonQuery();
                }
                catch (Exception ex)
                {

                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }
            return rows;
        }
    }
}
