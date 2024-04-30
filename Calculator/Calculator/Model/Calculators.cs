using SQLite;

namespace Calculator.Model
{
    [Table("Calculators")]
    public class Calculators
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string DisplayText { get; set; }
    
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

        public void Reset()
        {
            firstNumber = 0;
            operatorName = null;
            isOperatorClicked = false;
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
