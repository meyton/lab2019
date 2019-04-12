using App18.Model;
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
	public partial class StudentEditPage : ContentPage
	{
        private Teacher _teacher;
        private Student _student;

		public StudentEditPage(Teacher teacher, Student student = null)
		{
            _student = student;
            _teacher = teacher;
			InitializeComponent ();
            lblTeacher.Text = $"Teacher: {_teacher.FirstName} {_teacher.LastName}";
            if (_student != null)
            {
                entryFirstName.Text = _student.FirstName;
                entryLastName.Text = _student.LastName;
                entryGrade.Text = _student.Grade.ToString();
                dpBirthday.Date = _student.Birthday;
            }
		}

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var student = new Student()
            {
                FirstName = entryFirstName.Text,
                LastName = entryLastName.Text,
                Grade = int.Parse(entryGrade.Text),
                Birthday = dpBirthday.Date,
                TeacherID = _teacher.ID
            };

            if (_student != null)
            {
                student.ID = _student.ID;
            }

            await App.LocalDB.SaveItem(student);
            await DisplayAlert("Sukces", "Udało się zapisać zmiany", "OK");
            await Navigation.PopAsync();
        }
    }
}