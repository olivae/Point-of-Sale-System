using System;
using System.Collections.Generic;
using System.Text;

namespace API_consume.SaleFiles
{
    public interface IConsumeSaleAsync
    {
        IEnumerable<Sale> GetSales();
        Sale GetSale(int saleId);
        string CreateSale(int OrderId, int CustomerId, decimal OrderTotal, string PaymentType);
        string UpdateSale(int saleId, decimal OrderTotal, string PaymentType);
        string UpdateSaleOrderTotal(int saleId, decimal OrderTotal);
        tring UpdateSalePaymentType(int saleId, string PaymentType);
        string DeleteSale(int saleId);
    }
}
