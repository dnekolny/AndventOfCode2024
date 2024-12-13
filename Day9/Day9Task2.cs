namespace AndventOfCode2024.Day9
{
    public class Day9Task2 : AdventOfCodeBaseTask<List<MemoryBlock2>>
    {
        protected override string InputFilePath => "Day9/input.txt";
        //protected override string InputFilePath => "Day9/input_small.txt";

        protected override List<MemoryBlock2> ParseData(string rawData)
        {
            List<MemoryBlock2> memoryBlocks = [];
            var charIndex = 0;
            var fileId = 0;

            foreach (var character in rawData)
            {
                if (!int.TryParse(character.ToString(), out var count))
                {
                    continue;
                }

                var isFile = charIndex % 2 != 1;

                memoryBlocks.Add(new MemoryBlock2(isFile ? fileId : -1, count, !isFile));

                if (isFile)
                {
                    fileId++;
                }
                charIndex++;
            }
            return memoryBlocks;
        }

        protected override void ProcessData(List<MemoryBlock2> data)
        {
            for (int currentBlockIndex = data.Count - 1; currentBlockIndex >= 0; currentBlockIndex--)
            {
                var currentBlock = data[currentBlockIndex];

                if (currentBlock.IsFreeSpace || currentBlock.Moved)
                {
                    continue;
                }

                for (int freeSpaceIndex = 0; freeSpaceIndex < data.Count && freeSpaceIndex < currentBlockIndex; freeSpaceIndex++)
                {
                    var freeSpaceBlock = data[freeSpaceIndex];

                    if (!freeSpaceBlock.IsFreeSpace || freeSpaceBlock.Length < currentBlock.Length)
                    {
                        continue;
                    }

                    if (freeSpaceBlock.Length == currentBlock.Length)
                    {
                        data[freeSpaceIndex] = currentBlock;
                        data[currentBlockIndex] = freeSpaceBlock;
                    }
                    else if (freeSpaceBlock.Length >= currentBlock.Length)
                    {
                        freeSpaceBlock.Length -= currentBlock.Length;
                        data.Remove(currentBlock);
                        data.Insert(freeSpaceIndex, currentBlock);
                        data.Insert(currentBlockIndex, new MemoryBlock2(-1, currentBlock.Length, true));
                        currentBlockIndex++;
                    }

                    currentBlock.Moved = true;
                    break;
                }
            }

            Console.WriteLine($"Checksum: {CountChecksum()}");
        }

        public long CountChecksum()
        {
            long checksum = 0;
            int index = 0;

            foreach (var currentBlock in Data)
            {
                if (currentBlock.IsFreeSpace)
                {
                    index += currentBlock.Length;
                    continue;
                }

                for (int i = 0; i < currentBlock.Length; i++)
                {
                    checksum += index * currentBlock.Id;
                    index++;
                }

            }

            return checksum;
        }
    }

    public class MemoryBlock2
    {
        public int Id { get; set; }
        public int Length { get; set; }
        public bool IsFreeSpace { get; set; }
        public bool Moved { get; set; }

        public MemoryBlock2(int id, int length, bool isFreeSpace)
        {
            Id = id;
            Length = length;
            IsFreeSpace = isFreeSpace;
            Moved = false;
        }
    }
}
