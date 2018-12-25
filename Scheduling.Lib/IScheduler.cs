using FluentScheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduling.Lib
{
    public interface IScheduler
    {
        Action<JobStartInfo> JobStart { get; set; }
        Action<JobEndInfo> JobEnd { get; set; }
        Action<JobExceptionInfo> JobException { get; set; }

        int RunningJobs { get; }

        void StartJob(Job job);
        void StopAndBlock();
    }
}
