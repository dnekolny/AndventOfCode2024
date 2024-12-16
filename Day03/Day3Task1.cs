namespace AndventOfCode2024.Day3
{
    public class Day3Task1 : AdventOfCodeBaseTask<string>
    {
        protected override string InputFilePath => "Day3/input.txt";

        protected override string ParseData(string rawData)
        {
            return rawData;
        }

        protected override void ProcessData(string data)
        {
            var result = 0;
            var numberOfMuls = 0;
            var indexOfMul = -1;
            var comma = false;
            var firstNumber = "";
            var secondNumber = "";

            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] == 'm')
                {
                    indexOfMul = 0;
                    continue;
                }
                else if (indexOfMul == -1)
                {
                    continue;
                }
                else if (
                       (data[i] == 'u' && indexOfMul == 0)
                    || (data[i] == 'l' && indexOfMul == 1)
                    || (data[i] == '(' && indexOfMul == 2))
                {
                    indexOfMul++;
                    continue;
                }
                else if (indexOfMul > 2 && indexOfMul < 10 && (data[i] == ',' || IsCharNumber(data[i])))
                {
                    if (data[i] == ',')
                    {
                        comma = true;
                        continue;
                    }
                    if (comma)
                    {
                        secondNumber += data[i];
                        if (secondNumber.Length > 3)
                        {
                            indexOfMul = -1;
                        }
                        continue;
                    }
                    else
                    {
                        firstNumber += data[i];
                        if (firstNumber.Length > 3)
                        {
                            indexOfMul = -1;
                        }
                        continue;
                    }
                }
                else if (data[i] == ')' && comma && !string.IsNullOrWhiteSpace(firstNumber) && !string.IsNullOrWhiteSpace(secondNumber))
                {
                    result += int.Parse(firstNumber) * int.Parse(secondNumber);
                    numberOfMuls++;
                }

                indexOfMul = -1;
                comma = false;
                firstNumber = "";
                secondNumber = "";
            }

            Console.WriteLine(result);
            Console.WriteLine(numberOfMuls);
        }
    }
}
