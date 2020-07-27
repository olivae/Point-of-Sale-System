using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API_consume.OrderFile;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Card : PopupPage
	{
        private int cus;
        private string phone;
        private string list;
        private decimal total;
        private string type;

        public bool Payed { get; set; }
        public string PaymentType { get; set; }
        decimal Total;

        string MasterCard = "Master Card";
        string AmericanExpressCard = "American Express Card";
        string DiscoverCard = "Discover Card";
        string VisaCard = "Visa Card";

        long ccNumber;
        int ccv;
        int zipNumber;

        List<ProductInfo> Cart = new List<ProductInfo>() { }; // list of products that are in the cart

        public Card (int cusName, string cusPhone, string oList, decimal oTotal, string pType, List<ProductInfo> cart)
		{
			InitializeComponent ();
            Cart = cart;
            BackgroundColor = Color.White;
            string holder = cardName.Text;
            string date = cusDate.Text;
            cus = cusName;
            phone = cusPhone;
            list = oList;
            total = oTotal;
            type = pType;
		}

        //This Method Pulled from Stack Overflow to validate Credit Card Numbers
        public static bool IsCardNumberValid(string cardNumber)
        {
            int i, checkSum = 0;
        
           for (i = cardNumber.Length - 1; i >= 0; i -= 2)
               checkSum += (cardNumber[i] - '0');
          
           for (i = cardNumber.Length - 2; i >= 0; i -= 2)
            {
                int val = ((cardNumber[i] - '0') * 2);
                {
                    checkSum += (val % 10);
                    val /= 10;
                }
            }
        
            return ((checkSum % 10) == 0);
        }

        private bool CheckCardNumber()
        {
            bool isNumber;
            string CardNumber = cusNum.Text;
            isNumber = Int64.TryParse(cusNum.Text, out ccNumber);
            if (isNumber == true)
            {
                //mastercard check
                if (CardNumber[0] == '5' && (CardNumber[1] == '1' || CardNumber[1] == '2' || CardNumber[1] == '3' || CardNumber[1] == '4' || CardNumber[1] == '5') && CardNumber.Length == 16)
                {
                    PaymentType = MasterCard;
                    return true;
                }
                else if (CardNumber[0] == '2' && (CardNumber[1] == '2' || CardNumber[1] == '3' || CardNumber[1] == '4' || CardNumber[1] == '5' || CardNumber[1] == '6' || CardNumber[1] == '7') && CardNumber.Length == 16)
                {
                    PaymentType = MasterCard;
                    return true;
                }

                // vise card check
                else if (CardNumber[0] == '4' && (CardNumber.Length == 16 || CardNumber.Length == 17 || CardNumber.Length == 18 || CardNumber.Length == 19))
                {
                    PaymentType = VisaCard;
                    return true;
                }
                //discover card check

                else if (CardNumber[0] == '6' && CardNumber.Length == 16)
                {
                    PaymentType = DiscoverCard;
                    return true;
                }

                //amx check
                else if (CardNumber[0] == '3' && CardNumber.Length == 15 && (CardNumber[1] == '4') || CardNumber[1] == '7')
                {
                    PaymentType = AmericanExpressCard;
                    return true;
                }

                else
                {
                }
            }
            else
            {
            }
            return false;
        }

        private void OnContinue(object o, EventArgs e)
        {
            string cardNumber = cusNum.Text;
            if (CheckCardNumber() == true)
            {
                Application.Current.MainPage = (new Receipt(cus, phone, list, total, type, cardNumber, Cart));
                PopupNavigation.Instance.PopAsync();
            }
            else
            {
                DisplayAlert("Error", "Invalid Credit Card Information", "OK");
                PopupNavigation.Instance.PopAsync();
                PopupNavigation.Instance.PushAsync(new Card(cus, phone, list, total, type, Cart));
            }
        }

        private void onBack(object o, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }
    }


}