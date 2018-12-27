using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduling.QuartzLib
{
    public static class IJobExecutionContextExtensions
    {
        public static string GetJobName(this IJobExecutionContext context)
        {
            return context.JobDetail.Key.Name;
        }
    }
}
