using API_consume.CustomerFiles;
using API_consume.EmployeeFiles;
using API_consume.OrderFile;
using API_consume.ProductFiles;
using API_consume.SaleFiles;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Receipt : ContentPage
	{
        ConsumeCustomerAsync customerAPI = new ConsumeCustomerAsync();
        ConsumeProductAsync productAPI = new ConsumeProductAsync();
        ConsumeEmployeeAsync employeeAPI = new ConsumeEmployeeAsync();
        ConsumeSaleAsync saleAPI = new ConsumeSaleAsync();
        ConsumeOrderAsync orderAPI = new ConsumeOrderAsync();

        List<ProductInfo> Cart = new List<ProductInfo>() { }; // list of products that are in the cart

        int customerId;
        int orderId;
        int saleId;

        string customerName;
        string phone;
        string list;
        decimal total;
        string pType;

        public Receipt(int cusName, string cusPhone, string oList, decimal oTotal, string type, string cNum, List<ProductInfo> cart)
        {
			InitializeComponent ();
            customerId = cusName;
            Cart = cart;
            phone = cusPhone;
            list = oList;
            total = oTotal;
            string cardNumber = cNum;
            actName.Text = customerName;
            actMail.Text = phone;
            pType = type;
            listStack.Children.Add(new Label { TextColor = Color.Gray, Text = "\n" + list + "\n" + "Total:  $" + String.Format("{0:F2}", (total + (total*0.07m))) });
            if (pType == "Credit Card")
            {
                int temp = (cardNumber.Length - 4);
                cardNumber = cardNumber.Substring(temp, 4);
                payment.Children.Add(new Label { TextColor = Color.Green, Text = pType });
                payment.Children.Add(new Label { TextColor = Color.Green, Text = "Card ending in: " + cardNumber});
            }
            else
            {
                payment.Children.Add(new Label { TextColor = Color.Green, Text = pType });
            }
            DateTime time = DateTime.Now;
            int min = 25;
            time = time.AddMinutes(min);
            timeStack.Children.Add(new Label { TextColor = Color.Red, Text =  "\n" + time });

            //TO SEND TO DATABASE
            //cus, email, list, total
            CreateOrder();

            CreateSale();
        }

        private void GetCustomerName()
        {            
            var customer = customerAPI.GetCustomer(customerId);
            customerName = customer.Name; 
        }

        private void CreateOrder()
        {
            var result = orderAPI.CreateOrder(customerId, Cart);
            var Id = JsonConvert.DeserializeObject<Order>(result);
            orderId = Id.OrderId;
        }

        private void CreateSale()
        {
            var result = saleAPI.CreateSale(orderId, customerId, total, pType);
            var Id = JsonConvert.DeserializeObject<Sale>(result);
            saleId = Id.Id;
            
        }
        void onBack(object sender, System.EventArgs e)
        {
            Application.Current.MainPage = new Page1();
        }
    }
}