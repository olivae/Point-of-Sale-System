using System;
using System.Collections.Generic;
using System.Text;

namespace API_consume.OrderFile
{
    public class PutOrder
    {
        public ProductInfo[] ProductInfos { get; set; }
        public bool IsPickedUp { get; set; }
    }
}
