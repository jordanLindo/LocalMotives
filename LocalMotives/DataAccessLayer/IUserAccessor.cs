using DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface IUserAccessor
    {
        User AuthenticateUser(string username, string passwordHash);

        bool UpdatePasswordHash(int employeeID, string oldPassworHash, string newPasswordHash);

        List<User> SelectUsersByActive(bool active = true);

        int UpdateUser(User oldUser, User newUser);

        int InsertUser(User user);

        int InsertUserRole(int userID, string roleID);

        List<string> SelectRolesByEmployeeID(int employeeID);

        List<string> SelectAllRoles();

        int SetUserActive(bool active,int userID);

        int DeleteEmployeeRole(int employeeID, string role);

        User GetUserByEmail(string email);

        int CountAdmin();
    }
}
