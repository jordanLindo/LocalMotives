using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataObject;

namespace LogicLayer
{
    public class CustomerManager : ICustomerManager
    {
        private ICustomerAccessor _customerAccessor;
        

        public CustomerManager()
        {
            _customerAccessor = new CustomerAccessor();
        }

        public bool AddCustomer(Customer customer)
        {
            bool result = true;
            try
            {
                result = _customerAccessor.InsertCustomer(customer) > 0;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Customer not added. " + ex);
            }
            return result;
        }

        public Customer AuthenticateCustomerByEmail(string email)
        {
            Customer result = null;

            try
            {
                result = _customerAccessor.AuthenticateCustomerByEmail(email);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Commuication with the database failed. ",ex);
            }

            return result;
        }

        public bool EditCustomer(Customer oldCustomer, Customer newCustomer)
        {
            bool result = false;
            try
            {
                result = (1 == _customerAccessor.UpdateCustomer(oldCustomer, newCustomer));
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Update failed ",ex);
            }

            return result;
        }

        public bool FindCustomer(string email)
        {
            try
            {
                return null != _customerAccessor.SelectCustomerByEmail(email);
            }
            catch (Exception)
            {

                throw new ApplicationException("Data not found.");
            }
        }
    }
}
