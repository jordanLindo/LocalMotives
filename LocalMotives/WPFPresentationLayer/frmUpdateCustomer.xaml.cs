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
    /// Interaction logic for frmUpdateCustomer.xaml
    /// </summary>
    public partial class frmUpdateCustomer : Window
    {
        private ICustomerManager _customerManager;
        private Customer _customer;
        private Customer _oldCustomer;

        public frmUpdateCustomer(Customer customer, ICustomerManager customerManager)
        {
            InitializeComponent();
            _customerManager = customerManager;
            _customer = customer;

        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            _oldCustomer = _customerManager.AuthenticateCustomerByEmail(_customer.Email);
            MessageBox.Show(_oldCustomer.LastName);

            if(txtFirstName.Text != null && txtLastName.Text != null
                && !txtFirstName.Text.Equals(" ") && !txtLastName.Text.Equals(" "))
            {
                _customer.FirstName = txtFirstName.Text;
                _customer.LastName = txtLastName.Text;
                try
                {
                    _customerManager.EditCustomer(_oldCustomer, _customer);
                    
                    this.DialogResult = true;
                    this.Close();
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                    this.DialogResult = false;
                }
                
                
            }
        }

        
        
    }
}
