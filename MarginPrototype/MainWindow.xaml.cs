using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using MarginPrototype.Shapes;
using MarginPrototype.ViewModels;
using Telerik.Windows.Controls;

namespace MarginPrototype
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Thickness _margin;
        private List<MarginShape> _marginShapes;
        private ReportViewModel _reportViewModel;

        private const double DEFAULT_MARGIN = 20;

        public MainWindow()
        {
            InitializeComponent();

            _margin = new Thickness(DEFAULT_MARGIN);
            _marginShapes = new List<MarginShape>();

            InitializeReport();
            InitializeFirstPage();
            InitializeFirstPageMarginShape();
        }

        private void InitializeReport()
        {
            _reportViewModel = new ReportViewModel();

        }

        private void InitializeFirstPage()
        {
            _reportViewModel.Pages.Add(new PageViewModel()
            {
                PageNumber = 1,
                Top = 0,
                Bottom = Diagram.ActualHeight
            });
        }

        private void InitializeFirstPageMarginShape()
        {
            var marginShape = new MarginShape();
            marginShape.Position = new Point(_margin.Left,_margin.Top);
            marginShape.Loaded += UpdateMarginsShape;

            _marginShapes.Add(marginShape);

            Diagram.AddShape(marginShape);
        }

        private void UpdateMarginsShape(object sender,EventArgs e)
        {
            _marginShapes.ForEach(marginShape =>
                {
                    marginShape.Position = new Point(_margin.Left, _margin.Top);
                    marginShape.Height = Diagram.ActualHeight - (_margin.Top + _margin.Bottom);
                    marginShape.Width = Diagram.ActualWidth - (_margin.Left + _margin.Right);
                });
        }

        private void ToggleMarginButton_Click(object sender, RoutedEventArgs e)
        {
            _marginShapes.ForEach(marginShape =>
                {
                    marginShape.Visibility = marginShape.Visibility == Visibility.Visible
                                                  ? Visibility.Hidden
                                                  : Visibility.Visible;
                });

        }

        private void EditMarginButton_Click(object sender, RoutedEventArgs e)
        {
            var editMarginsWindow = new EditMarginsWindow(_margin);
            editMarginsWindow.MarginChanged += (o, args) =>
                {
                    _margin = args.Margin;
                    UpdateMarginsShape(o, args);
                };
            editMarginsWindow.Show();
        }

        private void NewPageButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
