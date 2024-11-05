using Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class ActivityLogsDAO
    {
        public static void AddActivityLog(ActivityLog activityLog)
        {
            try
            {
                using var db = new FuhrmContext();
                db.ActivityLogs.Add(activityLog);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
