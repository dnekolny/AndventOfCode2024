namespace AndventOfCode2024.Day6
{
    internal class Day6Task2 : Day6Task1
    {
        protected override void ProcessData(Day6Map data)
        {
            var loopCount = 0;

            var originalMap = CopyMap(data.Map);

            base.ProcessData(data);

            var visitedMap = CopyMap(data.Map);

            for (int x = 0; x < visitedMap.GetLength(0); x++)
            {
                for (int y = 0; y < visitedMap.GetLength(1); y++)
                {
                    if (visitedMap[x, y].Visited && IsPositionLoop(originalMap, new Position(x, y)))
                    {
                        loopCount++;
                    }
                }
            }

            Console.WriteLine(loopCount);
        }

        private bool IsPositionLoop(Day6Item[,] originalMap, Position position)
        {
            Data.Map = CopyMap(originalMap);

            Data.Map[position.X, position.Y].IsWall = true;

            return IsMapLoop();
        }

        private bool IsMapLoop()
        {
            List<Tuple<Position, Direction>> visitedPositions = [];

            position = Data.StartPosition;
            direction = Direction.Up;
            var keepGoing = true;

            while (keepGoing)
            {
                visitedPositions.Add(new Tuple<Position, Direction>(position, direction));

                keepGoing = NextPosition();

                if (keepGoing && visitedPositions.Any(visitedPos => visitedPos.Item1.X == position.X && visitedPos.Item1.Y == position.Y && visitedPos.Item2 == direction))
                {
                    return true;
                }
            }

            return false;
        }

        private Day6Item[,] CopyMap(Day6Item[,] originalMap)
        {
            int rows = originalMap.GetLength(0);
            int cols = originalMap.GetLength(1);

            Day6Item[,] deepCopy = new Day6Item[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    deepCopy[i, j] = (Day6Item)originalMap[i, j].Clone();
                }
            }
            return deepCopy;
        }
    }
}