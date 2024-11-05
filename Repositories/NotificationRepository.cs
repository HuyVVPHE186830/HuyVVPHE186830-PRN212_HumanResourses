using DataAccessLayer;
using Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        public void AddNotis(Notification employee) => NotifyDAO.AddNotis(employee);
        public void UpdateNotis(Notification employee) => NotifyDAO.UpdateNotis(employee);
        public void DeleteNotis(Notification employee) => NotifyDAO.DeleteNotis(employee);
        public List<Notification> GetNotis() => NotifyDAO.GetNotis();
        public List<Notification> GetNotisByDepartId(int id) => NotifyDAO.GetNotisByDepartId(id);
    }
}
