using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using App18.Droid;
using App18.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelperDroid))]
namespace App18.Droid
{
    public class FileHelperDroid : IFileHelper
    {
        public string GetLocalFilepath(string filename)
        {
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }
    }
}