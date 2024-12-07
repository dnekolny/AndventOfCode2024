namespace AndventOfCode2024.Day6
{
    internal class Day6Task1 : AdventOfCodeBaseTask<Day6Map>
    {
        protected override string InputFilePath => "Day6/input.txt";
        //protected override string InputFilePath => "Day6/input_small.txt";

        protected override Day6Map ParseData(string rawData)
        {
            var lines = rawData.Split(InputFilePath == "Day6/input_small.txt" ? "\r\n" : "\n");
            var map = new List<List<Day6Item>>();
            var currentPosition = new Position();

            for (int lineIndex = 0; lineIndex < lines.Length; lineIndex++)
            {
                var line = lines[lineIndex];
                var lineData = new List<Day6Item>();

                if (line == "")
                {
                    continue;
                }

                for (int i = 0; i < line.Length; i++)
                {
                    if (line[i] == '^')
                    {
                        currentPosition = new Position(i, lineIndex);
                    }

                    if (line[i] == '#')
                    {
                        lineData.Add(new Day6Item { IsWall = true });
                    }
                    else
                    {
                        lineData.Add(new Day6Item());
                    }
                }

                map.Add(lineData);
            }

            var arrayMap = new Day6Item[map.Count, map[0].Count];

            for (int i = 0; i < map.Count; i++)
            {
                for (int j = 0; j < map[i].Count; j++)
                {
                    arrayMap[j, i] = map[i][j];
                }
            }

            return new Day6Map()
            {
                Map = arrayMap,
                StartPosition = currentPosition,
            };
        }

        protected Direction direction = Direction.Up;
        protected Position position;

        protected override void ProcessData(Day6Map data)
        {
            position = data.StartPosition;
            var map = data.Map;
            var keepGoing = true;

            while (keepGoing)
            {
                map[position.X, position.Y].Visited = true;

                keepGoing = NextPosition();
            }

            var visitedCount = 0;
            foreach (var item in map)
            {
                visitedCount += item.Visited ? 1 : 0;
            }
            Console.WriteLine(visitedCount);
        }

        protected bool NextPosition()
        {
            var map = Data.Map;

            var newPosition = position;
            newPosition.X += direction == Direction.Left ? -1 : direction == Direction.Right ? 1 : 0;
            newPosition.Y += direction == Direction.Up ? -1 : direction == Direction.Down ? 1 : 0;

            if (newPosition.X < 0 || newPosition.X >= map.GetLength(0) || newPosition.Y < 0 || newPosition.Y >= map.GetLength(1))
            {
                return false;
            }

            var wallInFront = map[newPosition.X, newPosition.Y].IsWall;

            if (wallInFront)
            {
                ChangeDirection();
                return NextPosition();
            }
            else
            {
                position = newPosition;
                return true;
            }
        }

        protected void ChangeDirection()
        {
            direction = direction switch
            {
                Direction.Up => Direction.Right,
                Direction.Right => Direction.Down,
                Direction.Down => Direction.Left,
                Direction.Left => Direction.Up,
                _ => Direction.Up,
            };
        }
    }

    internal class Day6Map
    {
        public Day6Item[,] Map { get; set; }
        public Position StartPosition { get; set; }
    }

    internal class Day6Item : ICloneable
    {
        public bool IsWall { get; set; } = false;
        public bool Visited { get; set; } = false;

        public object Clone()
        {
            return new Day6Item
            {
                IsWall = IsWall,
                Visited = Visited
            };
        }
    }

    internal struct Position
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Position() { }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    internal enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
}