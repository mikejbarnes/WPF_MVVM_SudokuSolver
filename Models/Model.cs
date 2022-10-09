using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_MVVM_SudokuSolver.ViewModels;

namespace WPF_MVVM_SudokuSolver.Models
{
    class Model
    {
        private string[,][] _puzzleValues;
        private string[,][][] _possibleValues;

        public Model()
        {
            InitializePuzzleValues();
            InitializePossibleValues();
        }
    
        public DisplayPackage CreateDisplayPackage()
        {
            return new DisplayPackage(_puzzleValues, _possibleValues);
        }

        private void InitializePuzzleValues()
        {
            string[,][] newGrid = new string[ViewSettings.Numbers, ViewSettings.Numbers][];

            for(int i = 0; i < ViewSettings.Numbers; i++)
            {
                for(int j = 0; j < ViewSettings.Numbers; j++)
                {
                    newGrid[i, j] = new string[] { (i+1).ToString(), "Black" };
                }
            }

            _puzzleValues = newGrid;
        }

        private void InitializePossibleValues()
        {
            string[,][][] newGrid = new string[ViewSettings.Numbers, ViewSettings.Numbers][][];

            for (int i = 0; i < ViewSettings.Numbers; i++)
            {
                for (int j = 0; j < ViewSettings.Numbers; j++)
                {
                    string[] newValues = new string[ViewSettings.Numbers];
                    for(int k = 0; k < ViewSettings.Numbers; k++)
                    {
                        newValues[k] = (k + 1).ToString();
                    }

                    string[] newColors = new string[ViewSettings.Numbers];
                    for (int k = 0; k < ViewSettings.Numbers; k++)
                    {
                        newColors[k] = "Black";
                    }

                    string[][] newCell = new string[][] { newValues, newColors };
                    newGrid[i, j] = newCell;
                }
            }

            _possibleValues = newGrid;
        }
    }
}
