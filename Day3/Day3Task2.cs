namespace AndventOfCode2024.Day3
{
    public class Day3Task2 : Day3Task1
    {
        protected override void ProcessData(string data)
        {
            var result = 0;
            var numberOfMuls = 0;
            var indexOfMul = -1;
            var comma = false;
            var firstNumber = "";
            var secondNumber = "";
            var enabled = true;
            var indexOfDo = -1;

            for (int i = 0; i < data.Length; i++)
            {
                if (enabled)
                {
                    //don't
                    if (data[i] == 'd')
                    {
                        indexOfDo = 0;
                        continue;
                    }
                    if (
                           (data[i] == 'o' && indexOfDo == 0)
                        || (data[i] == 'n' && indexOfDo == 1)
                        || (data[i] == '\'' && indexOfDo == 2)
                        || (data[i] == 't' && indexOfDo == 3)
                        || (data[i] == '(' && indexOfDo == 4))
                    {
                        indexOfDo++;
                        continue;
                    }
                    if (data[i] == ')' && indexOfDo == 5)
                    {
                        enabled = false;
                    }

                    //mul()
                    if (enabled)
                    {
                        if (data[i] == 'm')
                        {
                            indexOfMul = 0;
                            continue;
                        }
                        if (indexOfMul == -1)
                        {
                            continue;
                        }
                        if (
                               (data[i] == 'u' && indexOfMul == 0)
                            || (data[i] == 'l' && indexOfMul == 1)
                            || (data[i] == '(' && indexOfMul == 2))
                        {
                            indexOfMul++;
                            continue;
                        }
                        if (indexOfMul > 2 && indexOfMul < 10 && (data[i] == ',' || IsCharNumber(data[i])))
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
                    }
                }
                else
                {
                    if (data[i] == 'd')
                    {
                        indexOfDo = 0;
                        continue;
                    }
                    if (
                           (data[i] == 'o' && indexOfDo == 0)
                        || (data[i] == '(' && indexOfDo == 1))
                    {
                        indexOfDo++;
                        continue;
                    }
                    if (data[i] == ')' && indexOfDo == 2)
                    {
                        enabled = true;
                    }
                }

                indexOfMul = -1;
                comma = false;
                firstNumber = "";
                secondNumber = "";
                indexOfDo = -1;
            }

            Console.WriteLine(result);
            Console.WriteLine(numberOfMuls);
        }
    }
}
