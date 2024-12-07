namespace AndventOfCode2024.Day7
{
    internal class Day7Task2 : Day7Task1
    {
        protected override void ProcessData(List<Day7Equation> data)
        {
            long result = 0;

            foreach (var equation in data)
            {
                var numberOfOperators = equation.Numbers.Length - 1;

                var operatorsPattern = new string('0', numberOfOperators);
                var endPattern = operatorsPattern;

                while (operatorsPattern.Length <= numberOfOperators)
                {
                    long equationResult = CalcEquation(equation, operatorsPattern);
                    if (equationResult == equation.GoalResult)
                    {
                        result += equationResult;
                        break;
                    }

                    int operatorsPatternNumber = FromBase3(operatorsPattern);
                    operatorsPatternNumber++;
                    operatorsPattern = ToBase3(operatorsPatternNumber).PadLeft(numberOfOperators, '0');
                }
            }

            Console.WriteLine(result);
        }

        protected long CalcEquation(Day7Equation equation, string operatorsPattern)
        {
            long equationResult = equation.Numbers[0];

            for (var i = 1; i < equation.Numbers.Length; i++)
            {
                var currentNumber = equation.Numbers[i];

                if (equationResult > equation.GoalResult)
                {
                    return equationResult;
                }

                if (operatorsPattern[i - 1] == '0')
                {
                    equationResult += currentNumber;
                }
                else if (operatorsPattern[i - 1] == '1')
                {
                    equationResult *= currentNumber;
                }
                else
                {
                    equationResult = long.Parse(equationResult.ToString() + currentNumber.ToString());
                }
            }
            return equationResult;
        }

        protected int FromBase3(string base3Number)
        {
            int result = 0;
            for (int i = 0; i < base3Number.Length; i++)
            {
                int digit = base3Number[i] - '0';
                result = result * 3 + digit;
            }
            return result;
        }

        string ToBase3(int decimalNumber)
        {
            if (decimalNumber == 0) return "0";

            string result = "";
            while (decimalNumber > 0)
            {
                int remainder = decimalNumber % 3;
                result = remainder + result;
                decimalNumber /= 3;
            }
            return result;
        }
    }
}
