using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_MVVM_SudokuSolver.Models
{
    class SectionLimits
    {
        public int MinRow { get; private set; }
        public int MaxRow { get; private set; }
        public int MinColumn { get; private set; }

        public int MaxColumn { get; private set; }

        public SectionLimits(int section)
        {
            MinRow = (section / ModelSettings.HeightSections) * ModelSettings.RowsPerSection;
            MaxRow = MinRow + ModelSettings.RowsPerSection;
            MinColumn = (section % ModelSettings.WidthSections) * ModelSettings.ColumnsPerSection;
            MaxColumn = MinColumn + ModelSettings.ColumnsPerSection;
        }
    }
}
