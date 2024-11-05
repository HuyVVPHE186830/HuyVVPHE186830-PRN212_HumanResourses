using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using Objects;

namespace Repositories
{
    public class AttendanceRepository : IAttendanceRepository
    {
        public void AddAttendance(Attendance attendance) => AttendanceDAO.AddAttendance(attendance);

        public bool AttendanceExists(int employeeId, DateOnly date)=> AttendanceDAO.AttendanceExists(employeeId, date);



        public List<Attendance> GetAttendancesByEmployeeId(int employeeId) => AttendanceDAO.GetAttendancesByEmployeeId(employeeId);
       
    }
}
