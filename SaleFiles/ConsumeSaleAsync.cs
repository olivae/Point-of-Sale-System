using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace API_consume.SaleFiles
{
    public class ConsumeSaleAsync : IConsumeSaleAsync
    {
        private string SaleAPIString = "http://45.79.193.33:5000/api/Sale";

        public string CreateSale(int OrderId, int CustomerId, decimal OrderTotal, string PaymentType)
        {
            using (var client = new WebClient())
            {
                var sale = new PostSale();
                sale.OrderId = OrderId;
                sale.CustomerId = CustomerId;
                sale.OrderTotal = OrderTotal;
                sale.PaymentType = PaymentType;
                client.Headers.Add("Content-Type:application/json");
                client.Headers.Add("Accept:application/json");
                var result = client.UploadString(SaleAPIString, JsonConvert.SerializeObject(sale));
                return result;
            }
        }

        public string DeleteSale(int saleId)
        {
            using (var client = new WebClient())
            {
                client.Headers.Add("Content-Type:application/json");
                client.Headers.Add("Accept:application/json");
                client.UploadString(SaleAPIString + "/" + saleId, "DELETE", "");
                return ($"Customer {saleId} has been deleted");
            }
        }

        public Sale GetSale(int saleId)
        {
            using (var client = new WebClient())
            {
                client.Headers.Add("Content-Type:application/json");
                client.Headers.Add("Accept:application/json");
                var result = client.DownloadString(SaleAPIString + "/" + saleId);
                var sale = JsonConvert.DeserializeObject<Sale>(result);

                return sale;
            }
        }

        public IEnumerable<Sale> GetSales()
        {
            using (var client = new WebClient())
            {
                client.Headers.Add("Content-Type:application/json");
                client.Headers.Add("Accept:application/json");
                var result = client.DownloadString(SaleAPIString);
                var sale = JsonConvert.DeserializeObject<List<Sale>>(result);

                return sale;
            }
        }

        public string UpdateSale(int saleId, decimal OrderTotal, string PaymentType)
        {
            using (var client = new WebClient())
            {
                var saleInfo = GetSale(saleId);
                var sale = new PutSale();
                sale.OrderId = saleInfo.OrderId;
                sale.CustomerId = saleInfo.CustomerId;
                sale.OrderTotal = OrderTotal;
                sale.PaymentType = PaymentType;
                client.Headers.Add("Content-Type:application/json");
                client.Headers.Add("Accept:application/json");
                var result = client.UploadString(SaleAPIString + "/" + saleId, "PUT", JsonConvert.SerializeObject(sale));
                return result;
            }
        }
        public string UpdateSaleOrderTotal(int saleId, decimal OrderTotal)
        {
            using (var client = new WebClient())
            {
                var saleInfo = GetSale(saleId);
                var sale = new PutSale();
                sale.OrderId = saleInfo.OrderId;
                sale.CustomerId = saleInfo.CustomerId;
                sale.OrderTotal = OrderTotal;
                sale.PaymentType = saleInfo.PaymentType;
                client.Headers.Add("Content-Type:application/json");
                client.Headers.Add("Accept:application/json");
                var result = client.UploadString(SaleAPIString + "/" + saleId, "PUT", JsonConvert.SerializeObject(sale));
                return result;
            }
        }
        public string UpdateSalePaymentType(int saleId, string PaymentType)
        {
            using (var client = new WebClient())
            {
                var saleInfo = GetSale(saleId);
                var sale = new PutSale();
                sale.OrderId = saleInfo.OrderId;
                sale.CustomerId = saleInfo.CustomerId;
                sale.OrderTotal = saleInfo.OrderTotal;
                sale.PaymentType = PaymentType;
                client.Headers.Add("Content-Type:application/json");
                client.Headers.Add("Accept:application/json");
                var result = client.UploadString(SaleAPIString + "/" + saleId, "PUT", JsonConvert.SerializeObject(sale));
                return result;
            }
        }
    }
}
