using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WPF_MVVM_SudokuSolver.Models;

namespace WPF_MVVM_SudokuSolver.ViewModels
{
    class ViewModel: INotifyPropertyChanged
    {
        private string[,][] _puzzleValues;
        private string[,][][] _possibleValues;
        private Model _model;
        private ViewBuilder _viewBuilder;

        public string[,][] PuzzleValues
        {
            get { return _puzzleValues;  }
            set
            {
                _puzzleValues = value;
            }
        }

        public string[,][][] PossibleValues
        {
            get { return _possibleValues; }
            set
            {
                _possibleValues = value;
            }
        }

        public ViewModel(Grid rootGrid)
        {
            _model = new Model();
            UpdateValues();
            _viewBuilder = new ViewBuilder(rootGrid, this);

            
        }

        private void UpdateValues()
        {
            DisplayPackage package = _model.CreateDisplayPackage();

            PuzzleValues = package.PuzzleValues;
            PossibleValues = package.PossibleValues;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyRaised([CallerMemberName] string propertyName = null)
        {
            if(PropertyChanged != null && propertyName != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
