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
        public static List<LeaveRequest> SearchLeaveRequest(string search)
        {
            try
            {
                using (var context = new FuhrmContext())
                {
                    var employees = context.LeaveRequests
                .Include(l => l.Employee)
                .Where(l => l.LeaveType.Contains(search) ||
                            l.Status.Contains(search) ||
                            l.Employee.FullName.Contains(search))
                .ToList();
                    return employees;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public static List<LeaveRequest> GetLeaveRequestsByYear(int year)
        {
            try
            {
                using (var context = new FuhrmContext())
                {
                    var leaveRequests = context.LeaveRequests
                .Include(l => l.Employee)
                .Where(l => l.StartDate.Year == year)
                .ToList();
                    return leaveRequests;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public static List<LeaveRequest> GetLeaveRequestsByStatus(string status)
        {
            try
            {
                using (var context = new FuhrmContext())
                {
                    var leaveRequests = context.LeaveRequests
                .Include(l => l.Employee)
                .Where(l => l.Status == status)
                .ToList();
                    return leaveRequests;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
