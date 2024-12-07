namespace AndventOfCode2024.Day2
{
    public class Day2Task : AdventOfCodeBaseTask<List<List<int>>>
    {
        protected override string InputFilePath => "Day2/input.txt";

        protected override List<List<int>> ParseData(string rawData)
        {
            var lines = SplitDataToLines(rawData);
            List<List<int>> data = [];

            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line))
                {
                    continue;
                }
                var digits = line.Split(' ');
                data.Add(digits.Select(d => int.Parse(d)).ToList());
            }
            return data;
        }

        protected override void ProcessData(List<List<int>> data)
        {
            var numberOfSafeLines = 0;

            foreach (var line in data)
            {
                var previousDigit = -1;
                var goingUp = true;
                var isLineSafe = true;

                for (int i = 0; i < line.Count; i++)
                {
                    var digit = line[i];

                    if (i > 0)
                    {
                        var diferrence = previousDigit - digit;

                        if (diferrence == 0 || diferrence > 3 || diferrence < -3)
                        {
                            isLineSafe = false;
                        }
                        else
                        {
                            if (i == 1)
                            {
                                goingUp = previousDigit < digit;
                            }
                            else
                            {
                                if (goingUp && diferrence > 0)
                                {
                                    isLineSafe = false;
                                }
                                else if (!goingUp && diferrence < 0)
                                {
                                    isLineSafe = false;
                                }
                            }
                        }

                        if (!isLineSafe)
                        {
                            break;
                        }
                    }
                    previousDigit = digit;
                }

                if (isLineSafe)
                {
                    numberOfSafeLines++;
                }
            }

            Console.WriteLine($"Number of safe lines: {numberOfSafeLines}");
        }
    }
}
