using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App18
{
    public partial class MainPage : ContentPage
    {
        private string login = "admin";
        private string pass = "admin123";

        public MainPage()
        {
            InitializeComponent();
            lblOne.FontSize = 28.0;

            stack.Children.Add(new Label() { Text = "456", FontSize = 30.0, HorizontalOptions = LayoutOptions.End });

            var btn = new Button() { Text = "Button 1" };
            btn.Clicked += Btn_Clicked;
            stack.Children.Add(btn);

            var btn2 = new Button() { Text = "Button 2" };
            btn2.Clicked += Btn_Clicked;
            stack.Children.Add(btn2);

            var btn3 = new Button() { Text = "Button 3" };
            btn3.Clicked += Btn_Clicked;
            stack.Children.Add(btn3);
        }

        private void Btn_Clicked(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            var btns = stack.Children.Where(x => x is Button).ToList();
            foreach (var b in btns)
            {
                b.IsVisible = true;
            }

            btn.IsVisible = false;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var login = entryLogin?.Text;
            var password = entryPassword?.Text;

            bool areFieldsEmpty = string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password);
            bool areFieldsCorrect = login == this.login && password == this.pass;

            if (!areFieldsEmpty && areFieldsCorrect)
            {
                await Navigation.PushAsync(new SecondPage("https://google.pl"));
            }
            else
            {
                lblValidation.IsVisible = true;
                entryLogin.BackgroundColor = Color.Red;
                entryPassword.BackgroundColor = Color.Red;
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            lblValidation.IsVisible = false;
            entryLogin.BackgroundColor = Color.White;
            entryPassword.BackgroundColor = Color.White;
            entryLogin.Text = string.Empty;
            entryPassword.Text = string.Empty;
        }

        private void EntryLogin_TextChanged(object sender, TextChangedEventArgs e)
        {
            btnLogin.IsEnabled = e.NewTextValue != null && e.NewTextValue.Length > 4;
        }

        private async void EntryUrl_Completed(object sender, EventArgs e)
        {
            var url = entryUrl?.Text;
            if (url.Length > 5)
            {
                if (await DisplayAlert("Czy na pewno?", $"Czy na pewno chcesz wejść na stronę {url}?", "TAK", "NIE"))
                {
                    Data.Properties.AppProperties["url"] = url;
                    await Application.Current.SavePropertiesAsync();
                    await Navigation.PushAsync(new HttpPage(url));
                }
                else
                {
                    await DisplayAlert("OK", "Wróć jak dorośniesz", "OK");
                }
            }
            else
            {
                await DisplayAlert("Błąd", "Podany URL jest nieprawidłowy", "OK");
            }
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            Data.Properties.AppProperties.Remove("url");
            await Application.Current.SavePropertiesAsync();
            Application.Current.MainPage = new NavigationPage(new MainPage());
            //Navigation.RemovePage(Navigation.NavigationStack[0]);
            //await Navigation.PushAsync(new GridPage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (Data.Properties.AppProperties.ContainsKey("url"))
            {
                lblOne.Text = $"Ostatnio odwiedzana strona: {Data.Properties.AppProperties["url"]}";
            }
        }
    }
}
