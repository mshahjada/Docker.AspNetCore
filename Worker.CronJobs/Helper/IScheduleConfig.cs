using System;
using System.Collections.Generic;
using System.Text;

namespace Worker.CronJobs.Helper
{
    public interface IScheduleConfig<T>
    {
        string CronExpression { get; set; }
        TimeZoneInfo TimeZoneInfo { get; set; }
    }
}
