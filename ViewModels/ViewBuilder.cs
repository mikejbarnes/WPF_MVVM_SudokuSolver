using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WPF_MVVM_SudokuSolver.ViewModels
{
    class ViewBuilder
    { 
        private ViewModel _dataContext;
        private Grid _rootGrid;
        private Grid _parentGrid;
        private int _gridSideLength;


        public ViewBuilder(Grid rootGrid, ViewModel dataContext)
        {
            _dataContext = dataContext;
            _rootGrid = rootGrid;
            _gridSideLength = ViewSettings.Numbers * ViewSettings.SquareSideLength;

            BuildGrid();
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

            for (int i = 0; i < ViewSettings.Numbers; i++)
            {
                RowDefinition rowDefinition = new RowDefinition();
                rowDefinition.Height = new GridLength(1.0, GridUnitType.Star);
                _parentGrid.RowDefinitions.Add(rowDefinition);

                ColumnDefinition columnDefinition = new ColumnDefinition();
                columnDefinition.Width = new GridLength(1.0, GridUnitType.Star);
                _parentGrid.ColumnDefinitions.Add(columnDefinition);
            }

            for(int row = 0; row < ViewSettings.Numbers; row++)
            {
                for(int column = 0; column < ViewSettings.Numbers; column++)
                {
                    SquareView newSquare = new SquareView(_parentGrid, row, column, _dataContext);
                }
            }

            _rootGrid.Children.Add(_parentGrid);
        }
    }
}
