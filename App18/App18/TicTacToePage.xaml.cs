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
	public partial class TicTacToePage : ContentPage
	{
        private string _currentSymbol;
        const string Circle = "O";
        const string Cross = "X";

        public TicTacToePage ()
		{
			InitializeComponent ();
            _currentSymbol = Circle;
		}

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var btn = sender as Button;
            if (btn.Text == "Pusto")
            {
                btn.Text = _currentSymbol;
                btn.IsEnabled = false;
                if (DoWeHaveWinner())
                {
                    await DisplayAlert("Wygrana", $"Wygrywa: {_currentSymbol}", "OK");
                }
                else
                {
                    _currentSymbol = _currentSymbol == Circle ? Cross : Circle;
                    lblMove.Text = _currentSymbol == Circle ? "Kółko" : "Krzyżyk";
                }
            }
        }

        private bool DoWeHaveWinner()
        {
            var grid = this.Content as Grid;
            var btnList = grid.Children.Where(g => g is Button).Cast<Button>().ToList();
            
            bool isFirstRowChecked = CheckButtons(btnList, 0, 1, 2);
            bool isSecondRowChecked = CheckButtons(btnList, 3, 4, 5);  
            bool isThirdRowChecked = CheckButtons(btnList, 6, 7, 8);
            bool isRowChecked = isFirstRowChecked || isSecondRowChecked || isThirdRowChecked;

            bool isFirstColumnChecked = CheckButtons(btnList, 0, 3, 6); 
            bool isSecondColumnChecked = CheckButtons(btnList, 1, 4, 7); 
            bool isThirdColumnChecked = CheckButtons(btnList, 2, 5, 8);
            bool isColumnChecked = isFirstColumnChecked || isSecondColumnChecked || isThirdColumnChecked;

            bool isFirstDiagonalChecked = CheckButtons(btnList, 0, 4, 8);
            bool isSecondDiagonalChecked = CheckButtons(btnList, 2, 4, 6);

            return isRowChecked || isColumnChecked || isFirstDiagonalChecked || isSecondDiagonalChecked;
        }

        private bool CheckButtons(List<Button> btnList, int a, int b, int c)
        {
            return btnList[a].Text != "Pusto" && btnList[a].Text == btnList[b].Text && btnList[b].Text == btnList[c].Text;
        }
    }
}