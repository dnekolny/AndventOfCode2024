namespace AndventOfCode2024.Day5
{
    public class Day5Task2 : Day5Task1
    {
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

                if (!isValid)
                {
                    resutl += RepairRow(row);
                }
            }

            Console.WriteLine(resutl);
        }

        private int RepairRow(List<int> row)
        {
            //Console.WriteLine(Environment.NewLine);
            //Console.WriteLine("Repairing row: " + string.Join(", ", row));
            var rulesToRow = Data.Rules.Where(r => row.Contains(r.Item1) && row.Contains(r.Item2));
            var somethingChanged = true;

            while (somethingChanged)
            {
                somethingChanged = false;

                for (int indexNumber = 0; indexNumber < row.Count; indexNumber++)
                {
                    var number = row[indexNumber];
                    var rulesToNumber = rulesToRow.Where(r => r.Item1 == number || r.Item2 == number);

                    for (int indexSecond = 0; indexSecond < row.Count; indexSecond++)
                    {
                        if (indexNumber == indexSecond) continue;

                        var secondNumber = row[indexSecond];
                        var rulesToSecond = rulesToNumber.Where(r => r.Item1 == secondNumber || r.Item2 == secondNumber);

                        //Console.WriteLine("...");
                        //Console.WriteLine("    row: " + string.Join(", ", row));
                        //Console.WriteLine("numbers: " + number + ", " + secondNumber);
                        //Console.WriteLine("   rule: " + string.Join(", ", rulesToSecond));

                        foreach (var rule in rulesToSecond)
                        {
                            if (rule.Item1 == number && indexNumber > indexSecond)
                            {
                                row.MoveBefore(number, secondNumber);
                                somethingChanged = true;
                                //Console.WriteLine("new row: " + string.Join(", ", row));
                            }
                            else if (rule.Item2 == number && indexNumber < indexSecond)
                            {
                                row.MoveAfter(number, secondNumber);
                                somethingChanged = true;
                                //Console.WriteLine("new row: " + string.Join(", ", row));
                            }
                        }
                    }
                }
            }

            //Console.WriteLine(Environment.NewLine);
            //Console.WriteLine("REPAIRED row: " + string.Join(", ", row));
            var middleNumber = row[row.Count / 2];

            //Console.WriteLine("Middle number: " + middleNumber);
            return middleNumber;
        }
    }
}