using SudokuCSP.Interfaces;
using static SudokuCSP.Elements.Constraints;

namespace SudokuCSP.Elements
{
    public class Domain
    {
        public List<int> Values { get; }
        public List<IConstraint> Constraints { get; set; }

        public Domain(List<int> values)
        {
            Values = values;
            Constraints = new List<IConstraint> { new RowConstraint(), new ColumnConstraint(), new BoxConstraint() };
        }
    }
}
