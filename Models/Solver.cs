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
            if(!CheckIfPuzzledIsSolved())
            {
                _canSolveNextStep = true;

                SolvePuzzle();
            }
        }

        public void SolveAll()
        {
            if (!CheckIfPuzzledIsSolved())
            {
                _canSolveAll = true;

                SolvePuzzle();
            }
        }

        public void ResetSolver()
        {
            _currentSquare = 0;
            _loops = 0;
        }

        public void SolvePuzzle()
        {
            while(_canSolveNextStep || _canSolveAll)
            {
                _model.ResetColors();

                int row = _currentSquare / ModelSettings.Numbers;
                int column = _currentSquare % ModelSettings.Numbers;

                Square targetSquare = _model.Squares[row, column];

                targetSquare.SetSquareAsTarget();

                //Keeps track of whether any changes have occurred in any squares so it can automatically cycle through squares
                //until a significant change happens instead of manually clicking through each iteration where nothing is happening
                bool squaresHaveChanged = false;

                if (targetSquare.PuzzleValue.Equals('0'))
                {
                    squaresHaveChanged = targetSquare.CheckForOnlyRemainingPossibleValue() || squaresHaveChanged;
                }

                if (!targetSquare.PuzzleValue.Equals('0'))
                {
                    squaresHaveChanged = UpdatePossibleValuesInRow(targetSquare) || squaresHaveChanged;
                    squaresHaveChanged = UpdatePossibleValuesInColumn(targetSquare) || squaresHaveChanged;
                    squaresHaveChanged = UpdatePossibleValuesInSection(targetSquare) || squaresHaveChanged;
                }

                if (targetSquare.PuzzleValue.Equals('0'))
                {
                    squaresHaveChanged = CheckRowForUniqueValue(targetSquare) || squaresHaveChanged;
                    squaresHaveChanged = CheckColumnForUniqueValue(targetSquare) || squaresHaveChanged;
                    squaresHaveChanged = CheckSectionForUniqueValue(targetSquare) || squaresHaveChanged;
                }

                IterateCurrentSquare();

                if (CheckIfPuzzledIsSolved() || _loops >= ModelSettings.MaximumLoops)
                {
                    _canSolveAll = false;
                    _model.ResetColors();
                }
                else if(!squaresHaveChanged)
                {
                    continue;    
                }
                
                _canSolveNextStep = false;
            }
        }

        private void IterateCurrentSquare()
        {
            _currentSquare = (_currentSquare + 1) % (ModelSettings.Numbers * ModelSettings.Numbers);
            _loops += _currentSquare/(ModelSettings.Numbers * ModelSettings.Numbers - 1);
        }

        private bool UpdatePossibleValuesInRow(Square square)
        {
            bool valuesHaveChanged = false;

            for (int column = 0; column < ModelSettings.Numbers; column++)
            {
                if (_model.Squares[square.Row, column].PuzzleValue.Equals('0') && column != square.Column)
                {
                    valuesHaveChanged = _model.Squares[square.Row, column].RemovePossibleValues(square.PuzzleValue) || valuesHaveChanged;
                }
            }

            return valuesHaveChanged;
        }

        private bool UpdatePossibleValuesInColumn(Square square)
        {
            bool valuesHaveChanged = false;

            for(int row = 0; row < ModelSettings.Numbers; row++)
            {
                if(_model.Squares[row, square.Column].PuzzleValue.Equals('0') && row != square.Row)
                {
                    valuesHaveChanged = _model.Squares[row, square.Column].RemovePossibleValues(square.PuzzleValue) || valuesHaveChanged;
                }
            }

            return valuesHaveChanged;
        }

        private bool UpdatePossibleValuesInSection(Square square)
        {
            bool valuesHaveChanged = false;

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
                        valuesHaveChanged = _model.Squares[row, column].RemovePossibleValues(square.PuzzleValue) || valuesHaveChanged;
                    }
                }
            }

            return valuesHaveChanged;
        }

        private bool CheckRowForUniqueValue(Square square)
        {
            square.InitializePossibleUniqueValues();

            for (int column = 0; column < ModelSettings.Numbers; column++)
            {
                if (column != square.Column)
                {
                    square.RemovePossibleUniqueValues(_model.Squares[square.Row, column].PossibleValues);
                }
            }

            return square .CheckForUniqueValue();
        }

        private bool CheckColumnForUniqueValue(Square square)
        {
            square.InitializePossibleUniqueValues();

            for (int row = 0; row < ModelSettings.Numbers; row++)
            {
                if (row != square.Row)
                {
                    square.RemovePossibleUniqueValues(_model.Squares[row, square.Column].PossibleValues);
                }
            }

            return square.CheckForUniqueValue();
        }

        private bool CheckSectionForUniqueValue(Square square)
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
                    if (square.Row != row || square.Column != column)
                    {
                        square.RemovePossibleUniqueValues(_model.Squares[row, column].PossibleValues);
                    }
                }
            }

            return square.CheckForUniqueValue();
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
