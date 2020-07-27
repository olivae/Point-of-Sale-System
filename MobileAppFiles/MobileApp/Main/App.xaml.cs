using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MobileApp.Views;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MobileApp
{
    public partial class Application : Xamarin.Forms.Application
    {

        public Application()
        {
            //InitializeComponent();
            //MainPage = new Page1();

            InitializeComponent();
            MainPage = new Page1();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
