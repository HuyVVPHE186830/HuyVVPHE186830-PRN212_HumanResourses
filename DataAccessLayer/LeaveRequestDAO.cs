using Microsoft.EntityFrameworkCore;
using Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class LeaveRequestDAO
    {
        public static List<LeaveRequest> GetLeaveRequests()
        {
            try
            {
                using (var context = new FuhrmContext())
                {
                    return context.LeaveRequests.Include(l => l.Employee).ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static LeaveRequest GetLeaveRequestById(int id)
        {
            try
            {
                using (var context = new FuhrmContext())
                {
                    return context.LeaveRequests.Find(id);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void AddLeaveRequest(LeaveRequest leaveRequest)
        {
            try
            {
                using (var context = new FuhrmContext())
                {
                    context.LeaveRequests.Add(leaveRequest);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void UpdateLeaveRequest(LeaveRequest leaveRequest)
        {
            try
            {
                using (var context = new FuhrmContext())
                {
                    context.Update(leaveRequest);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void DeleteLeaveRequest(LeaveRequest leaveRequest)
        {
            try
            {
                using (var context = new FuhrmContext())
                {
                    context.LeaveRequests.Remove(leaveRequest);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
