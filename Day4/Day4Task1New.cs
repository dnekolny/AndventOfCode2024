namespace AndventOfCode2024.Day4
{
    public class Day4Task1New : Day4Task1
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
                    if (data[iRow, iCol] == 'X')
                    {
                        result += GetCountOfXmasFromPosition(data, iRow, iCol);
                    }
                }
            }

            Console.WriteLine($"Result: {result}");
        }

        private int GetCountOfXmasFromPosition(char[,] data, int x, int y)
        {
            var count = 0;

            count += IsXmas(data, x, y, x + 3, y);
            count += IsXmas(data, x, y, x - 3, y);
            count += IsXmas(data, x, y, x, y + 3);
            count += IsXmas(data, x, y, x, y - 3);
            count += IsXmas(data, x, y, x + 3, y + 3);
            count += IsXmas(data, x, y, x + 3, y - 3);
            count += IsXmas(data, x, y, x - 3, y - 3);
            count += IsXmas(data, x, y, x - 3, y + 3);

            return count;
        }

        private int IsXmas(char[,] data, int startX, int startY, int endX, int endY)
        {
            if (endX < 0 || endX >= data.GetLength(0) || endY < 0 || endY >= data.GetLength(1))
            {
                return 0;
            }

            var x = startX;
            var y = startY;
            var text = "" + data[x, y];

            for (int i = 0; i < 3; i++)
            {
                if (x < endX) x++;
                if (x > endX) x--;
                if (y < endY) y++;
                if (y > endY) y--;

                text += data[x, y];
            }

            return text == "XMAS" ? 1 : 0;
        }
    }
}

