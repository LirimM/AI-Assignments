namespace NQueensAStar
{
    class PriorityQueue<T> where T : IComparable<T>
    {
        private List<Cell> elements = new List<Cell>();

        public int Count => elements.Count;

        public void Enqueue(Cell item)
        {
            elements.Add(item);
            elements.Sort();
        }

        public Cell Dequeue()
        {
            if (elements.Count == 0)
            {
                throw new InvalidOperationException("PriorityQueue is empty.");
            }

            Cell item = elements[0];
            elements.RemoveAt(0);
            return item;
        }
        public static void PrintQueens(int[] queens)
        {
            Console.WriteLine("Solution:");
            for (int i = 0; i < queens.Length; i++)
            {
                for (int j = 0; j < queens.Length; j++)
                {
                    Console.Write(queens[i] == j ? "Q " : ". ");
                }
                Console.WriteLine();
            }
        }

        public void Print()
        {
            Console.WriteLine("\n\nStart");

            foreach (var item in elements)
            {
                Console.WriteLine("\nLevel " + item.Level);

                Console.WriteLine("Heuristic " + item.Heuristic);

                foreach (var itm in item.State)
                {
                    Console.Write(itm + ", ");

                }
                Console.WriteLine();

                PrintQueens(item!.State);
            }
        }

    }
}