using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObject;

namespace DataAccessLayer
{
    public class CustomerAccessor : ICustomerAccessor
    {

        public Customer AuthenticateCustomerByEmail(string email)
        {
            Customer result = null;

            var conn = DBConnection.GetConnection();

            var cmd = new SqlCommand("sp_count_customer_by_email", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 250);
            cmd.Parameters["@Email"].Value = email;

            try
            {
                conn.Open();
                
                if(1 == Convert.ToInt32(cmd.ExecuteScalar()))
                {
                    result = SelectCustomerByEmail(email);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        public Customer SelectCustomerByEmail(string email)
        {
            Customer customer = null;

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_customer_by_email",conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Email", email);

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    customer = new Customer()
                    {
                        CustomerID = reader.GetInt32(0),
                        Email = email,
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2)

                    };
                }
                else
                {
                    throw new ApplicationException("Customer not found.");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return customer;
        }

        public int InsertCustomer(Customer customer)
        {
            int customerID = 0;

            var conn = DBConnection.GetConnection();

            var cmd = new SqlCommand("sp_new_customer", conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Email", customer.Email);
            cmd.Parameters.AddWithValue("@FirstName", customer.FirstName);
            cmd.Parameters.AddWithValue("@LastName", customer.LastName);

            try
            {
                conn.Open();
                customerID = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return customerID;
        }

        public int UpdateCustomer(Customer oldCustomer, Customer newCustomer)
        {
            int rows = 0;
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_update_customer", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CustomerID",SqlDbType.Int);
            cmd.Parameters["@CustomerID"].Value = oldCustomer.CustomerID;
            cmd.Parameters.AddWithValue("@OldEmail", oldCustomer.Email);
            cmd.Parameters.AddWithValue("@OldFirstName", oldCustomer.FirstName);
            cmd.Parameters.AddWithValue("@OldLastName", oldCustomer.LastName);
            cmd.Parameters.AddWithValue("@NewEmail", newCustomer.Email);
            cmd.Parameters.AddWithValue("@NewFirstName", newCustomer.FirstName);
            cmd.Parameters.AddWithValue("@NewLastName", newCustomer.LastName);

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }
            
            return rows;
        }
    }
}
