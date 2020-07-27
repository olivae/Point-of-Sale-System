using API_consume.CustomerFiles;
using API_consume.OrderFile;
using System;
using System.Collections.Generic;
using System.Text;

namespace API_consume.SaleFiles
{
    public class Sale
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public decimal OrderTotal { get; set; }
        public string PaymentType { get; set; }

        public Customer Customer { get; set; }
        public Order Order { get; set; }
    }
}
