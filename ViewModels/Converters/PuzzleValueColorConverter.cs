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
    class PuzzleValueColorConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            BindingConditioner puzzleSquare = new BindingConditioner(values);

            Brush color;
            switch (puzzleSquare.Square.PuzzleValueColor)
            {
                case ModelSettings.ColorState.Normal:
                    color = ViewSettings.PuzzleValueColor;
                    break;
                case ModelSettings.ColorState.Changed:
                    color = ViewSettings.ChangedValueColor;
                    break;
                case ModelSettings.ColorState.Target:
                    color = ViewSettings.TargetValueColor;
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
