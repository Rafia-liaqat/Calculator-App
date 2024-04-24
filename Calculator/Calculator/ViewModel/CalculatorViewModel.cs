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

        private Calculators calculatorModel;
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

        private string previousButtonClicked; // Field to track previous button clicked

        public CalculatorViewModel()
        {
            calculatorModel = new Calculators();
            ButtonCommand = new Command<string>(ButtonClicked);
            DisplayText = "0";
        }

        private void ButtonClicked(string buttonText)
        {
            if (buttonText == "AC")
            {
                DisplayText = "0";
                calculatorModel.FirstNumber = 0;
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
                // Check if the previous button clicked is the same as the current one
                if (previousButtonClicked != buttonText)
                {
                    calculatorModel.FirstNumber = Convert.ToDecimal(DisplayText);
                    calculatorModel.OperatorName = buttonText;
                    DisplayText = "0";
                }
                // Update the previousButtonClicked
                previousButtonClicked = buttonText;
            }
            else if (buttonText == "=")
            {
                decimal secondNumber = Convert.ToDecimal(DisplayText);
                string finalResult = calculatorModel.Calculate(secondNumber).ToString("0.##");
                DisplayText = finalResult;
            }
            else
            {
                if (DisplayText == "0" || calculatorModel.IsOperatorClicked)
                {
                    calculatorModel.IsOperatorClicked = false;
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