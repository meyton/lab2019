﻿using App18.Model;
using System;
using System.Collections.Generic;
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
        private bool _isSelectable;
        private List<Student> _studentsSelected;
        private List<Student> _students;

        public StudentsPage(Teacher teacher)
        {
            _studentsSelected = new List<Student>();
            _students = new List<Student>();
            _isSelectable = false;
            _teacher = teacher;
            InitializeComponent();
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            var student = e.Item as Student;

            if (!_isSelectable)
            {
                if (await DisplayAlert($"{student.FirstName} {student.LastName}", $"Ocena: {student.Grade}. Ur. {student.Birthday.ToLongDateString()}. Czy przejść do edycji?", "TAK", "NIE"))
                {
                    await Navigation.PushAsync(new StudentEditPage(_teacher, student));

                    //Deselect Item
                    ((ListView)sender).SelectedItem = null;
                }
            }
            else
            {
                if (_studentsSelected.Contains(student))
                {
                    _studentsSelected.Remove(student);
                    var firstName = student.FirstName.Remove(0, 2);
                    _students.Where(s => s.ID == student.ID).First().FirstName = firstName;
                }
                else
                {
                    var studentSelected = _students.Where(s => s.ID == student.ID).First();
                    studentSelected.FirstName = "X " + student.FirstName;
                    _studentsSelected.Add(studentSelected);
                }

                MyListView.ItemsSource = null;
                MyListView.ItemsSource = _students;
                btnSelect.Text = $"Remove students ({_studentsSelected.Count})";
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new StudentEditPage(_teacher));
        }

        private async Task RefreshData()
        {
            _students = await App.LocalDB.GetAll<Student>();
            _students.RemoveAll(s => s.TeacherID != _teacher.ID);
            MyListView.ItemsSource = _students;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await RefreshData();
        }

        private async void Select_Clicked(object sender, EventArgs e)
        {
            if (_isSelectable)
            {
                if (_studentsSelected.Any())
                {
                    foreach (var s in _studentsSelected)
                    {
                        await App.LocalDB.DeleteItem(s);
                    }

                    _studentsSelected.Clear();
                    await DisplayAlert("Sukces", "Usunięto rekordy", "OK");
                    await RefreshData();
                }
            }

            _isSelectable = !_isSelectable;
            btnSelect.Text = _isSelectable ? "Select students" : "Remove students";
        }
    }
}
