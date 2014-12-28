using System;
using System.Windows;
using Telerik.Windows.Controls;

namespace MarginPrototype
{
    /// <summary>
    /// Interaction logic for EditMarginsWindow.xaml
    /// </summary>
    public partial class EditMarginsWindow : Window
    {

        public EventHandler<MarginChangedEventArgs> MarginChanged;
        public Thickness _margin;
        public EditMarginsWindow(Thickness margin)
        {
            InitializeComponent();
            _margin = margin;

            this.DataContext = _margin;
        }

        private void SaveMarginsButton_Click(object sender, RoutedEventArgs e)
        {
            var top = double.Parse(TopMarginTextBox.Text);
            var bottom = double.Parse(BottomMarginTextBox.Text);
            var left = double.Parse(LeftMarginTextBox.Text);
            var right = double.Parse(RightMarginTextBox.Text);


            MarginChangedEventArgs args = new MarginChangedEventArgs();
            args.Margin = _margin = new Thickness(left, top, right, bottom);
            MarginChanged(this, args);

            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
