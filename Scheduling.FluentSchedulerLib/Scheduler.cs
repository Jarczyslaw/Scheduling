using FluentScheduler;
using Scheduling.Common;
using System;
using System.Linq;

namespace Scheduling.FluentSchedulerLib
{
    public class Scheduler : IScheduler
    {
        public Action<JobInfo> JobStart { get; set; }
        public Action<JobInfo> JobEnd { get; set; }

        public int RunningJobs
        {
            get => JobManager.RunningSchedules.Count();
        }

        private int jobsCounter;

        public Scheduler()
        {
            JobManager.InitializeWithoutStarting();
            JobManager.JobStart += j => JobStart?.Invoke(new JobInfo
            {
                JobName = j.Name,
                Timestamp = j.StartTime
            });
            JobManager.JobEnd += j => JobEnd?.Invoke(new JobInfo
            {
                JobName = j.Name,
                Timestamp = j.StartTime + j.Duration
            });
        }

        public void StartJob()
        {
            jobsCounter++;
            var jobName = $"Job_{jobsCounter}";
            var job = new Job
            {
                Name = jobName,
            };
            JobManager.AddJob(job, (s) => s.WithName(jobName).ToRunNow());
        }

        public void StopAndBlock()
        {
            JobManager.StopAndBlock();
        }
    }
}
