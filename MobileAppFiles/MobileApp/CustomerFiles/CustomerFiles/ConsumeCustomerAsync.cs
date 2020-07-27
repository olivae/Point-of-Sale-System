using API_consume.CustomerFiles;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace API_consume.CustomerFiles
{
    public class ConsumeCustomerAsync : IConsumeCustomerAysnc
    {
        private string CustomerAPIString = "http://45.79.193.33:5000/api/Customers";

        public IEnumerable<Customer> GetCustomers()
        {
            using (var client = new WebClient())
            {
                client.Headers.Add("Content-Type:application/json");
                client.Headers.Add("Accept:application/json");
                var result = client.DownloadString(CustomerAPIString);
                var customers = JsonConvert.DeserializeObject<List<Customer>>(result);

                return customers;
            }
        }
        public Customer GetCustomer(int customerId)
        {
            using (var client = new WebClient())
            {
                client.Headers.Add("Content-Type:application/json");
                client.Headers.Add("Accept:application/json");
                var result = client.DownloadString(CustomerAPIString +"/" + customerId);
                var customer = JsonConvert.DeserializeObject<Customer>(result);
                
                return customer;
            }
        }

        public string CreateCustomer(string name, string email)
        {
            using (var client = new WebClient())
            {
                var customer = new PostCustomer();
                customer.Name = name;
                customer.Email = email;
                client.Headers.Add("Content-Type:application/json");
                client.Headers.Add("Accept:application/json");
                var result = client.UploadString(CustomerAPIString, JsonConvert.SerializeObject(customer));
                return result;
            }
        }

        public string UpdateCustomer(int customerId, string name, string email)
        {
            using (var client = new WebClient())
            {
                var customer = new PutCustomer();
                customer.Name = name;
                customer.Email = email;
                client.Headers.Add("Content-Type:application/json");
                client.Headers.Add("Accept:application/json");
                var result = client.UploadString(CustomerAPIString + "/" + customerId, "PUT", JsonConvert.SerializeObject(customer));
                return result;
            }
        }

        public string UpdateCustomerName(int customerId, string name)
        {
            using (var client = new WebClient())
            {
                var customerInfo = GetCustomer(customerId);
                var customer = new PutCustomer();
                customer.Name = name;
                customer.Email = customerInfo.Email;
                client.Headers.Add("Content-Type:application/json");
                client.Headers.Add("Accept:application/json");
                var result = client.UploadString(CustomerAPIString + "/" + customerId, "PUT", JsonConvert.SerializeObject(customer));
                return result;
            }
        }

        public string UpdateCustomerEmail(int customerId, string Email)
        {
            using (var client = new WebClient())
            {
                var customerInfo = GetCustomer(customerId);
                var customer = new PutCustomer();
                customer.Name = customerInfo.Name;
                customer.Email = Email;
                client.Headers.Add("Content-Type:application/json");
                client.Headers.Add("Accept:application/json");
                var result = client.UploadString(CustomerAPIString + "/" + customerId, "PUT", JsonConvert.SerializeObject(customer));
                return result;
            }
        }

        public string DeleteCustomer(int customerId)
        {
            using (var client = new WebClient())
            {
                client.Headers.Add("Content-Type:application/json");
                client.Headers.Add("Accept:application/json");
                client.UploadString(CustomerAPIString + "/" + customerId, "DELETE", "");
                return ($"Customer {customerId} has been deleted");
            }
        }
    }
}
