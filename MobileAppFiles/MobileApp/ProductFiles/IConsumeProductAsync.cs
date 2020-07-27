using System;
using System.Collections.Generic;
using System.Text;

namespace API_consume.ProductFiles
{
    public interface IConsumeProductAsync
    {
        IEnumerable<Product> GetProducts();
        Product GetProduct(int productId);
        string CreateProduct(string name, decimal price);
        string UpdateProduct(int productId, string name, decimal price);
        string UpdateProductName(int productId, string name);
        string UpdateProductPrice(int productId, decimal price);
        string DeleteProduct(int productId);
    }
}
