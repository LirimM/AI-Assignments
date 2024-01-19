namespace SudokuCSP.Helpers
{
    public static class SudokuReader
    {
        public static bool TryReadSudokusFromFile(string filePath, int rowNumber, out int[,] boards)
        {
            rowNumber = rowNumber - 1;

            boards = null!;

            try
            {
                // Read all lines from the CSV file
                string[] lines = File.ReadAllLines(filePath);

                // Check if the requested row number is within the valid range
                if (rowNumber < 0 || rowNumber >= lines.Length)
                {
                    Console.WriteLine($"Row number {rowNumber} is out of range.");
                    return false;
                }

                // Get the specified row
                string row = lines[rowNumber];

                // Split the row into individual numbers
                string[] values = row.Split(',');

                // Create a 2D array to store the Sudoku board
                boards = new int[9, 9];

                // Parse the values into the Sudoku board
                for (int i = 0; i < values.Length; i++)
                {
                    int value;
                    if (int.TryParse(values[i], out value))
                    {
                        boards[i / 9, i % 9] = value;
                    }
                    else
                    {
                        Console.WriteLine($"Error parsing value at position {i} in row {rowNumber}.");
                        return false;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }

        internal static bool TryReadSudokusFromFile(string filePath, object value, out int[,] sudoku)
        {
            throw new NotImplementedException();
        }
    }
}
