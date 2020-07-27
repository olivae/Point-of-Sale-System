using API_consume.CustomerFiles;
using API_consume.EmployeeFiles;
using API_consume.OrderFile;
using API_consume.ProductFiles;
using API_consume.SaleFiles;
using MobileApp.Views;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
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
    public partial class OrderScreen : ContentPage
    {
        ConsumeCustomerAsync customerAPI = new ConsumeCustomerAsync();
        ConsumeProductAsync productAPI = new ConsumeProductAsync();
        ConsumeOrderAsync orderAPI = new ConsumeOrderAsync();
        ConsumeEmployeeAsync employeeAPI = new ConsumeEmployeeAsync();
        ConsumeSaleAsync saleAPI = new ConsumeSaleAsync();


        int customerID;
        string phone;
        string list;
        string iName;
        decimal total;
        decimal itemTotal;
        string item;
        string Otoppings;
        string OPtopping;
        int Oquantity;

        List<ProductInfo> Cart = new List<ProductInfo>() { }; // list of products that are in the cart
        List<Product> Products = new List<Product> { }; // list of all product in the database

        public OrderScreen(int cusName, string cusPhone, string oItem, int oQuantity, string oToppings, string opToppings, string oList, decimal oTotal, decimal iTotal, List<ProductInfo> cart)
        {
            InitializeComponent();
            customerID = cusName;
            phone = cusPhone;
            total = oTotal;
            itemTotal = iTotal;
            item = oItem;
            Otoppings = oToppings;
            OPtopping = opToppings;
            Oquantity = oQuantity;
            list = oList;
            Cart = cart;
            actName.Text = "Customer Account: " + cusName;
            if (item != null)
            {
                if (Otoppings == "remove")
                {
                    int index = list.LastIndexOf(item);
                    if (index < 30)
                    {
                        list = "";
                        Application.Current.MainPage = (new OrderScreen(customerID, phone, null, 0, null, null, null, 0.0m, 0.0m, Cart));
                    }
                    else
                    {
                        total = (total - itemTotal);

                        list = list.Substring(0, index - 26);
                        int temp = list.LastIndexOf("(");
                        int len = temp - (list.LastIndexOf("-") + 2);
                        string pItem = list.Substring(list.LastIndexOf("-") + 2, len);
                        string nItemTotal = list.Substring(list.LastIndexOf("$"), 4);
                        orderStack.Children.Add(new Label { Text = "Cart: " + "\n" + "\n" + list + "\n" + "_________________________" + "\n" + "Total:  $" + String.Format("{0:F2}", (total + (total * 0.07m))) });
                        item = pItem;
                        decimal number;
                        if (Decimal.TryParse(nItemTotal, out number))
                            itemTotal = number;

                    }
                }
                else
                {

                    list = list + "-------------------------" + "\n" + item + "  " + "(" + Oquantity + ")" + "                            $" + String.Format("{0:F2}", itemTotal) + "\n" + "       Extras: " + "\n" + "       " + Otoppings + "\n       " + OPtopping + "\n";
                    orderStack.Children.Add(new Label { Text = "Cart: " + "\n" + "\n" + list + "\n" + "_________________________" + "\n" + "Total:  $" + String.Format("{0:F2}", (total + (total * 0.07m))) });
                }
            }
            iName = item;

        }

       


        void onBack(object sender, System.EventArgs e)
        {
            Application.Current.MainPage = new Page1();
        }

        private void burgClick(object o, EventArgs e)
        {
            var button = (Button) o;
            var itemName = button.ClassId;
            
            PopupNavigation.Instance.PushAsync(new cExtras(customerID, itemName, list, total, Cart));
        }

        private void chxClick(object o, EventArgs e)
        {
            var button = (Button)o;
            var itemName = button.ClassId;
            PopupNavigation.Instance.PushAsync(new fExtras(customerID, itemName, list, total , Cart));
        }

        private void salClick(object o, EventArgs e)
        {
            var button = (Button)o;
            var itemName = button.ClassId;
            PopupNavigation.Instance.PushAsync(new salExtras(customerID, itemName, list, total, Cart));
        }

        private void sideClick(object o, EventArgs e)
        {
            var button = (Button)o;
            var itemName = button.ClassId;
            PopupNavigation.Instance.PushAsync(new sExtras(customerID, itemName, list, total, Cart));
        }

        private void drinkClick(object o, EventArgs e)
        {
            var button = (Button)o;
            var itemName = button.ClassId;
            PopupNavigation.Instance.PushAsync(new dExtras(customerID, itemName, list, total, Cart));
        }

        private void clearOrder(object o, EventArgs e)
        {
            list = "";
            Application.Current.MainPage = (new OrderScreen(customerID, phone, null, 0, null, null, null, 0.0m, 0.0m, Cart));
            Cart.Clear();
        }

        private void clearItem(object o, EventArgs e)
        {
            string pList = list;
            Application.Current.MainPage = (new OrderScreen(customerID, phone, iName, 0, "remove", null, pList, total, itemTotal, Cart));
            Cart.RemoveAt(Cart.Count-1);
        }

        void onCheckout(object sender, System.EventArgs e)
        {
            Application.Current.MainPage = (new Checkout(customerID, phone, list, total, Cart));
        }
    }
}