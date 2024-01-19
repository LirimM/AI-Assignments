using SudokuCSP.Elements;

namespace SudokuCSP.Interfaces
{
    public interface IConstraint
    {
        bool IsSatisfied(int[,] board, Variable variable);
    }
}
