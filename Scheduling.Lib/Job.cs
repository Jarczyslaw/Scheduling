using FluentScheduler;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduling.Lib
{
    public class Job : AsyncJob
    {

        protected override async Task ExecuteAsync()
        {
            Debug.WriteLine($"{Name} started");
            await Task.Delay(3000).ConfigureAwait(false);
            Debug.WriteLine($"{Name} finished");
        }
    }
}
