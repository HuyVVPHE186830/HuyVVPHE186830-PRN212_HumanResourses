using Microsoft.EntityFrameworkCore;
using Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class NotifyDAO
    {
        private static List<Notification> listNotis;
        public static List<Notification> GetNotis()
        {
            List<Notification> listNotis = new List<Notification>();
            try
            {
                using var db = new FuhrmContext();
                listNotis = db.Notifications.Include(n => n.Department).ToList();
            }
            catch (Exception e)
            {

            }
            return listNotis;
        }
        public static List<Notification> GetNotisByDepartId(int departmentId)
        {
            List<Notification> listNotis = new List<Notification>();
            try
            {
                using var db = new FuhrmContext();
                listNotis = db.Notifications
                    .Include(n => n.Department)
                    .Where(n => n.DepartmentId == departmentId)
                    .ToList();
            }
            catch (Exception e)
            {

            }
            return listNotis;
        }


        public static void AddNotis(Notification account)
        {
            try
            {
                using var db = new FuhrmContext();
                db.Notifications.Add(account);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public static void UpdateNotis(Notification employee)
        {
            try
            {
                using var db = new FuhrmContext();
                db.Entry<Notification>(employee).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public static void DeleteNotis(Notification employee)
        {
            try
            {
                using var db = new FuhrmContext();
                var b1 = db.Notifications.SingleOrDefault(b => b.NotificationId == employee.NotificationId);
                db.Notifications.Remove(b1);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
