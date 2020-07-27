using System;
using System.Collections.Generic;
using System.Text;

namespace API_consume.OrderFile
{
    public class ProductInfo
    {
        public int ProductId { get; set; }
        public int Quanitity { get; set; }
        public string ToppingString { get; set; }
        public bool Complete { get; set; }
    }
}
