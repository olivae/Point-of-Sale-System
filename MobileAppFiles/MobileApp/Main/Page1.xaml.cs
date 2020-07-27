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
	public partial class Page1 : ContentPage
	{
        void onClick(object sender, System.EventArgs e)
        {
            Application.Current.MainPage = new Guest();
        }

        void onLogin(object sender, System.EventArgs e)
        {
            Application.Current.MainPage = new Login();
        }
		public Page1 ()
		{
			InitializeComponent (); 
		}

        //private void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
        //{
        //    label.Text = String.Format("Value is {0:F0}", e.NewValue);
        //}
    }
}