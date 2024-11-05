using Objects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository iEmployeeRepository;
        public NotificationService()
        {
            iEmployeeRepository = new NotificationRepository();
        }
        public void AddNotis(Notification employee)
        {
            iEmployeeRepository.AddNotis(employee);
        }
        public void UpdateNotis(Notification employee)
        {
            iEmployeeRepository.UpdateNotis(employee);
        }
        public void DeleteNotis(Notification employee)
        {
            iEmployeeRepository.DeleteNotis(employee);
        }

        public List<Notification> GetNotis()
        {
            return iEmployeeRepository.GetNotis();
        }
        public List<Notification> GetNotisByDepartId(int id)
        {
            return iEmployeeRepository.GetNotisByDepartId(id);
        }
    }
}
