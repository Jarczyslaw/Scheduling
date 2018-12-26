﻿using FluentScheduler;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Scheduling.Common;
using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows.Data;
using System.Windows.Threading;

namespace Scheduling.ViewModels
{
    internal class MainWindowViewModel : BindableBase
    {
        private readonly IScheduler scheduler;
        private readonly IContainerExtension container;
        private readonly DispatcherTimer timer;

        private static readonly object sync = new object();

        public MainWindowViewModel(IContainerExtension container, IScheduler scheduler)
        {
            this.container = container;
            this.scheduler = scheduler;

            JobLogs = new ObservableCollection<JobLogViewModel>();
            BindingOperations.EnableCollectionSynchronization(JobLogs, sync);

            scheduler.JobStart += SchedulerJobStart;
            scheduler.JobEnd += SchedulerJobEnd;

            timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(100),
            };
            timer.Tick += (s, e) => RaisePropertyChanged(nameof(RunningJobs));
            timer.IsEnabled = true;

            JobStartCommand = new DelegateCommand(() => scheduler.StartJob());
        }

        public DelegateCommand JobStartCommand { get; }
        public ObservableCollection<JobLogViewModel> JobLogs { get; }

        public int RunningJobs => scheduler.RunningJobs;

        private void AddJobLog(DateTime timeStamp, string message)
        {
            lock (sync)
            {
                JobLogs.Insert(0, new JobLogViewModel
                {
                    Id = JobLogs.Count + 1,
                    ThreadId = Thread.CurrentThread.ManagedThreadId,
                    TimeStamp = timeStamp,
                    Message = message
                });
            }
        }

        private void SchedulerJobEnd(JobInfo endInfo)
        {
            AddJobLog(endInfo.Timestamp, $"{endInfo.JobName} finished");
        }

        private void SchedulerJobStart(JobInfo startInfo)
        {
            AddJobLog(startInfo.Timestamp, $"{startInfo.JobName} started");
        }
    }
}
