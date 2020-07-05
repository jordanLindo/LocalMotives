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
    /// Interaction logic for frmUpdateUser.xaml
    /// </summary>
    public partial class frmUpdateUser : Window
    {
        IUserManager _userManager;
        User _user;

        public frmUpdateUser(IUserManager userManager, User user)
        {
            _userManager = userManager;
            _user = user;
            InitializeComponent();
            lblEmployeeID.Content = _user.EmployeeID;
            lblFirstName.Content = _user.FirstName;
            lblLastName.Content = _user.LastName;
            lblEmail.Content = _user.Email;
            lblPhone.Content = _user.PhoneNumber;
            checkActive();
            populateRoles();
        }

        private void checkActive()
        {
            if (_user.Active)
            {
                chkActive.IsChecked = true;
            }
            else
            {
                chkActive.IsChecked = false;
            }
        }

        private void populateRoles()
        {
            try
            {
                var eRoles = _userManager.RetrieveEmployeeRoles(_user.EmployeeID);
                lstAssignedRoles.ItemsSource = eRoles;

                var roles = _userManager.RetrieveAllEmployeeRoles();
                foreach (string r in eRoles)
                {
                    roles.Remove(r);
                }
                lstUnassignedRoles.ItemsSource = roles;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }
        }

        private void ChkActive_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _userManager.UpdateUserActive((bool)chkActive.IsChecked,_user.EmployeeID);
            }
            catch (Exception ex)
            {

                MessageBox.Show("Failed to update active. "+ ex.InnerException);
            }
        }

        private void LstUnassignedRoles_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lstUnassignedRoles.SelectedItems.Count == 0)
            {
                return;
            }
            if (MessageBox.Show("Are you sure?", "Change Role Assignment",
                    MessageBoxButton.YesNo, MessageBoxImage.Warning)
                    == MessageBoxResult.No)
            {
                return;
            }

            try
            {
                if (_userManager.InsertUserRole(_user.EmployeeID,
                    (string)lstUnassignedRoles.SelectedItem))
                {
                    populateRoles();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }
        }

        private void LstAssignedRoles_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lstAssignedRoles.SelectedItems.Count == 0)
            {
                return;
            }
            if (MessageBox.Show("Are you sure?", "Change Role Assignment",
                    MessageBoxButton.YesNo, MessageBoxImage.Warning)
                    == MessageBoxResult.No)
            {
                return;
            }
            try
            {
                if (_userManager.DeleteUserRole(_user.EmployeeID,
                    (string)lstAssignedRoles.SelectedItem))
                {
                    populateRoles();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
