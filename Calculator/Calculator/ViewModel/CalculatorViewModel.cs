using Calculator.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
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
        private ObservableCollection<string> savedDisplayTexts;
        public ObservableCollection<string> SavedDisplayTexts
        {
            get { return savedDisplayTexts; }
            set
            {
                if (savedDisplayTexts != value)
                {
                    savedDisplayTexts = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SavedDisplayTexts"));
                }
            }
        }
        private string previousButtonClicked;
        private decimal firstNumber;
        private string currentOperator;

        public CalculatorViewModel()
        {
            calculators = new Calculators();
            ButtonCommand = new Command<string>(ButtonClicked);
            DisplayText = "0";
            SavedDisplayTexts = new ObservableCollection<string>();
        }
        private void ButtonClicked(string buttonText)
        {
            if (buttonText == "AC")
            {
                DisplayText = "";
                SavedDisplayTexts.Clear();
                calculators.Reset();
                firstNumber = 0;
                currentOperator = null;
            }
            else if (buttonText == "=")
            {
                CalculateExpression();
                string savedDisplayText = $"{DisplayText}";
                SavedDisplayTexts.Add(savedDisplayText);
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
            else if (buttonText == "*" || buttonText == "/" || buttonText == "+" || buttonText == "-")
            {
                previousButtonClicked = buttonText;
                currentOperator = null;
                DisplayText += buttonText;
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
        private void CalculateExpression()
        {
            string expression = DisplayText;

            string[] elements = expression.Split(new char[] { '*', '/', '+', '-' }, StringSplitOptions.RemoveEmptyEntries);
            List<decimal> numbers = new List<decimal>();
            List<string> operators = new List<string>();

            bool isNegative = expression.StartsWith("-");

            foreach (char c in expression)
            {
                if (c == '*' || c == '/' || c == '+' || c == '-')
                {
                    operators.Add(c.ToString());
                }
            }

            if (isNegative)
            {
                if (operators.Count > 0)
                {
                    operators.RemoveAt(0);
                }
                if (decimal.TryParse(elements[0], out decimal firstNumber))
                {
                    numbers.Add(-firstNumber);
                    elements = elements.Skip(1).ToArray();
                }
                else
                {
                    DisplayText = "Error";
                    return;
                }
            }

            foreach (string element in elements)
            {
                if (decimal.TryParse(element, out decimal number))
                {
                    numbers.Add(number);
                }
                else
                {
                    DisplayText = "Error";
                    return;
                }
            }

            StringBuilder sb = new StringBuilder();
            sb.Append(expression).Append(" = ");

            for (int i = 0; i < operators.Count;)
            {
                if (operators[i] == "*" || operators[i] == "/")
                {
                    decimal num1 = numbers[i];
                    decimal num2 = numbers[i + 1];
                    string currentOperator = operators[i];

                    decimal result = currentOperator == "*" ? num1 * num2 : num1 / num2;

                    numbers[i] = result;
                    numbers.RemoveAt(i + 1);
                    operators.RemoveAt(i);
                }
                else
                {
                    i++;
                }
            }

            decimal finalResult = numbers[0];
            for (int i = 0; i < operators.Count; i++)
            {
                decimal nextNumber = numbers[i + 1];
                string currentOperator = operators[i];

                switch (currentOperator)
                {
                    case "+":
                        finalResult += nextNumber;
                        break;
                    case "-":
                        finalResult -= nextNumber;
                        break;
                }
            }

            DisplayText = finalResult.ToString();

            sb.Append(finalResult.ToString());
            string savedDisplayText = sb.ToString();
            SavedDisplayTexts.Add(savedDisplayText);
        }





    }
}

