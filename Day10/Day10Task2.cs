namespace AndventOfCode2024.Day10
{
    internal class Day10Task2 : Day10Task1
    {
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
    }
}
