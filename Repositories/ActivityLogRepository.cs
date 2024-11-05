using DataAccessLayer;
using Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class ActivityLogRepository : IActivityLogRepository
    {
        public void AddActivityLog(ActivityLog activityLog) => ActivityLogsDAO.AddActivityLog(activityLog);
    }
}
