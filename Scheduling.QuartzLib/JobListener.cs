using Quartz;
using Scheduling.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Scheduling.QuartzLib
{
    public class JobListener : IJobListener
    {
        private readonly IJobScheduler scheduler;

        public JobListener(IJobScheduler scheduler)
        {
            this.scheduler = scheduler;
        }

        public string Name => nameof(Scheduler);

        public Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.CompletedTask;
        }

        public Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = default(CancellationToken))
        {
            scheduler.JobStart?.Invoke(new JobInfo
            {
                JobName = context.GetJobName(),
                Timestamp = DateTime.Now
            });
            return Task.CompletedTask;
        }

        public Task JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException, CancellationToken cancellationToken = default(CancellationToken))
        {
            scheduler.JobEnd?.Invoke(new JobInfo
            {
                JobName = context.GetJobName(),
                Timestamp = DateTime.Now
            });
            return Task.CompletedTask;
        }
    }
}
