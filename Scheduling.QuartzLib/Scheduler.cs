using Quartz;
using Quartz.Impl;
using Scheduling.Common;
using System;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace Scheduling.QuartzLib
{
    public class Scheduler : IJobScheduler
    {
        private IScheduler scheduler;
        private int jobsCounter;

        public Scheduler()
        {
            Init();
        }

        public Action<JobInfo> JobStart { get; set; }
        public Action<JobInfo> JobEnd { get; set; }

        public int RunningJobs
        {
            get
            {
                return GetRunningJobs().GetAwaiter().GetResult();
            }
        }

        private async Task<int> GetRunningJobs()
        {
            var jobs = await scheduler.GetCurrentlyExecutingJobs().ConfigureAwait(false);
            return jobs.Count;
        }

        public async void StartJob()
        {
            jobsCounter++;
            await AddJob<Job, int>($"Job_{jobsCounter}", jobsCounter, TriggerFactory.GetStartNowTrigger())
                .ConfigureAwait(false);
        }

        public void StopAndBlock()
        {
            scheduler.Shutdown(true).Wait();
        }

        private async Task AddJob<TJobType, TInputType>(string jobName, TInputType input, ITrigger trigger)
            where TJobType : IJob
        {
            var job = JobBuilder.Create<TJobType>()
                .WithIdentity(jobName)
                .Build();
            job.JobDataMap.Put("input", input);
            await scheduler.ScheduleJob(job, trigger).ConfigureAwait(false);
        }

        private async Task<IScheduler> CreateScheduler(NameValueCollection properties)
        {
            var factory = new StdSchedulerFactory(properties);
            return await factory.GetScheduler().ConfigureAwait(false);
        }

        private NameValueCollection CreateSchedulerProperties(int threadCount = -1)
        {
            return new NameValueCollection
            {
                { "quartz.threadPool.threadCount", threadCount.ToString() }
            };
        }

        private async void Init()
        {
            scheduler = await CreateScheduler(CreateSchedulerProperties(5)).ConfigureAwait(false);
            scheduler.ListenerManager.AddJobListener(new JobListener(this));
            await scheduler.Start().ConfigureAwait(false);
        }
    }
}
