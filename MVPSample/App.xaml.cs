using Microsoft.Extensions.DependencyInjection;
using MVPSample.Models;
using MVPSample.Presenters;
using MVPSample.Views;
using System;
using System.Windows;
using Unity;

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
            // Microsoft.Extensions.DependencyInjectionでの実装
            IServiceCollection services = new ServiceCollection();
            services.AddTransient<App>();
            services.AddScoped<IRectangleView, MainWindow>();
            services.AddScoped<IRectangleModel, RectangleModel>();
            services.AddTransient<RectanglePresenter>();
            // インスタンスを提供してくれる人を作る
            using var provider = services.BuildServiceProvider();
            provider.GetService<RectanglePresenter>();
            provider.GetService<IRectangleView>()?.Show();
            provider.GetService<App>()?.Run();

            // UnityContainerでの実装
            //using IUnityContainer container = new UnityContainer();
            //container.RegisterType<IRectangleView, MainWindow>();
            //container.RegisterType<IRectangleModel, RectangleModel>();
            //container.Resolve<RectanglePresenter>();
            //container.Resolve<IRectangleView>().Show();
            //container.Resolve<App>().Run();

            // 依存性を注入する部分はこういう風にもかける。だけど、ほぼ意味はないでしょう。
            //①
            //container.RegisterInstance(new RectanglePresenter(view, container.Resolve<IRectangleModel>()));
            //②
            //container.RegisterType<RectanglePresenter>(
            //    new InjectionConstructor(
            //        new ResolvedParameter<IRectangleView>(),
            //        new ResolvedParameter<IRectangleModel>()
            //    )
            //);

            // 手動DIでの実装
            //IRectangleModel model = new RectangleModel();
            //IRectangleView view = new MainWindow();
            //_ = new RectanglePresenter(view, model);
            //view.Show();
            //new App().Run();
        }
    }
}
