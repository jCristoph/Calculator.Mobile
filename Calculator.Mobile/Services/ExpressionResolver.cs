using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Calculator.Mobile.Services
{
    internal static class ExpressionResolver
    {
        private static char[] mathSigns = { '+', '-', '/', '*', '^', '√' };
        private static ArrayList operationOrder = new ArrayList();
        private static List<string> numbers = new List<string>();

        public static double Compute(string expression)
        {
            numbers = (expression.Split(mathSigns)).ToList();
            SetExpressionOrder(expression);  
            while(operationOrder.Count > 0)
            {
                var index = 0;
                var result = 0.0;
                if (operationOrder[0].Equals("√"))
                {
                    index = operationOrder.IndexOf("√");
                    if(numbers[index] == "")
                        result = Math.Sqrt(Convert.ToDouble(numbers[index + 1]));
                    else
                        result = Convert.ToDouble(numbers[index]) * Math.Sqrt(Convert.ToDouble(numbers[index + 1]));
                }
                else if (operationOrder[0].Equals("^"))
                {
                    index = operationOrder.IndexOf("^");
                    result = Math.Pow(Convert.ToDouble(numbers[index]), Convert.ToDouble(numbers[index + 1]));
                }
                else if (operationOrder[0].Equals("*"))
                {
                    index = operationOrder.IndexOf("*");
                    result = Convert.ToDouble(numbers[index]) * Convert.ToDouble(numbers[index + 1]);
                }
                else if (operationOrder[0].Equals("/"))
                {
                    index = operationOrder.IndexOf("/");
                    result = Convert.ToDouble(numbers[index]) / Convert.ToDouble(numbers[index + 1]);
                }
                else if (operationOrder[0].Equals("+"))
                {
                    index = operationOrder.IndexOf("+");
                    result = Convert.ToDouble(numbers[index]) + Convert.ToDouble(numbers[index + 1]);
                }
                else if (operationOrder[0].Equals("-"))
                {
                    index = operationOrder.IndexOf("-");
                    result = Convert.ToDouble(numbers[index]) - Convert.ToDouble(numbers[index + 1]);
                }
                numbers[index] = result.ToString();
                numbers.RemoveAt(index + 1);
                operationOrder.RemoveAt(index);
            }
            return Convert.ToDouble(numbers[0]);
        }

        private static void SetExpressionOrder(string expression)
        {
            char[] array = expression.ToCharArray();
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == '+')
                    operationOrder.Add("+");
                if (array[i] == '-')
                    operationOrder.Add("-");
                if(array[i] == '*')
                    operationOrder.Add("*");
                if (array[i] == '/')
                    operationOrder.Add("/");
                if(array[i] == '^')
                    operationOrder.Add("^");
                if(array[i] == '√')
                    operationOrder.Add("√");
            }
        }
    }
}