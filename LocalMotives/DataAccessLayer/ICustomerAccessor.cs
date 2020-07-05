using DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface ICustomerAccessor
    {
        Customer AuthenticateCustomerByEmail(string email);

        Customer SelectCustomerByEmail(string email);

        int InsertCustomer(Customer customer);

        int UpdateCustomer(Customer oldCustomer, Customer newCustomer);
    }
}
