namespace AndventOfCode2024.Day5
{
    public class Day5Task1 : AdventOfCodeBaseTask<Day5Data>
    {
        protected override string InputFilePath => "Day5/input.txt";
        //protected override string InputFilePath => "Day5/input_small.txt";

        protected override Day5Data ParseData(string rawData)
        {
            var lines = rawData.Split(InputFilePath == "Day5/input_small.txt" ? "\r\n" : "\n");

            List<Tuple<int, int>> rules = [];
            List<List<int>> data = [];
            var isData = false;

            foreach (var line in lines)
            {
                if (line == "")
                {
                    isData = true;
                    continue;
                }

                if (isData)
                {
                    var numbers = line.Split(",");
                    data.Add(numbers.Select(int.Parse).ToList());
                }
                else
                {
                    var rule = line.Split("|");
                    rules.Add(new Tuple<int, int>(int.Parse(rule[0]), int.Parse(rule[1])));
                }
            }

            return new Day5Data
            {
                Rules = rules,
                Data = data
            };
        }

        protected override void ProcessData(Day5Data data)
        {
            var resutl = 0;

            foreach (var row in data.Data)
            {
                var middleNumber = 0;
                var isValid = true;

                for (int indexNumber = 0; indexNumber < row.Count; indexNumber++)
                {
                    var number = row[indexNumber];
                    var rulesToCheck = data.Rules.Where(r => r.Item1 == number || r.Item2 == number);

                    foreach (var ruleToCheck in rulesToCheck)
                    {
                        if (ruleToCheck.Item1 == number)
                        {
                            for (int indexBigger = 0; indexBigger < row.Count; indexBigger++)
                            {
                                if (row[indexBigger] == ruleToCheck.Item2 && indexBigger < indexNumber)
                                {
                                    isValid = false;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            for (int indexSmaller = 0; indexSmaller < row.Count; indexSmaller++)
                            {
                                if (row[indexSmaller] == ruleToCheck.Item1 && indexSmaller > indexNumber)
                                {
                                    isValid = false;
                                    break;
                                }
                            }
                        }

                        if (!isValid) break;
                    }

                    if (indexNumber == ((int)row.Count / 2))
                    {
                        middleNumber = number;
                    }

                    if (!isValid) break;
                }

                resutl += isValid ? middleNumber : 0;
            }

            Console.WriteLine(resutl);
        }
    }

    public class Day5Data
    {
        public List<Tuple<int, int>> Rules;
        public List<List<int>> Data;
    }
}