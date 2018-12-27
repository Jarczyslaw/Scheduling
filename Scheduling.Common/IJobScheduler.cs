using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduling.Common
{
    public interface IJobScheduler
    {
        Action<JobInfo> JobStart { get; set; }
        Action<JobInfo> JobEnd { get; set; }

        int RunningJobs { get; }

        void StartJob();
        void StopAndBlock();
    }
}
