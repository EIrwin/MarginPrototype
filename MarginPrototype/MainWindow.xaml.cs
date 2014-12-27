using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;

namespace MarginPrototype
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private RadDiagramShape _marginShape;

        private const double DEFAULT_MARGIN = 20;

        private Thickness _margin;

        public MainWindow()
        {
            InitializeComponent();
            InitializeMarginLines();
        }

        private void InitializeMarginLines()
        {
            _margin = new Thickness(DEFAULT_MARGIN);

            _marginShape = new RadDiagramShape();
            _marginShape.Background = new SolidColorBrush(Colors.Transparent);
            _marginShape.Position = new Point(_margin.Top,_margin.Left);
            _marginShape.IsEnabled = false;
            _marginShape.Loaded += UpdateMarginsShape;

            Diagram.SizeChanged += UpdateMarginsShape;
            Diagram.AddShape(_marginShape);
        }

        private void UpdateMarginsShape(object sender,EventArgs e)
        {
            _marginShape.Position = new Point(_margin.Top, _margin.Left);
            _marginShape.Height = Diagram.ActualHeight - (_margin.Top + _margin.Bottom);
            _marginShape.Width = Diagram.ActualWidth - (_margin.Left + _margin.Right);
        }

        private void ToggleMarginButton_Click(object sender, RoutedEventArgs e)
        {
            _marginShape.Visibility = _marginShape.Visibility == Visibility.Visible ? Visibility.Hidden : Visibility.Visible;
        }

        private void EditMarginButton_Click(object sender, RoutedEventArgs e)
        {
            EditMarginsWindow editMarginsWindow = new EditMarginsWindow(_marginShape,Diagram,_margin);
            editMarginsWindow.Show();
        }
    }
}
