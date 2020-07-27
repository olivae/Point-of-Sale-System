using API_consume.ProductFiles;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace API_consume.ProductFiles
{
    public class ConsumeProductAsync : IConsumeProductAsync
    {
        private string ProductAPIString = "http://45.79.193.33:5000/api/Product";

        public IEnumerable<Product> GetProducts()
        {
            using (var client = new WebClient())
            {
                client.Headers.Add("Content-Type:application/json");
                client.Headers.Add("Accept:application/json");
                var result = client.DownloadString(ProductAPIString);
                var products = JsonConvert.DeserializeObject<List<Product>>(result);
                return products;
            }
        }

        public Product GetProduct(int productId)
        {
            using (var client = new WebClient())
            {
                client.Headers.Add("Content-Type:application/json");
                client.Headers.Add("Accept:application/json");
                var result = client.DownloadString(ProductAPIString + "/" + productId);
                var product = JsonConvert.DeserializeObject<Product>(result);
                return product;
            }
        }

        public string CreateProduct(string name, decimal price)
        {
            using (var client = new WebClient())
            {
                var product = new PostProduct();
                product.Name = name;
                product.Price = price;
                client.Headers.Add("Content-Type:application/json");
                client.Headers.Add("Accept:application/json");
                var result = client.UploadString(ProductAPIString, JsonConvert.SerializeObject(product));
                return result;
            }
        }

        public string UpdateProduct(int productId, string name, decimal price)
        {
            using (var client = new WebClient())
            {
                var product = new PutProduct();
                product.Name = name;
                product.Price = price;
                client.Headers.Add("Content-Type:application/json");
                client.Headers.Add("Accept:application/json");
                var result = client.UploadString(ProductAPIString + "/" + productId, "PUT", JsonConvert.SerializeObject(product));
                return result;
            }
        }

        public string UpdateProductName(int productId, string name)
        {
            using (var client = new WebClient())
            {
                var productIfo = GetProduct(productId);
                var product = new PutProduct();
                product.Name = name;
                product.Price = productIfo.Price;
                client.Headers.Add("Content-Type:application/json");
                client.Headers.Add("Accept:application/json");
                var result = client.UploadString(ProductAPIString + "/" + productId, "PUT", JsonConvert.SerializeObject(product));
                return result;
            }
        }
        public string UpdateProductPrice(int productId, decimal price)
        {
            using (var client = new WebClient())
            {
                var productIfo = GetProduct(productId);
                var product = new PutProduct();
                product.Name = productIfo.Name;
                product.Price =price;
                client.Headers.Add("Content-Type:application/json");
                client.Headers.Add("Accept:application/json");
                var result = client.UploadString(ProductAPIString + "/" + productId, "PUT", JsonConvert.SerializeObject(product));
                return result;
            }
        }

        public string DeleteProduct(int productId)
        {
            using (var client = new WebClient())
            {
                client.Headers.Add("Content-Type:application/json");
                client.Headers.Add("Accept:application/json");
                client.UploadString(ProductAPIString + "/" + productId, "DELETE", "");
                return($"Customer {productId} has been deleted");
            }
        }
    }

   

}
