using System.Windows;
using Telerik.Windows.Controls;

namespace MarginPrototype
{
    /// <summary>
    /// Interaction logic for EditMarginsWindow.xaml
    /// </summary>
    public partial class EditMarginsWindow : Window
    {
        private RadDiagramShape _marginShape;
        private RadDiagram _diagram;
        private Thickness _margin;

        public EditMarginsWindow(RadDiagramShape marginShape,RadDiagram diagram,Thickness margin)
        {
            InitializeComponent();

            _marginShape = marginShape;
            _diagram = diagram;
            _margin = margin;

            this.DataContext = _margin;
        }

        private void SaveMarginsButton_Click(object sender, RoutedEventArgs e)
        {
            double top = double.Parse(TopMarginTextBox.Text);
            double bottom = double.Parse(BottomMarginTextBox.Text);
            double left = double.Parse(LeftMarginTextBox.Text);
            double right = double.Parse(RightMarginTextBox.Text);

            UpdateMargins(top,bottom,left,right);

            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void UpdateMargins(double top, double bottom, double left, double right)
        {
            _marginShape.Position = new Point(left, top);
            _marginShape.Height = _diagram.ActualHeight - (top + bottom);
            _marginShape.Width = _diagram.ActualWidth - (left + right);
            _margin = new Thickness(left,top,right,bottom); //Not updating binding source
        }


    }
}
