using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduling.Common
{
    public class HeavyTask
    {
        private readonly string dateTimeFormat = "yyyy-MM-dd HH:mm:ss";

        public async Task Run(string jobName)
        {
            Debug.WriteLine($"{DateTime.Now.ToString(dateTimeFormat)} - {jobName} started");
            await Task.Delay(3000).ConfigureAwait(false);
            Debug.WriteLine($"{DateTime.Now.ToString(dateTimeFormat)} - {jobName} finished");
        }
    }
}
