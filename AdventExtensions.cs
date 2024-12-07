namespace AndventOfCode2024
{
    public static class AdventExtensions
    {
        public static void MoveByIdexis<T>(this List<T> list, int oldIndex, int newIndex)
        {
            if (oldIndex > -1 && oldIndex < list.Count)
            {
                var item = list[oldIndex];

                list.RemoveAt(oldIndex);

                if (newIndex > oldIndex) newIndex--;

                list.Insert(newIndex, item);
            }
        }

        public static void MoveBefore<T>(this List<T> list, T value, T newValue)
        {
            var oldIndex = list.IndexOf(value);
            var newIndex = list.IndexOf(newValue);

            if (oldIndex > -1 && newIndex > -1)
            {
                list.MoveByIdexis(oldIndex, newIndex);
            }
        }

        public static void MoveAfter<T>(this List<T> list, T value, T newValue)
        {
            var oldIndex = list.IndexOf(value);
            var newIndex = list.IndexOf(newValue);

            newIndex++;

            if (oldIndex > -1 && newIndex > -1)
            {
                list.MoveByIdexis(oldIndex, newIndex);
            }
        }
    }
}
