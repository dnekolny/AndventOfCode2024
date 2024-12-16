namespace AndventOfCode2024.Day9
{
    public class Day9Task1 : AdventOfCodeBaseTask<List<MemoryBlock>>
    {
        protected override string InputFilePath => "Day9/input.txt";
        //protected override string InputFilePath => "Day9/input_small.txt";

        protected override List<MemoryBlock> ParseData(string rawData)
        {
            List<MemoryBlock> memoryBlocks = [];
            var charIndex = 0;
            var fileId = 0;

            foreach (var character in rawData)
            {
                if (!int.TryParse(character.ToString(), out var count))
                {
                    continue;
                }

                var isFile = charIndex % 2 != 1;

                for (var j = 0; j < count; j++)
                {
                    memoryBlocks.Add(new MemoryBlock(isFile ? fileId : -1, !isFile));
                }

                if (isFile)
                {
                    fileId++;
                }
                charIndex++;
            }
            return memoryBlocks;
        }

        protected override void ProcessData(List<MemoryBlock> data)
        {
            int frontBlockIndex = 0;
            int backBlockIndex = data.Count - 1;

            while (frontBlockIndex <= backBlockIndex)
            {
                var frontBlock = data[frontBlockIndex];
                var backBlock = data[backBlockIndex];

                if (frontBlock.IsFreeSpace && !backBlock.IsFreeSpace)
                {
                    data[frontBlockIndex] = backBlock;
                    data[backBlockIndex] = frontBlock;
                    (backBlock, frontBlock) = (frontBlock, backBlock);
                }

                if (backBlock.IsFreeSpace)
                {
                    backBlockIndex--;
                }

                if (!frontBlock.IsFreeSpace)
                {
                    frontBlockIndex++;
                }
            }

            Console.WriteLine($"Checksum: {CountChecksum()}");
        }

        public long CountChecksum()
        {
            long checksum = 0;

            for (int i = 0; i < Data.Count; i++)
            {
                var currentBlock = Data[i];

                if (currentBlock.IsFreeSpace)
                {
                    i = Data.Count;
                }
                else
                {
                    checksum += i * currentBlock.Id;
                }
            }
            return checksum;
        }
    }

    public record MemoryBlock(int Id, bool IsFreeSpace);
}
