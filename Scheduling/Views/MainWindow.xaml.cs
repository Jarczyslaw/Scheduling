using Scheduling.Common;
using Scheduling.ViewModels;
using System.Windows;
using WPFCustomMessageBox;

namespace Scheduling.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IJobScheduler scheduler;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as MainWindowViewModel;
            if (CustomMessageBox.ShowYesNo("Select scheduling library", "Scheduling", "FluentScheduler", "Quartz") == MessageBoxResult.Yes)
                scheduler = new FluentSchedulerLib.Scheduler();
            else
                scheduler = new QuartzLib.Scheduler();
            viewModel.SetScheduler(scheduler);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            scheduler.StopAndBlock();
        }
    }
}
