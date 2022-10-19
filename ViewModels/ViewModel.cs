using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WPF_MVVM_SudokuSolver.Models;
using WPF_MVVM_SudokuSolver.ViewModels.Commands;

namespace WPF_MVVM_SudokuSolver.ViewModels
{
    class ViewModel: INotifyPropertyChanged
    {
        private Model _model;
        private Solver _solver;
        private Square[,] _squares;
        private ViewBuilder _viewBuilder;
        private string _inputText;

        public Square[,] Squares
        {
            get { return _squares; }
            set
            {
                _squares = value;
                OnPropertyRaised();
            }
        }

        public string InputText 
        {
            get { return _inputText; } 
            set 
            {
                _inputText = value;
                OnPropertyRaised();
            } 
        }

        public RelayCommand LoadPuzzleCommand { get; set; }
        public RelayCommand SolveNextStepCommand { get; set; }
        public RelayCommand SolvePuzzleCommand { get; set; }

        public ViewModel(Grid rootGrid)
        {
            _model = new Model();
            _solver = new Solver(_model);
            _squares = new Square[ModelSettings.Numbers, ModelSettings.Numbers];
            UpdateValues();
            _viewBuilder = new ViewBuilder(rootGrid, this);

            LoadPuzzleCommand = new RelayCommand(LoadPuzzle, CanUseLoadPuzzle);
            SolveNextStepCommand = new RelayCommand(SolveNextStep, CanUseSolveNextStep);
            SolvePuzzleCommand = new RelayCommand(SolvePuzzle, CanUseSolvePuzzle);
        }

        private void UpdateValues()
        {
            Squares = _model.Squares;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyRaised([CallerMemberName] string propertyName = null)
        {
            if(PropertyChanged != null && propertyName != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #region LoadPuzzle()
        public void LoadPuzzle()
        {
            string conditionedText;
            bool inputTextIsOk = InputTextConditioner.ConditionInputText(_inputText, out conditionedText);
            if (inputTextIsOk)
            {
                _model.ResetSquares();
                _model.LoadPuzzle(conditionedText);
                _solver.ResetSolver();
                InputText = "";
                UpdateValues();
            }
            else
            {
                MessageBox.Show("Puzzle text not formatted correctly.");
            }
        }

        public bool CanUseLoadPuzzle()
        {
            return true;
        }
        #endregion

        #region SolveNextStep()
        public void SolveNextStep()
        {
            _solver.SolveNextStep();
            UpdateValues();
        }

        public bool CanUseSolveNextStep()
        {
            return true;
        }
        #endregion

        #region SolvePuzzle()
        public void SolvePuzzle()
        {
            _solver.SolveAll();
            UpdateValues();
        }

        public bool CanUseSolvePuzzle()
        {
            return true;
        }
        #endregion
    }
}
