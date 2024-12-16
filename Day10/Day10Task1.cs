namespace AndventOfCode2024.Day10
{
    internal class Day10Task1 : AdventOfCodeBaseTask<Day10Map>
    {
        protected override string InputFilePath => "Day10/input.txt";
        //protected override string InputFilePath => "Day10/input_small.txt";

        protected override Day10Map ParseData(string rawData)
        {
            var map = new Day10Map();
            var lines = SplitDataToLines(rawData);

            for (var y = 0; y < lines.Length; y++)
            {
                var line = lines[y];

                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                var mapPoints = new List<MapPoint>();

                for (int x = 0; x < line.Length; x++)
                {
                    var height = int.Parse(line[x].ToString());

                    mapPoints.Add(new MapPoint
                    {
                        X = x,
                        Y = y,
                        Height = height,
                        Visited = false
                    });
                }
                map.Add(mapPoints);
            }

            return map;
        }

        protected override void ProcessData(Day10Map data)
        {
            var hikes = 0;

            foreach (var mapPoints in data)
            {
                foreach (var mapPoint in mapPoints)
                {
                    if (mapPoint.Height == 0)
                    {
                        var currentMap = CopyMap(data);
                        FindHikesFromPoint(mapPoint, currentMap, ref hikes);
                    }
                }
            }

            Console.WriteLine(hikes);
        }

        private void FindHikesFromPoint(MapPoint point, Day10Map map, ref int hikes)
        {
            if (point.Visited)
            {
                return;
            }

            point.Visited = true;

            if (point.Height == 9)
            {
                hikes++;
                return;
            }

            var neighbours = GetNeighbours(point, map);

            foreach (var neighbour in neighbours)
            {
                var nextHeight = point.Height + 1;

                if (neighbour.Height == nextHeight)
                {
                    FindHikesFromPoint(neighbour, map, ref hikes);
                }
            }
        }

        protected List<MapPoint> GetNeighbours(MapPoint point, Day10Map map)
        {
            var neighbours = new List<MapPoint>();

            if (point.X > 0)
            {
                neighbours.Add(map[point.Y][point.X - 1]);
            }

            if (point.X < map.XCount - 1)
            {
                neighbours.Add(map[point.Y][point.X + 1]);
            }

            if (point.Y > 0)
            {
                neighbours.Add(map[point.Y - 1][point.X]);
            }

            if (point.Y < map.YCount - 1)
            {
                neighbours.Add(map[point.Y + 1][point.X]);
            }

            return neighbours;
        }

        protected Day10Map CopyMap(Day10Map map)
        {
            var newMap = new Day10Map();

            foreach (var mapPoints in map)
            {
                var newMapPoints = new List<MapPoint>();

                foreach (var mapPoint in mapPoints)
                {
                    newMapPoints.Add((MapPoint)mapPoint.Clone());
                }

                newMap.Add(newMapPoints);
            }

            return newMap;
        }
    }

    internal class Day10Map : List<List<MapPoint>>
    {
        public int YCount => this.Count;
        public int XCount => YCount > 0 ? this[0].Count : 0;
    }

    internal class MapPoint : ICloneable
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Height { get; set; }
        public bool Visited { get; set; }

        public object Clone()
        {
            return new MapPoint
            {
                X = X,
                Y = Y,
                Height = Height,
                Visited = Visited
            };
        }
    }
}
