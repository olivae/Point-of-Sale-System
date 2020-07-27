using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API_consume.CustomerFiles;
using API_consume.EmployeeFiles;
using API_consume.OrderFile;
using API_consume.ProductFiles;
using API_consume.SaleFiles;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class fExtras : PopupPage
	{
        ConsumeCustomerAsync customerAPI = new ConsumeCustomerAsync();
        ConsumeProductAsync productAPI = new ConsumeProductAsync();
        ConsumeOrderAsync orderAPI = new ConsumeOrderAsync();
        ConsumeEmployeeAsync employeeAPI = new ConsumeEmployeeAsync();
        ConsumeSaleAsync saleAPI = new ConsumeSaleAsync();

        List<Product> Products = new List<Product> { }; // list of all product in the database
        public ProductInfo CreatedItem { get; set; }
        List<ProductInfo> Cart = new List<ProductInfo>() { }; // list of products that are in the cart 

        int itemID;

        private int cus;
        private string item;
        private string list;
        private decimal total;
        private decimal iTotal;
        private int val;

        public fExtras (int cusName, string itemName, string oList, decimal oTotal, List<ProductInfo> cart)
		{
			InitializeComponent ();
            BackgroundColor = Color.White;
            item = itemName;
            itemLabel.Text = item;
            cus = cusName;
            list = oList;
            total = oTotal;
            iTotal = 0;
            Cart = cart;
            GetProductsInfo();
            if (item.ToLower().Contains("chicken"))
            {
                if (item.ToLower().Contains("grilled"))
                { iTotal += 5.00m; item = "Grilled Chicken"; }
                if (item.ToLower().Contains("fried"))
                { iTotal += 4.50m; item = "Fried Chicken"; }
            }         
            if (item.ToLower().Contains("fish"))
            {
                if (item.ToLower().Contains("grilled"))
                { iTotal += 6.00m; item = "Grilled Fish"; }
                if (item.ToLower().Contains("fried"))
                { iTotal += 5.50m; item = "Fried Fish"; }
            }
        }

        private void GetProductsInfo()
        {
            // gets all the products
            Products = productAPI.GetProducts().ToList();
        }

        void onBack(object sender, System.EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }

        void onContinue(object sender, EventArgs e)
        {

            int name = cus;
            string itemVal = item;
            int quantity = val;
            decimal itemTotal = total;
            string tops = toppings.Text;
            string pTops = pToppings.Text;

            if (quantity == 0)
            {
                DisplayAlert("Error!", "Please specify quantity of items for your order", "OK");
                //PopupNavigation.Instance.PopAsync();
            }
            else
            {
                if (pTops != null)
                {
                    if (pTops.ToLower().Contains("cajun"))
                    { iTotal += 1.00m; item += " Cajun"; }
                    else if (pTops.ToLower().Contains("lemon"))
                    { iTotal += 1.00m; item += " Lemon"; }
                    else if (pTops.ToLower().Contains("avocado"))
                    { iTotal += 1.00m; item += " Avocado"; }

                }
                else
                { item += " Plain"; }


                iTotal = (iTotal * quantity);
                itemTotal = itemTotal + iTotal;
                PopupNavigation.Instance.PopAsync();
                foreach (var product in Products)
                {
                    if (product.Name == item)
                    {
                        itemID = product.Id;
                    }
                }
                CreatedItem = new ProductInfo()
                {
                    ProductId = itemID,
                    Quanitity = quantity,
                    ToppingString = tops + " " + pTops,
                    Complete = false
                };
                Cart.Add(CreatedItem);
                Application.Current.MainPage = (new OrderScreen(name, null, itemVal, quantity, tops, pTops, list, itemTotal, iTotal, Cart));
                
            }
        }

        void OnSliderValueChanged(object sender, ValueChangedEventArgs args)
        {
            double dValue = args.NewValue;
            val = Convert.ToInt32(dValue);
            sLabel.Text = String.Format("Number of Items: " + val);
            //displayLabel.Text = String.Format("The Slider value is {0}", value);

        }
    }
}