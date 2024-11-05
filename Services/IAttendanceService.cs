using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Objects;
namespace Services
{
    public interface IAttendanceService
    {
        void AddAttendance(Attendance attendance);
        List<Attendance> GetAttendancesByEmployeeId(int employeeId);
        Boolean AttendanceExists(int employeeId, DateOnly date);
    }
}
