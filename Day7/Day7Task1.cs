namespace AndventOfCode2024.Day7
{
    internal class Day7Task1 : AdventOfCodeBaseTask<List<Day7Equation>>
    {
        protected override string InputFilePath => "Day7/input.txt";
        //protected override string InputFilePath => "Day7/input_small.txt";

        protected override List<Day7Equation> ParseData(string rawData)
        {
            var lines = SplitDataToLines(rawData);
            var equations = new List<Day7Equation>();

            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line))
                {
                    continue;
                }

                var equation = new Day7Equation();

                var parts = line.Split(':');
                equation.GoalResult = long.Parse(parts[0]);

                var numbers = parts[1].Trim().Split(' ');
                equation.Numbers = numbers.Select(int.Parse).ToArray();
                equations.Add(equation);
            }

            return equations;
        }

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

                    int operatorsPatternNumber = Convert.ToInt32(operatorsPattern, 2);
                    operatorsPatternNumber++;
                    operatorsPattern = Convert.ToString(operatorsPatternNumber, 2).PadLeft(numberOfOperators, '0');
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
                else
                {
                    equationResult *= currentNumber;
                }
            }
            return equationResult;
        }
    }

    internal class Day7Equation
    {
        public long GoalResult { get; set; }
        public int[] Numbers { get; set; }
    }
}
