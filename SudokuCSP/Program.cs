using SudokuCSP.Elements;
using SudokuCSP.Helpers;

namespace SudokuCSP
{
    public class Program
    {
        public static List<Variable> variables = new List<Variable>();
        public static void Main(string[] args)
        {
            string fileName = "sudokus.csv";
            string folder = "Data";
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), folder, fileName);

            Random rnd = new Random();
            int randomSudokuNr = rnd.Next(1, 60);

            if (SudokuReader.TryReadSudokusFromFile(filePath, randomSudokuNr, out int[,] sudoku))
            {
                Console.WriteLine($"Loaded sudoku nr: {randomSudokuNr}");

                variables = GetInitialVariables(sudoku);

                BoardHelpers.PrintBoard(sudoku);

                SolveDFS(sudoku);

                BoardHelpers.PrintBoard(sudoku);
            }
            else
            {
                Console.WriteLine($"No sudoku board found!");
            }
        }
        private static List<Variable> GetInitialVariables(int[,] board)
        {
            var variables = new List<Variable>();

            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    if (board[row, col] == 0)
                    {
                        variables.Add(new Variable(row, col, 0));
                    }
                }
            }

            return variables;
        }


        public static bool SolveDFS(int[,] board)
        {
            Variable emptyVar = CreateVariableFromEmptyCell(board);

            bool noEmptyCellFound = emptyVar.Row == -1 && emptyVar.Col == -1;
            bool allVariablesHaveValidPosition = variables.All(x => x.Domain.Values.Count == 9);

            // If no empty cell is found and all variables have a domain of all values (meaning they are all in a valid position) then the board is solved
            if (noEmptyCellFound && allVariablesHaveValidPosition)
            {
                return true;
            }

            for (int num = 1; num <= 9; num++)
            {
                emptyVar = new Variable(emptyVar.Row, emptyVar.Col, num);

                Variable varToPlaceValue = variables.First(x => x.Row == emptyVar.Row && x.Col == emptyVar.Col);

                if (varToPlaceValue.Domain.Values.Contains(num) && emptyVar.Domain.Constraints.All(e => e.IsSatisfied(board, emptyVar)))
                {
                    //add variable to position & update affected domains
                    variables.AddWithUpdate(emptyVar);

                    board[emptyVar.Row, emptyVar.Col] = num;

                    if (SolveDFS(board))
                    {
                        return true;
                    }

                    // If placing the number leads to an invalid solution
                    // Remove from variables and update affected domains & backtrack
                    variables.RemoveWithUpdate(emptyVar);
                    board[emptyVar.Row, emptyVar.Col] = 0;
                }
            }

            return false;
        }

        static Variable CreateVariableFromEmptyCell(int[,] board)
        {
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    if (board[row, col] == 0)
                    {
                        return new Variable(row, col, 0);
                    }
                }
            }

            // Generate an invalid variable when no empty cells are left
            return new Variable(-1, -1, 0);
        }
    }
}
