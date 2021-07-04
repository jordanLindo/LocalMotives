using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataObject;

namespace LogicLayer
{
    public class UserManager : IUserManager
    {
        private IUserAccessor _userAccessor;

        public UserManager()
        {
            _userAccessor = new UserAccessor();
        }
        public UserManager(IUserAccessor userAccessor)
        {
            _userAccessor = userAccessor;
        }

        public User AuthenticateUser(string email, string password)
        {
            User result = null;

            var passwordHash = hashPassword(password);
            password = null;

            try
            {
                result = _userAccessor.AuthenticateUser(email, passwordHash);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Login failed", ex);
            }

            return result;
        }

        public User GetUserByEmail(string email)
        {

            User result = null;

            try
            {
                result = _userAccessor.GetUserByEmail(email);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Failed to get user.", ex);
            }

            return result;
        }

        private string hashPassword(string source)
        {
            string result = null;

            byte[] data;

            using (SHA256 sha256hash = SHA256.Create())
            {
                data = sha256hash.ComputeHash(Encoding.UTF8.GetBytes(source));
            }
            var s = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                s.Append(data[i].ToString("x2"));
            }
            result = s.ToString().ToUpper();

            return result;
        }

        public List<User> RetrieveUserListByActive(bool active = true)
        {
            try
            {
                return _userAccessor.SelectUsersByActive(active);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data exception", ex);
            }
        }

        public bool UpdatePassword(int employeeID, string oldPassword, string newPassword)
        {
            bool isUpdated = false;
            if (!oldPassword.Equals(newPassword))
            {
                string newPasswordHash = hashPassword(newPassword);
                string oldPasswordHash = hashPassword(oldPassword);

                try
                {
                    isUpdated = _userAccessor.UpdatePasswordHash(employeeID, oldPasswordHash, newPasswordHash);
                }
                catch (Exception ex)
                {

                    throw new ApplicationException("Update Failed", ex);
                }

            }
            return isUpdated;
        }

        public bool UpdateUser(User oldUser, User newUser)
        {
            try
            {
                return 1 == _userAccessor.UpdateUser(oldUser, newUser);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Update failed.", ex);
            }
        }

        public bool Validate(string input)
        {
            bool valid = false;

            if (!input.Contains(";") && !input.Contains("'")
                && !input.Contains("--") && !input.Contains("/*")
                    && !input.Contains("*/") && !input.Contains("xp_")
                    && (input.IndexOf("drop", StringComparison.OrdinalIgnoreCase) == -1))
            {
                valid = true;
            }

            return valid;
        }

        public bool AddUser(User user)
        {
            bool success = false;

            try
            {
                success = 1 == _userAccessor.InsertUser(user);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Failed to add user.", ex);
            }

            return success;
        }

        public List<string> RetrieveEmployeeRoles(int employeeID)
        {
            List<string> roles = null;

            try
            {
                roles = _userAccessor.SelectRolesByEmployeeID(employeeID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Roles not found", ex);
            }

            return roles;
        }

        public List<string> RetrieveAllEmployeeRoles()
        {
            List<string> roles = null;

            try
            {
                roles = _userAccessor.SelectAllRoles();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Roles not found.", ex);
            }

            return roles;
        }

        public bool InsertUserRole(int userID, string roleID)
        {
            bool success = false;
            try
            {
                success = 1 == _userAccessor.InsertUserRole(userID, roleID);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Failed to add role.", ex);
            }


            return success;
        }

        public bool UpdateUserActive(bool active, int userID)
        {
            bool success = false;
            try
            {
                success = 1 == _userAccessor.SetUserActive(active, userID);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Failed to update.", ex);
            }

            return success;
        }

        public bool DeleteUserRole(int employeeID, string role)
        {

            bool result = false;
            try
            {
                result = 1 == _userAccessor.DeleteEmployeeRole(employeeID, role);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Role not removed!", ex);
            }
            return result;
        }

        public bool FindUser(string email)
        {
            try
            {
                return null != _userAccessor.GetUserByEmail(email);
            }
            catch (ApplicationException ax)
            {
                if (ax.Message == "User not found.")
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Database Error", ex);
            }
        }

        public int RetrieveUserIDFromEmail(string email)
        {
            try
            {
                return _userAccessor.GetUserByEmail(email).EmployeeID;
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Database Error", ex);
            }
        }

        public int CountAdmin()
        {
            try
            {
                return _userAccessor.CountAdmin();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
