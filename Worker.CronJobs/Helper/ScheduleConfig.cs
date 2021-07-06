using System;
using System.Collections.Generic;
using System.Text;

namespace Worker.CronJobs.Helper
{
    public class ScheduleConfig<T> : IScheduleConfig<T>
    {
        public string CronExpression { get; set; }
        public TimeZoneInfo TimeZoneInfo { get; set; }
    }
}
