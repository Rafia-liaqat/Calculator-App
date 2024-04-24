using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator.Model
{
    class Calculators
    {
        private decimal firstNumber;
        private string operatorName;
        private bool isOperatorClicked;

        public decimal FirstNumber
        {
            get { return firstNumber; }
            set { firstNumber = value; }
        }

        public string OperatorName
        {
            get { return operatorName; }
            set { operatorName = value; }
        }

        public bool IsOperatorClicked
        {
            get { return isOperatorClicked; }
            set { isOperatorClicked = value; }
        }

        public decimal Calculate(decimal secondNumber)
        {
            decimal result = 0;
            if (operatorName == "+")
            {
                result = firstNumber + secondNumber;
            }
            else if (operatorName == "-")
            {
                result = firstNumber - secondNumber;
            }
            else if (operatorName == "*")
            {
                result = firstNumber * secondNumber;
            }
            else if (operatorName == "/")
            {
                result = firstNumber / secondNumber;
            }
            return result;
        }
    }
}