using API_consume.OrderItemFiles;
using System;
using System.Collections.Generic;
using System.Text;

namespace API_consume.ProductFiles
{
    public  class Product
    {
        public Product()
        {
            OrderItem = new HashSet<OrderItem>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public ICollection<OrderItem> OrderItem { get; set; }
    }
}
