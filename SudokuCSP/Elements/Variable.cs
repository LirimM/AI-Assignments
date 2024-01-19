namespace SudokuCSP.Elements
{
    public class Variable
    {
        public int Row { get; }
        public int Col { get; }
        public int Value { get; }
        public Domain Domain { get; set; }

        public Variable(int row, int col, int value)
        {
            Row = row;
            Col = col;
            Domain = new Domain(Enumerable.Range(1, 9).ToList());
            Value = value;
        }
    }
}
