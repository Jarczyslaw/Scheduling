using Quartz;
using Scheduling.Common;
using System.Threading.Tasks;

namespace Scheduling.QuartzLib
{
    public class Job : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            var heavyTask = new HeavyTask();
            await heavyTask.Run(context.GetJobName()).ConfigureAwait(false);
        }
    }
}
