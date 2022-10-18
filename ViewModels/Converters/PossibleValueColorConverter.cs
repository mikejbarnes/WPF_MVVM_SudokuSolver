using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using WPF_MVVM_SudokuSolver.Models;

namespace WPF_MVVM_SudokuSolver.ViewModels.Converters
{
    class PossibleValueColorConverter: IMultiValueConverter
    {
public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            BindingConditioner possibleSquareColor = new BindingConditioner(values);
            int index = (int)values[3];

            Brush color;

            switch (possibleSquareColor.Square.PossibleValueColors[index])
            {
                case ModelSettings.ColorState.Normal:
                    color = ViewSettings.PossibleValueColor;
                    break;
                case ModelSettings.ColorState.Changed:
                    color = ViewSettings.ChangedValueColor;
                    break;
                default:
                    color = ViewSettings.ErrorValueColor;
                    break;
            }

            return color;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
