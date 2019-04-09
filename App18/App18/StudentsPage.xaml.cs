using App18.Model;
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
    public partial class StudentsPage : ContentPage
    {
        private Teacher _teacher;

        public StudentsPage(Teacher teacher)
        {
            _teacher = teacher;
            InitializeComponent();
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            Random r = new Random();
            var student = new Student()
            {
                FirstName = "inż. Testowy",
                LastName = r.Next(1, 1000).ToString(),
                Grade = 2,
                TeacherID = _teacher.ID
            };

            await App.LocalDB.SaveItem(student);
            await RefreshData();
        }

        private async Task RefreshData()
        {
            var students = await App.LocalDB.GetAll<Student>();
            students.RemoveAll(s => s.TeacherID != _teacher.ID);
            MyListView.ItemsSource = students;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await RefreshData();
        }
    }
}
