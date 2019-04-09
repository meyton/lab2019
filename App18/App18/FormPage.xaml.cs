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
	public partial class FormPage : ContentPage
	{
		public FormPage ()
		{
			InitializeComponent ();
		}

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(entryFirstName.Text) || string.IsNullOrWhiteSpace(entryLastName.Text))
            {
                await DisplayAlert("Błąd!", "Nie wprowadzono danych", "OK");
                return;
            }

            Data.Properties.AppProperties["firstName"] = entryFirstName.Text;
            Data.Properties.AppProperties["lastName"] = entryLastName.Text;

            await Application.Current.SavePropertiesAsync();

            lblFirstName.Text = entryFirstName.Text;
            lblLastName.Text = entryLastName.Text;
            ClearFields();
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void ClearFields()
        {
            entryFirstName.Text = string.Empty;
            entryLastName.Text = string.Empty;
        }
    }
}