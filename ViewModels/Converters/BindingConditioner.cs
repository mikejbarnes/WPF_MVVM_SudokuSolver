using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_MVVM_SudokuSolver.Models;

namespace WPF_MVVM_SudokuSolver.ViewModels.Converters
{
    class BindingConditioner
    {
        public Square Square { get; private set; }

        public BindingConditioner(object[] values)
        {
            Square[,] squares = (Square[,])values[0];
            int row = (int)values[1];
            int column = (int)values[2];

            Square = squares[row, column];
        }
    }
}
