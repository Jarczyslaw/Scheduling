using Scheduling.Common;
using System.Threading.Tasks;

namespace Scheduling.FluentSchedulerLib
{
    public class Job : AsyncJob
    {
        protected override async Task ExecuteAsync()
        {
            var heavyTask = new HeavyTask();
            await heavyTask.Run(Name).ConfigureAwait(false);
        }
    }
}
