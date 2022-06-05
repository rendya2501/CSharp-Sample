using MVPSample.Models;
using MVPSample.Presenters;
using MVPSample.Views;
using System;
using System.Windows;

namespace MVPSample
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        [STAThread]
        public static void Main()
        {
            IRectangleModel model = new RectangleModel();
            IRectangleView view = new MainWindow();
            _ = new RectanglePresenter(view, model);
            view.Show();
            new App().Run();
        }
    }
}
