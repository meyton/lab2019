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
        private string _startingSymbol;
        const string Circle = "O";
        const string Cross = "X";
        const string Empty = "Pusto";

        public TicTacToePage ()
		{
			InitializeComponent();
            Initialize(Circle);
		}

        private void Initialize(string symbol)
        {
            if (Data.Properties.AppProperties.ContainsKey(Circle))
            {
                lblCircle.Text = Data.Properties.AppProperties[Circle].ToString();
            }

            if (Data.Properties.AppProperties.ContainsKey(Empty))
            {
                lblDraw.Text = Data.Properties.AppProperties[Empty].ToString();
            }

            if (Data.Properties.AppProperties.ContainsKey(Cross))
            {
                lblCross.Text = Data.Properties.AppProperties[Cross].ToString();
            }

            _startingSymbol = symbol;
            _currentSymbol = symbol;
            var buttons = GetButtons();
            buttons.ForEach(b => { b.Text = Empty; b.IsEnabled = true; });
        }

        private List<Button> GetButtons()
        {
            var grid = this.Content as Grid;
            var btnList = grid.Children.Where(g => g is Button).Cast<Button>().ToList();
            btnList.RemoveAt(9);
            return btnList;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var btn = sender as Button;
            if (btn.Text == Empty)
            {
                btn.Text = _currentSymbol;
                btn.IsEnabled = false;

                var btns = GetButtons();

                if (DoWeHaveWinner(btns))
                {
                    await DisplayAlert("Wygrana", $"Wygrywa: {_currentSymbol}", "OK");

                    if (Data.Properties.AppProperties.ContainsKey(_currentSymbol))
                    {
                        int symbolCount = int.Parse(Data.Properties.AppProperties[_currentSymbol].ToString());
                        Data.Properties.AppProperties[_currentSymbol] = symbolCount + 1;
                    }
                    else
                    {
                        Data.Properties.AppProperties[_currentSymbol] = 1;
                    }

                    await Application.Current.SavePropertiesAsync();
                    Initialize(_startingSymbol == Cross ? Circle : Cross);
                }
                else
                {
                    _currentSymbol = _currentSymbol == Circle ? Cross : Circle;
                    lblMove.Text = _currentSymbol == Circle ? "Kółko" : "Krzyżyk";
                }

                if (!AreThereEmptyFields(btns))
                {
                    await DisplayAlert("Remis", "Gra zakończyła się remisem.", "OK");
                    if (Data.Properties.AppProperties.ContainsKey(Empty))
                    {
                        int symbolCount = int.Parse(Data.Properties.AppProperties[Empty].ToString());
                        Data.Properties.AppProperties[Empty] = symbolCount + 1;
                    }
                    else
                    {
                        Data.Properties.AppProperties[Empty] = 1;
                    }

                    await Application.Current.SavePropertiesAsync();
                    Initialize(_startingSymbol == Cross ? Circle : Cross);
                }
            }
        }

        private bool AreThereEmptyFields(List<Button> btns)
        {
            return btns.Any(b => b.Text == Empty);
        }

        private bool DoWeHaveWinner(List<Button> btnList)
        {            
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
            return btnList[a].Text != Empty && btnList[a].Text == btnList[b].Text && btnList[b].Text == btnList[c].Text;
        }
    }
}