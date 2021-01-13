using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableOfEquations.Table
{
    public class Cell
    {
        private string equation;
        private string value;
        private CellState state;
        public string Equation { get { return equation; } }
        public string Value { get { return value; } }
        public CellState State { get { return state; } }

        public Cell()
        {
            equation = string.Empty;
            value = string.Empty;
            state = CellState.Updated;
        }

        public Cell(string equation)
        {
            this.equation = equation;
            value = string.Empty;
            state = CellState.Updated;
        }

        public enum CellState
        {
            Validated,
            Updated
        }
    }

    

}
