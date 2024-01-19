namespace NQueensAStar
{
    class Cell : IComparable<Cell>
    {
        public int[] State { get; }
        public int Level { get; }
        public int Heuristic { get; }

        public Cell(int[] state, int level, int heuristic)
        {
            State = state;
            Level = level;
            Heuristic = heuristic;
        }

        public int CompareTo(Cell other)
        {
            return (Level + Heuristic).CompareTo(other.Level + other.Heuristic);
        }
    }
}
