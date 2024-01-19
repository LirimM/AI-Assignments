namespace SudokuCSP.Helpers
{
    public class Constants
    {
        public static readonly int gridSize = 9;

        public static readonly char emptySpace = ' ';
        public static readonly char dash = '-';
        public static readonly char verticalBar = '|';

        public static readonly string defaultSpace = new string(emptySpace, 2);
        public static readonly string rowDelimiter = emptySpace + new string(dash, 35);
        public static readonly string columnDelimiter = defaultSpace + verticalBar + defaultSpace;
        public static readonly string startColumnDelimiter = verticalBar.ToString();
        public static readonly string endColumnDelimiter = defaultSpace + verticalBar;
        public static readonly string numbersDelimiter = defaultSpace;
    }
}
