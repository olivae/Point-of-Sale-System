using API_consume.OrderFile;
using System;
using System.Collections.Generic;
using System.Text;

namespace API_consume.OrderFiles
{
    public interface IConsumeOrderAysnc
    {
        IEnumerable<Order> GetOrders();
        Order GetOrder(int orderId);
        string CreateOrder(int customerId, List<ProductInfo> product);
        string UpdateOrder(int orderId, int productId, int quanitity, string toppings);
        string UpdateOrderItemStatus(int orderId, int productId);
        string UpdateOrderStatus(int orderId, bool isPickedUp);
        string DeleteOrder(int orderId);
    }
}
