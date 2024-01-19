using Google.OrTools.Sat;

namespace SATProblem;

public class Program
{

    static void Main()
    {
        SolveSeatingProblem();
    }

    static void SolveSeatingProblem()
    {
        CpModel model = new CpModel();

        // Number of guests and tables
        int numGuests = 100;
        int numTables = 10;

        // Boolean variables for seating arrangements
        IntVar[][] x = new IntVar[numGuests][];

        for (int i = 0; i < numGuests; i++)
        {
            x[i] = new IntVar[numTables];
            for (int j = 0; j < numTables; j++)
            {
                x[i][j] = model.NewBoolVar($"x_{i}_{j}");
            }
        }

        // Constraints: Each guest must sit at exactly one table
        for (int i = 0; i < numGuests; i++)
        {
            model.Add(LinearExpr.Sum(x[i]) == 1);
        }

        // Constraints: Guests should be seated at tables with exactly ten seats
        for (int j = 0; j < numTables; j++)
        {
            model.Add(LinearExpr.Sum(x.Select(row => row[j])) == 10);
        }

        // Define pairs of guests who cannot sit together
        int[][] cannotSitTogetherPairs = { new int[] { 1, 5 }, new int[] { 2, 7 }, new int[] { 10, 15 } };

        foreach (int[] pair in cannotSitTogetherPairs)
        {
            int G1 = pair[0];
            int G5 = pair[1];

            for (int j = 0; j < numTables; j++)
            {
                ILiteral varG1 = (ILiteral)x[G1 - 1][j];
                ILiteral varG5 = (ILiteral)x[G5 - 1][j];
                model.AddBoolOr(new ILiteral[] { varG1.Not(), varG5.Not() });
            }
        }

        // Define pairs of guests who must sit together
        int[][] mustSitTogetherPairs = { new int[] { 3, 6 }, new int[] { 8, 12 }, new int[] { 20, 25 } };
        foreach (int[] pair in mustSitTogetherPairs)
        {
            int G3 = pair[0];
            int G6 = pair[1];

            for (int j = 0; j < numTables; j++)
            {
                x[G3 - 1][j] = model.NewBoolVar($"not_x[G3 - 1][j]");

                var impl1 = (ILiteral)x[G3 - 1][j];
                var impl2 = (ILiteral)x[G6 - 1][j];

                model.AddImplication(impl1, impl1.Not());
                model.AddImplication(impl2, impl1.Not());
            }
        }

        for (int i = 0; i < numGuests - 1; i++)
        {
            for (int j = 0; j < numTables; j++)
            {
                for (int k = j + 1; k < numTables; k++)
                {
                    // Use the Not method to represent negation
                    ILiteral var1 = (ILiteral)x[i][j];
                    ILiteral var2 = (ILiteral)x[i + 1][k];
                    ILiteral[] vars = { var1.Not(), var2.Not() };

                    model.AddBoolOr(vars).OnlyEnforceIf(vars);
                }
            }
        }

        CpSolver solver = new CpSolver();
        CpSolverStatus status = solver.Solve(model);

        if (status == CpSolverStatus.Optimal)
        {
            Console.WriteLine("Solution found:");

            for (int i = 0; i < numGuests; i++)
            {
                for (int j = 0; j < numTables; j++)
                {
                    if (solver.Value(x[i][j]) == 1)
                    {
                        Console.WriteLine($"Guest {i + 1} seated at Table {j + 1}");
                    }
                }
            }
        }
        else if (status == CpSolverStatus.Infeasible)
        {
            Console.WriteLine("No solution exists.");
        }
        else
        {
            Console.WriteLine("Solver did not find an optimal solution.");
        }
    }
}