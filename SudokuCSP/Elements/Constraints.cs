using SudokuCSP.Interfaces;

namespace SudokuCSP.Elements
{
    public class Constraints
    {
        public class RowConstraint : IConstraint
        {
            public bool IsSatisfied(int[,] board, Variable variable)
            {
                for (int i = 0; i < 9; i++)
                {
                    if (board[variable.Row, i] == variable.Value)
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        public class ColumnConstraint : IConstraint
        {
            public bool IsSatisfied(int[,] board, Variable variable)
            {
                for (int i = 0; i < 9; i++)
                {
                    if (board[i, variable.Col] == variable.Value)
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        public class BoxConstraint : IConstraint
        {
            public bool IsSatisfied(int[,] board, Variable variable)
            {
                int currentBoxRow = variable.Row - variable.Row % 3;
                int currentBoxColumn = variable.Col - variable.Col % 3;

                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (board[currentBoxRow + i, currentBoxColumn + j] == variable.Value)
                        {
                            return false;
                        }
                    }
                }

                return true;
            }
        }
    }
}
