using System;
using System.Collections.Generic;
using System.Linq;
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

        private const double DefaultMargin = 40;
        private const double DefaultPageHeight = 750;

        public int CurrentPage { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            _margin = new Thickness(DefaultMargin);
            _marginShapes = new List<MarginShape>();

            InitializeDiagram();
            InitializeReport();
            InitializeFirstPage();

            CurrentPage = 0;
        }

        private void InitializeDiagram()
        {
            Diagram.Height = DefaultPageHeight;
        }

        private void InitializeReport()
        {
            _reportViewModel = new ReportViewModel();
            DataContext = _reportViewModel;
        }

        private void InitializeFirstPage()
        {
            var pageViewModel = new PageViewModel()
            {
                PageNumber = 1,
                Top = 0,
                Bottom = DefaultPageHeight
            };

            var marginShape = new MarginShape
                {
                    Position = new Point(_margin.Left, _margin.Top),
                    DataContext = pageViewModel
                };
            marginShape.Loaded += UpdateMarginsShape;

            _marginShapes.Add(marginShape);
            _reportViewModel.Pages.Add(pageViewModel);

            Diagram.AddShape(marginShape);

        }

        private void UpdateMarginsShape(object sender,EventArgs e)
        {
            _marginShapes.ForEach(marginShape =>
                {
                    var pageViewModel = (PageViewModel) marginShape.DataContext;

                    marginShape.Position = new Point(_margin.Left,pageViewModel.Top + _margin.Top);
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
            var lastPage = _reportViewModel.Pages.Last();

            var pageViewModel = new PageViewModel()
                {
                    Top = lastPage.Bottom + 1,
                    Bottom = (lastPage.Bottom + 1) + DefaultPageHeight,
                    PageNumber = lastPage.PageNumber + 1
                };

            var marginShape = new MarginShape
                {
                    Position = new Point(_margin.Left, pageViewModel.Top + _margin.Top),
                    DataContext = pageViewModel
                };

            marginShape.Loaded += UpdateMarginsShape;
            
            _reportViewModel.Pages.Add(pageViewModel);
            _marginShapes.Add(marginShape);

            Diagram.AddShape(marginShape);
        }

        private void Diagram_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //TODO: This should be easier with our actual integrated diagam
            //TODO: Convert this to a math function that determines page based on height, margin and offset(s)

            Point currentLocation = e.GetPosition((IInputElement) sender);
            _reportViewModel.Pages.ForEach(p =>
                {
                    if (currentLocation.Y >= p.Top && currentLocation.Y <= p.Bottom)
                    {
                        CurrentPosition.Text = currentLocation.X + "," +
                                               currentLocation.Y;
                    }
                });

        }
    }
}
