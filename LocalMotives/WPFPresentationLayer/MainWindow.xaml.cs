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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfPresentationLayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private User _user = null;
        private IUserManager _userManager;

        [STAThread]
        static void Main(string[] args)
        {
            var login = new MainWindow();
            login.ShowDialog();
        }
        public MainWindow()
        {
            InitializeComponent();
            _user = new User();
            _userManager = new UserManager();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            var email = txtEmail.Text;
            var password = pwdPassword.Password;

            if(email.Length < 7 || password.Length < 7)
            {
                MessageBox.Show("Bad username or password.",
                    "Login failed", MessageBoxButton.OK, MessageBoxImage.Error);
                txtEmail.Text = "";
                pwdPassword.Password = "";
                txtEmail.Focus();
                return;

            }
            try
            {
                _user = _userManager.AuthenticateUser(email, password);

                
                if(pwdPassword.Password == "newuser")
                {
                    var updatePassword = new frmUpdatePassword(_user, _userManager);
                    if(updatePassword.ShowDialog() == false)
                    {
                        LogOutUser();
                    }
                    else
                    {
                        EmployeeHomePage();
                    }
                }
                else
                {
                    EmployeeHomePage();
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void EmployeeHomePage()
        {
            var home = new EmployeeHome(_user, _userManager);
            home.ShowDialog();
            LogOutUser();
        }

        private void LogOutUser()
        {
            _user = null;
            txtEmail.Text = "";
            pwdPassword.Password = "";
            return;
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            LogOutUser();
        }
    }
}
