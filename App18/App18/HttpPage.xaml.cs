using System;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App18
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HttpPage : ContentPage
	{
        private string _url;

		public HttpPage(string url)
		{
            _url = url;
			InitializeComponent ();
		}

        private async void Button_Clicked(object sender, EventArgs e)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(_url);
                await DisplayAlert("Result", $"Strona {_url} odpowiada {response.StatusCode}", "OK");
            }
        }
    }
}