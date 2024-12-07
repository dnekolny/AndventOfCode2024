namespace AndventOfCode2024.Day4
{
    public class Day4Task1 : AdventOfCodeBaseTask<char[,]>
    {
        protected override string InputFilePath => "Day4/input.txt";
        //protected override string InputFilePath => "Day4/input_small.txt";

        protected override char[,] ParseData(string rawData)
        {
            var rows = rawData.Split("\n").Where(r => !string.IsNullOrWhiteSpace(r)).ToArray();
            char[,] result = null;

            for (int iRow = 0; iRow < rows.Length; iRow++)
            {
                char[] row = rows[iRow].ToCharArray();

                if (result == null)
                {
                    result = new char[rows.Length, row.Length];
                }

                for (int iCol = 0; iCol < row.Length; iCol++)
                {
                    result[iRow, iCol] = row[iCol];
                }
            }
            return result;
        }

        protected override void ProcessData(char[,] data)
        {
            var iRow = 0;
            var iCol = 0;

            string horizontal = "";
            string horizontalReverse = "";

            for (iRow = 0; iRow < data.GetLength(0); iRow++)
            {
                for (iCol = 0; iCol < data.GetLength(1); iCol++)
                {
                    horizontal += data[iRow, iCol];
                    horizontalReverse += data[data.GetLength(0) - 1 - iRow, data.GetLength(1) - 1 - iCol];
                }
            }

            string vertical = "";
            string verticalReverse = "";

            for (iCol = 0; iCol < data.GetLength(1); iCol++)
            {
                for (iRow = 0; iRow < data.GetLength(0); iRow++)
                {
                    vertical += data[iRow, iCol];
                    verticalReverse += data[data.GetLength(0) - 1 - iRow, data.GetLength(1) - 1 - iCol];
                }
            }

            string diagonal1 = "";
            string diagonal1Reverse = "";
            var nextRow = false;
            var nextCol = false;
            iRow = data.GetLength(0) - 1;
            iCol = 0;
            var run = false;

            var numberOfDiagonalLines = data.GetLength(0) + data.GetLength(1) - 1;

            diagonal1 += data[4, 0];

            diagonal1 += data[3, 0];
            diagonal1 += data[4, 1];

            diagonal1 += data[2, 0];
            diagonal1 += data[3, 1];
            diagonal1 += data[4, 2];

            diagonal1 += data[1, 0];
            diagonal1 += data[2, 1];
            diagonal1 += data[3, 2];
            diagonal1 += data[4, 3];

            for (var line = 0; line < numberOfDiagonalLines - 1; line++)
            {
                for (iRow = data.GetLength(0) - line; iRow < data.GetLength(0); iRow--)
                {
                    for (iCol = 0; iCol < data.GetLength(1); iCol++)
                    {
                        if (nextRow)
                        {
                            diagonal1 += data[iRow, iCol];
                        }
                        if (nextCol)
                        {
                            diagonal1 += data[iRow, iCol];
                        }
                    }
                }
            }
        }
    }
}

