using DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public interface ICustomerManager
    {
        Customer AuthenticateCustomerByEmail(string email);
        bool AddCustomer(Customer customer);
        bool EditCustomer(Customer oldCustomer, Customer newCustomer);
        bool FindCustomer(string email);
    }
}
