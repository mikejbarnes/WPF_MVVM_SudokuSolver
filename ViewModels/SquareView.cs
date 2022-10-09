using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using WPF_MVVM_SudokuSolver.ViewModels.Converters;

namespace WPF_MVVM_SudokuSolver.ViewModels
{
    class SquareView
    {
        private Grid _grid;
        private Grid _subGrid;
        private Grid _subRow;
        private Grid _valueGrid;
        private ViewModel _dataContext;

        public int Row { get; private set; }
        public int Column { get; private set; }

        public SquareView(Grid parentGrid, int row, int column, ViewModel dataContext)
        {
            _dataContext = dataContext;

            InitializeGrid(row, column);
            InitializeValueCells(row, column);

            parentGrid.Children.Add(_grid);
        }

        private void InitializeGrid(int row, int column)
        {
            _grid = new Grid();

            _grid.Width = ViewSettings.SquareSideLength;
            _grid.Height = ViewSettings.SquareSideLength;

            Grid.SetRow(_grid, row);
            Grid.SetColumn(_grid, column);

            int leftThickness = ViewSettings.BorderThickness;
            int topThickness = ViewSettings.BorderThickness;
            int rightThickness = ViewSettings.BorderThickness;
            int bottomThickness = ViewSettings.BorderThickness;

            if (column == 3 || column == 6) leftThickness = ViewSettings.SectionBorderThickness;
            if (row == 3 || row == 6) topThickness = ViewSettings.SectionBorderThickness;
            if (column == 2 || column == 5) rightThickness = ViewSettings.SectionBorderThickness;
            if (row == 2 || row == 5) bottomThickness = ViewSettings.SectionBorderThickness;

            Border border = new Border();
            border.BorderThickness = new System.Windows.Thickness(leftThickness, topThickness, rightThickness, bottomThickness);
            border.BorderBrush = Brushes.Gray;

            _grid.Children.Add(border);
        }

        private void InitializeValueCells(int row, int column)
        {
            InitializeSubGrid();
            InitializeValueGrid();
            InitializeSubRow();

            CreateValueBlock(row, column);
            CreatePossibleValueBlocks(row, column);

            _subGrid.Children.Add(_valueGrid);
            _subGrid.Children.Add(_subRow);
            _grid.Children.Add(_subGrid);
        }

        private void InitializeSubGrid()
        {
            _subGrid = new Grid();

            for (int i = 0; i < ViewSettings.SubGridRows; i++)
            {
                RowDefinition rowDefinition = new RowDefinition();
                rowDefinition.Height = new GridLength(1.0, GridUnitType.Star);
                _subGrid.RowDefinitions.Add(rowDefinition);
            }

            for (int i = 0; i < ViewSettings.SubGridColumns; i++)
            {
                ColumnDefinition columnDefinition = new ColumnDefinition();
                columnDefinition.Width = new GridLength(1.0, GridUnitType.Star);
                _subGrid.ColumnDefinitions.Add(columnDefinition);
            }
        }

        private void InitializeSubRow()
        {
            _subRow = new Grid();

            Grid.SetRow(_subRow, ViewSettings.SubRowPlacement);
            Grid.SetColumn(_subRow, 0);
            Grid.SetColumnSpan(_subRow, ViewSettings.SubGridColumns);

            for (int i = 0; i < ViewSettings.SubRowColumns; i++)
            {
                ColumnDefinition columnDefinition = new ColumnDefinition();
                columnDefinition.Width = new GridLength(1.0, GridUnitType.Star);
                _subRow.ColumnDefinitions.Add(columnDefinition);
            }
        }

        private void InitializeValueGrid()
        {
            _valueGrid = new Grid();

            Grid.SetRow(_valueGrid, ViewSettings.ValuePosition[0]);
            Grid.SetColumn(_valueGrid, ViewSettings.ValuePosition[1]);
            Grid.SetRowSpan(_valueGrid, ViewSettings.ValueGridRowSpan);
            Grid.SetColumnSpan(_valueGrid, ViewSettings.ValueGridColumnSpan);
        }

        private void CreatePossibleValueBlocks(int row, int column)
        {
            for (int i = 0; i < ViewSettings.Numbers - ViewSettings.SubRowValues; i++)
            {
                _subGrid.Children.Add(CreateTextBlock(i));
            }

            CreateSubRowBlocks(row, column);
        }

        private void CreateSubRowBlocks(int row, int column)
        {
            for (int i = ViewSettings.Numbers - ViewSettings.SubRowValues; i < ViewSettings.Numbers; i++)
            {
                _subRow.Children.Add(CreateTextBlock(i));
            }
        }

        private void CreateValueBlock(int row, int column)
        {
            Row = row;
            Column = column;

            TextBlock textBlock = new TextBlock();
            textBlock.HorizontalAlignment = HorizontalAlignment.Center;
            textBlock.VerticalAlignment = VerticalAlignment.Center;
            textBlock.Foreground = Brushes.Black;
            textBlock.FontSize =  ViewSettings.ValueBlockFontSize;

            MultiBinding binding = new MultiBinding();
            binding.Converter = new PuzzleValueConverter();
            binding.Bindings.Add(new Binding("PuzzleValues") { Source = _dataContext });
            binding.Bindings.Add(new Binding("Row") { Source = this });
            binding.Bindings.Add(new Binding("Column") { Source = this });
            textBlock.SetBinding(TextBlock.TextProperty, binding);
            
            _valueGrid.Children.Add(textBlock);
        }

        private TextBlock CreateTextBlock(int number)
        {
            TextBlock textBlock = new TextBlock();
            textBlock.HorizontalAlignment = HorizontalAlignment.Center;
            textBlock.VerticalAlignment = VerticalAlignment.Center;
            textBlock.Foreground = Brushes.DarkGray;

            textBlock.Text = "A";
            Grid.SetRow(textBlock, ViewSettings.PossibleValuePosition[number, 0]);
            Grid.SetColumn(textBlock, ViewSettings.PossibleValuePosition[number, 1]);

            return textBlock;
        }
    }
}
