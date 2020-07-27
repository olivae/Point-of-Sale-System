using API_consume.OrderFiles;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace API_consume.OrderFile
{
    public class ConsumeOrderAsync : IConsumeOrderAysnc
    {
        private string OrdersAPIString = "http://45.79.193.33:5000/api/Orders";

        public IEnumerable<Order> GetOrders()
        {
            using (var client = new WebClient())
            {
                client.Headers.Add("Content-Type:application/json");
                client.Headers.Add("Accept:application/json");
                var result = client.DownloadString(OrdersAPIString);
                var orders = JsonConvert.DeserializeObject<List<Order>>(result);

                return orders;
            }
        }

        public Order GetOrder(int orderId)
        {
            using (var client = new WebClient())
            {
                client.Headers.Add("Content-Type:application/json");
                client.Headers.Add("Accept:application/json");
                var result = client.DownloadString(OrdersAPIString + "/" + orderId);
                var order = JsonConvert.DeserializeObject<Order>(result);
                return order;
            }
        }

        public string CreateOrder(int customerId, List <ProductInfo> product)
        {
            using (var client = new WebClient())
            {

                var order = new PostOrder
                {
                    CustomerId = customerId,
                    ProductInfos = product.ToArray(),
                    IsPickUp = false
                };

                client.Headers.Add("Content-Type:application/json");
                client.Headers.Add("Accept:application/json");
                var result = client.UploadString(OrdersAPIString, JsonConvert.SerializeObject(order));

                return result;
            }
        }

        public string UpdateOrder(int orderId, int productId, int quanitity, string toppings)
        {
            using (var client = new WebClient())
            {
                var product = new ProductInfo()
                {
                    ProductId = productId,
                    Quanitity = quanitity,
                    ToppingString = toppings,
                    Complete = false
                };

                var order = new PutOrder
                {
                    IsPickedUp = false,
                    ProductInfos = new[] { product }
                };

                client.Headers.Add("Content-Type:application/json");
                client.Headers.Add("Accept:application/json");
                var result = client.UploadString(OrdersAPIString + "/" + orderId, "PUT", JsonConvert.SerializeObject(order));
                return result;
            }
        }

        public string UpdateOrderItemStatus(int orderId, int productId)
        {
            using (var client = new WebClient())
            {

                var order = GetOrder(orderId);

                var orderItems = new List<ProductInfo>() { };

                foreach (var oi in order.OrderItems)
                {
                    if (productId == oi.ProductId)
                    {
                        orderItems.Add(new ProductInfo { ProductId = productId, Quanitity = oi.Quanitity, ToppingString = oi.ToppingString, Complete = true });
                    }
                    else
                    {
                        orderItems.Add(new ProductInfo { ProductId = oi.ProductId, Quanitity = oi.Quanitity, ToppingString = oi.ToppingString, Complete = oi.Complete });
                    }
                }

                var orderStatus = new PutOrder
                {
                    IsPickedUp = false,
                    ProductInfos = orderItems.ToArray()
                };

                client.Headers.Add("Content-Type:application/json");
                client.Headers.Add("Accept:application/json");
                var result = client.UploadString(OrdersAPIString + "/" + orderId, "PUT", JsonConvert.SerializeObject(orderStatus));
                return result;
            }
        }

        public string UpdateOrderStatus(int orderId, bool isPickedUp)
        {
            using (var client = new WebClient())
            {

                var order = GetOrder(orderId);

                var orderItems = new List<ProductInfo>() { };

                foreach (var oi in order.OrderItems)
                {
                    orderItems.Add(new ProductInfo {ProductId = oi.ProductId, Quanitity = oi.Quanitity, ToppingString = oi.ToppingString,Complete = oi.Complete });
                }

                var orderStatus = new PutOrder
                {
                    IsPickedUp = isPickedUp,
                    ProductInfos = orderItems.ToArray()
                };

                client.Headers.Add("Content-Type:application/json");
                client.Headers.Add("Accept:application/json");
                var result = client.UploadString(OrdersAPIString + "/" + orderId, "PUT", JsonConvert.SerializeObject(orderStatus));
                return result;
            }
        }

        public string DeleteOrder(int orderId)
        {
            using (var client = new WebClient())
            {
                client.Headers.Add("Content-Type:application/json");
                client.Headers.Add("Accept:application/json");
                client.UploadString(OrdersAPIString + "/" + orderId, "DELETE", "");
                return ($"Customer {orderId} has been deleted");
            }
        }
    }
}
