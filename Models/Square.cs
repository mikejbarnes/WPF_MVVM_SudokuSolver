using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WPF_MVVM_SudokuSolver.Models
{
    class Square
    {
        private char _puzzleValue = '0';
        private StringBuilder _possibleValues;
        private StringBuilder _possibleUniqueValues;

        public int Row { get; private set; }
        public int Column { get; private set; }
        public int Section { get; private set; }

        public char PuzzleValue 
        {
            get { return _puzzleValue; }
            set
            {
                _puzzleValue = value;
                SetOnlyPossibleValue(value);
            }
        }
        public ModelSettings.ColorState PuzzleValueColor { get; private set; }
        public StringBuilder PossibleValues 
        {   get { return _possibleValues; } 
            private set { _possibleValues = value; } 
        }
        public ModelSettings.ColorState[] PossibleValueColors { get; private set; }

        public Square(int row, int column)
        {
            Row = row;
            Column = column;
            CalculateSection();
            ResetSquare();
        }

        private void CalculateSection()
        {
            Section = ModelSettings.WidthSections * (Row / ModelSettings.HeightSections) + Column / ModelSettings.WidthSections;
        }

        public void ResetSquare()
        {
            PuzzleValue = '0';
            PuzzleValueColor = ModelSettings.ColorState.Normal;
            InitializePossibleValues(ref _possibleValues);
            InitializePossibleValueColors();
        }

        private void InitializePossibleValues(ref StringBuilder possibleValues)
        {
            possibleValues = new StringBuilder(ModelSettings.Numbers);

            for(int i = 0; i < ModelSettings.Numbers; i++)
            {
                possibleValues.Append(i + 1);   
            }
        }

        public void InitializePossibleUniqueValues()
        {
            InitializePossibleValues(ref _possibleUniqueValues);
        }

        private void InitializePossibleValueColors()
        {
            PossibleValueColors = new ModelSettings.ColorState[ModelSettings.Numbers];

            for (int i = 0; i < ModelSettings.Numbers; i++)
            {
                PossibleValueColors[i] = ModelSettings.ColorState.Normal;
            }
        }

        private void SetOnlyPossibleValue(char value)
        {
            if (!value.Equals('0'))
            {
                PuzzleValueColor = ModelSettings.ColorState.Changed;

                for (int i = 0; i < PossibleValues.Length; i++)
                {
                    if (!PossibleValues[i].Equals(value))
                    {
                        PossibleValues.Replace(PossibleValues[i], '0');
                        PossibleValueColors[i] = ModelSettings.ColorState.Changed;
                    }
                }
            }
        }

        public void ResetColors()
        {
            PuzzleValueColor = ModelSettings.ColorState.Normal;

            for(int i = 0; i < ModelSettings.Numbers; i++)
            {
                PossibleValueColors[i] = ModelSettings.ColorState.Normal;
            }
        }

        public bool RemovePossibleValues(char value)
        {
            bool valuesHaveChanged = false;

            for (int i = 0; i < PossibleValues.Length; i++)
            {
                if (!value.Equals('0') && PossibleValues[i].Equals(value))
                {
                    PossibleValues.Replace(value, '0');
                    PossibleValueColors[i] = ModelSettings.ColorState.Changed;
                    valuesHaveChanged = true;
                }
            }

            valuesHaveChanged = CheckForSoloValue(_possibleValues) || valuesHaveChanged;

            return valuesHaveChanged;
        }

        public void RemovePossibleUniqueValues(StringBuilder otherSquarePossibleValues)
        {
            for(int i = 0; i < _possibleUniqueValues.Length; i++)
            {
                if(!otherSquarePossibleValues[i].Equals('0'))
                {
                    _possibleUniqueValues.Replace(otherSquarePossibleValues[i], '0');   
                }
            }
        }

        private bool CheckForSoloValue(StringBuilder possibleValues)
        {
            bool valuesHaveChanged = false;

            StringBuilder possibleValuesRemaining = new StringBuilder();

            for(int i = 0; i < possibleValues.Length; i++)
            {
                if(!possibleValues[i].Equals('0'))
                {
                    possibleValuesRemaining.Append(possibleValues[i]);
                }
            }

            if(possibleValuesRemaining.Length == 1)
            {
                PuzzleValue = possibleValuesRemaining[0];
                valuesHaveChanged = true;
            }

            return valuesHaveChanged;
        }

        public bool CheckForOnlyRemainingPossibleValue()
        {
            return CheckForSoloValue(_possibleValues);
        }

        public bool CheckForUniqueValue()
        {
            return CheckForSoloValue(_possibleUniqueValues);
        }

        public void SetSquareAsTarget()
        {
            PuzzleValueColor = ModelSettings.ColorState.Target;
        }
    }
}
