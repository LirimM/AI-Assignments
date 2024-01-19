using SudokuCSP.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuCSP.Helpers
{
    public static class Extensions
    {
        public static void AddWithUpdate(this List<Variable> variables, Variable variable)
        {
            variables.Where(x => x.Value == 0).ToList().ForEach(e =>
            {
                if (e.Row > variable.Row) return;
                
                if (e.Row == variable.Row || e.Col == variable.Col)
                {
                    e.Domain.Values.Remove(variable.Value);
                }
                
                if (e.Col == variable.Col && e.Row == variable.Row)
                {
                    variables.Remove(e);
                    variables.Add(variable);
                }
            });
        }

        public static void RemoveWithUpdate(this List<Variable> variables, Variable variable)
        {
            variables.Where(x => x.Value == 0).ToList().ForEach(e =>
            {
                if (e.Row > variable.Row) return;

                if (e.Row == variable.Row || e.Col == variable.Col)
                {
                    e.Domain.Values.Add(variable.Value);
                }
               
                if (e.Col == variable.Col && e.Row == variable.Row)
                {
                    variables.Remove(e);
                    variables.Add(new Variable(variable.Row, variable.Col, 0));
                }
            });
        }
    }
}
