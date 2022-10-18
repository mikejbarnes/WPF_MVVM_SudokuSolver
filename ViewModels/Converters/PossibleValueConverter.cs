using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WPF_MVVM_SudokuSolver.ViewModels.Converters
{
    class PossibleValueConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            BindingConditioner possibleSquare = new BindingConditioner(values);
            int index = (int)values[3];

            string outputString = possibleSquare.Square.PossibleValues[index].ToString();

            return outputString.Equals("0") ? "_" : outputString;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
