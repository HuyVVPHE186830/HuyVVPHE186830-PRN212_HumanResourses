using Microsoft.EntityFrameworkCore;
using Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public static class AttendanceDAO
    {
        

        

        public static void AddAttendance(Attendance attendance)
        {
            using (var _context = new FuhrmContext())
            {
                _context.Attendances.Add(attendance);
                _context.SaveChanges();

           
            }
        }
        public static bool AttendanceExists(int employeeId, DateOnly date)
        {
            using (var _context = new FuhrmContext()) 
            {
                return _context.Attendances.Any(a => a.EmployeeId == employeeId && a.Date == date);
            }
        }
        public static List<Attendance> GetAttendancesByEmployeeId(int employeeId)
        {
            using (var context = new FuhrmContext())
            {
                return context.Attendances.Where(a => a.EmployeeId == employeeId).ToList();
            }
        }
       
    }
}
