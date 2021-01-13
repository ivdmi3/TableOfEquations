using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableOfEquations.Table
{
    public class Table
    {
        Cell[,] Cells;

        public Table()
        {
        }

        public Table(int rows, int columns)
        {
            Cells = new Cell[rows, columns];
        }

        public Table(Cell[,] content)
        {
            Cells = content;
        }

        public Table(DataTable dt)
        {
            Cells = new Cell[dt.Rows.Count, dt.Columns.Count];
            for(int x=0; x< dt.Rows.Count; x++)
                for (int y = 0; y < dt.Columns.Count; y++)
                        Cells[x, y] = dt.Rows[x].Equals(DBNull.Value) ? new Cell() : new Cell(dt.Rows[x][y].ToString());
        }               

        public Cell this[Tuple<int,int> address]{ get { return Cells[address.Item1, address.Item2]; } }
        public Cell this[int rowIndex, int ColumnIndex] { get { return Cells[rowIndex, ColumnIndex]; } }
        public int Count { get { return Cells.Length; } }


    }
}
