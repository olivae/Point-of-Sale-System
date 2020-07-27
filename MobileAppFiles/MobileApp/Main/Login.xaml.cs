using API_consume.OrderFile;
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
	public partial class Login : ContentPage
	{
        List<ProductInfo> Cart = new List<ProductInfo>() { }; // list of products that are in the cart

        public Login ()
		{
			InitializeComponent ();
		}

        void onBack(object sender, System.EventArgs e)
        {
            Application.Current.MainPage = new Page1();
        }

        void onContinue(object sender, EventArgs e)
        {
            string user = cusUName.Text;
            string pass = cusPass.Text;


            //REPLACE WITH DATABSE VERIFICATION
            string DBusername = "admin";
            string DBpassword = "admin1234";
            if (user == DBusername && user != null)
            {
                if (pass == DBpassword && pass != null)
                {
                    Application.Current.MainPage = new OrderScreen(0, pass, null, 0, null, null, null, 0.0m, 0.0m, Cart);
                }
                else
                {
                    DisplayAlert("Error!", "Password does not match existing account", "OK");
                }
            }
            else
            {
                DisplayAlert("Error!", "No existing account matches that username", "OK");
            }

        }
    }
}