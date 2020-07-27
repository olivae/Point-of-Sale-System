using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API_consume.OrderFile;
using Rg.Plugins.Popup.Services;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Checkout : ContentPage
	{

        List<ProductInfo> Cart = new List<ProductInfo>() { }; // list of products that are in the cart

        private int cus;
        private string phone;
        private string list;
        private decimal total;

        public Checkout(int cusName, string cusPhone, string oList, decimal oTotal, List<ProductInfo> cart)
        {
			InitializeComponent ();
            Cart = cart;
            cus = cusName;
            phone = cusPhone;
            list = oList;
            total = oTotal;
            listStack.Children.Add(new Label { TextColor = Color.Black, Text = "\n" + list + "\n" + "Total:  $" + String.Format("{0:F2}", (total + (total*0.07m))) });

        }

        void onBack(object sender, System.EventArgs e)
        {
            Application.Current.MainPage = new OrderScreen(cus, phone, null, 0, "back", null, list, total, 0.0m, Cart);
        }
        void onCancel(object sender, System.EventArgs e)
        {
            DisplayAlert("Alert", "Your order has been successfully canceled", "OK");
            Application.Current.MainPage = new Page1();
        }

        private void ccClicked(object o, EventArgs e)
        {
            string type = "Credit Card";
            PopupNavigation.Instance.PushAsync(new Card(cus, phone, list, total, type, Cart));
        }

        private void cashClicked(object o, EventArgs e)
        {
            string type = "Cash";
            Application.Current.MainPage = new Receipt(cus, phone, list, total, type, "Cash", Cart);
        }
    }
}