using DataAccessLayer;
using DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessFakes
{
    public class FakeUserAccessor : IUserAccessor
    {

        private List<User> users = new List<User>
        {
            new User
            {
                EmployeeID = 1000,
                Email = "name@work.com",
                PasswordHash = "9C9064C59F1FFA2E174EE754D2979BE80DD30DB552EC03E7E327E9B1A4BD594E",
                Active = true,
                Roles = new List<string>
                {
                    "Role1",
                    "Role2"
                }

            },
            new User
            {
                EmployeeID = 1001,
                Email = "othername@work.com",
                PasswordHash = "9C9064C59F1FFA2E174EE754D2979BE80DD30DB552EC03E7E327E9B1A4BD594E",
                Active = false,
                Roles = new List<string>
                {
                    "Role1",
                    "Role2"
                }

            }
        };

        private List<String> roles = new List<string>
        {
            "Role1",
            "Role2",
            "Role3"
        };

        public User AuthenticateUser(string username, string passwordHash)
        {
            User result = null;
            foreach (User user in users)
            {
                if (user.Email == username && user.PasswordHash == passwordHash)
                {
                    result = user;
                }
            }
            return result;
        }

        public int DeleteEmployeeRole(int employeeID, string role)
        {
            int rows = 0;
            foreach (User user in users)
            {
                if (user.EmployeeID == employeeID)
                {
                    user.Roles.Remove(role);
                    rows++;
                }
            }
            return rows;
        }

        public User GetUserByEmail(string email)
        {
            User result = null;

            foreach (User user in users)
            {
                if (user.Email == email)
                {
                    result = user;
                }
            }
            return result;

        }

        public int InsertUser(User user)
        {
            users.Add(user);
            return 1;
        }

        public int InsertUserRole(int userID, string roleID)
        {
            int result = 0;
            foreach (User user in users)
            {
                if (user.EmployeeID == userID)
                {
                    user.Roles.Add(roleID);
                    result++;
                }
            }
            return result;
        }

        public List<string> SelectAllRoles()
        {
            return roles;
        }

        public List<string> SelectRolesByEmployeeID(int employeeID)
        {
            List<string> result = new List<string>();
            foreach (User user in users)
            {
                if(user.EmployeeID == employeeID)
                {
                    result = user.Roles;
                }
            }
            return result;
        }

        public List<User> SelectUsersByActive(bool active = true)
        {
            List<User> result = new List<User>();
            foreach (User user in users)
            {
                if(user.Active == active)
                {
                    result.Add(user);
                }
            }
            return result;
        }

        public int SetUserActive(bool active, int userID)
        {
            int result = 0;
            foreach (User user in users)
            {
                if(user.EmployeeID == userID)
                {
                    user.Active = active;
                    result++;
                }
            }

            return result;
        }

        public bool UpdatePasswordHash(int employeeID, string oldPassworHash, string newPasswordHash)
        {
            bool result = false;
            foreach (User user in users)
            {
                if(user.EmployeeID == employeeID && user.PasswordHash == oldPassworHash)
                {
                    user.PasswordHash = newPasswordHash;
                    result = true;
                }
            }
            return result;
        }

        public int UpdateUser(User oldUser, User newUser)
        {
            int result = 0;
            bool done = false;
            for (int i = 0; i < users.Count && !done; i++)
            {
                User user = users[i];
                if (user.EmployeeID == oldUser.EmployeeID)
                {
                    users[i] = newUser;
                    done = true;
                    result++;
                }
            }
            return result;
        }
    }
}
