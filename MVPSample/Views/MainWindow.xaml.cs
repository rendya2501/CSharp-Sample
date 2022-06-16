using MVPSample.Views;
using System;
using System.Windows;

namespace MVPSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// https://www.youtube.com/watch?v=UgnbIJYUTQY
    /// </summary>
    public partial class MainWindow : Window, IRectangleView
    {
        public MainWindow() => InitializeComponent();
        public string LengthText { get => txtLength.Text; set => txtLength.Text = value; }
        public string BreadthText { get => txtBreath.Text; set => txtBreath.Text = value; }
        public string AreaText { get => txtBlockArea.Text; set => txtBlockArea.Text = value + "Sq CM"; }
        public event EventHandler Calculate;
        private void Button_Click(object sender, RoutedEventArgs e) => Calculate?.Invoke(sender, e);
    }
}
