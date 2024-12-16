namespace AndventOfCode2024.Day2
{
    public class Day2TaskPart2 : Day2Task
    {
        protected override void ProcessData(List<List<int>> data)
        {
            var numberOfSafeLines = 0;

            foreach (var line in data)
            {
                var isLineSafe = true;

                for (int removeIndex = -1; removeIndex < line.Count; removeIndex++)
                {
                    var lineCopy = new List<int>(line);
                    var previousDigit = -1;
                    var goingUp = true;
                    isLineSafe = true;

                    if (removeIndex >= 0)
                    {
                        lineCopy.RemoveAt(removeIndex);
                    }

                    for (int i = 0; i < lineCopy.Count; i++)
                    {
                        var digit = lineCopy[i];

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
                        break;
                    }
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
