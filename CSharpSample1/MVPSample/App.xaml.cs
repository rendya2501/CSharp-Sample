using MVPSample.Models;
using MVPSample.Presenters;
using MVPSample.Views;
using System;
using System.Windows;
using Unity;
using Unity.Injection;

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
            using IUnityContainer container = new UnityContainer();
            container.RegisterType<IRectangleView, MainWindow>();
            container.RegisterType<IRectangleModel, RectangleModel>();
            //①
            //container.RegisterInstance(new RectanglePresenter(view, container.Resolve<IRectangleModel>()));
            //②
            //container.RegisterType<RectanglePresenter>(
            //    new InjectionConstructor(
            //        new ResolvedParameter<IRectangleView>(),
            //        new ResolvedParameter<IRectangleModel>()
            //    )
            //);
            container.Resolve<RectanglePresenter>();
            container.Resolve<IRectangleView>().Show();
            container.Resolve<App>().Run();

            //IRectangleModel model = new RectangleModel();
            //IRectangleView view = new MainWindow();
            //_ = new RectanglePresenter(view, model);
            //view.Show();
            //new App().Run();
        }
    }
}
