using API_consume.OrderFile;
using API_consume.ProductFiles;
using System;
using System.Collections.Generic;
using System.Text;

namespace API_consume.OrderItemFiles
{
    public  class OrderItem
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quanitity { get; set; }
        public string ToppingString { get; set; }
        public bool Complete { get; set; }

        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
