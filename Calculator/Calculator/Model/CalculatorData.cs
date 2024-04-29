using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator.Model
{
    [Table("CalculatorData")]
    public class CalculatorData
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string DisplayText { get; set; }
    }
}