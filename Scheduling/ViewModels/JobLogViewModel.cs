using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduling.ViewModels
{
    internal class JobLogViewModel
    {
        public int Id { get; set; }
        public int ThreadId { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Message { get; set; }
    }
}
