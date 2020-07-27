using System;
using System.Collections.Generic;
using System.Text;

namespace API_consume.CustomerFiles
{
    public interface IConsumeCustomerAysnc
    {
        IEnumerable<Customer> GetCustomers();
        Customer GetCustomer(int customerId);
        string CreateCustomer(string name, string email);
        string UpdateCustomer(int customerId, string name, string email);
        string UpdateCustomerName(int customerId, string name);
        string UpdateCustomerEmail(int customerId, string Email);
        string DeleteCustomer(int customerId);
    }
}
