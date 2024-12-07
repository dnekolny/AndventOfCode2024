namespace AndventOfCode2024.Day1
{
    public class Day1TaskPart2 : Day1Task
    {
        protected override void ProcessData((List<int>, List<int>) data)
        {
            var numberList1 = data.Item1;
            var numberList2 = data.Item2;
            var similarityScore = 0;

            foreach (var number1 in numberList1)
            {
                var numberOfSimilarDigits = 0;

                foreach (var number2 in numberList2)
                {

                    if (number1 == number2)
                    {
                        numberOfSimilarDigits++;
                    }
                }

                similarityScore += number1 * numberOfSimilarDigits;
            }

            Console.WriteLine($"Similarity score: {similarityScore}");
        }
    }
}
