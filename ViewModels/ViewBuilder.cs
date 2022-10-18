using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using WPF_MVVM_SudokuSolver.Models;

namespace WPF_MVVM_SudokuSolver.ViewModels
{
    class ViewBuilder
    { 
        private ViewModel _dataContext;
        private Grid _rootGrid;
        private Grid _parentGrid;
        private int _gridSideLength;

        private StackPanel _loadingPanel;
        private TextBox _input;
        private Button _loadingButton;

        private StackPanel _solvePanel;
        private Button _solveButton;
        private Button _nextStepButton;

        public ViewBuilder(Grid rootGrid, ViewModel dataContext)
        {
            _dataContext = dataContext;
            _rootGrid = rootGrid;
            _gridSideLength = ModelSettings.Numbers * ViewSettings.SquareSideLength;

            BuildGrid();
            BuildTextInput();
            BuildSolveButtons();
        }

        private void BuildGrid()
        {
            InitializeParentGrid();
        }

        private void InitializeParentGrid()
        {
            _parentGrid = new Grid();
            _parentGrid.Width = _gridSideLength;
            _parentGrid.Height = _gridSideLength;

            Grid.SetColumn(_parentGrid, 1);

            for (int i = 0; i < ModelSettings.Numbers; i++)
            {
                RowDefinition rowDefinition = new RowDefinition();
                rowDefinition.Height = new GridLength(1.0, GridUnitType.Star);
                _parentGrid.RowDefinitions.Add(rowDefinition);

                ColumnDefinition columnDefinition = new ColumnDefinition();
                columnDefinition.Width = new GridLength(1.0, GridUnitType.Star);
                _parentGrid.ColumnDefinitions.Add(columnDefinition);
            }

            for(int row = 0; row < ModelSettings.Numbers; row++)
            {
                for(int column = 0; column < ModelSettings.Numbers; column++)
                {
                    SquareView newSquare = new SquareView(_parentGrid, row, column, _dataContext);
                }
            }

            _rootGrid.Children.Add(_parentGrid);
        }

        public void BuildTextInput()
        {
            _loadingPanel = new StackPanel();
            _loadingPanel.VerticalAlignment = VerticalAlignment.Center;
            Grid.SetColumn(_loadingPanel, ViewSettings.LoadingColumn);

            _input = new TextBox();
            _input.Width = ViewSettings.InputBoxWidth;
            _input.Height = ViewSettings.InputBoxHeight;
            _input.VerticalAlignment = VerticalAlignment.Center;
            _input.TextWrapping = TextWrapping.NoWrap;
            _input.AcceptsReturn = true;

            Binding inputTextBinding = new Binding("InputText");
            inputTextBinding.Mode = BindingMode.TwoWay;
            _input.SetBinding(TextBox.TextProperty, inputTextBinding);

            _loadingButton = new Button();
            _loadingButton.VerticalAlignment = VerticalAlignment.Center;
            _loadingButton.Content = "Load Puzzle";
            _loadingButton.Width = ViewSettings.LoadingButtonWidth;
            _loadingButton.Height = ViewSettings.LoadingButtonHeight;
            _loadingButton.Margin = new Thickness(ViewSettings.LoadingButtonMargin);

            Binding loadBinding = new Binding("LoadPuzzleCommand");
            _loadingButton.SetBinding(Button.CommandProperty, loadBinding);

            _loadingPanel.Children.Add(_input);
            _loadingPanel.Children.Add(_loadingButton);

            _rootGrid.Children.Add(_loadingPanel);
        }

        public void BuildSolveButtons()
        {
            _solvePanel = new StackPanel();
            _solvePanel.VerticalAlignment = VerticalAlignment.Center;
            Grid.SetColumn(_solvePanel, ViewSettings.SolvingColumn);

            _nextStepButton = new Button();
            _nextStepButton.Content = "Next Step";
            _nextStepButton.VerticalAlignment = VerticalAlignment.Center;
            _nextStepButton.Width = ViewSettings.SolvingButtonWidth;
            _nextStepButton.Height = ViewSettings.SolvingButtonHeight;
            _nextStepButton.Margin = new Thickness(ViewSettings.SolvingButtonMargin);

            Binding nextStepBinding = new Binding("SolveNextStepCommand");
            _nextStepButton.SetBinding(Button.CommandProperty, nextStepBinding);

            _solveButton = new Button();
            _solveButton.Content = "Solve Puzzle";
            _solveButton.VerticalAlignment = VerticalAlignment.Center;
            _solveButton.Width = ViewSettings.SolvingButtonWidth;
            _solveButton.Height = ViewSettings.SolvingButtonHeight;
            _solveButton.Margin = new Thickness(ViewSettings.SolvingButtonMargin);

            Binding solveBinding = new Binding("SolvePuzzleCommand");
            _solveButton.SetBinding(Button.CommandProperty, solveBinding);

            _solvePanel.Children.Add(_nextStepButton);
            _solvePanel.Children.Add(_solveButton);

            _rootGrid.Children.Add(_solvePanel);
        }
    }
}
