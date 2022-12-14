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
            BindingConditioner puzzleSquare = new BindingConditioner(values);

            string outputString = puzzleSquare.Square.PuzzleValue.ToString();

            return outputString.Equals("0") ? "_" : outputString;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
