using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App18
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SecondPage : ContentPage
	{
		public SecondPage(string url)
		{
			InitializeComponent ();
            wvWeb.Source = url;
		}

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}