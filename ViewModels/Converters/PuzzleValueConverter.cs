using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WPF_MVVM_SudokuSolver.ViewModels.Converters
{
    class PuzzleValueConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string[,][] puzzleValue = (string[,][])values[0];
            int row = (int)values[1];
            int column = (int)values[2];
            Console.WriteLine("{0} {1} {2} {3}", row, column, puzzleValue.GetLength(0), puzzleValue.GetLength(1));
            return puzzleValue[row, column][0];
            //return "24";
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
