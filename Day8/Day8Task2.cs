using AndventOfCode2024.Day6;

namespace AndventOfCode2024.Day8
{
    internal class Day8Task2 : AdventOfCodeBaseTask<Day8Map>
    {
        protected override string InputFilePath => "Day8/input.txt";
        //protected override string InputFilePath => "Day8/input_small.txt";
        //protected override string InputFilePath => "Day8/input_small2.txt";

        protected override Day8Map ParseData(string rawData)
        {
            List<List<char>> data = [];
            var lines = SplitDataToLines(rawData);

            for (int y = 0; y < lines.Count(); y++)
            {
                var line = lines[y];

                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                data.Add(new List<char>());

                foreach (var character in line)
                {
                    data[y].Add(character);
                }
            }

            return new Day8Map
            {
                Items = data.Select((line, y) => line.Select((character, x) => new Day8Item { X = x, Y = y, Frequency = character }).ToArray()).ToArray()
            };
        }

        protected override void ProcessData(Day8Map data)
        {
            Dictionary<char, List<Day8Item>> frequencies = new();
            var antinodeMap = new bool[data.Items.Length, data.Items[0].Length];

            for (int y = 0; y < data.Items.Length; y++)
            {
                for (int x = 0; x < data.Items[y].Length; x++)
                {
                    var item = data.Items[y][x];

                    if (item.Frequency == '.')
                    {
                        continue;
                    }

                    if (!frequencies.ContainsKey(item.Frequency))
                    {
                        frequencies[item.Frequency] = new List<Day8Item>();
                    }

                    var sameFrequnecyItems = frequencies[item.Frequency];

                    foreach (var sameFrequnecyItem in sameFrequnecyItems)
                    {
                        CalculateAntinodes(item, sameFrequnecyItem, antinodeMap);
                    }

                    frequencies[item.Frequency].Add(item);
                }
            }

            var antinodeCount = 0;

            foreach (var antinode in antinodeMap)
            {
                if (antinode)
                {
                    antinodeCount++;
                }
            }
            Console.WriteLine(antinodeCount);
        }

        protected void CalculateAntinodes(Day8Item item1, Day8Item item2, bool[,] antinodeMap)
        {
            //Console.WriteLine($"Calc for {item1.X},{item1.Y} and {item2.X},{item2.Y}");
            antinodeMap[item1.X, item1.Y] = true;
            antinodeMap[item2.X, item2.Y] = true;

            var xDiff = Math.Abs(item1.X - item2.X);
            var yDiff = Math.Abs(item1.Y - item2.Y);

            Position firstAntinode = new Position(item1.X, item1.Y);
            Position secondAntinode = new Position(item2.X, item2.Y);

            var antinodesNotOut = true;

            while (antinodesNotOut)
            {
                if (item1.X < item2.X)
                {
                    firstAntinode.X = item1.X - xDiff;
                    secondAntinode.X = item2.X + xDiff;
                }
                if (item1.X > item2.X)
                {
                    firstAntinode.X = item1.X + xDiff;
                    secondAntinode.X = item2.X - xDiff;
                }
                if (item1.Y < item2.Y)
                {
                    firstAntinode.Y = item1.Y - yDiff;
                    secondAntinode.Y = item2.Y + yDiff;
                }
                if (item1.Y > item2.Y)
                {
                    firstAntinode.Y = item1.Y + yDiff;
                    secondAntinode.Y = item2.Y - yDiff;
                }

                var firstAntinodeNotOut = firstAntinode.X >= 0 && firstAntinode.X < antinodeMap.GetLength(0) && firstAntinode.Y >= 0 && firstAntinode.Y < antinodeMap.GetLength(1);
                if (firstAntinodeNotOut)
                {
                    antinodeMap[firstAntinode.X, firstAntinode.Y] = true;
                    //Console.WriteLine($"Antinode {firstAntinode.X},{firstAntinode.Y}");
                }

                var secondAntinodeNotOut = secondAntinode.X >= 0 && secondAntinode.X < antinodeMap.GetLength(0) && secondAntinode.Y >= 0 && secondAntinode.Y < antinodeMap.GetLength(1);
                if (secondAntinodeNotOut)
                {
                    antinodeMap[secondAntinode.X, secondAntinode.Y] = true;
                    //Console.WriteLine($"Antinode {secondAntinode.X},{secondAntinode.Y}");
                }

                item1 = new() { X = firstAntinode.X, Y = firstAntinode.Y };
                item2 = new() { X = secondAntinode.X, Y = secondAntinode.Y };

                antinodesNotOut = firstAntinodeNotOut || secondAntinodeNotOut;
            }
        }
    }
}
