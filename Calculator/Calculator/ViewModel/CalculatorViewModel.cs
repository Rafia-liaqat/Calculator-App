using Calculator.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Calculator.ViewModel
{

    public class CalculatorViewModel : INotifyPropertyChanged
    {
       
        public event PropertyChangedEventHandler PropertyChanged;

        private Calculators calculators;
        public ICommand ButtonCommand { get; private set; }

        private string displayText;
        public string DisplayText
        {
            get { return displayText; }
            set
            {
                if (displayText != value)
                {
                    displayText = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DisplayText"));
                }
            }
        }

        private string previousButtonClicked;

        public CalculatorViewModel()
        {
            calculators = new Calculators();
            ButtonCommand = new Command<string>(ButtonClicked);
            DisplayText = "0";
        }

        private void ButtonClicked(string buttonText)
        {
            if (buttonText == "AC")
            {
                DisplayText = "0";
                calculators.FirstNumber = 0;
            }
            else if (buttonText == "--")
            {
                if (DisplayText != "0")
                {
                    DisplayText = DisplayText.Substring(0, DisplayText.Length - 1);
                    if (string.IsNullOrEmpty(DisplayText))
                    {
                        DisplayText = "0";
                    }
                }
            }
            else if (buttonText == "%")
            {
                decimal percentValue = Convert.ToDecimal(DisplayText);
                string result = (percentValue / 100).ToString("0.##");
                DisplayText = result;
            }
            else if (buttonText == "+" || buttonText == "-" || buttonText == "*" || buttonText == "/")
            {
                if (previousButtonClicked != buttonText)
                {
                    calculators.FirstNumber = Convert.ToDecimal(DisplayText);
                    calculators.OperatorName = buttonText;
                    DisplayText = "0";
                }
                previousButtonClicked = buttonText;
            }
            else if (buttonText == "=")
            {
                decimal secondNumber = Convert.ToDecimal(DisplayText);
                string expression = $"{calculators.FirstNumber} {calculators.OperatorName} {secondNumber} = ";
                string finalResult = calculators.Calculate(secondNumber).ToString("0.##");
                DisplayText = $"{expression}{finalResult}";
            }
            else
            {
                if (DisplayText == "0" || calculators.IsOperatorClicked)
                {
                    calculators.IsOperatorClicked = false;
                    DisplayText = buttonText;
                }
                else
                {
                    DisplayText += buttonText;
                }
                previousButtonClicked = null;
            }
        }
    }
}