using API_consume.OrderFile;
using System;
using System.Collections.Generic;
using System.Text;

namespace API_consume.CustomerFiles
{
    public  class Customer
    {
        public Customer()
        {
           Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
