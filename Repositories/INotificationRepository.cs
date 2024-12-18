﻿using Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface INotificationRepository
    {
        void AddNotis(Notification employee);
        void UpdateNotis(Notification employee);
        void DeleteNotis(Notification employee);
        List<Notification> GetNotis();
        List<Notification> GetNotisByDepartId(int id);
    }
}
