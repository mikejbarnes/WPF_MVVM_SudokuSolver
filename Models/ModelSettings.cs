using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_MVVM_SudokuSolver.Models
{
    class ModelSettings
    {
        public const int Numbers = 9;
        public const int WidthSections = 3;
        public const int HeightSections = 3;
        public const int RowsPerSection = 3;
        public const int ColumnsPerSection = 3;
        public const int MaximumLoops = 1000;

        public enum ColorState
        {
            Normal,
            Changed,
            Target
        };
    }
}
