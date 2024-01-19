namespace NQueensAStar
{
    public class Program
    {
        static void Main()
        {
            int n = 5;
            SolveNQueens(n);
        }

        static void SolveNQueens(int n)
        {
            int[] queens = new int[n];
            AStarSearch(queens, n);
        }

        static void AStarSearch(int[] queens, int n)
        {
            PriorityQueue<Cell> priorityQueue = new PriorityQueue<Cell>();

            Cell root = new Cell(queens, 0, CalculateHeuristic(queens));

            priorityQueue.Enqueue(root);

            while (priorityQueue.Count > 0)
            {
                priorityQueue.Print();

                Cell current = priorityQueue.Dequeue();

                //Check admissibility for the current node
                int heuristic = CalculateHeuristic(current.State);
                IsAdmissibleHeuristic(current.State, heuristic);

                if (current.Level == n)
                {
                    PrintQueens(current.State);
                    return;
                }

                for (int i = 0; i < n; i++)
                {
                    int[] childState = (int[])current.State.Clone();
                    childState[current.Level] = i;

                    if (CanPlace(childState, current.Level))
                    {
                        Cell child = new Cell(childState, current.Level + 1, CalculateHeuristic(childState));
                        priorityQueue.Enqueue(child);
                    }
                }
            }

            Console.WriteLine("No solution found.");
        }

        static bool CanPlace(int[] queens, int level)
        {
            for (int i = 0; i < level; i++)
            {
                if (queens[i] == queens[level] || Math.Abs(queens[i] - queens[level]) == Math.Abs(i - level))
                {
                    return false;
                }
            }
            return true;
        }

        public static int CalculateHeuristic(int[] queens)
        {
            int heuristic = 0;

            for (int i = 0; i < queens.Length; i++)
            {
                for (int j = i + 1; j < queens.Length; j++)
                {
                    if (queens[i] == queens[j] || Math.Abs(queens[i] - queens[j]) == Math.Abs(i - j))
                    {
                        heuristic++;
                    }
                }
            }

            return heuristic;
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

        static bool IsAdmissibleHeuristic(int[] queens, int heuristic)
        {
            for (int i = 0; i < queens.Length; i++)
            {
                for (int j = i + 1; j < queens.Length; j++)
                {
                    if (queens[i] == queens[j] || Math.Abs(queens[i] - queens[j]) == Math.Abs(i - j))
                    {
                        int cost = Math.Abs(queens[i] - queens[j]) == Math.Abs(i - j) ? 1 : 0;

                        if (heuristic > cost + CalculateHeuristic(queens))
                        {
                            Console.WriteLine("\nHeuristic is not consistent!");
                            return false;
                        }
                    }
                }
            }

            Console.WriteLine("Heuristic is admissible!");
            return true;
        }
    }
}