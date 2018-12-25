using FluentScheduler;
using System;
using System.Linq;

namespace Scheduling.Lib
{
    public class Scheduler : IScheduler
    {
        public Action<JobStartInfo> JobStart { get; set; }
        public Action<JobEndInfo> JobEnd { get; set; }
        public Action<JobExceptionInfo> JobException { get; set; }

        public int RunningJobs
        {
            get => JobManager.RunningSchedules.Count();
        }

        private int jobsCounter;

        public Scheduler()
        {
            JobManager.InitializeWithoutStarting();
            JobManager.JobStart += j => JobStart?.Invoke(j);
            JobManager.JobEnd += j => JobEnd?.Invoke(j);
            JobManager.JobException += j => JobException?.Invoke(j);
        }

        public void StartJob(Job job)
        {
            jobsCounter++;
            var jobName = $"Job_{jobsCounter}";
            job.Name = jobName;
            JobManager.AddJob(job, (s) => s.WithName(jobName).ToRunNow());
        }

        public void StopAndBlock()
        {
            JobManager.StopAndBlock();
        }
    }
}
