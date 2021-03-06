﻿using App18.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App18
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TeachersPage : ContentPage
    {
        public TeachersPage()
        {
            InitializeComponent();
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            await Navigation.PushAsync(new StudentsPage((Teacher)e.Item));

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(entryFirstName.Text)
                || string.IsNullOrEmpty(entryLastName.Text))
            {
                return;
            }
            
            var teacher = new Teacher()
            {
                FirstName = entryFirstName.Text,
                LastName = entryLastName.Text
            };

            await App.LocalDB.SaveItem(teacher);
            await RefreshData();
        }

        private async Task RefreshData()
        {
            var teachers = await App.LocalDB.GetAll<Teacher>();
            MyListView.ItemsSource = teachers;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await RefreshData();
        }
    }
}
