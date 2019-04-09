using App18.Data;
using App18.Interfaces;
using App18.Model;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace App18
{
    public partial class App : Application
    {
        private static LocalDatabase localDb;

        public static LocalDatabase LocalDB
        {
            get
            {
                if (localDb == null)
                {
                    var fileHelper = DependencyService.Get<IFileHelper>();
                    var fullPath = fileHelper.GetLocalFilepath("app.database");
                    localDb = new LocalDatabase(fullPath);
                }

                return localDb;
            }
        }

        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new TeachersPage());
        }
    }
}
