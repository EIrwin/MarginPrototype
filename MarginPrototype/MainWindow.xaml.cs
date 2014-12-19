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
        private RadDiagramShape _margin;

        public MainWindow()
        {
            InitializeComponent();
            InitializeMarginLines();

            Diagram.SizeChanged += (o, e) =>
                {
                    _margin.Height = Diagram.ActualHeight - 20;
                    _margin.Width = Diagram.ActualWidth - 20;
                };
        }

        private void InitializeMarginLines()
        {
            _margin = new RadDiagramShape();
            _margin.Background = new SolidColorBrush(Colors.Transparent);
            _margin.Position = new Point(20, 20);
            _margin.Loaded += (o, e) =>
                {
                    var s = (RadDiagramShape)o;
                    s.Height = Diagram.ActualHeight - 20;
                    s.Width = Diagram.ActualWidth - 20;
                };

            Diagram.AddShape(_margin);
        }
    }
}
