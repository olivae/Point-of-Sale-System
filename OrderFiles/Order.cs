using API_consume.CustomerFiles;
using API_consume.OrderItemFiles;
using System;
using System.Collections.Generic;
using System.Text;

namespace API_consume.OrderFile
{
    public  class Order
    {
        public Order()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public bool Complete { get; set; }
        public bool IsPickedUp { get; set; }

        public Customer Customer { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
