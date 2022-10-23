using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_MVVM_SudokuSolver.Models
{
    class UniqueValueTracker
    {
        private int[] _rowValueTracker;
        private int[] _columnValueTracker;

        public int[] RowValueTracker 
        { 
            get { return _rowValueTracker; } 
            private set { _rowValueTracker = value; } 
        }
        public int[] ColumnValueTracker 
        { 
            get { return _columnValueTracker; } 
            private set { _columnValueTracker = value; } 
        }

        public UniqueValueTracker()
        {
            RowValueTracker = CreateValueTrackerArray();
            ColumnValueTracker = CreateValueTrackerArray();
        }

        private int[] CreateValueTrackerArray()
        {
            int[] valueTrackerArray = new int[ModelSettings.Numbers];

            for(int i = 0; i < ModelSettings.Numbers; i++)
            {
                valueTrackerArray[i] = -1; //Start with a value that is not a row/column index within a section so that this value can store the row/column that contains the possible value
            }

            return valueTrackerArray;
        }
    
        public void TrackValues(Square square)
        {
            int rowWithinSection = square.Row - Model.SectionDimensions[square.Section].MinRow;
            int columnWithinSection = square.Column - Model.SectionDimensions[square.Section].MinColumn;

            for(int i = 0; i < square.PossibleValues.Length; i++)
            {
                if(!square.PossibleValues[i].Equals('0'))
                {
                    CompareValueWithTracker(ref _rowValueTracker, i, rowWithinSection);
                    CompareValueWithTracker(ref _columnValueTracker, i, columnWithinSection);
                }
            }
        }

        private void CompareValueWithTracker(ref int[] tracker, int trackerIndex, int squareIndex)
        {
            if(tracker[trackerIndex] == -1)
            {
                tracker[trackerIndex] = squareIndex;
            }
            else if(tracker[trackerIndex] != squareIndex)
            {
                tracker[trackerIndex] = -2; //Mark value as not unique
            }
        }
    }
}
