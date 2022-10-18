using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_MVVM_SudokuSolver.Models
{
    class Solver
    {
        private Model _model;
        private bool _canSolveNextStep;
        private bool _canSolveAll;
        private int _currentSquare = 0;
        private int _loops = 0;

        public Solver(Model model)
        {
            _model = model;
        }

        public void SolveNextStep()
        {
            _canSolveNextStep = true;

            SolvePuzzle();
        }

        public void SolveAll()
        {
            _canSolveAll = true;

            SolvePuzzle();
        }

        public void ResetSolver()
        {
            _currentSquare = 0;
            _loops = 0;
        }

        public void SolvePuzzle()
        {
            _model.ResetColors();

            while(_canSolveNextStep || _canSolveAll)
            {
                int row = _currentSquare / ModelSettings.Numbers;
                int column = _currentSquare % ModelSettings.Numbers;

                Square targetSquare = _model.Squares[row, column];

                targetSquare.SetSquareAsTarget();

                if (targetSquare.PuzzleValue.Equals('0'))
                {
                    targetSquare.CheckForOnlyRemainingPossibleValue();
                }

                if (!targetSquare.PuzzleValue.Equals('0'))
                {
                    UpdatePossibleValuesInRow(targetSquare);
                    UpdatePossibleValuesInColumn(targetSquare);
                    UpdatePossibleValuesInSection(targetSquare);
                }

                if (targetSquare.PuzzleValue.Equals('0'))
                {
                    CheckRowForUniqueValue(targetSquare);
                    CheckColumnForUniqueValue(targetSquare);
                    CheckSectionForUniqueValue(targetSquare);
                }


                IterateCurrentSquare();
                _canSolveNextStep = false;
                
                if(_loops >= ModelSettings.MaximumLoops || CheckIfPuzzledIsSolved())
                {
                    _canSolveAll = false;
                }
            }
        }

        private void IterateCurrentSquare()
        {
            _currentSquare = (_currentSquare + 1) % (ModelSettings.Numbers * ModelSettings.Numbers);
            _loops++;
        }

        private void UpdatePossibleValuesInRow(Square square)
        {
            for (int column = 0; column < ModelSettings.Numbers; column++)
            {
                if (_model.Squares[square.Row, column].PuzzleValue.Equals('0') && column != square.Column)
                {
                    _model.Squares[square.Row, column].RemovePossibleValues(square.PuzzleValue);
                }
            }
        }

        private void UpdatePossibleValuesInColumn(Square square)
        {
            for(int row = 0; row < ModelSettings.Numbers; row++)
            {
                if(_model.Squares[row, square.Column].PuzzleValue.Equals('0') && row != square.Row)
                {
                    _model.Squares[row, square.Column].RemovePossibleValues(square.PuzzleValue);
                }
            }
        }

        private void UpdatePossibleValuesInSection(Square square)
        {
            int minRow = (square.Section / ModelSettings.HeightSections) * ModelSettings.RowsPerSection;
            int maxRow = minRow + ModelSettings.RowsPerSection;
            int minColumn = (square.Section % ModelSettings.WidthSections) * ModelSettings.ColumnsPerSection;
            int maxColumn = minColumn + ModelSettings.ColumnsPerSection;

            for (int row = minRow; row < maxRow; row++)
            {
                for (int column = minColumn; column < maxColumn; column++)
                {
                    if (_model.Squares[row, column].PuzzleValue.Equals('0') && (square.Row != row || square.Column != column))
                    {
                        _model.Squares[row, column].RemovePossibleValues(square.PuzzleValue);
                    }
                }
            }
        }

        private void CheckRowForUniqueValue(Square square)
        {
            square.InitializePossibleUniqueValues();

            for (int column = 0; column < ModelSettings.Numbers; column++)
            {
                if (_model.Squares[square.Row, column].PuzzleValue.Equals('0') && column != square.Column)
                {
                    square.RemovePossibleUniqueValues(_model.Squares[square.Row, column].PossibleValues);
                }
            }

            square.CheckForUniqueValue();
        }

        private void CheckColumnForUniqueValue(Square square)
        {
            square.InitializePossibleUniqueValues();

            for (int row = 0; row < ModelSettings.Numbers; row++)
            {
                if (_model.Squares[row, square.Column].PuzzleValue.Equals('0') && row != square.Row)
                {
                    square.RemovePossibleUniqueValues(_model.Squares[row, square.Column].PossibleValues);
                }
            }

            square.CheckForUniqueValue();
        }

        private void CheckSectionForUniqueValue(Square square)
        {
            int minRow = (square.Section / ModelSettings.HeightSections) * ModelSettings.RowsPerSection;
            int maxRow = minRow + ModelSettings.RowsPerSection;
            int minColumn = (square.Section % ModelSettings.WidthSections) * ModelSettings.ColumnsPerSection;
            int maxColumn = minColumn + ModelSettings.ColumnsPerSection;

            square.InitializePossibleUniqueValues();

            for (int row = minRow; row < maxRow; row++)
            {
                for (int column = minColumn; column < maxColumn; column++)
                {
                    if (_model.Squares[row, column].PuzzleValue.Equals('0') && (square.Row != row || square.Column != column))
                    {
                        square.RemovePossibleUniqueValues(_model.Squares[row, column].PossibleValues);
                    }
                }
            }

            square.CheckForUniqueValue();
        }

        private bool CheckIfPuzzledIsSolved()
        {
            bool puzzleIsSolved = true;

            foreach(Square square in _model.Squares)
            {
                puzzleIsSolved = puzzleIsSolved && !square.PuzzleValue.Equals('0');
            }

            return puzzleIsSolved;
        }
    }
}
