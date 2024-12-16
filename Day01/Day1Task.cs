namespace AndventOfCode2024.Day1
{
    public class Day1Task : AdventOfCodeBaseTask<(List<int>, List<int>)>
    {
        protected override string InputFilePath { get => "Day1/input.txt"; }

        protected override (List<int>, List<int>) ParseData(string rawData)
        {
            var rows = SplitDataToLines(rawData);
            List<int> numberList1 = [];
            List<int> numberList2 = [];

            for (int i = 0; i < rows.Length; i++)
            {
                var row = rows[i];
                if (string.IsNullOrEmpty(row))
                {
                    continue;
                }

                var pairOfNumbers = row.Split("   ");
                numberList1.Add(int.Parse(pairOfNumbers[0]));
                numberList2.Add(int.Parse(pairOfNumbers[1]));
            }
            return (numberList1, numberList2);
        }

        protected override void ProcessData((List<int>, List<int>) data)
        {
            var numberList1 = data.Item1;
            var numberList2 = data.Item2;
            var totalDiferrence = 0;

            numberList1 = [.. numberList1.OrderBy(num => num)];
            numberList2 = [.. numberList2.OrderBy(num => num)];

            for (int i = 0; i < numberList1.Count; i++)
            {
                var number1 = numberList1[i];
                var number2 = numberList2[i];

                var difference = number1 - number2;

                if (difference < 0)
                {
                    difference = difference * -1;
                }

                totalDiferrence += difference;
            }

            Console.WriteLine($"Total difference: {totalDiferrence}");
        }
    }
}
