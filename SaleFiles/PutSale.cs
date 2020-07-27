using System;
using System.Collections.Generic;
using System.Text;

namespace API_consume.SaleFiles
{
    public class PutSale
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public decimal OrderTotal { get; set; }
        public string PaymentType { get; set; }
    }
}
