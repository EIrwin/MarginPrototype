using System;
using System.Windows;

namespace MarginPrototype
{
    public class MarginChangedEventArgs:EventArgs
    {
        public Thickness Margin {get;set;}
    }
}