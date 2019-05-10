using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App18.ViewModel
{
    public class TestViewModel : BaseViewModel
    {
        private string _infoText;
        
        public Color InfoColor
        {
            get
            {
                if (InfoText == "Fetching data...")
                    return Color.Red;
                else if (InfoText == "Connected. Synchronized.")
                    return Color.Green;

                return Color.Black;
            }
        }

        public string InfoText
        {
            get => _infoText;
            set
            {
                if (_infoText != value)
                {
                    _infoText = value;
                    RaisePropertyChanged(nameof(InfoText));
                    RaisePropertyChanged(nameof(InfoColor));
                }
            }
        }
        
        public TestViewModel()
        {
            InfoText = "Pierwsze info";
            ConnectToServer();
        }

        private async void ConnectToServer()
        {
            await Task.Delay(2000);
            InfoText = "Connecting...";
            await Task.Delay(3000);
            InfoText = "Fetching data...";
            await Task.Delay(3000);
            InfoText = "Connected. Synchronized.";
        }
    }
}
