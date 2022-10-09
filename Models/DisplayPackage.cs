using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_MVVM_SudokuSolver.Models
{
    class DisplayPackage
    {
        public string[,][] PuzzleValues { get; private set; }
        public string[,][][] PossibleValues { get; private set; }

        public DisplayPackage(string[,][] puzzleValues, string[,][][] possibleValues)
        {
            ConvertToDisplayPuzzleValues(puzzleValues);
            ConvertToDisplayPossibleValues(possibleValues);
        }

        public void ConvertToDisplayPuzzleValues(string[,][] puzzleValues)
        {
            int numbers = puzzleValues.GetLength(0);
            string[,][] newGrid = new string[numbers, numbers][];

            for(int i = 0; i < numbers; i++)
            {
                for(int j = 0; j < numbers; j++)
                {
                    string[] newValue = new string[2];

                    newValue[0] = puzzleValues[i, j][0].Equals("0") ? "_" : puzzleValues[i, j][0];

                    newValue[1] = puzzleValues[i, j][1];

                    newGrid[i, j] = newValue;
                }
            }

            PuzzleValues = newGrid;
        }

        public void ConvertToDisplayPossibleValues(string[,][][] possibleValues)
        {
            int numbers = possibleValues.GetLength(0);
            string[,][][] newGrid = new string[numbers, numbers][][];

            for (int i = 0; i < numbers; i++)
            {
                for (int j = 0; j < numbers; j++)
                {
                    string[] newValues = new string[numbers];
                    for (int k = 0; k < numbers; k++)
                    {
                        newValues[k] = possibleValues[i, j][0][k].Equals("0") ? "_" : possibleValues[i, j][0][k];
                    }

                    string[] newColors = new string[numbers];
                    for (int k = 0; k < numbers; k++)
                    {
                        newColors[k] = possibleValues[i, j][1][k];
                    }

                    string[][] newCell = new string[][] { newValues, newColors };
                    newGrid[i, j] = newCell;
                }
            }

            PossibleValues = newGrid;
        }
    }
}
