using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using WPF_MVVM_SudokuSolver.Models;
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
        public int[] Indexes { get; private set; }

        public SquareView(Grid parentGrid, int row, int column, ViewModel dataContext)
        {
            _dataContext = dataContext;
            Row = row;
            Column = column;

            InitializeIndexes();
            InitializeGrid();
            InitializeValueCells();

            parentGrid.Children.Add(_grid);
        }

        private void InitializeIndexes()
        {
            Indexes = new int[ModelSettings.Numbers];

            for (int i = 0; i < ModelSettings.Numbers; i++)
            {
                Indexes[i] = i;
            }
        }

        private void InitializeGrid()
        {
            _grid = new Grid();

            _grid.Width = ViewSettings.SquareSideLength;
            _grid.Height = ViewSettings.SquareSideLength;

            Grid.SetRow(_grid, Row);
            Grid.SetColumn(_grid, Column);

            int leftThickness = ViewSettings.BorderThickness;
            int topThickness = ViewSettings.BorderThickness;
            int rightThickness = ViewSettings.BorderThickness;
            int bottomThickness = ViewSettings.BorderThickness;

            if (Column == 3 || Column == 6) leftThickness = ViewSettings.SectionBorderThickness;
            if (Row == 3 || Row == 6) topThickness = ViewSettings.SectionBorderThickness;
            if (Column == 2 || Column == 5) rightThickness = ViewSettings.SectionBorderThickness;
            if (Row == 2 || Row == 5) bottomThickness = ViewSettings.SectionBorderThickness;

            Border border = new Border();
            border.BorderThickness = new System.Windows.Thickness(leftThickness, topThickness, rightThickness, bottomThickness);
            border.BorderBrush = Brushes.Gray;

            _grid.Children.Add(border);
        }

        private void InitializeValueCells()
        {
            InitializeSubGrid();
            InitializeValueGrid();
            InitializeSubRow();

            CreateValueBlock();
            CreatePossibleValueBlocks();

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

        private void CreatePossibleValueBlocks()
        {
            for (int i = 0; i < ModelSettings.Numbers - ViewSettings.SubRowValues; i++)
            {
                _subGrid.Children.Add(CreateTextBlock(i));
            }

            CreateSubRowBlocks(Row, Column);
        }

        private void CreateSubRowBlocks(int row, int column)
        {
            for (int i = ModelSettings.Numbers - ViewSettings.SubRowValues; i < ModelSettings.Numbers; i++)
            {
                _subRow.Children.Add(CreateTextBlock(i));
            }
        }

        private void CreateValueBlock()
        {
            TextBlock textBlock = new TextBlock();
            textBlock.HorizontalAlignment = HorizontalAlignment.Center;
            textBlock.VerticalAlignment = VerticalAlignment.Center;
            textBlock.FontSize =  ViewSettings.ValueBlockFontSize;

            MultiBinding valueBinding = new MultiBinding();
            valueBinding.Converter = new PuzzleValueConverter();
            valueBinding.Bindings.Add(new Binding("Squares") { Source = _dataContext });
            valueBinding.Bindings.Add(new Binding("Row") { Source = this });
            valueBinding.Bindings.Add(new Binding("Column") { Source = this });
            textBlock.SetBinding(TextBlock.TextProperty, valueBinding);

            MultiBinding colorBinding = new MultiBinding();
            colorBinding.Converter = new PuzzleValueColorConverter();
            colorBinding.Bindings.Add(new Binding("Squares") { Source = _dataContext });
            colorBinding.Bindings.Add(new Binding("Row") { Source = this });
            colorBinding.Bindings.Add(new Binding("Column") { Source = this });
            textBlock.SetBinding(TextBlock.ForegroundProperty, colorBinding);

            _valueGrid.Children.Add(textBlock);
        }

        private TextBlock CreateTextBlock(int number)
        {
            TextBlock textBlock = new TextBlock();
            textBlock.HorizontalAlignment = HorizontalAlignment.Center;
            textBlock.VerticalAlignment = VerticalAlignment.Center;
            textBlock.Foreground = Brushes.DarkGray;

            //textBlock.Text = "A";
            Grid.SetRow(textBlock, ViewSettings.PossibleValuePosition[number, 0]);
            Grid.SetColumn(textBlock, ViewSettings.PossibleValuePosition[number, 1]);

            MultiBinding valueBinding = new MultiBinding();
            valueBinding.Converter = new PossibleValueConverter();
            valueBinding.Bindings.Add(new Binding("Squares") { Source = _dataContext });
            valueBinding.Bindings.Add(new Binding("Row") { Source = this });
            valueBinding.Bindings.Add(new Binding("Column") { Source = this });
            valueBinding.Bindings.Add(new Binding($"Indexes[{number}]") { Source = this });
            textBlock.SetBinding(TextBlock.TextProperty, valueBinding);

            MultiBinding colorBinding = new MultiBinding();
            colorBinding.Converter = new PossibleValueColorConverter();
            colorBinding.Bindings.Add(new Binding("Squares") { Source = _dataContext });
            colorBinding.Bindings.Add(new Binding("Row") { Source = this });
            colorBinding.Bindings.Add(new Binding("Column") { Source = this });
            colorBinding.Bindings.Add(new Binding($"Indexes[{number}]") { Source = this });
            textBlock.SetBinding(TextBlock.ForegroundProperty, colorBinding);

            return textBlock;
        }
    }
}
