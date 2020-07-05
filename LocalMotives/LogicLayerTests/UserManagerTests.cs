using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccessLayer;
using DataAccessFakes;
using LogicLayer;
using DataObject;

namespace LogicLayerTests
{
    /// <summary>
    /// Summary description for UserManagerTests
    /// </summary>
    [TestClass]
    public class UserManagerTests
    {
        IUserAccessor _userAcccessor;
        IUserManager _userManager;

        public UserManagerTests()
        {
        }

        [TestMethod]
        public void AuthenticateUserTest()
        {
            _userAcccessor = new FakeUserAccessor();
            _userManager = new UserManager(_userAcccessor);
            string email = "name@work.com";
            string password = "newuser";

            User result = _userManager.AuthenticateUser(email, password);

            Assert.AreEqual(result.EmployeeID, 1000);
        }

        [TestMethod]
        public void UpdateUserActiveTest()
        {
            _userAcccessor = new FakeUserAccessor();
            _userManager = new UserManager(_userAcccessor);
            int employeeID = 1000;

            bool result = _userManager.UpdateUserActive(false, employeeID);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void RetrieveUserIDFromEmailTest()
        {
            _userAcccessor = new FakeUserAccessor();
            _userManager = new UserManager(_userAcccessor);
            string email = "name@work.com";

            int target = 1000;

            int result = _userManager.RetrieveUserIDFromEmail(email);

            Assert.AreEqual(target, result);
        }

        [TestMethod]
        public void AddUserTest()
        {
            _userAcccessor = new FakeUserAccessor();
            _userManager = new UserManager(_userAcccessor);
            User user = new User
            {
                Email = "anothername@work.com",
                PasswordHash = "9C9064C59F1FFA2E174EE754D2979BE80DD30DB552EC03E7E327E9B1A4BD594E",
                Active = true,
                Roles = new List<string>
                {
                    "Role1",
                    "Role2"
                }
            };

            bool result = _userManager.AddUser(user);

            Assert.IsTrue(result);

        }

        [TestMethod]
        public void DeleteEmployeeRole()
        {
            _userAcccessor = new FakeUserAccessor();
            _userManager = new UserManager(_userAcccessor);
            int employeeID = 1000;
            string role = "Role2";

            bool result = _userManager.DeleteUserRole(employeeID, role);
            
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void FindUserTest()
        {
            _userAcccessor = new FakeUserAccessor();
            _userManager = new UserManager(_userAcccessor);
            string email = "name@work.com";
            string notAnEmail = "notAnEmail";

            bool result = _userManager.FindUser(email);
            bool badResult = _userManager.FindUser(notAnEmail);

            Assert.IsTrue(result);
            Assert.IsFalse(badResult);

        }

        [TestMethod]
        public void RetrieveUserListByActiveTest()
        {
            _userAcccessor = new FakeUserAccessor();
            _userManager = new UserManager(_userAcccessor);

            List<User> result = _userManager.RetrieveUserListByActive();
            List<User> resultInactive = _userManager.RetrieveUserListByActive(false);

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(1, resultInactive.Count);
        }

        [TestMethod]
        public void ValidateTest()
        {
            _userAcccessor = new FakeUserAccessor();
            _userManager = new UserManager(_userAcccessor);
            string goodWords = "Good Words.";
            List<string> badStrings = new List<string>
            {
                ";",
                "'",
                "--",
                "/*",
                "*/",
                "xp_",
                "drop with words after",
                "words before drop"
            };

            bool result = _userManager.Validate(goodWords);

            Assert.IsTrue(result);

            foreach (string badString in badStrings)
            {
                result = _userManager.Validate(badString);

                Assert.IsFalse(result);

            }
        }

        [TestMethod]
        public void UpdatePasswordTest()
        {
            _userAcccessor = new FakeUserAccessor();
            _userManager = new UserManager(_userAcccessor);
            int employeeID = 1000;
            string oldPassword = "newuser";
            string newPassword = "olduser";

            bool result = _userManager.UpdatePassword(employeeID, oldPassword, newPassword);
            bool badResult = _userManager.UpdatePassword(employeeID, oldPassword, oldPassword);

            Assert.IsTrue(result);
            Assert.IsFalse(badResult);
        }

        [TestMethod]
        public void UpdateUser()
        {
            _userAcccessor = new FakeUserAccessor();
            _userManager = new UserManager(_userAcccessor);
            User oldUser = new User
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
            };
            User newUser = new User
            {
                EmployeeID = 1001,
                Email = "someothername@work.com",
                PasswordHash = "9C9064C59F1FFA2E174EE754D2979BE80DD30DB552EC03E7E327E9B1A4BD594E",
                Active = false,
                Roles = new List<string>
                {
                    "Role1",
                    "Role2"
                }
            };

            bool result = _userManager.UpdateUser(oldUser, newUser);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void RetrieveEmployeeRolesTest()
        {
            _userAcccessor = new FakeUserAccessor();
            _userManager = new UserManager(_userAcccessor);
            int employeeID = 1000;

            List<string> roles = _userManager.RetrieveEmployeeRoles(employeeID);

            Assert.AreEqual(2, roles.Count);
        }

        [TestMethod]
        public void RetrieveAllEmployeeRoles()
        {
            _userAcccessor = new FakeUserAccessor();
            _userManager = new UserManager(_userAcccessor);

            List<string> result = _userManager.RetrieveAllEmployeeRoles();

            Assert.AreEqual(3, result.Count);
        }

        [TestMethod]
        public void InsertUserRoleTest()
        {
            _userAcccessor = new FakeUserAccessor();
            _userManager = new UserManager(_userAcccessor);
            int userID = 1000;
            string role = "Role3";

            bool result = _userManager.InsertUserRole(userID, role);

            Assert.IsTrue(result);
        }
    }
}
