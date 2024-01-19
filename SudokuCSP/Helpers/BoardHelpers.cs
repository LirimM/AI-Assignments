using static SudokuCSP.Helpers.Constants;

namespace SudokuCSP.Helpers
{
    public static class BoardHelpers
    {
        private static void PrintBoardHelper(int[,] board, int milliSecondsDelay = 0)
        {
            for (int row = 0; row < gridSize; row++)
            {
                if (row % 3 == 0)
                {
                    Console.WriteLine(rowDelimiter);
                    Console.Write(startColumnDelimiter);
                }
                else Console.Write(startColumnDelimiter);

                for (int column = 0; column < gridSize; column++)
                {
                    if (column % 3 == 0 && column != 0)
                    {
                        Console.Write(columnDelimiter);
                    }
                    else
                    {
                        Console.Write(numbersDelimiter);
                    }
                    Thread.Sleep(milliSecondsDelay);
                    if (board[row, column] == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write('X');
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else Console.Write(board[row, column]);
                }
                Console.Write(endColumnDelimiter);
                Console.WriteLine();
            }

            Console.WriteLine(rowDelimiter);
        }

        public static void PrintBoard(int[,] board)
        {
            PrintBoardHelper(board);
            Console.WriteLine();
        }
    }
}
