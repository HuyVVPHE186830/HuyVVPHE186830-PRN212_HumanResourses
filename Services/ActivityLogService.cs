using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Objects;

namespace Services
{
    public class ActivityLogService : IActivityLogService
    {
        private readonly IActivityLogRepository iActivityLogRepository;
        public ActivityLogService()
        {
            iActivityLogRepository = new ActivityLogRepository();
        }
        public void AddActivityLog(ActivityLog activitylog)
        {
            iActivityLogRepository.AddActivityLog(activitylog);
        }
    }
}
