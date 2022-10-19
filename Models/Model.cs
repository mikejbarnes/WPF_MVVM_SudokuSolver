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
        public Square[,] Squares;

        public Model()
        {
            InitializeSquares();
        }

        private void InitializeSquares()
        {
            Squares = new Square[ModelSettings.Numbers, ModelSettings.Numbers];

            for(int row = 0; row < ModelSettings.Numbers; row++)
            {
                for(int column = 0; column < ModelSettings.Numbers; column++)
                {
                    Squares[row, column] = new Square(row, column);
                }
            }
        }

        public void LoadPuzzle(string rawPuzzle)
        {
            int numberOfSquares = ModelSettings.Numbers * ModelSettings.Numbers;
            for(int i = 0; i < numberOfSquares; i++)
            {
                int row = i / ModelSettings.Numbers;
                int column = i % ModelSettings.Numbers;

                Squares[row, column].PuzzleValue = rawPuzzle[i];
            }
        }

        public void ResetColors()
        {
            for(int row = 0; row < ModelSettings.Numbers; row++)
            {
                for(int column = 0; column < ModelSettings.Numbers; column++)
                {
                    Squares[row, column].ResetColors();
                }
            }
        }

        public void ResetSquares()
        {
            for(int row = 0; row < ModelSettings.Numbers; row++)
            {
                for(int column = 0; column < ModelSettings.Numbers; column++)
                {
                    Squares[row, column].ResetSquare();
                }
            }
        }
    }
}
