using System.Windows;
using MVPSample.Views;
using MVPSample.Presenters;

namespace MVPSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// https://www.youtube.com/watch?v=UgnbIJYUTQY
    /// </summary>
    public partial class MainWindow : Window, IRectangle
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public string LengthText { get => txtLength.Text; set => txtLength.Text = value; }
        public string BreadthText { get => txtBreath.Text; set => txtBreath.Text = value; }
        public string AreaText { get => txtBlockArea.Text; set => txtBlockArea.Text = value + "Sq CM"; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RectanglePresenter presenter = new RectanglePresenter(this);
            presenter.CalculateArea();
        }
    }
}
