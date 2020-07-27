using API_consume.CustomerFiles;
using API_consume.OrderFile;
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
	public partial class Guest : ContentPage
	{
        ConsumeCustomerAsync customerAPI = new ConsumeCustomerAsync();
        public int customerId;
        List<ProductInfo> Cart = new List<ProductInfo>() { }; // list of products that are in the cart

        public Guest ()
		{
			InitializeComponent ();
            
		}

        void onBack(object sender, System.EventArgs e)
        {
            Application.Current.MainPage = new Page1();
        }

        void onContinue(object sender, EventArgs e)
        {
            string text = cusName.Text;
            string mail = cusEmail.Text;
            string phone = cusPhone.Text;
          

            if (text != null)
            {
                if (mail != null)
                {

                    if (mail.Contains("@"))
                    {
                        var result = customerAPI.CreateCustomer(text, mail);
                        var Id = JsonConvert.DeserializeObject<Customer>(result);
                        customerId = Id.Id;
                        Application.Current.MainPage = new OrderScreen(customerId, mail, null, 0, null, null, null, 0.0m, 0.0m,Cart);
                    }
                    else { DisplayAlert("Error!", "Must enter a valid Email Address", "OK"); }
                }
                else
                {
                    DisplayAlert("Error!", "Must list an Email Address for the Order", "OK");
                }
            }
            else
            {
                DisplayAlert("Error!", "Must have a name for the Order", "OK");
            }
            
            
            
        }

    }
}