using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_MVVM_SudokuSolver.Models;

namespace WPF_MVVM_SudokuSolver.ViewModels
{
    class InputTextConditioner
    {
        public static bool ConditionInputText(string inputText, out string conditionedText)
        {
            conditionedText = inputText.Replace("\n", "").Replace("\r", "");

            if (conditionedText.Length == ModelSettings.Numbers * ModelSettings.Numbers)
            {
                return true;
            }
            return false;
        }
    }
}
