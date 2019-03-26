using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace App18.Data
{
    public class Properties
    {
        public static IDictionary<string, object> AppProperties { get => Application.Current.Properties; }
    }
}
