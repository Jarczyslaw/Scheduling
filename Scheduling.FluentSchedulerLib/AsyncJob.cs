using FluentScheduler;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduling.FluentSchedulerLib
{
    public abstract class AsyncJob : IJob, IDisposable
    {
        public string Name { get; set; }

        public void Dispose()
        {
            Debug.WriteLine($"{Name} disposed");
        }

        public void Execute()
        {
            ExecuteAsync().Wait();
        }

        protected abstract Task ExecuteAsync();
    }
}
