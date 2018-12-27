using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduling.QuartzLib
{
    public static class TriggerFactory
    {
        public static ITrigger GetDelayTrigger(TimeSpan interval)
        {
            return TriggerBuilder.Create().StartAt(DateTimeOffset.Now.Add(interval))
                .Build();
        }

        public static ITrigger GetIntervalTrigger(TimeSpan interval)
        {
            return TriggerBuilder.Create()
                .WithSimpleSchedule(x => x.WithInterval(interval).RepeatForever())
                .StartNow()
                .Build();
        }

        public static ITrigger GetStartNowTrigger()
        {
            return TriggerBuilder.Create()
                .StartNow()
                .Build();
        }
    }
}
