using DataObject;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfPresentationLayer
{
    /// <summary>
    /// Interaction logic for frmUpdatePassword.xaml
    /// </summary>
    public partial class frmUpdatePassword : Window
    {
        IUserManager _userManager = null;
        User _user = null;

        public frmUpdatePassword(User user, IUserManager userManager)
        {
            InitializeComponent();

            _userManager = userManager;
            _user = user;

            if(_user.PasswordHash == "9C9064C59F1FFA2E174EE754D29" +
                "79BE80DD30DB552EC03E7E327E9B1A4BD594E")
            {
                this.pwdOld.Password = "newuser";
                this.pwdOld.IsEnabled = false;
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if(pwdOld.Password.Length < 7)
            {
                MessageBox.Show("Current password is too short. Try again.");
                pwdOld.Password = "";
                pwdOld.Focus();
                return;
            }
            if(pwdNew.Password.Length<7 || pwdNew.Password == pwdOld.Password)
            {
                MessageBox.Show("The new password you entered was too short " +
                    "or matched your current password.");
                pwdNew.Password = "";
                pwdNew.Focus();
                return;
            }
            if(pwdNew.Password != pwdRetype.Password)
            {
                MessageBox.Show("New Password and Retype must match.");
                pwdNew.Password = "";
                pwdRetype.Password = "";
                pwdNew.Focus();
                return;
            }
            try
            {
                if (_userManager.UpdatePassword(_user.EmployeeID
                    , pwdOld.Password.ToString()
                    , pwdNew.Password.ToString()))
                {
                    MessageBox.Show("Password Updated.");
                    this.DialogResult = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                this.DialogResult = false;
            }

            
        }
    }
}
