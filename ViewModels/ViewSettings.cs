using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_MVVM_SudokuSolver.ViewModels
{
    class ViewSettings
    {
        public const int Numbers = 9;
        public const int SubGridRows = 4;
        public const int SubGridColumns = 4;
        public const int SubRowColumns = 5;
        public const int SubRowPlacement = 3;
        public const int SubRowValues = 3;
        public const int ValueGridRowSpan = 2;
        public const int ValueGridColumnSpan = 2;
        public const int SquareSideLength = 90;
        public const int BorderThickness = 1;
        public const int SectionBorderThickness = 4;
        public const int ValueBlockFontSize = 30;

        public static readonly int[] BeforeSectionRowsAndColumns = new int[] { 2, 5 };
        public static readonly int[] AfterSectionRowAndColumns = new int[] { 3, 6 };
        public static readonly int[] ValuePosition = new int[] { 1, 1 };
        public static readonly int[,] PossibleValuePosition = new int[,] { { 2, 0 }, { 1, 0 }, { 0, 1 }, { 0, 2 }, { 1, 3 }, { 2, 3 }, { 0, 3 }, { 0, 2 }, { 0, 1 } };
    }
}
