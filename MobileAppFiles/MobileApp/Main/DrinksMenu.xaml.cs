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
    public partial class dExtras : PopupPage
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


        public dExtras(int cusName, string itemName, string oList, decimal oTotal, List<ProductInfo> cart)
        {
            InitializeComponent();
            BackgroundColor = Color.White;
            item = itemName;
            itemLabel.Text = item;
            cus = cusName;
            list = oList;
            total = oTotal;
            iTotal = 0;
            Cart = cart;
            GetProductsInfo();


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
            string size = sizeLabel.Text;
            if (quantity == 0)
            {
                DisplayAlert("Error!", "Please specify quantity of items for your order", "OK");
                //PopupNavigation.Instance.PopAsync();
            }
            else
            {
                if (size != null && (size.ToLower() == "small" || size.ToLower() == "medium" || size.ToLower() == "large"))
                {
                    if (size.ToLower().Contains("small"))
                    {
                        iTotal += 1.00m;
                        item = item + " " + "Small";
                    }
                    else if (size.ToLower().Contains("medium")) { iTotal += 2.00m; item = item + " " + "Medium"; }
                    else if (size.ToLower().Contains("large")) { iTotal += 3.00m; item = item + " " + "Large"; }


                    if (size == null)
                    {
                        DisplayAlert("Error!", "Please specify a size for your order", "OK");
                    }
                    else
                    {
                        iTotal = (iTotal * quantity);
                        total = total + iTotal;
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
                            ToppingString = size,
                            Complete = false
                        };
                        Cart.Add(CreatedItem);
                        Application.Current.MainPage = (new OrderScreen(name, null, itemVal, quantity, size, null, list, total, iTotal, Cart));
                    }
                }

                else
                { DisplayAlert("Error!", "Please choose a size listed", "OK"); };
            }
        }



        private void GetProductsInfo()
        {
            // gets all the products
            Products = productAPI.GetProducts().ToList();
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
