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
	public partial class cExtras : PopupPage
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
        string topppingString = "Toppings: ";

        private int cus;
        private string item;
        private string list;
        private decimal total;
        private decimal iTotal;
        private int val;

        public cExtras(int cusName, string itemName, string oList, decimal oTotal, List<ProductInfo> cart)
        {
            InitializeComponent();
            BackgroundColor = Color.White;
            item = itemName;
            itemLabel.Text = item;
            Cart = cart;
            cus = cusName;
            GetProductsInfo();
            list = oList;
            total = oTotal;
            iTotal = 0;


            if (item.ToLower().Contains("double"))
            {
                item = "Double Burger: ";
                iTotal += 4.00m;
            }
            else if (item.ToLower().Contains("triple"))
            {
                item = "Triple Burger: ";
                iTotal += 5.00m;
            }
            else if (item.ToLower().Contains("omg"))
            {
                item = "OMG Burger: ";
                iTotal += 7.00m;
            }
            else
            {
                item = "Burger: ";
                iTotal += 3.00m;
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
                    if (pTops.ToLower().Contains("angus"))
                    {
                        item += "Angus Beef";
                        iTotal += 3.00m;
                    }
                    else if (pTops.ToLower().Contains("buffalo"))
                    {
                        item += "Buffalo Beef";
                        iTotal += 4.00m;
                    }
                    else if (pTops.ToLower().Contains("wagyu"))
                    {
                        item += "Wagyu Beef";
                        iTotal += 5.00m;
                    }

                }
                else
                {
                    item += "Beef";
                }

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