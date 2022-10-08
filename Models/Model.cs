using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            DisplayPackage package = new DisplayPackage();

            package.PuzzleValues = _puzzleValues;
            package.PossibleValues = _possibleValues;

            return package;
        }

        private void InitializePuzzleValues()
        {
            string[,][] newGrid = new string[9, 9][];

            for(int i = 0; i < 9; i++)
            {
                for(int j = 0; j < 9; i++)
                {
                    newGrid[i, j] = new string[] { "0", "Black" };
                }
            }

            _puzzleValues = newGrid;
        }

        private void InitializePossibleValues()
        {
            string[,][][] newGrid = new string[9, 9][][];

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; i++)
                {
                    string[] newValues = new string[9];
                    for(int k = 0; k < 9; k++)
                    {
                        newValues[k] = (k + 1).ToString();
                    }

                    string[] newColors = new string[9];
                    for (int k = 0; k < 9; k++)
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
