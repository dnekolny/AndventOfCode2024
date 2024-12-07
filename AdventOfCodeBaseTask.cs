namespace AndventOfCode2024
{
    public abstract class AdventOfCodeBaseTask<T> : IAdventOfCodeTask
    {
        protected abstract string InputFilePath { get; }
        protected T Data { get; set; }

        public void RunTask()
        {
            LoadInputDataFromFile();
            ProcessData(Data);
        }

        private void LoadInputDataFromFile()
        {
            var rawData = File.ReadAllText(InputFilePath);
            Data = ParseData(rawData);
        }

        protected abstract T ParseData(string rawData);

        protected abstract void ProcessData(T data);

        /* HELPERS */
        protected bool IsCharNumber(char character)
        {
            return character >= '0' && character <= '9';
        }

        protected string[] SplitDataToLines(string rawData)
        {
            if (rawData.Contains("\r\n"))
            {
                return rawData.Split("\r\n");
            }
            return rawData.Split("\n");
        }
    }
}
