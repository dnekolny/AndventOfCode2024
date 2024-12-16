namespace AndventOfCode2024.Day4
{
    public class Day4Task2 : Day4Task1
    {
        protected override void ProcessData(char[,] data)
        {
            var result = 0;
            var iRow = 0;
            var iCol = 0;

            for (iRow = 0; iRow < data.GetLength(0); iRow++)
            {
                for (iCol = 0; iCol < data.GetLength(1); iCol++)
                {
                    if (data[iRow, iCol] == 'A')
                    {
                        result += IsCrossfXmasFromPosition(data, iRow, iCol) ? 1 : 0;
                    }
                }
            }

            Console.WriteLine($"Result: {result}");
        }

        private bool IsCrossfXmasFromPosition(char[,] data, int x, int y)
        {
            int successCount = 0;

            if (x < 1 || x >= data.GetLength(0) - 1 || y < 1 || y >= data.GetLength(1) - 1)
            {
                return false;
            }

            if (data[x - 1, y + 1] == 'M' && data[x + 1, y - 1] == 'S')
            {
                successCount++;
            }
            else if (data[x - 1, y + 1] == 'S' && data[x + 1, y - 1] == 'M')
            {
                successCount++;
            }

            if (data[x + 1, y + 1] == 'M' && data[x - 1, y - 1] == 'S')
            {
                successCount++;
            }
            else if (data[x + 1, y + 1] == 'S' && data[x - 1, y - 1] == 'M')
            {
                successCount++;
            }

            return successCount == 2;
        }
    }
}

