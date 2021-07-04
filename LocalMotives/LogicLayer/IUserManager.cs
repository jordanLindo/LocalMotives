using DataObject;
using System.Collections.Generic;

namespace LogicLayer
{
    public interface IUserManager
    {
        User AuthenticateUser(string email, string password);

        User GetUserByEmail(string email);

        bool UpdatePassword(int employeeID, string oldPassword, string newPassword);

        List<User> RetrieveUserListByActive(bool active = true);

        bool UpdateUser(User oldUser, User newUser);

        bool AddUser(User user);

        bool InsertUserRole(int userID, string roleID);

        bool Validate(string input);

        List<string> RetrieveEmployeeRoles(int employeeID);

        List<string> RetrieveAllEmployeeRoles();

        bool UpdateUserActive(bool active,int user);

        bool DeleteUserRole(int employeeID, string role);

        bool FindUser(string email);

        int RetrieveUserIDFromEmail(string email);

        int CountAdmin();
    }
}