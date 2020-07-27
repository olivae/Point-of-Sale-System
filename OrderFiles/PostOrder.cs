using System;
using System.Collections.Generic;
using System.Text;

namespace API_consume.OrderFile
{
    public class PostOrder
    {
        public int CustomerId { get; set; }
        //public bool OrderComplete {get; set;}
        public ProductInfo[] ProductInfos { get; set; }
        public bool IsPickUp { get; internal set; }
    }
}
