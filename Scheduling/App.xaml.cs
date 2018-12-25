using Prism.Ioc;
using Prism.Unity;
using Scheduling.Lib;
using Scheduling.Views;
using System.Windows;

namespace Scheduling
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IScheduler, Scheduler>();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            var scheduler = Container.Resolve<IScheduler>();
            scheduler.StopAndBlock();

            base.OnExit(e);
        }
    }
}
