using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_MVVM_SudokuSolver.Models
{
    class DisplayPackage
    {
        public string[,][] PuzzleValues { get; set; }
        public string[,][][] PossibleValues { get; set; }
    }
}
