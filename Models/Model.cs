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

        public static SectionLimits[] SectionDimensions { get; private set; }

        public Model()
        {
            InitializeSquares();
            CalculateSectionLimits();
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

        private void CalculateSectionLimits()
        {
            SectionDimensions = new SectionLimits[ModelSettings.WidthSections * ModelSettings.HeightSections];

            for(int i = 0; i < SectionDimensions.Length; i++)
            {
                SectionDimensions[i] = new SectionLimits(i);
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
